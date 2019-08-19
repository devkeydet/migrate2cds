using System;
using System.Linq;
using System.Collections.Generic;

using System.Windows.Forms;
using XrmToolBox.Extensibility;
using Microsoft.Xrm.Sdk;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk.Metadata;
using System.ComponentModel;
using System.Drawing;

namespace CDSTools
{
    public partial class MigrateToCDSControl : PluginControlBase
    {
        private Settings mySettings;
        private MigrateDataBase _migrateDB;
        private bool _binding = false;

        public MigrateToCDSControl()
        {
            InitializeComponent();
        }

        #region Plugin Control methods 
        private void MyPluginControl_Load(object sender, EventArgs e)
        {
            // Loads or creates the settings for the plugin
            if (!SettingsManager.Instance.TryLoad(GetType(), out mySettings))
            {
                mySettings = new Settings();
            }

            // init some UI elements 
            comboRelationshipType.DataSource = null;
            comboRelationshipType.Items.Clear();
            comboRelationshipType.DisplayMember = "Description";
            comboRelationshipType.ValueMember = "Value";

            comboRelationshipType.DataSource = Enum.GetValues(typeof(RelationshipType))
                .Cast<Enum>()
                .Select(value => new {
                    (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                    value
                })
                .OrderBy(item => item.value)
                .ToList();

            // update UI stuff based on whether Service connection is set
            UpdateUIForConnection();

            //if (mySettings.LastUsedProviderType != null && mySettings.LastUsedConnectionString != null)
            //{
            //    IDbProvider provider = null;
            //    if (mySettings.LastUsedProviderType == SQLDBProvider.Name) {
            //        provider = new SQLDBProvider(mySettings.LastUsedConnectionString);
            //    }
            //    else {
            //        provider = new AccessDBProvider(mySettings.LastUsedConnectionString);
            //    }
            //    _migrateDB = new MigrateDataBase(provider);
            //    ExecuteMethod(ReadDatabase);
            //}

        }

        #endregion

        /// <summary>
        /// Connect to a DataSource 
        /// </summary>
        private void ConnectToDataSource()
        {
            _migrateDB = null;

            // open the connection dialog and get a new provider
            Connect connDlg = new Connect();
            IDbProvider provider = null;

            if (connDlg.ShowDialog(this) == DialogResult.OK) {
                provider = connDlg.Provider;
            }
            else {
                return;
            }

            // set up the selected connection string 
            tstxtConnectionString.Text = provider.GetConnectionString();
            _migrateDB = new MigrateDataBase(provider);

            // save the current provider settings 
            mySettings.LastUsedConnectionString = provider.GetConnectionString();
            mySettings.LastUsedProviderType = (provider is SQLDBProvider) ? SQLDBProvider.Name:AccessDBProvider.Name;

            ReadDatabase();
        }

        /// <summary>
        /// Load all the schema info from the connection =
        /// </summary>
        private void ReadDatabase()
        {
            var prefix = comboPrefix.Text?.Trim().ToLower();

            if (string.IsNullOrEmpty(prefix))
            {
                MessageBox.Show(this, "No Prefix has been selected from the list. Choose a prefix and choose Reload", "Missing Prefix", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // update the prefix for the DB 
            _migrateDB.Prefix = prefix;

            // do some processing ... may take a few moments
            WorkAsync(new WorkAsyncInfo
            {
                Message = $"Loading Database: {_migrateDB.Provider.ToString()}",
                AsyncArgument = _migrateDB,
                Work = (worker, args) =>
                {
                    _migrateDB.ReadDataBase();
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null) {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    ClearLists();
                    BindEntitiesList();
                    BindNewRelationshipList();
                    BindRelationshipLists();

                }
            });
        }

        #region List helper methods

        /// <summary>
        /// Load the list of Publishers for the current CDS connection 
        /// </summary>
        private void LoadPublishers()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = $"Loading Publishers",
                AsyncArgument = Service,
                Work = (worker, args) =>
                {
                    var svc = args.Argument as IOrganizationService;
                    args.Result = CDSOrgServiceCommon.RetrievePublishers(svc);
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    var publishers = args.Result as List<Entity>;

                    // bind the list of publishers
                    comboPrefix.Items.Clear();
                    var list = publishers
                        .Where(p => p.Attributes["customizationprefix"]?.ToString().Length > 0)
                        .Select(p => p.Attributes["customizationprefix"].ToString())
                        .Distinct()
                        .ToList();

                    list.Insert(0, "migrate2");
                    comboPrefix.Items.AddRange(list.ToArray());

                    comboPrefix.SelectedIndex = 0;
                }
            });

        }

