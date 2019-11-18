namespace CDSTools
{
    partial class MigrateToCDSControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MigrateToCDSControl));
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.checkedListTables = new System.Windows.Forms.CheckedListBox();
            this.panelOptions = new System.Windows.Forms.Panel();
            this.labelCDSConnection = new System.Windows.Forms.Label();
            this.labelConnect = new System.Windows.Forms.Label();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonReload = new System.Windows.Forms.Button();
            this.labelMainInstruct = new System.Windows.Forms.Label();
            this.textBoxDBConnection = new System.Windows.Forms.TextBox();
            this.labelPrefix = new System.Windows.Forms.Label();
            this.comboPrefix = new System.Windows.Forms.ComboBox();
            this.checkAddNewFields = new System.Windows.Forms.CheckBox();
            this.labelTableProps = new System.Windows.Forms.Label();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageTableSelections = new System.Windows.Forms.TabPage();
            this.panelAttribProps = new System.Windows.Forms.Panel();
            this.propGridAtribute = new System.Windows.Forms.PropertyGrid();
            this.labelAttribProps = new System.Windows.Forms.Label();
            this.splitterAttribList = new System.Windows.Forms.Splitter();
            this.panelAttribList = new System.Windows.Forms.Panel();
            this.checkedListAttributes = new System.Windows.Forms.CheckedListBox();
            this.panelAttribsCheckOptions = new System.Windows.Forms.Panel();
            this.linkAttribCheckAll = new System.Windows.Forms.LinkLabel();
            this.linkAttribCheckNone = new System.Windows.Forms.LinkLabel();
            this.labelTableAttributes = new System.Windows.Forms.Label();
            this.splitterTableProps = new System.Windows.Forms.Splitter();
            this.panelTableProps = new System.Windows.Forms.Panel();
            this.propGridTable = new System.Windows.Forms.PropertyGrid();
            this.labelTableDetails = new System.Windows.Forms.Label();
            this.splitterTableList = new System.Windows.Forms.Splitter();
            this.panelTableList = new System.Windows.Forms.Panel();
            this.panelTablesCheckOptions = new System.Windows.Forms.Panel();
            this.linkTableCheckAll = new System.Windows.Forms.LinkLabel();
            this.linkTableCheckNone = new System.Windows.Forms.LinkLabel();
            this.labelTablesFound = new System.Windows.Forms.Label();
            this.tabPageRelationships = new System.Windows.Forms.TabPage();
            this.panelRelationProps = new System.Windows.Forms.Panel();
            this.propGridRelation = new System.Windows.Forms.PropertyGrid();
            this.flowPanelRemoveRelation = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonRemoveRelationship = new System.Windows.Forms.Button();
            this.splitterRelationList = new System.Windows.Forms.Splitter();
            this.listRelationships = new System.Windows.Forms.ListBox();
            this.panelAddRelation = new System.Windows.Forms.Panel();
            this.buttonAddRelationship = new System.Windows.Forms.Button();
            this.labelRelatedEntity = new System.Windows.Forms.Label();
            this.labelPrimaryEntity = new System.Windows.Forms.Label();
            this.labelRelationshipType = new System.Windows.Forms.Label();
            this.comboRelationshipType = new System.Windows.Forms.ComboBox();
            this.comboRelationshipSecondary = new System.Windows.Forms.ComboBox();
            this.comboRelationshipPrimary = new System.Windows.Forms.ComboBox();
            this.labelRelationInstructions = new System.Windows.Forms.Label();
            this.tabPageCommit = new System.Windows.Forms.TabPage();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.RichTextSummary = new System.Windows.Forms.RichTextBox();
            this.checkPublish = new System.Windows.Forms.CheckBox();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.tsLabelConnString = new System.Windows.Forms.ToolStripLabel();
            this.tstxtConnectionString = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenu.SuspendLayout();
            this.panelOptions.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageTableSelections.SuspendLayout();
            this.panelAttribProps.SuspendLayout();
            this.panelAttribList.SuspendLayout();
            this.panelAttribsCheckOptions.SuspendLayout();
            this.panelTableProps.SuspendLayout();
            this.panelTableList.SuspendLayout();
            this.panelTablesCheckOptions.SuspendLayout();
            this.tabPageRelationships.SuspendLayout();
            this.panelRelationProps.SuspendLayout();
            this.flowPanelRemoveRelation.SuspendLayout();
            this.panelAddRelation.SuspendLayout();
            this.tabPageCommit.SuspendLayout();
            this.statusStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.toolStripSeparator});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new System.Drawing.Size(1123, 25);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(40, 22);
            this.tsbClose.Text = "Close";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // checkedListTables
            // 
            this.checkedListTables.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.checkedListTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListTables.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListTables.FormattingEnabled = true;
            this.checkedListTables.Location = new System.Drawing.Point(4, 58);
            this.checkedListTables.Margin = new System.Windows.Forms.Padding(2);
            this.checkedListTables.Name = "checkedListTables";
            this.checkedListTables.Size = new System.Drawing.Size(224, 261);
            this.checkedListTables.TabIndex = 10;
            this.checkedListTables.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CheckedList_ItemCheck);
            this.checkedListTables.SelectedIndexChanged += new System.EventHandler(this.CheckedListTables_SelectedIndexChanged);
            // 
            // panelOptions
            // 
            this.panelOptions.Controls.Add(this.labelCDSConnection);
            this.panelOptions.Controls.Add(this.labelConnect);
            this.panelOptions.Controls.Add(this.buttonConnect);
            this.panelOptions.Controls.Add(this.buttonReload);
            this.panelOptions.Controls.Add(this.labelMainInstruct);
            this.panelOptions.Controls.Add(this.textBoxDBConnection);
            this.panelOptions.Controls.Add(this.labelPrefix);
            this.panelOptions.Controls.Add(this.comboPrefix);
            this.panelOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelOptions.Location = new System.Drawing.Point(0, 25);
            this.panelOptions.Margin = new System.Windows.Forms.Padding(2);
            this.panelOptions.Name = "panelOptions";
            this.panelOptions.Padding = new System.Windows.Forms.Padding(5);
            this.panelOptions.Size = new System.Drawing.Size(1123, 100);
            this.panelOptions.TabIndex = 16;
            // 
            // labelCDSConnection
            // 
            this.labelCDSConnection.AutoSize = true;
            this.labelCDSConnection.Location = new System.Drawing.Point(254, 43);
            this.labelCDSConnection.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCDSConnection.Name = "labelCDSConnection";
            this.labelCDSConnection.Size = new System.Drawing.Size(82, 13);
            this.labelCDSConnection.TabIndex = 23;
            this.labelCDSConnection.Text = "(not connected)";
            // 
            // labelConnect
            // 
            this.labelConnect.AutoSize = true;
            this.labelConnect.Location = new System.Drawing.Point(11, 71);
            this.labelConnect.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelConnect.Name = "labelConnect";
            this.labelConnect.Size = new System.Drawing.Size(125, 13);
            this.labelConnect.TabIndex = 18;
            this.labelConnect.Text = "Connect to Data Source:";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(483, 66);
            this.buttonConnect.Margin = new System.Windows.Forms.Padding(2);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(57, 23);
            this.buttonConnect.TabIndex = 17;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.ButtonConnect_Click);
            // 
            // buttonReload
            // 
            this.buttonReload.Location = new System.Drawing.Point(546, 66);
            this.buttonReload.Margin = new System.Windows.Forms.Padding(2);
            this.buttonReload.Name = "buttonReload";
            this.buttonReload.Size = new System.Drawing.Size(57, 23);
            this.buttonReload.TabIndex = 19;
            this.buttonReload.Text = "Reload";
            this.buttonReload.UseVisualStyleBackColor = true;
            this.buttonReload.Click += new System.EventHandler(this.ButtonReload_Click);
            // 
            // labelMainInstruct
            // 
            this.labelMainInstruct.BackColor = System.Drawing.SystemColors.Info;
            this.labelMainInstruct.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelMainInstruct.Location = new System.Drawing.Point(5, 5);
            this.labelMainInstruct.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelMainInstruct.Name = "labelMainInstruct";
            this.labelMainInstruct.Padding = new System.Windows.Forms.Padding(1);
            this.labelMainInstruct.Size = new System.Drawing.Size(1113, 25);
            this.labelMainInstruct.TabIndex = 16;
            this.labelMainInstruct.Text = resources.GetString("labelMainInstruct.Text");
            // 
            // textBoxDBConnection
            // 
            this.textBoxDBConnection.HideSelection = false;
            this.textBoxDBConnection.Location = new System.Drawing.Point(139, 70);
            this.textBoxDBConnection.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxDBConnection.Name = "textBoxDBConnection";
            this.textBoxDBConnection.ReadOnly = true;
            this.textBoxDBConnection.Size = new System.Drawing.Size(333, 20);
            this.textBoxDBConnection.TabIndex = 22;
            // 
            // labelPrefix
            // 
            this.labelPrefix.AutoSize = true;
            this.labelPrefix.Location = new System.Drawing.Point(11, 43);
            this.labelPrefix.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPrefix.Name = "labelPrefix";
            this.labelPrefix.Size = new System.Drawing.Size(121, 13);
            this.labelPrefix.TabIndex = 21;
            this.labelPrefix.Text = "Choose Publisher Prefix:";
            // 
            // comboPrefix
            // 
            this.comboPrefix.FormattingEnabled = true;
            this.comboPrefix.Location = new System.Drawing.Point(139, 41);
            this.comboPrefix.Margin = new System.Windows.Forms.Padding(2);
            this.comboPrefix.Name = "comboPrefix";
            this.comboPrefix.Size = new System.Drawing.Size(101, 21);
            this.comboPrefix.TabIndex = 20;
            // 
            // checkAddNewFields
            // 
            this.checkAddNewFields.AutoSize = true;
            this.checkAddNewFields.Location = new System.Drawing.Point(772, 25);
            this.checkAddNewFields.Name = "checkAddNewFields";
            this.checkAddNewFields.Size = new System.Drawing.Size(123, 17);
            this.checkAddNewFields.TabIndex = 35;
            this.checkAddNewFields.Text = "Add Fields To Form?";
            this.checkAddNewFields.UseVisualStyleBackColor = true;
            // 
            // labelTableProps
            // 
            this.labelTableProps.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTableProps.Location = new System.Drawing.Point(10, 10);
            this.labelTableProps.Name = "labelTableProps";
            this.labelTableProps.Size = new System.Drawing.Size(468, 37);
            this.labelTableProps.TabIndex = 15;
            this.labelTableProps.Text = "Table Details";
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageTableSelections);
            this.tabControlMain.Controls.Add(this.tabPageRelationships);
            this.tabControlMain.Controls.Add(this.tabPageCommit);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Enabled = false;
            this.tabControlMain.ItemSize = new System.Drawing.Size(200, 30);
            this.tabControlMain.Location = new System.Drawing.Point(0, 125);
            this.tabControlMain.Margin = new System.Windows.Forms.Padding(2);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1123, 385);
            this.tabControlMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControlMain.TabIndex = 35;
            this.tabControlMain.SelectedIndexChanged += new System.EventHandler(this.tabControlMain_SelectedIndexChanged);
            // 
            // tabPageTableSelections
            // 
            this.tabPageTableSelections.Controls.Add(this.panelAttribProps);
            this.tabPageTableSelections.Controls.Add(this.splitterAttribList);
            this.tabPageTableSelections.Controls.Add(this.panelAttribList);
            this.tabPageTableSelections.Controls.Add(this.splitterTableProps);
            this.tabPageTableSelections.Controls.Add(this.panelTableProps);
            this.tabPageTableSelections.Controls.Add(this.splitterTableList);
            this.tabPageTableSelections.Controls.Add(this.panelTableList);
            this.tabPageTableSelections.Location = new System.Drawing.Point(4, 34);
            this.tabPageTableSelections.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageTableSelections.Name = "tabPageTableSelections";
            this.tabPageTableSelections.Padding = new System.Windows.Forms.Padding(12);
            this.tabPageTableSelections.Size = new System.Drawing.Size(1115, 347);
            this.tabPageTableSelections.TabIndex = 0;
            this.tabPageTableSelections.Text = "Table and Entity Details";
            // 
            // panelAttribProps
            // 
            this.panelAttribProps.Controls.Add(this.propGridAtribute);
            this.panelAttribProps.Controls.Add(this.labelAttribProps);
            this.panelAttribProps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAttribProps.Location = new System.Drawing.Point(837, 12);
            this.panelAttribProps.Margin = new System.Windows.Forms.Padding(2);
            this.panelAttribProps.Name = "panelAttribProps";
            this.panelAttribProps.Padding = new System.Windows.Forms.Padding(4);
            this.panelAttribProps.Size = new System.Drawing.Size(266, 323);
            this.panelAttribProps.TabIndex = 26;
            // 
            // propGridAtribute
            // 
            this.propGridAtribute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propGridAtribute.Location = new System.Drawing.Point(4, 41);
            this.propGridAtribute.Margin = new System.Windows.Forms.Padding(2);
            this.propGridAtribute.Name = "propGridAtribute";
            this.propGridAtribute.Size = new System.Drawing.Size(258, 278);
            this.propGridAtribute.TabIndex = 14;
            // 
            // labelAttribProps
            // 
            this.labelAttribProps.BackColor = System.Drawing.SystemColors.Info;
            this.labelAttribProps.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelAttribProps.Location = new System.Drawing.Point(4, 4);
            this.labelAttribProps.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelAttribProps.Name = "labelAttribProps";
            this.labelAttribProps.Padding = new System.Windows.Forms.Padding(2);
            this.labelAttribProps.Size = new System.Drawing.Size(258, 37);
            this.labelAttribProps.TabIndex = 17;
            this.labelAttribProps.Text = "Review and update the details for the selected Field/Attribute";
            // 
            // splitterAttribList
            // 
            this.splitterAttribList.Location = new System.Drawing.Point(829, 12);
            this.splitterAttribList.Margin = new System.Windows.Forms.Padding(2);
            this.splitterAttribList.Name = "splitterAttribList";
            this.splitterAttribList.Size = new System.Drawing.Size(8, 323);
            this.splitterAttribList.TabIndex = 25;
            this.splitterAttribList.TabStop = false;
            // 
            // panelAttribList
            // 
            this.panelAttribList.Controls.Add(this.checkedListAttributes);
            this.panelAttribList.Controls.Add(this.panelAttribsCheckOptions);
            this.panelAttribList.Controls.Add(this.labelTableAttributes);
            this.panelAttribList.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelAttribList.Location = new System.Drawing.Point(548, 12);
            this.panelAttribList.Margin = new System.Windows.Forms.Padding(2);
            this.panelAttribList.Name = "panelAttribList";
            this.panelAttribList.Padding = new System.Windows.Forms.Padding(4);
            this.panelAttribList.Size = new System.Drawing.Size(281, 323);
            this.panelAttribList.TabIndex = 24;
            // 
            // checkedListAttributes
            // 
            this.checkedListAttributes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.checkedListAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListAttributes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListAttributes.FormattingEnabled = true;
            this.checkedListAttributes.Location = new System.Drawing.Point(4, 61);
            this.checkedListAttributes.Margin = new System.Windows.Forms.Padding(6);
            this.checkedListAttributes.Name = "checkedListAttributes";
            this.checkedListAttributes.Size = new System.Drawing.Size(273, 258);
            this.checkedListAttributes.TabIndex = 15;
            this.checkedListAttributes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CheckedList_ItemCheck);
            this.checkedListAttributes.SelectedIndexChanged += new System.EventHandler(this.CheckedListAttributes_SelectedIndexChanged);
            // 
            // panelAttribsCheckOptions
            // 
            this.panelAttribsCheckOptions.Controls.Add(this.linkAttribCheckAll);
            this.panelAttribsCheckOptions.Controls.Add(this.linkAttribCheckNone);
            this.panelAttribsCheckOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelAttribsCheckOptions.Location = new System.Drawing.Point(4, 41);
            this.panelAttribsCheckOptions.Margin = new System.Windows.Forms.Padding(2);
            this.panelAttribsCheckOptions.Name = "panelAttribsCheckOptions";
            this.panelAttribsCheckOptions.Size = new System.Drawing.Size(273, 20);
            this.panelAttribsCheckOptions.TabIndex = 18;
            // 
            // linkAttribCheckAll
            // 
            this.linkAttribCheckAll.Dock = System.Windows.Forms.DockStyle.Right;
            this.linkAttribCheckAll.Location = new System.Drawing.Point(133, 0);
            this.linkAttribCheckAll.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkAttribCheckAll.Name = "linkAttribCheckAll";
            this.linkAttribCheckAll.Size = new System.Drawing.Size(70, 20);
            this.linkAttribCheckAll.TabIndex = 1;
            this.linkAttribCheckAll.TabStop = true;
            this.linkAttribCheckAll.Text = "Check All";
            this.linkAttribCheckAll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkAttribCheckAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAttribCheckAll_LinkClicked);
            // 
            // linkAttribCheckNone
            // 
            this.linkAttribCheckNone.Dock = System.Windows.Forms.DockStyle.Right;
            this.linkAttribCheckNone.Location = new System.Drawing.Point(203, 0);
            this.linkAttribCheckNone.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkAttribCheckNone.Name = "linkAttribCheckNone";
            this.linkAttribCheckNone.Size = new System.Drawing.Size(70, 20);
            this.linkAttribCheckNone.TabIndex = 0;
            this.linkAttribCheckNone.TabStop = true;
            this.linkAttribCheckNone.Text = "Check None";
            this.linkAttribCheckNone.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkAttribCheckNone.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAttribCheckNone_LinkClicked);
            // 
            // labelTableAttributes
            // 
            this.labelTableAttributes.BackColor = System.Drawing.SystemColors.Info;
            this.labelTableAttributes.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTableAttributes.Location = new System.Drawing.Point(4, 4);
            this.labelTableAttributes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTableAttributes.Name = "labelTableAttributes";
            this.labelTableAttributes.Padding = new System.Windows.Forms.Padding(2);
            this.labelTableAttributes.Size = new System.Drawing.Size(273, 37);
            this.labelTableAttributes.TabIndex = 16;
            this.labelTableAttributes.Text = "Review and update the Fields to be created with the new Entity";
            // 
            // splitterTableProps
            // 
            this.splitterTableProps.Location = new System.Drawing.Point(540, 12);
            this.splitterTableProps.Margin = new System.Windows.Forms.Padding(2);
            this.splitterTableProps.Name = "splitterTableProps";
            this.splitterTableProps.Size = new System.Drawing.Size(8, 323);
            this.splitterTableProps.TabIndex = 21;
            this.splitterTableProps.TabStop = false;
            // 
            // panelTableProps
            // 
            this.panelTableProps.Controls.Add(this.propGridTable);
            this.panelTableProps.Controls.Add(this.labelTableDetails);
            this.panelTableProps.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelTableProps.Location = new System.Drawing.Point(252, 12);
            this.panelTableProps.Margin = new System.Windows.Forms.Padding(2);
            this.panelTableProps.Name = "panelTableProps";
            this.panelTableProps.Padding = new System.Windows.Forms.Padding(4);
            this.panelTableProps.Size = new System.Drawing.Size(288, 323);
            this.panelTableProps.TabIndex = 22;
            // 
            // propGridTable
            // 
            this.propGridTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propGridTable.Location = new System.Drawing.Point(4, 41);
            this.propGridTable.Margin = new System.Windows.Forms.Padding(2, 5, 2, 2);
            this.propGridTable.Name = "propGridTable";
            this.propGridTable.Size = new System.Drawing.Size(280, 278);
            this.propGridTable.TabIndex = 18;
            this.propGridTable.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propGridAtribute_PropertyValueChanged);
            // 
            // labelTableDetails
            // 
            this.labelTableDetails.BackColor = System.Drawing.SystemColors.Info;
            this.labelTableDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTableDetails.Location = new System.Drawing.Point(4, 4);
            this.labelTableDetails.Margin = new System.Windows.Forms.Padding(2, 0, 2, 4);
            this.labelTableDetails.Name = "labelTableDetails";
            this.labelTableDetails.Padding = new System.Windows.Forms.Padding(2);
            this.labelTableDetails.Size = new System.Drawing.Size(280, 37);
            this.labelTableDetails.TabIndex = 19;
            this.labelTableDetails.Text = "Review and update selected Table to Entity Details";
            // 
            // splitterTableList
            // 
            this.splitterTableList.Location = new System.Drawing.Point(244, 12);
            this.splitterTableList.Margin = new System.Windows.Forms.Padding(2);
            this.splitterTableList.Name = "splitterTableList";
            this.splitterTableList.Size = new System.Drawing.Size(8, 323);
            this.splitterTableList.TabIndex = 20;
            this.splitterTableList.TabStop = false;
            // 
            // panelTableList
            // 
            this.panelTableList.Controls.Add(this.checkedListTables);
            this.panelTableList.Controls.Add(this.panelTablesCheckOptions);
            this.panelTableList.Controls.Add(this.labelTablesFound);
            this.panelTableList.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelTableList.Location = new System.Drawing.Point(12, 12);
            this.panelTableList.Margin = new System.Windows.Forms.Padding(2);
            this.panelTableList.Name = "panelTableList";
            this.panelTableList.Padding = new System.Windows.Forms.Padding(4);
            this.panelTableList.Size = new System.Drawing.Size(232, 323);
            this.panelTableList.TabIndex = 1;
            // 
            // panelTablesCheckOptions
            // 
            this.panelTablesCheckOptions.Controls.Add(this.linkTableCheckAll);
            this.panelTablesCheckOptions.Controls.Add(this.linkTableCheckNone);
            this.panelTablesCheckOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTablesCheckOptions.Location = new System.Drawing.Point(4, 38);
            this.panelTablesCheckOptions.Margin = new System.Windows.Forms.Padding(2);
            this.panelTablesCheckOptions.Name = "panelTablesCheckOptions";
            this.panelTablesCheckOptions.Size = new System.Drawing.Size(224, 20);
            this.panelTablesCheckOptions.TabIndex = 17;
            // 
            // linkTableCheckAll
            // 
            this.linkTableCheckAll.Dock = System.Windows.Forms.DockStyle.Right;
            this.linkTableCheckAll.Location = new System.Drawing.Point(84, 0);
            this.linkTableCheckAll.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkTableCheckAll.Name = "linkTableCheckAll";
            this.linkTableCheckAll.Size = new System.Drawing.Size(70, 20);
            this.linkTableCheckAll.TabIndex = 1;
            this.linkTableCheckAll.TabStop = true;
            this.linkTableCheckAll.Text = "Check All";
            this.linkTableCheckAll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkTableCheckAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkTableCheckAll_LinkClicked);
            // 
            // linkTableCheckNone
            // 
            this.linkTableCheckNone.Dock = System.Windows.Forms.DockStyle.Right;
            this.linkTableCheckNone.Location = new System.Drawing.Point(154, 0);
            this.linkTableCheckNone.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkTableCheckNone.Name = "linkTableCheckNone";
            this.linkTableCheckNone.Size = new System.Drawing.Size(70, 20);
            this.linkTableCheckNone.TabIndex = 0;
            this.linkTableCheckNone.TabStop = true;
            this.linkTableCheckNone.Text = "Check None";
            this.linkTableCheckNone.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkTableCheckNone.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkTableCheckNone_LinkClicked);
            // 
            // labelTablesFound
            // 
            this.labelTablesFound.BackColor = System.Drawing.SystemColors.Info;
            this.labelTablesFound.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTablesFound.Location = new System.Drawing.Point(4, 4);
            this.labelTablesFound.Margin = new System.Windows.Forms.Padding(2, 0, 2, 5);
            this.labelTablesFound.Name = "labelTablesFound";
            this.labelTablesFound.Padding = new System.Windows.Forms.Padding(2);
            this.labelTablesFound.Size = new System.Drawing.Size(224, 34);
            this.labelTablesFound.TabIndex = 16;
            this.labelTablesFound.Text = "Check the Tables that you would like to import as CDS Entities.";
            // 
            // tabPageRelationships
            // 
            this.tabPageRelationships.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageRelationships.Controls.Add(this.panelRelationProps);
            this.tabPageRelationships.Controls.Add(this.splitterRelationList);
            this.tabPageRelationships.Controls.Add(this.listRelationships);
            this.tabPageRelationships.Controls.Add(this.panelAddRelation);
            this.tabPageRelationships.Controls.Add(this.labelRelationInstructions);
            this.tabPageRelationships.Location = new System.Drawing.Point(4, 34);
            this.tabPageRelationships.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageRelationships.Name = "tabPageRelationships";
            this.tabPageRelationships.Padding = new System.Windows.Forms.Padding(12);
            this.tabPageRelationships.Size = new System.Drawing.Size(1115, 347);
            this.tabPageRelationships.TabIndex = 1;
            this.tabPageRelationships.Text = "Entity Relationships";
            // 
            // panelRelationProps
            // 
            this.panelRelationProps.Controls.Add(this.propGridRelation);
            this.panelRelationProps.Controls.Add(this.flowPanelRemoveRelation);
            this.panelRelationProps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRelationProps.Location = new System.Drawing.Point(805, 55);
            this.panelRelationProps.Name = "panelRelationProps";
            this.panelRelationProps.Size = new System.Drawing.Size(298, 280);
            this.panelRelationProps.TabIndex = 6;
            // 
            // propGridRelation
            // 
            this.propGridRelation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propGridRelation.Location = new System.Drawing.Point(0, 0);
            this.propGridRelation.Margin = new System.Windows.Forms.Padding(2);
            this.propGridRelation.Name = "propGridRelation";
            this.propGridRelation.Size = new System.Drawing.Size(298, 238);
            this.propGridRelation.TabIndex = 22;
            // 
            // flowPanelRemoveRelation
            // 
            this.flowPanelRemoveRelation.Controls.Add(this.buttonRemoveRelationship);
            this.flowPanelRemoveRelation.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowPanelRemoveRelation.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowPanelRemoveRelation.Location = new System.Drawing.Point(0, 238);
            this.flowPanelRemoveRelation.Margin = new System.Windows.Forms.Padding(2);
            this.flowPanelRemoveRelation.Name = "flowPanelRemoveRelation";
            this.flowPanelRemoveRelation.Size = new System.Drawing.Size(298, 42);
            this.flowPanelRemoveRelation.TabIndex = 17;
            // 
            // buttonRemoveRelationship
            // 
            this.buttonRemoveRelationship.Enabled = false;
            this.buttonRemoveRelationship.Location = new System.Drawing.Point(6, 6);
            this.buttonRemoveRelationship.Margin = new System.Windows.Forms.Padding(6);
            this.buttonRemoveRelationship.Name = "buttonRemoveRelationship";
            this.buttonRemoveRelationship.Size = new System.Drawing.Size(119, 27);
            this.buttonRemoveRelationship.TabIndex = 16;
            this.buttonRemoveRelationship.Text = "Remove Relationship";
            this.buttonRemoveRelationship.UseVisualStyleBackColor = true;
            this.buttonRemoveRelationship.Click += new System.EventHandler(this.ButtonRemoveRelationship_Click);
            // 
            // splitterRelationList
            // 
            this.splitterRelationList.Location = new System.Drawing.Point(797, 55);
            this.splitterRelationList.Margin = new System.Windows.Forms.Padding(2);
            this.splitterRelationList.Name = "splitterRelationList";
            this.splitterRelationList.Size = new System.Drawing.Size(8, 280);
            this.splitterRelationList.TabIndex = 22;
            this.splitterRelationList.TabStop = false;
            // 
            // listRelationships
            // 
            this.listRelationships.DisplayMember = "SummaryName";
            this.listRelationships.Dock = System.Windows.Forms.DockStyle.Left;
            this.listRelationships.FormattingEnabled = true;
            this.listRelationships.Location = new System.Drawing.Point(386, 55);
            this.listRelationships.Margin = new System.Windows.Forms.Padding(6);
            this.listRelationships.Name = "listRelationships";
            this.listRelationships.Size = new System.Drawing.Size(411, 280);
            this.listRelationships.TabIndex = 4;
            this.listRelationships.ValueMember = "Name";
            this.listRelationships.SelectedIndexChanged += new System.EventHandler(this.ListRelationships_SelectedIndexChanged);
            // 
            // panelAddRelation
            // 
            this.panelAddRelation.Controls.Add(this.buttonAddRelationship);
            this.panelAddRelation.Controls.Add(this.labelRelatedEntity);
            this.panelAddRelation.Controls.Add(this.labelPrimaryEntity);
            this.panelAddRelation.Controls.Add(this.labelRelationshipType);
            this.panelAddRelation.Controls.Add(this.comboRelationshipType);
            this.panelAddRelation.Controls.Add(this.comboRelationshipSecondary);
            this.panelAddRelation.Controls.Add(this.comboRelationshipPrimary);
            this.panelAddRelation.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelAddRelation.Location = new System.Drawing.Point(12, 55);
            this.panelAddRelation.Name = "panelAddRelation";
            this.panelAddRelation.Size = new System.Drawing.Size(374, 280);
            this.panelAddRelation.TabIndex = 5;
            // 
            // buttonAddRelationship
            // 
            this.buttonAddRelationship.Enabled = false;
            this.buttonAddRelationship.Location = new System.Drawing.Point(238, 112);
            this.buttonAddRelationship.Margin = new System.Windows.Forms.Padding(6);
            this.buttonAddRelationship.Name = "buttonAddRelationship";
            this.buttonAddRelationship.Size = new System.Drawing.Size(119, 27);
            this.buttonAddRelationship.TabIndex = 15;
            this.buttonAddRelationship.Text = "Add Relationship";
            this.buttonAddRelationship.UseVisualStyleBackColor = true;
            this.buttonAddRelationship.Click += new System.EventHandler(this.ButtonAddRelationship_Click);
            // 
            // labelRelatedEntity
            // 
            this.labelRelatedEntity.AutoSize = true;
            this.labelRelatedEntity.Location = new System.Drawing.Point(10, 81);
            this.labelRelatedEntity.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelRelatedEntity.Name = "labelRelatedEntity";
            this.labelRelatedEntity.Size = new System.Drawing.Size(73, 13);
            this.labelRelatedEntity.TabIndex = 14;
            this.labelRelatedEntity.Text = "Related Entity";
            // 
            // labelPrimaryEntity
            // 
            this.labelPrimaryEntity.AutoSize = true;
            this.labelPrimaryEntity.Location = new System.Drawing.Point(10, 48);
            this.labelPrimaryEntity.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelPrimaryEntity.Name = "labelPrimaryEntity";
            this.labelPrimaryEntity.Size = new System.Drawing.Size(70, 13);
            this.labelPrimaryEntity.TabIndex = 13;
            this.labelPrimaryEntity.Text = "Primary Entity";
            // 
            // labelRelationshipType
            // 
            this.labelRelationshipType.AutoSize = true;
            this.labelRelationshipType.Location = new System.Drawing.Point(10, 14);
            this.labelRelationshipType.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelRelationshipType.Name = "labelRelationshipType";
            this.labelRelationshipType.Size = new System.Drawing.Size(92, 13);
            this.labelRelationshipType.TabIndex = 12;
            this.labelRelationshipType.Text = "Relationship Type";
            // 
            // comboRelationshipType
            // 
            this.comboRelationshipType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboRelationshipType.Enabled = false;
            this.comboRelationshipType.FormattingEnabled = true;
            this.comboRelationshipType.Items.AddRange(new object[] {
            "",
            "1:N Relationship",
            "N:N Relationship"});
            this.comboRelationshipType.Location = new System.Drawing.Point(107, 12);
            this.comboRelationshipType.Margin = new System.Windows.Forms.Padding(6);
            this.comboRelationshipType.Name = "comboRelationshipType";
            this.comboRelationshipType.Size = new System.Drawing.Size(252, 21);
            this.comboRelationshipType.TabIndex = 11;
            this.comboRelationshipType.SelectedIndexChanged += new System.EventHandler(this.comboRelationshipType_SelectedIndexChanged);
            // 
            // comboRelationshipSecondary
            // 
            this.comboRelationshipSecondary.Enabled = false;
            this.comboRelationshipSecondary.FormattingEnabled = true;
            this.comboRelationshipSecondary.Location = new System.Drawing.Point(107, 78);
            this.comboRelationshipSecondary.Margin = new System.Windows.Forms.Padding(6);
            this.comboRelationshipSecondary.Name = "comboRelationshipSecondary";
            this.comboRelationshipSecondary.Size = new System.Drawing.Size(252, 21);
            this.comboRelationshipSecondary.TabIndex = 10;
            this.comboRelationshipSecondary.SelectedIndexChanged += new System.EventHandler(this.ComboRelationEntity_SelectedIndexChanged);
            // 
            // comboRelationshipPrimary
            // 
            this.comboRelationshipPrimary.Enabled = false;
            this.comboRelationshipPrimary.FormattingEnabled = true;
            this.comboRelationshipPrimary.Location = new System.Drawing.Point(107, 45);
            this.comboRelationshipPrimary.Margin = new System.Windows.Forms.Padding(6);
            this.comboRelationshipPrimary.Name = "comboRelationshipPrimary";
            this.comboRelationshipPrimary.Size = new System.Drawing.Size(252, 21);
            this.comboRelationshipPrimary.TabIndex = 9;
            this.comboRelationshipPrimary.SelectedIndexChanged += new System.EventHandler(this.ComboRelationEntity_SelectedIndexChanged);
            // 
            // labelRelationInstructions
            // 
            this.labelRelationInstructions.BackColor = System.Drawing.SystemColors.Info;
            this.labelRelationInstructions.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelRelationInstructions.Location = new System.Drawing.Point(12, 12);
            this.labelRelationInstructions.Name = "labelRelationInstructions";
            this.labelRelationInstructions.Padding = new System.Windows.Forms.Padding(5);
            this.labelRelationInstructions.Size = new System.Drawing.Size(1091, 43);
            this.labelRelationInstructions.TabIndex = 5;
            this.labelRelationInstructions.Text = resources.GetString("labelRelationInstructions.Text");
            // 
            // tabPageCommit
            // 
            this.tabPageCommit.Controls.Add(this.buttonCreate);
            this.tabPageCommit.Controls.Add(this.RichTextSummary);
            this.tabPageCommit.Controls.Add(this.checkPublish);
            this.tabPageCommit.Controls.Add(this.checkAddNewFields);
            this.tabPageCommit.Location = new System.Drawing.Point(4, 34);
            this.tabPageCommit.Name = "tabPageCommit";
            this.tabPageCommit.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCommit.Size = new System.Drawing.Size(1115, 347);
            this.tabPageCommit.TabIndex = 2;
            this.tabPageCommit.Text = "Commit Changes";
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(772, 74);
            this.buttonCreate.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(89, 35);
            this.buttonCreate.TabIndex = 38;
            this.buttonCreate.Text = "Create";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.ButtonCreate_Click);
            // 
            // RichTextSummary
            // 
            this.RichTextSummary.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RichTextSummary.Dock = System.Windows.Forms.DockStyle.Left;
            this.RichTextSummary.Location = new System.Drawing.Point(3, 3);
            this.RichTextSummary.Margin = new System.Windows.Forms.Padding(2);
            this.RichTextSummary.Name = "RichTextSummary";
            this.RichTextSummary.ReadOnly = true;
            this.RichTextSummary.Size = new System.Drawing.Size(712, 341);
            this.RichTextSummary.TabIndex = 37;
            this.RichTextSummary.Text = "";
            // 
            // checkPublish
            // 
            this.checkPublish.AutoSize = true;
            this.checkPublish.Location = new System.Drawing.Point(772, 50);
            this.checkPublish.Margin = new System.Windows.Forms.Padding(6);
            this.checkPublish.Name = "checkPublish";
            this.checkPublish.Size = new System.Drawing.Size(171, 17);
            this.checkPublish.TabIndex = 36;
            this.checkPublish.Text = "Publish ? (No Rollback Option)";
            this.checkPublish.UseVisualStyleBackColor = true;
            // 
            // statusStripMain
            // 
            this.statusStripMain.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsLabelConnString,
            this.tstxtConnectionString});
            this.statusStripMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.statusStripMain.Location = new System.Drawing.Point(0, 510);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Padding = new System.Windows.Forms.Padding(1, 0, 8, 0);
            this.statusStripMain.Size = new System.Drawing.Size(1123, 17);
            this.statusStripMain.TabIndex = 36;
            this.statusStripMain.Text = "statusStrip";
            // 
            // tsLabelConnString
            // 
            this.tsLabelConnString.Name = "tsLabelConnString";
            this.tsLabelConnString.Size = new System.Drawing.Size(106, 15);
            this.tsLabelConnString.Text = "Connection String:";
            // 
            // tstxtConnectionString
            // 
            this.tstxtConnectionString.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tstxtConnectionString.Name = "tstxtConnectionString";
            this.tstxtConnectionString.ReadOnly = true;
            this.tstxtConnectionString.Size = new System.Drawing.Size(500, 16);
            // 
            // MigrateToCDSControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.panelOptions);
            this.Controls.Add(this.toolStripMenu);
            this.Controls.Add(this.statusStripMain);
            this.Name = "MigrateToCDSControl";
            this.Size = new System.Drawing.Size(1123, 527);
            this.OnCloseTool += new System.EventHandler(this.MigrateToCDS_OnCloseTool);
            this.Load += new System.EventHandler(this.MigrateToCDSControl_Load);
            this.Resize += new System.EventHandler(this.MigrateToCDSControl_Resize);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.panelOptions.ResumeLayout(false);
            this.panelOptions.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.tabPageTableSelections.ResumeLayout(false);
            this.panelAttribProps.ResumeLayout(false);
            this.panelAttribList.ResumeLayout(false);
            this.panelAttribsCheckOptions.ResumeLayout(false);
            this.panelTableProps.ResumeLayout(false);
            this.panelTableList.ResumeLayout(false);
            this.panelTablesCheckOptions.ResumeLayout(false);
            this.tabPageRelationships.ResumeLayout(false);
            this.panelRelationProps.ResumeLayout(false);
            this.flowPanelRemoveRelation.ResumeLayout(false);
            this.panelAddRelation.ResumeLayout(false);
            this.panelAddRelation.PerformLayout();
            this.tabPageCommit.ResumeLayout(false);
            this.tabPageCommit.PerformLayout();
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.CheckedListBox checkedListTables;
        private System.Windows.Forms.Label labelTableProps;
        private System.Windows.Forms.Panel panelOptions;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageTableSelections;
        private System.Windows.Forms.TabPage tabPageRelationships;
        private System.Windows.Forms.CheckBox checkAddNewFields;
        private System.Windows.Forms.Panel panelAddRelation;
        private System.Windows.Forms.Button buttonAddRelationship;
        private System.Windows.Forms.Label labelRelatedEntity;
        private System.Windows.Forms.Label labelPrimaryEntity;
        private System.Windows.Forms.Label labelRelationshipType;
        private System.Windows.Forms.ComboBox comboRelationshipType;
        private System.Windows.Forms.ComboBox comboRelationshipSecondary;
        private System.Windows.Forms.ComboBox comboRelationshipPrimary;
        private System.Windows.Forms.Label labelRelationInstructions;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripLabel tsLabelConnString;
        private System.Windows.Forms.ToolStripTextBox tstxtConnectionString;
        private System.Windows.Forms.TabPage tabPageCommit;
        private System.Windows.Forms.CheckBox checkPublish;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.Label labelMainInstruct;
        private System.Windows.Forms.ListBox listRelationships;
        private System.Windows.Forms.RichTextBox RichTextSummary;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonReload;
        private System.Windows.Forms.Label labelConnect;
        private System.Windows.Forms.TextBox textBoxDBConnection;
        private System.Windows.Forms.Label labelPrefix;
        private System.Windows.Forms.ComboBox comboPrefix;
        private System.Windows.Forms.Panel panelAttribProps;
        private System.Windows.Forms.PropertyGrid propGridAtribute;
        private System.Windows.Forms.Label labelAttribProps;
        private System.Windows.Forms.Splitter splitterAttribList;
        private System.Windows.Forms.Panel panelAttribList;
        private System.Windows.Forms.CheckedListBox checkedListAttributes;
        private System.Windows.Forms.Label labelTableAttributes;
        private System.Windows.Forms.Splitter splitterTableProps;
        private System.Windows.Forms.Panel panelTableProps;
        private System.Windows.Forms.PropertyGrid propGridTable;
        private System.Windows.Forms.Label labelTableDetails;
        private System.Windows.Forms.Splitter splitterTableList;
        private System.Windows.Forms.Panel panelTableList;
        private System.Windows.Forms.Label labelTablesFound;
        private System.Windows.Forms.Label labelCDSConnection;
        private System.Windows.Forms.Panel panelRelationProps;
        private System.Windows.Forms.PropertyGrid propGridRelation;
        private System.Windows.Forms.FlowLayoutPanel flowPanelRemoveRelation;
        private System.Windows.Forms.Button buttonRemoveRelationship;
        private System.Windows.Forms.Splitter splitterRelationList;
        private System.Windows.Forms.Panel panelTablesCheckOptions;
        private System.Windows.Forms.LinkLabel linkTableCheckAll;
        private System.Windows.Forms.LinkLabel linkTableCheckNone;
        private System.Windows.Forms.Panel panelAttribsCheckOptions;
        private System.Windows.Forms.LinkLabel linkAttribCheckAll;
        private System.Windows.Forms.LinkLabel linkAttribCheckNone;
    }
}
