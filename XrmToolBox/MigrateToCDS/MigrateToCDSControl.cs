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

            tsComboPrefix.SelectedIndex = 0;

            // update UI stuff based on whether Service connection is set
            UpdateUIForConnection();

            if (mySettings.LastUsedProviderType != null && mySettings.LastUsedConnectionString != null)
            {
                IDbProvider provider = null;
                if (mySettings.LastUsedProviderType == SQLDBProvider.Name) {
                    provider = new SQLDBProvider(mySettings.LastUsedConnectionString);
                }
                else {
                    provider = new AccessDBProvider(mySettings.LastUsedConnectionString);
                }

                _migrateDB = new MigrateDataBase(provider);

                ExecuteMethod(ReadDatabase);
            }

        }

        #endregion

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
            var prefix = tsComboPrefix.Text?.Trim().ToLower();

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
                    BindMainRelationshipList();
                    BindRelationshipLists();
                }
            });
        }


        #region List helper methods
        private void ClearLists() {
            ChkListTables.Items.Clear();
            ChkListAttributes.Items.Clear();
        }

        private void BindFieldList(MigrateEntity entity)
        {
            ChkListAttributes.SuspendLayout();

            ChkListAttributes.Items.Clear();

            foreach (var field in entity.Fields)
            {
                ChkListAttributes.Items.Add(
                    new ListDisplayItem(field.SchemaName, field.ToString(), null, field), 
                    field.Import);
            }
            var box = ((ListBox)ChkListAttributes);
            box.DisplayMember = "SummaryName";
            box.ValueMember = "Object.Import";

            ChkListAttributes.ResumeLayout();
        }

        private void BindEntitiesList() {

            ChkListTables.SuspendLayout();

            ChkListTables.Items.Clear();
            foreach (var entity in _migrateDB.NewEntities)
            {
                ChkListTables.Items.Add(
                    new ListDisplayItem(entity.SchemaName, entity.ToString(), null, entity), 
                    entity.Import);
            }
            var box = ((ListBox)ChkListTables);
            box.DisplayMember = "SummaryName";
            box.ValueMember = "Object.Import";

            ChkListTables.ResumeLayout();
        }

        private void BindMainRelationshipList()
        {
            _binding = true;

            lstRelationships.SuspendLayout();

            lstRelationships.DataSource = null;
            lstRelationships.Items.Clear();

            lstRelationships.DataSource = _migrateDB.NewRelationships.Select(r => new ListDisplayItem(r.SchemaName, r.ToString(), null, r)).ToList();
            lstRelationships.DisplayMember = "SummaryName";
            lstRelationships.ValueMember = "Name";

            lstRelationships.ResumeLayout();

            _binding = false;
        }

        private void BindRelationshipLists()
        {
            _binding = true;

            dynamic relTypeItem = comboRelationshipType.SelectedItem;

            if (relTypeItem == null)
            {
                return;
            }

            var relPrimary = new SortedDictionary<string, string>();
            var relRelated = new SortedDictionary<string, string>();

            comboRelationshipPrimary.DataSource = null;
            comboRelationshipSecondary.DataSource = null;

            comboRelationshipPrimary.Items.Clear();
            comboRelationshipSecondary.Items.Clear();

            relPrimary.Add(string.Empty, string.Empty);
            relRelated.Add(string.Empty, string.Empty);

            RelationshipType relationType = relTypeItem.value;

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
                            relPrimary.Add(ent.LogicalName, _migrateDB.GetEntityDisplayName(ent));
                        }

                        foreach (EntityMetadata ent in entityMeta.Where(ent => ent.CanBeRelatedEntityInRelationship.Value == true))
                        {
                            relRelated.Add(ent.LogicalName, _migrateDB.GetEntityDisplayName(ent));
                        }

                        // now add the entities loaded from the database 
                        foreach (var ent in _migrateDB.NewEntities)
                        {
                            if (ent.Import)
                            {
                                relPrimary.Add(ent.SchemaName, ent.DisplayName);
                                relRelated.Add(ent.SchemaName, ent.DisplayName);
                            }
                        }

                        comboRelationshipPrimary.DataSource = relPrimary.ToList();
                        comboRelationshipSecondary.DataSource = relRelated.ToList();
                    }

                    if (relationType == RelationshipType.NToNRelationship)
                    {
                        foreach (EntityMetadata ent in entityMeta.Where(ent => ent.CanBeInManyToMany.Value == true))
                        {
                            relPrimary.Add(ent.LogicalName, _migrateDB.GetEntityDisplayName(ent));
                        }

                        foreach (var ent in _migrateDB.NewEntities)
                        {
                            if (ent.Import)
                            {
                                relPrimary.Add(ent.SchemaName, ent.DisplayName);
                            }
                        }

                        comboRelationshipPrimary.DataSource = relPrimary.ToList();
                        comboRelationshipSecondary.DataSource = relPrimary.ToList();
                    }

                    comboRelationshipPrimary.DisplayMember = "Value";
                    comboRelationshipPrimary.ValueMember = "Key";
                    comboRelationshipPrimary.SelectedIndex = -1;

                    comboRelationshipSecondary.DisplayMember = "Value";
                    comboRelationshipSecondary.ValueMember = "Key";
                    comboRelationshipSecondary.SelectedIndex = -1;

                    comboRelationshipPrimary.Enabled = true;
                    comboRelationshipSecondary.Enabled = true;
                }
            });
            _binding = false;
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
            SettingsManager.Instance.Save(GetType(), mySettings);
        }

        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);

            if (mySettings != null && detail != null)
            {
                mySettings.LastUsedOrganizationWebappUrl = detail.WebApplicationUrl;
                LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);
            }

            // update UI now that connection has updated
            UpdateUIForConnection();
        }

        private void UpdateUIForConnection()
        {
            var serviceSet = (Service != null);
            comboRelationshipType.Enabled = serviceSet;
            tabControlMain.Enabled = serviceSet;

            // some other stuff 
            // TDOO clean this up
            tsButtonReload.Enabled = (_migrateDB != null);

            // bind some UI controls using CRM data
            if (serviceSet) {
                LoadPublishers();
            }
        }

        private void ChkListAttributes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var listItem = ChkListAttributes.SelectedItem as ListDisplayItem;
            var attrib = listItem.Object as MigrateField;

            propGridAtribute.SelectedObject = attrib;
        }

        private void MigrateToCDSControl_Resize(object sender, EventArgs e)
        {
            try
            {
                int width = (int)ClientSize.Width / 4;
                splitContainerTablesMain.SplitterDistance = width;
                splitterAttribs.SplitPosition = width;
                splitterTableProps.SplitPosition = width;
            }
            catch { }
        }

        #region UI event handlers

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void ButtonConnect_Click(object sender, EventArgs e)
        {
            ExecuteMethod(ConnectToDataSource);

            UpdateUIForConnection();
        }
        private void ChkListTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get the entity from the list 
            var listItem = ChkListTables.SelectedItem as ListDisplayItem;
            var ent = listItem.Object as MigrateEntity;

            propGridTable.SelectedObject = ent;

            BindFieldList(ent);
        }

        private void ChkList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // if we are databinding, keep the event from firing over and over... 
            if (_binding) return;

            // get the entity from the list
            var dispItem = ((CheckedListBox)sender ).SelectedItem as ListDisplayItem;

            if (dispItem == null)
                return;

            var item = dispItem.Object as MigrateItemBase;

            var import = (e.NewValue == CheckState.Checked);
            // item.Import = import;
            if (item is MigrateEntity)
                (dispItem.Object as MigrateEntity).Import = import;
            else
                (dispItem.Object as MigrateField).Import = import;
        }

        private void propGridAtribute_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            propGridAtribute.SelectedObject = propGridAtribute.SelectedObject;
        }

        private void ComboPrefix_Update(object sender, EventArgs e)
        {
            // labelSelectedPrefix.Text = toolStripComboBox1.Text;
        }

        private void tsButtonReload_Click(object sender, EventArgs e)
        {
            ReadDatabase();
        }

        private void comboRelationshipType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion

        /// <summary>
        /// Get display name from either metadata 
        /// </summary>
        /// <param name="logicalName"></param>
        /// <param name="entityMeta"></param>
        /// <returns></returns>
        private string GetEntityDisplayName(string logicalName, List<EntityMetadata> entityMeta)
        {
            string name = string.Empty;
            EntityMetadata em = entityMeta.Where(e => e.LogicalName == logicalName).FirstOrDefault();
            if (em != null)
            {
                name = _migrateDB.GetEntityDisplayName(em);
            }

            if (name == string.Empty)
            {
                name = _migrateDB.NewEntities.Where(e => e.SchemaName == logicalName).FirstOrDefault()?.DisplayName;
            }
            return (name == null) ? logicalName : name;
        }

        private void LoadPublishers() {

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
                    tsComboPrefix.Items.Clear();
                    var list = publishers
                        .Where(p => p.Attributes["customizationprefix"]?.ToString().Length >0)
                        .Select(p => p.Attributes["customizationprefix"].ToString())
                        .ToList();

                    list.Insert(0, "migrate2");
                    tsComboPrefix.Items.AddRange( list.ToArray());

                    tsComboPrefix.SelectedIndex = 0;
                }
            });

        }

        private void tabControlMain_SelectedIndexChanged(object sender, EventArgs args)
        {
            // display the summary 
            if (tabControlMain.SelectedTab == tabPageCommit) {

                RichTextSummary.Clear();

                var importEnt = _migrateDB.NewEntities.Where(e => e.Import).ToList();

                var fontHeader = new Font("Tahoma", 11, FontStyle.Bold);
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

                var relations = _migrateDB.NewRelationships;
                RichTextSummary.AppendText($"Importing {relations.Count} Fields\n", Color.DarkBlue, fontHeader);
                foreach (var relation in relations) {
                    RichTextSummary.AppendText($"\t{relation.ToString()}\n", null, fontLineItem);
                }
            }
        }
    }
}