        /// <summary>
        /// Update UI elements based on whether we are connected to CDS and/or a DB
        /// </summary>
        private void UpdateUIForConnection()
        {
            var serviceSet = (Service != null);
            var dbLoaded = (_migrateDB != null);

            comboRelationshipType.Enabled = serviceSet;
            comboPrefix.Enabled = serviceSet;

            buttonConnect.Enabled = serviceSet;
            buttonReload.Enabled = (dbLoaded && serviceSet);
            textBoxDBConnection.Text = _migrateDB?.Provider.ToString();

            tabControlMain.Enabled = dbLoaded;
        }
        
        /// <summary>
        /// Helper to clear out lists.
        /// </summary>
        private void ClearLists()
        {
            checkedListTables.Items.Clear();
            checkedListAttributes.Items.Clear();
            propGridAtribute.SelectedObject = null;
            propGridTable.SelectedObject = null;
        }

        private void BindFieldList(MigrateEntity entity)
        {
            checkedListAttributes.SuspendLayout();

            var box = ((ListBox)checkedListAttributes);

            box.DataSource = null;

            checkedListAttributes.Items.Clear();

            foreach (var field in entity.Fields)
            {
                checkedListAttributes.Items.Add(
                    new ListDisplayItem(field.SchemaName, field.ToString(), null, field), 
                    field.Import);
            }
            box.DisplayMember = "DisplayName";
            box.ValueMember = "Object.Import";

            checkedListAttributes.ResumeLayout();
        }

        /// <summary>
        /// Bind the list of entites to the listbox
        /// </summary>
        private void BindEntitiesList() {

            checkedListTables.SuspendLayout();
            var box = ((ListBox)checkedListTables);
            box.DataSource = null;

            checkedListTables.Items.Clear();
            foreach (var entity in _migrateDB.NewEntities)
            {
                checkedListTables.Items.Add(
                    new ListDisplayItem(entity.SchemaName, entity.ToString(), null, entity), 
                    entity.Import);
            }
            box.DisplayMember = "DisplayName";
            box.ValueMember = "Object.Import";

            checkedListTables.ResumeLayout();
        }

        /// <summary>
        /// Bind the list of relationships that will be imported
        /// </summary>
        private void BindNewRelationshipList()
        {
            _binding = true;

            listRelationships.SuspendLayout();

            listRelationships.DataSource = null;
            listRelationships.Items.Clear();
            
            var relationList = _migrateDB.NewRelationships
                .Where(r=> r.Import == true)
                .Select(r => new ListDisplayItem(r.SchemaName, r.ToString(), null, r))
                .ToList();

            listRelationships.DataSource = relationList;
            listRelationships.DisplayMember = "DisplayName";
            listRelationships.ValueMember = "Object";

            listRelationships.ResumeLayout();

            _binding = false;
        }

        /// <summary>
        /// Bnd the list of relationships
        /// </summary>
        private void BindRelationshipLists()
        {
            dynamic relTypeItem = comboRelationshipType.SelectedItem;

            if ((_migrateDB == null) || (relTypeItem == null)) {
                return;
            }

            _binding = true;

            RelationshipType relationType = relTypeItem.value;

            var relPrimary = new List<ListDisplayItem>();
            var relRelated = new List<ListDisplayItem>();

            comboRelationshipPrimary.DataSource = null;
            comboRelationshipSecondary.DataSource = null;

            comboRelationshipPrimary.Items.Clear();
            comboRelationshipSecondary.Items.Clear();

            var blank = new ListDisplayItem("", "", "", null);
            relPrimary.Add(blank); 
            relRelated.Add(blank);

            List<EntityMetadata> entityMeta = null;

            // do some processing ... may take a few moments
            WorkAsync(new WorkAsyncInfo
            {
                Message = $"Loading Entity Metadata",
                AsyncArgument = Service,
                Work = (worker, args) =>
                {
                    var svc = args.Argument as IOrganizationService;
                    args.Result = CDSOrgServiceCommon.RetrieveAllEntities(svc, new List<EntityFilters>() { EntityFilters.Relationships });
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null) {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    entityMeta = args.Result as List<EntityMetadata>;

                    if (relationType == RelationshipType.OneToNRelationship)
                    {
                        // add the current entities from CRM
                        foreach (var ent in entityMeta.Where(ent => ent.CanBePrimaryEntityInRelationship.Value == true))
                        {
                            var dispName = _migrateDB.GetEntityDisplayName(ent);
                            relPrimary.Add(new ListDisplayItem(ent.SchemaName, dispName, dispName, ent));
                        }

                        foreach (EntityMetadata ent in entityMeta.Where(ent => ent.CanBeRelatedEntityInRelationship.Value == true))
                        {
                            var dispName = _migrateDB.GetEntityDisplayName(ent);
                            relRelated.Add(new ListDisplayItem(ent.SchemaName, dispName, dispName, ent));
                        }

                        // now add the entities loaded from the database 
                        foreach (var ent in _migrateDB.NewEntities)
                        {
                            if (ent.Import)
                            {
                                relRelated.Add(new ListDisplayItem(ent.SchemaName, ent.ToString(), ent.DisplayName, ent.ToMetadata()));
                                relPrimary.Add(new ListDisplayItem(ent.SchemaName, ent.ToString(), ent.DisplayName, ent.ToMetadata()));
                            }
                        }

                        comboRelationshipPrimary.DataSource = relPrimary.ToList();
                        comboRelationshipSecondary.DataSource = relRelated.ToList();
                    }

                    if (relationType == RelationshipType.NToNRelationship)
                    {
                        foreach (EntityMetadata ent in entityMeta.Where(ent => ent.CanBeInManyToMany.Value == true))
                        {
                            var dispName = _migrateDB.GetEntityDisplayName(ent);
                            relPrimary.Add(new ListDisplayItem(ent.SchemaName, dispName, dispName, ent));
                        }

                        foreach (var ent in _migrateDB.NewEntities)
                        {
                            if (ent.Import)
                            {
                                relPrimary.Add(new ListDisplayItem(ent.SchemaName, ent.ToString(), ent.DisplayName, ent.ToMetadata()));
                            }
                        }

                        comboRelationshipPrimary.DataSource = relPrimary.ToList();
                        comboRelationshipSecondary.DataSource = relPrimary.ToList();
                    }

                    comboRelationshipPrimary.DisplayMember = "DisplayName";
                    comboRelationshipPrimary.ValueMember = "Object";
                    comboRelationshipPrimary.SelectedIndex = -1;

                    comboRelationshipSecondary.DisplayMember = "DisplayName";
                    comboRelationshipSecondary.ValueMember = "Object";
                    comboRelationshipSecondary.SelectedIndex = -1;

                    comboRelationshipPrimary.Enabled = true;
                    comboRelationshipSecondary.Enabled = true;

                    // NOW allow some edits
                    UpdateUIForConnection();
                }
            });
            _binding = false;
        }

        /// <summary>
        /// Add a new relationship 
        /// </summary>
        private void AddRelationship()
        {
            var ent1 = comboRelationshipPrimary.SelectedValue as EntityMetadata;
            var ent2 = comboRelationshipSecondary.SelectedValue as EntityMetadata;

            var ent1Display = _migrateDB.GetEntityDisplayName(ent1, false);
            var ent2Display = _migrateDB.GetEntityDisplayName(ent2, false);

            dynamic relTypeItem = comboRelationshipType.SelectedItem;
            RelationshipType relationType = relTypeItem.value;

            if ((relTypeItem == null) || (ent1 == null) || (ent2 == null))
            {
                return;
            }

            var relation = new MigrateRelationship(ent1.LogicalName, ent2.LogicalName, ent1Display, ent2Display, relationType, _migrateDB.Prefix, _migrateDB.LanguageCode, _migrateDB.RemoveExtraPrefixes);
            _migrateDB.NewRelationships.Add(relation);

            BindNewRelationshipList();
        }

        private void CreateEntities()
        {
            _migrateDB.CreateEntities(Service, checkPublish.Checked, checkAddNewFields.Checked);
        }

        /// <summary>
        /// Build a list of the actions to be performed for the user to review 
        /// </summary>
        private void BuildActionsSummary()
        {

            // display the summary 
            if (tabControlMain.SelectedTab == tabPageCommit)
            {
                RichTextSummary.Clear();

                var importEnt = _migrateDB.NewEntities.Where(e => e.Import).ToList();

                var fontHeader = new Font("Tahoma", 10, FontStyle.Bold);
                var fontLineItem = new Font("Tahoma", 10);

                RichTextSummary.AppendText($"Importing {importEnt.Count} Entities\n", Color.DarkBlue, fontHeader);

                // iterate on entities and show a summary of the planned changes
                foreach (var entity in importEnt)
                {
                    RichTextSummary.AppendText($"{entity.ToString()}\n", null, fontLineItem);

                    var fields = entity.Fields.Where(f => f.Import).ToList();
                    RichTextSummary.AppendText($"\tImporting {fields.Count} Fields\n", Color.DarkBlue, fontHeader);

                    foreach (var attr in fields) {
                        RichTextSummary.AppendText($"\t{attr.ToString()}\n", null, fontLineItem);
                    }
                }

                var relations = _migrateDB.NewRelationships.Where(r => r.Import).ToList();
                RichTextSummary.AppendText($"Importing {relations.Count} Fields\n", Color.DarkBlue, fontHeader);

                foreach (var relation in relations) {
                    RichTextSummary.AppendText($"\t{relation.ToString()}\n", null, fontLineItem);
                }
            }
        }

        private void ValdateRelationshipImports() {

            List<EntityMetadata> entityMeta = null;

            // do some processing ... may take a few moments
            WorkAsync(new WorkAsyncInfo
            {
                Message = $"Loading Entity Metadata",
                AsyncArgument = Service,
                Work = (worker, args) =>
                {
                    var svc = args.Argument as IOrganizationService;
                    args.Result = CDSOrgServiceCommon.RetrieveAllEntities(svc, new List<EntityFilters>() { EntityFilters.Relationships });
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    entityMeta = args.Result as List<EntityMetadata>;

                    // build a list of schema/logical names from CDS and from new list
                    var entityList = entityMeta.Select(ent => ent.LogicalName).ToList();

                    entityList.AddRange(_migrateDB.NewEntities
                        .Where(ent => ent.Import)
                        .Select(ent => ent.SchemaName.ToLower())
                        .ToList());

                    // iterate on the relationships ... if either of the entities are not in the new list or 
                    // already in CDS, then do not import the relation as it will not be valid
                    foreach (var rel in _migrateDB.NewRelationships) {
                        var import = false;
                        // two diff entities for the relation, both must be present
                        if (rel.Entity1.ToLower() != rel.Entity2.ToLower())
                        {
                            var count = entityList
                                        .Where(ent => ent == rel.Entity1.ToLower() ||
                                               ent == rel.Entity2.ToLower())
                                        .Count();
                            import = (count == 2);
                        }
                        else {
                            // self reference, only find entity name once
                            import = entityList
                                        .Where(ent => ent == rel.Entity1.ToLower())
                                        .Any();
                        }
                        // update relation 
                        rel.Import = import;
                    }
                    BindNewRelationshipList();
                }
            });
        }
        #endregion

        /// <summary>
        /// This event occurs when the plugin is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MigrateToCDS_OnCloseTool(object sender, EventArgs e)
        {
            // Before leaving, save the settings
            // SettingsManager.Instance.Save(GetType(), mySettings);
        }

        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);

            // bind some UI controls using CRM data
            if (Service!=null)
            {
                labelCDSConnection.Text = ConnectionDetail.ConnectionName;

                LoadPublishers();
            }

            // update UI now that connection has updated
            UpdateUIForConnection();
        }

        #region UI event handlers

        private void CheckedListAttributes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var listItem = checkedListAttributes.SelectedItem as ListDisplayItem;
            var attrib = listItem.Object as MigrateField;

            propGridAtribute.SelectedObject = attrib;
        }
        /// <summary>
        /// Size splitters evenly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MigrateToCDSControl_Resize(object sender, EventArgs e)
        {
            ResizeSplitter();
        }

        /// <summary>
        /// Helper to init tab resize
        /// </summary>
        /// <param name="tab"></param>
        private void ResizeSplitter(TabPage tab = null)
        {
            if (tab == null) {
                tab = tabControlMain.SelectedTab;
            }

            try
            {
                int width = (int)ClientSize.Width / 4;
                switch (tab.Name) {
                    case "tabPageTableSelections":
                        splitterTableList.SplitPosition = width;
                        splitterAttribList.SplitPosition = width;
                        splitterTableProps.SplitPosition = width;
                        break;
                    case "tabPageRelationships":
                        width = ((int)ClientSize.Width - panelAddRelation.Width) / 2;
                        splitterRelationList.SplitPosition = width;
                        flowPanelRemoveRelation.Padding = new Padding(((int)flowPanelRemoveRelation.Width / 2 - (int)buttonRemoveRelationship.Width / 2), 0, 0, 0);
                        break;
                }
            }
            catch { }
        }

        /// <summary>
        /// Close this DB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        /// <summary>
        /// Connect to your DB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonConnect_Click(object sender, EventArgs e)
        {
            ExecuteMethod(ConnectToDataSource);
        }

        /// <summary>
        /// Display the details of the selected Table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckedListTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get the entity from the list 
            var listItem = checkedListTables.SelectedItem as ListDisplayItem;
            var ent = listItem.Object as MigrateEntity;

            propGridTable.SelectedObject = ent;

            BindFieldList(ent);
        }

        /// <summary>
        /// Update the list of items to be imported
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckedList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // if we are databinding, keep the event from firing over and over... 
            if (_binding) return;

            // get the entity from the list
            var dispItem = ((CheckedListBox)sender).SelectedItem as ListDisplayItem;

            if (dispItem == null)
                return;

            var item = dispItem.Object as MigrateItemBase;

            var import = (e.NewValue == CheckState.Checked);

            if (item is MigrateEntity)
            {
                var ent = (dispItem.Object as MigrateEntity);
                ent.Import = import;

                // check to see if the entity is part of the DB or going to be imported
                var relations = _migrateDB
                    .NewRelationships
                    .Where(r => r.Entity1 == ent.SchemaName.ToLower() ||
                                r.Entity2 == ent.SchemaName.ToLower())
                    .ToList();
                // update the import value on the relation... if the entity isn't going to be imported, 
                // don't import the relation 
                if (relations.Count > 0) {
                    foreach (var r in relations) {
                        r.Import = import;
                    }
                }
                BindNewRelationshipList();
            }
            else {
                (dispItem.Object as MigrateField).Import = import;
            }
        }
        /// <summary>
        /// Update context when a Property changes
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void propGridAtribute_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            propGridAtribute.SelectedObject = propGridAtribute.SelectedObject;
        }

        /// <summary>
        /// Reload the DB using the current selections 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonReload_Click(object sender, EventArgs e)
        {
            // TODO prompt user 
            ReadDatabase();
        }


        private void comboRelationshipType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindRelationshipLists();
        }

        /// <summary>
        /// Build the summary of actions to be taken
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void tabControlMain_SelectedIndexChanged(object sender, EventArgs args)
        {
            var tab = tabControlMain.SelectedTab;
            ResizeSplitter(tab);

            switch (tab.Name) {
                case "tabPageCommit":
                    BuildActionsSummary();
                    break;
                case "tabPageRelationships":
                    ValdateRelationshipImports();
                    break;
            }
        }

        private void ButtonCreate_Click(object sender, EventArgs e)
        {
            CreateEntities();

        }

        private void ButtonAddRelationship_Click(object sender, EventArgs e)
        {
            // add a new relationship to the list 
            AddRelationship();
        }

        private void ListRelationships_SelectedIndexChanged(object sender, EventArgs e)
        {
            var listItem = listRelationships.SelectedItem as ListDisplayItem;
            
            propGridRelation.SelectedObject = listItem?.Object as MigrateRelationship;

            buttonRemoveRelationship.Enabled = (propGridRelation.SelectedObject != null);
        }

        private void ComboRelationEntity_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonAddRelationship.Enabled = 
                ((comboRelationshipPrimary.SelectedValue != null) &&
                (comboRelationshipSecondary.SelectedValue != null));                
        }

        private void ButtonRemoveRelationship_Click(object sender, EventArgs e)
        {
            var listItem = listRelationships.SelectedItem as ListDisplayItem;
            var rel = listItem?.Object as MigrateRelationship;

            if (rel != null)
            {
                _migrateDB.NewRelationships.Remove(rel);
                BindNewRelationshipList();
            }
        }

        #endregion

        ///// <summary>
        ///// Get display name from either metadata 
        ///// </summary>
        ///// <param name="logicalName"></param>
        ///// <param name="entityMeta"></param>
        ///// <returns></returns>
        //private string GetEntityDisplayName(string logicalName, List<EntityMetadata> entityMeta)
        //{
        //    string name = string.Empty;
        //    EntityMetadata em = entityMeta.Where(e => e.LogicalName == logicalName).FirstOrDefault();
        //    if (em != null)
        //    {
        //        name = _migrateDB.GetEntityDisplayName(em);
        //    }

        //    if (name == string.Empty)
        //    {
        //        name = _migrateDB.NewEntities.Where(e => e.SchemaName == logicalName).FirstOrDefault()?.DisplayName;
        //    }
        //    return (name == null) ? logicalName : name;
        //}

    }
}