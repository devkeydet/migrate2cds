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
            this.tssSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsComboPrefix = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsButtonConnect = new System.Windows.Forms.ToolStripButton();
            this.tsButtonReload = new System.Windows.Forms.ToolStripButton();
            this.ChkListTables = new System.Windows.Forms.CheckedListBox();
            this.labelTablesFound = new System.Windows.Forms.Label();
            this.ChkListAttributes = new System.Windows.Forms.CheckedListBox();
            this.propGridAtribute = new System.Windows.Forms.PropertyGrid();
            this.panelOptions = new System.Windows.Forms.Panel();
            this.labelMainInstruct = new System.Windows.Forms.Label();
            this.chkAddNewFields = new System.Windows.Forms.CheckBox();
            this.labelTableProps = new System.Windows.Forms.Label();
            this.labelTableAttributes = new System.Windows.Forms.Label();
            this.propGridTable = new System.Windows.Forms.PropertyGrid();
            this.labelAttribProps = new System.Windows.Forms.Label();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageTableSelections = new System.Windows.Forms.TabPage();
            this.splitContainerTablesMain = new System.Windows.Forms.SplitContainer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.splitterAttribs = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitterTableProps = new System.Windows.Forms.Splitter();
            this.panelTableProps = new System.Windows.Forms.Panel();
            this.labelTableDetails = new System.Windows.Forms.Label();
            this.tabPageRelationships = new System.Windows.Forms.TabPage();
            this.lstRelationships = new System.Windows.Forms.ListBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnRelationshipRemove = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRelationshipAdd = new System.Windows.Forms.Button();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboRelationshipType = new System.Windows.Forms.ComboBox();
            this.comboRelationshipSecondary = new System.Windows.Forms.ComboBox();
            this.comboRelationshipPrimary = new System.Windows.Forms.ComboBox();
            this.labelRelationInstructions = new System.Windows.Forms.Label();
            this.tabPageCommit = new System.Windows.Forms.TabPage();
            this.RichTextSummary = new System.Windows.Forms.RichTextBox();
            this.chkPublish = new System.Windows.Forms.CheckBox();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.tsLabelConnString = new System.Windows.Forms.ToolStripLabel();
            this.tstxtConnectionString = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenu.SuspendLayout();
            this.panelOptions.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageTableSelections.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTablesMain)).BeginInit();
            this.splitContainerTablesMain.Panel1.SuspendLayout();
            this.splitContainerTablesMain.Panel2.SuspendLayout();
            this.splitContainerTablesMain.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelTableProps.SuspendLayout();
            this.tabPageRelationships.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPageCommit.SuspendLayout();
            this.statusStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.tssSeparator1,
            this.toolStripLabel1,
            this.tsComboPrefix,
            this.toolStripSeparator3,
            this.tsButtonConnect,
            this.tsButtonReload});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripMenu.Size = new System.Drawing.Size(2059, 38);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(67, 35);
            this.tsbClose.Text = "Close";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // tssSeparator1
            // 
            this.tssSeparator1.Name = "tssSeparator1";
            this.tssSeparator1.Size = new System.Drawing.Size(6, 38);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(90, 35);
            this.toolStripLabel1.Text = "Prefixes:";
            // 
            // tsComboPrefix
            // 
            this.tsComboPrefix.AutoCompleteCustomSource.AddRange(new string[] {
            "migrate2"});
            this.tsComboPrefix.Items.AddRange(new object[] {
            "migrate2"});
            this.tsComboPrefix.Name = "tsComboPrefix";
            this.tsComboPrefix.Size = new System.Drawing.Size(219, 38);
            this.tsComboPrefix.SelectedIndexChanged += new System.EventHandler(this.ComboPrefix_Update);
            this.tsComboPrefix.TextUpdate += new System.EventHandler(this.ComboPrefix_Update);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 38);
            // 
            // tsButtonConnect
            // 
            this.tsButtonConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsButtonConnect.Name = "tsButtonConnect";
            this.tsButtonConnect.Size = new System.Drawing.Size(94, 35);
            this.tsButtonConnect.Text = "Connect";
            this.tsButtonConnect.Click += new System.EventHandler(this.ButtonConnect_Click);
            // 
            // tsButtonReload
            // 
            this.tsButtonReload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsButtonReload.Enabled = false;
            this.tsButtonReload.Image = ((System.Drawing.Image)(resources.GetObject("tsButtonReload.Image")));
            this.tsButtonReload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsButtonReload.Name = "tsButtonReload";
            this.tsButtonReload.Size = new System.Drawing.Size(80, 35);
            this.tsButtonReload.Text = "Reload";
            this.tsButtonReload.Click += new System.EventHandler(this.tsButtonReload_Click);
            // 
            // ChkListTables
            // 
            this.ChkListTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChkListTables.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkListTables.FormattingEnabled = true;
            this.ChkListTables.Location = new System.Drawing.Point(0, 66);
            this.ChkListTables.Margin = new System.Windows.Forms.Padding(11);
            this.ChkListTables.Name = "ChkListTables";
            this.ChkListTables.Size = new System.Drawing.Size(399, 690);
            this.ChkListTables.TabIndex = 10;
            this.ChkListTables.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ChkList_ItemCheck);
            this.ChkListTables.SelectedIndexChanged += new System.EventHandler(this.ChkListTables_SelectedIndexChanged);
            // 
            // labelTablesFound
            // 
            this.labelTablesFound.BackColor = System.Drawing.SystemColors.Info;
            this.labelTablesFound.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTablesFound.Location = new System.Drawing.Point(0, 0);
            this.labelTablesFound.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTablesFound.Name = "labelTablesFound";
            this.labelTablesFound.Padding = new System.Windows.Forms.Padding(2);
            this.labelTablesFound.Size = new System.Drawing.Size(399, 66);
            this.labelTablesFound.TabIndex = 15;
            this.labelTablesFound.Text = "Select Tables to be converted to Entities.  Checked Items will be imported.\r\n";
            // 
            // ChkListAttributes
            // 
            this.ChkListAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChkListAttributes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkListAttributes.FormattingEnabled = true;
            this.ChkListAttributes.Location = new System.Drawing.Point(0, 66);
            this.ChkListAttributes.Margin = new System.Windows.Forms.Padding(11);
            this.ChkListAttributes.Name = "ChkListAttributes";
            this.ChkListAttributes.Size = new System.Drawing.Size(513, 690);
            this.ChkListAttributes.TabIndex = 15;
            this.ChkListAttributes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ChkList_ItemCheck);
            this.ChkListAttributes.SelectedIndexChanged += new System.EventHandler(this.ChkListAttributes_SelectedIndexChanged);
            // 
            // propGridAtribute
            // 
            this.propGridAtribute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propGridAtribute.Location = new System.Drawing.Point(0, 66);
            this.propGridAtribute.Margin = new System.Windows.Forms.Padding(4);
            this.propGridAtribute.Name = "propGridAtribute";
            this.propGridAtribute.Size = new System.Drawing.Size(521, 690);
            this.propGridAtribute.TabIndex = 14;
            this.propGridAtribute.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propGridAtribute_PropertyValueChanged);
            // 
            // panelOptions
            // 
            this.panelOptions.Controls.Add(this.labelMainInstruct);
            this.panelOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelOptions.Location = new System.Drawing.Point(0, 38);
            this.panelOptions.Margin = new System.Windows.Forms.Padding(4);
            this.panelOptions.Name = "panelOptions";
            this.panelOptions.Size = new System.Drawing.Size(2059, 65);
            this.panelOptions.TabIndex = 16;
            // 
            // labelMainInstruct
            // 
            this.labelMainInstruct.BackColor = System.Drawing.SystemColors.Info;
            this.labelMainInstruct.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelMainInstruct.Location = new System.Drawing.Point(0, 0);
            this.labelMainInstruct.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelMainInstruct.Name = "labelMainInstruct";
            this.labelMainInstruct.Padding = new System.Windows.Forms.Padding(2);
            this.labelMainInstruct.Size = new System.Drawing.Size(2059, 66);
            this.labelMainInstruct.TabIndex = 16;
            this.labelMainInstruct.Text = "Once connected to CRM, you can choose a prefix from the publishers on the system " +
    "or enter your own.  Choose Connect to open Database and read the schema: Tables," +
    " Fields, and Relationships.";
            // 
            // chkAddNewFields
            // 
            this.chkAddNewFields.AutoSize = true;
            this.chkAddNewFields.Enabled = false;
            this.chkAddNewFields.Location = new System.Drawing.Point(1415, 47);
            this.chkAddNewFields.Margin = new System.Windows.Forms.Padding(6);
            this.chkAddNewFields.Name = "chkAddNewFields";
            this.chkAddNewFields.Size = new System.Drawing.Size(221, 29);
            this.chkAddNewFields.TabIndex = 35;
            this.chkAddNewFields.Text = "Add Fields To Form?";
            this.chkAddNewFields.UseVisualStyleBackColor = true;
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
            // labelTableAttributes
            // 
            this.labelTableAttributes.BackColor = System.Drawing.SystemColors.Info;
            this.labelTableAttributes.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTableAttributes.Location = new System.Drawing.Point(0, 0);
            this.labelTableAttributes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTableAttributes.Name = "labelTableAttributes";
            this.labelTableAttributes.Padding = new System.Windows.Forms.Padding(2);
            this.labelTableAttributes.Size = new System.Drawing.Size(513, 66);
            this.labelTableAttributes.TabIndex = 16;
            this.labelTableAttributes.Text = "Review and update the fileds related to the Table/Entity";
            // 
            // propGridTable
            // 
            this.propGridTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propGridTable.Location = new System.Drawing.Point(0, 66);
            this.propGridTable.Margin = new System.Windows.Forms.Padding(4);
            this.propGridTable.Name = "propGridTable";
            this.propGridTable.Size = new System.Drawing.Size(526, 690);
            this.propGridTable.TabIndex = 18;
            // 
            // labelAttribProps
            // 
            this.labelAttribProps.BackColor = System.Drawing.SystemColors.Info;
            this.labelAttribProps.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelAttribProps.Location = new System.Drawing.Point(0, 0);
            this.labelAttribProps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAttribProps.Name = "labelAttribProps";
            this.labelAttribProps.Padding = new System.Windows.Forms.Padding(2);
            this.labelAttribProps.Size = new System.Drawing.Size(521, 66);
            this.labelAttribProps.TabIndex = 17;
            this.labelAttribProps.Text = "Review and update the details for the selected Field/Attribute";
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageTableSelections);
            this.tabControlMain.Controls.Add(this.tabPageRelationships);
            this.tabControlMain.Controls.Add(this.tabPageCommit);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Enabled = false;
            this.tabControlMain.ItemSize = new System.Drawing.Size(200, 30);
            this.tabControlMain.Location = new System.Drawing.Point(0, 103);
            this.tabControlMain.Margin = new System.Windows.Forms.Padding(4);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(2059, 838);
            this.tabControlMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControlMain.TabIndex = 35;
            this.tabControlMain.SelectedIndexChanged += new System.EventHandler(this.tabControlMain_SelectedIndexChanged);
            // 
            // tabPageTableSelections
            // 
            this.tabPageTableSelections.Controls.Add(this.splitContainerTablesMain);
            this.tabPageTableSelections.Location = new System.Drawing.Point(4, 34);
            this.tabPageTableSelections.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageTableSelections.Name = "tabPageTableSelections";
            this.tabPageTableSelections.Padding = new System.Windows.Forms.Padding(22);
            this.tabPageTableSelections.Size = new System.Drawing.Size(2051, 800);
            this.tabPageTableSelections.TabIndex = 0;
            this.tabPageTableSelections.Text = "Table and Entity Details";
            // 
            // splitContainerTablesMain
            // 
            this.splitContainerTablesMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerTablesMain.Location = new System.Drawing.Point(22, 22);
            this.splitContainerTablesMain.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainerTablesMain.Name = "splitContainerTablesMain";
            // 
            // splitContainerTablesMain.Panel1
            // 
            this.splitContainerTablesMain.Panel1.Controls.Add(this.ChkListTables);
            this.splitContainerTablesMain.Panel1.Controls.Add(this.labelTablesFound);
            // 
            // splitContainerTablesMain.Panel2
            // 
            this.splitContainerTablesMain.Panel2.Controls.Add(this.panel3);
            this.splitContainerTablesMain.Panel2.Controls.Add(this.splitterAttribs);
            this.splitContainerTablesMain.Panel2.Controls.Add(this.panel2);
            this.splitContainerTablesMain.Panel2.Controls.Add(this.splitterTableProps);
            this.splitContainerTablesMain.Panel2.Controls.Add(this.panelTableProps);
            this.splitContainerTablesMain.Size = new System.Drawing.Size(2007, 756);
            this.splitContainerTablesMain.SplitterDistance = 399;
            this.splitContainerTablesMain.SplitterWidth = 18;
            this.splitContainerTablesMain.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.propGridAtribute);
            this.panel3.Controls.Add(this.labelAttribProps);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(1069, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(521, 756);
            this.panel3.TabIndex = 22;
            // 
            // splitterAttribs
            // 
            this.splitterAttribs.Location = new System.Drawing.Point(1054, 0);
            this.splitterAttribs.Margin = new System.Windows.Forms.Padding(4);
            this.splitterAttribs.Name = "splitterAttribs";
            this.splitterAttribs.Size = new System.Drawing.Size(15, 756);
            this.splitterAttribs.TabIndex = 23;
            this.splitterAttribs.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ChkListAttributes);
            this.panel2.Controls.Add(this.labelTableAttributes);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(541, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(513, 756);
            this.panel2.TabIndex = 21;
            // 
            // splitterTableProps
            // 
            this.splitterTableProps.Location = new System.Drawing.Point(526, 0);
            this.splitterTableProps.Margin = new System.Windows.Forms.Padding(4);
            this.splitterTableProps.Name = "splitterTableProps";
            this.splitterTableProps.Size = new System.Drawing.Size(15, 756);
            this.splitterTableProps.TabIndex = 19;
            this.splitterTableProps.TabStop = false;
            // 
            // panelTableProps
            // 
            this.panelTableProps.Controls.Add(this.propGridTable);
            this.panelTableProps.Controls.Add(this.labelTableDetails);
            this.panelTableProps.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelTableProps.Location = new System.Drawing.Point(0, 0);
            this.panelTableProps.Margin = new System.Windows.Forms.Padding(4);
            this.panelTableProps.Name = "panelTableProps";
            this.panelTableProps.Size = new System.Drawing.Size(526, 756);
            this.panelTableProps.TabIndex = 20;
            // 
            // labelTableDetails
            // 
            this.labelTableDetails.BackColor = System.Drawing.SystemColors.Info;
            this.labelTableDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTableDetails.Location = new System.Drawing.Point(0, 0);
            this.labelTableDetails.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTableDetails.Name = "labelTableDetails";
            this.labelTableDetails.Padding = new System.Windows.Forms.Padding(2);
            this.labelTableDetails.Size = new System.Drawing.Size(526, 66);
            this.labelTableDetails.TabIndex = 19;
            this.labelTableDetails.Text = "Review and update selected Table/Entity Details";
            // 
            // tabPageRelationships
            // 
            this.tabPageRelationships.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageRelationships.Controls.Add(this.lstRelationships);
            this.tabPageRelationships.Controls.Add(this.panel4);
            this.tabPageRelationships.Controls.Add(this.panel1);
            this.tabPageRelationships.Controls.Add(this.labelRelationInstructions);
            this.tabPageRelationships.Location = new System.Drawing.Point(4, 34);
            this.tabPageRelationships.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageRelationships.Name = "tabPageRelationships";
            this.tabPageRelationships.Padding = new System.Windows.Forms.Padding(22);
            this.tabPageRelationships.Size = new System.Drawing.Size(2051, 800);
            this.tabPageRelationships.TabIndex = 1;
            this.tabPageRelationships.Text = "Entity Relationships";
            // 
            // lstRelationships
            // 
            this.lstRelationships.DisplayMember = "SummaryName";
            this.lstRelationships.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRelationships.FormattingEnabled = true;
            this.lstRelationships.ItemHeight = 24;
            this.lstRelationships.Location = new System.Drawing.Point(623, 101);
            this.lstRelationships.Margin = new System.Windows.Forms.Padding(11);
            this.lstRelationships.Name = "lstRelationships";
            this.lstRelationships.Size = new System.Drawing.Size(1157, 677);
            this.lstRelationships.TabIndex = 4;
            this.lstRelationships.ValueMember = "Name";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnRelationshipRemove);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(1780, 101);
            this.panel4.Margin = new System.Windows.Forms.Padding(6);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(249, 677);
            this.panel4.TabIndex = 6;
            // 
            // btnRelationshipRemove
            // 
            this.btnRelationshipRemove.Enabled = false;
            this.btnRelationshipRemove.Location = new System.Drawing.Point(17, 11);
            this.btnRelationshipRemove.Margin = new System.Windows.Forms.Padding(11);
            this.btnRelationshipRemove.Name = "btnRelationshipRemove";
            this.btnRelationshipRemove.Size = new System.Drawing.Size(218, 50);
            this.btnRelationshipRemove.TabIndex = 16;
            this.btnRelationshipRemove.Text = "Remove Relationship";
            this.btnRelationshipRemove.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRelationshipAdd);
            this.panel1.Controls.Add(this.label32);
            this.panel1.Controls.Add(this.label31);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.comboRelationshipType);
            this.panel1.Controls.Add(this.comboRelationshipSecondary);
            this.panel1.Controls.Add(this.comboRelationshipPrimary);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(22, 101);
            this.panel1.Margin = new System.Windows.Forms.Padding(6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(601, 677);
            this.panel1.TabIndex = 5;
            // 
            // btnRelationshipAdd
            // 
            this.btnRelationshipAdd.Enabled = false;
            this.btnRelationshipAdd.Location = new System.Drawing.Point(207, 205);
            this.btnRelationshipAdd.Margin = new System.Windows.Forms.Padding(11);
            this.btnRelationshipAdd.Name = "btnRelationshipAdd";
            this.btnRelationshipAdd.Size = new System.Drawing.Size(218, 50);
            this.btnRelationshipAdd.TabIndex = 15;
            this.btnRelationshipAdd.Text = "Add Relationship";
            this.btnRelationshipAdd.UseVisualStyleBackColor = true;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(18, 150);
            this.label32.Margin = new System.Windows.Forms.Padding(11, 0, 11, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(131, 25);
            this.label32.TabIndex = 14;
            this.label32.Text = "Related Entity";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(18, 89);
            this.label31.Margin = new System.Windows.Forms.Padding(11, 0, 11, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(131, 25);
            this.label31.TabIndex = 13;
            this.label31.Text = "Primary Entity";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 26);
            this.label2.Margin = new System.Windows.Forms.Padding(11, 0, 11, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(168, 25);
            this.label2.TabIndex = 12;
            this.label2.Text = "Relationship Type";
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
            this.comboRelationshipType.Location = new System.Drawing.Point(196, 22);
            this.comboRelationshipType.Margin = new System.Windows.Forms.Padding(11);
            this.comboRelationshipType.Name = "comboRelationshipType";
            this.comboRelationshipType.Size = new System.Drawing.Size(380, 32);
            this.comboRelationshipType.TabIndex = 11;
            this.comboRelationshipType.SelectedIndexChanged += new System.EventHandler(this.comboRelationshipType_SelectedIndexChanged);
            // 
            // comboRelationshipSecondary
            // 
            this.comboRelationshipSecondary.Enabled = false;
            this.comboRelationshipSecondary.FormattingEnabled = true;
            this.comboRelationshipSecondary.Location = new System.Drawing.Point(196, 144);
            this.comboRelationshipSecondary.Margin = new System.Windows.Forms.Padding(11);
            this.comboRelationshipSecondary.Name = "comboRelationshipSecondary";
            this.comboRelationshipSecondary.Size = new System.Drawing.Size(380, 32);
            this.comboRelationshipSecondary.TabIndex = 10;
            // 
            // comboRelationshipPrimary
            // 
            this.comboRelationshipPrimary.Enabled = false;
            this.comboRelationshipPrimary.FormattingEnabled = true;
            this.comboRelationshipPrimary.Location = new System.Drawing.Point(196, 83);
            this.comboRelationshipPrimary.Margin = new System.Windows.Forms.Padding(11);
            this.comboRelationshipPrimary.Name = "comboRelationshipPrimary";
            this.comboRelationshipPrimary.Size = new System.Drawing.Size(380, 32);
            this.comboRelationshipPrimary.TabIndex = 9;
            // 
            // labelRelationInstructions
            // 
            this.labelRelationInstructions.BackColor = System.Drawing.SystemColors.Info;
            this.labelRelationInstructions.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelRelationInstructions.Location = new System.Drawing.Point(22, 22);
            this.labelRelationInstructions.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelRelationInstructions.Name = "labelRelationInstructions";
            this.labelRelationInstructions.Padding = new System.Windows.Forms.Padding(9);
            this.labelRelationInstructions.Size = new System.Drawing.Size(2007, 79);
            this.labelRelationInstructions.TabIndex = 5;
            this.labelRelationInstructions.Text = resources.GetString("labelRelationInstructions.Text");
            // 
            // tabPageCommit
            // 
            this.tabPageCommit.Controls.Add(this.RichTextSummary);
            this.tabPageCommit.Controls.Add(this.chkPublish);
            this.tabPageCommit.Controls.Add(this.chkAddNewFields);
            this.tabPageCommit.Location = new System.Drawing.Point(4, 34);
            this.tabPageCommit.Margin = new System.Windows.Forms.Padding(6);
            this.tabPageCommit.Name = "tabPageCommit";
            this.tabPageCommit.Padding = new System.Windows.Forms.Padding(6);
            this.tabPageCommit.Size = new System.Drawing.Size(2051, 800);
            this.tabPageCommit.TabIndex = 2;
            this.tabPageCommit.Text = "Commit Changes";
            // 
            // RichTextSummary
            // 
            this.RichTextSummary.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RichTextSummary.Location = new System.Drawing.Point(48, 48);
            this.RichTextSummary.Name = "RichTextSummary";
            this.RichTextSummary.ReadOnly = true;
            this.RichTextSummary.Size = new System.Drawing.Size(1305, 723);
            this.RichTextSummary.TabIndex = 37;
            this.RichTextSummary.Text = "";
            // 
            // chkPublish
            // 
            this.chkPublish.AutoSize = true;
            this.chkPublish.Enabled = false;
            this.chkPublish.Location = new System.Drawing.Point(1415, 93);
            this.chkPublish.Margin = new System.Windows.Forms.Padding(11);
            this.chkPublish.Name = "chkPublish";
            this.chkPublish.Size = new System.Drawing.Size(304, 29);
            this.chkPublish.TabIndex = 36;
            this.chkPublish.Text = "Publish ? (No Rollback Option)";
            this.chkPublish.UseVisualStyleBackColor = true;
            // 
            // statusStripMain
            // 
            this.statusStripMain.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsLabelConnString,
            this.tstxtConnectionString});
            this.statusStripMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.statusStripMain.Location = new System.Drawing.Point(0, 941);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Padding = new System.Windows.Forms.Padding(2, 0, 15, 0);
            this.statusStripMain.Size = new System.Drawing.Size(2059, 32);
            this.statusStripMain.TabIndex = 36;
            this.statusStripMain.Text = "statusStrip";
            // 
            // tsLabelConnString
            // 
            this.tsLabelConnString.Name = "tsLabelConnString";
            this.tsLabelConnString.Size = new System.Drawing.Size(183, 30);
            this.tsLabelConnString.Text = "Connection String:";
            // 
            // tstxtConnectionString
            // 
            this.tstxtConnectionString.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tstxtConnectionString.Name = "tstxtConnectionString";
            this.tstxtConnectionString.ReadOnly = true;
            this.tstxtConnectionString.Size = new System.Drawing.Size(917, 28);
            // 
            // MigrateToCDSControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.panelOptions);
            this.Controls.Add(this.toolStripMenu);
            this.Controls.Add(this.statusStripMain);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "MigrateToCDSControl";
            this.Size = new System.Drawing.Size(2059, 973);
            this.OnCloseTool += new System.EventHandler(this.MigrateToCDS_OnCloseTool);
            this.Load += new System.EventHandler(this.MyPluginControl_Load);
            this.Resize += new System.EventHandler(this.MigrateToCDSControl_Resize);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.panelOptions.ResumeLayout(false);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageTableSelections.ResumeLayout(false);
            this.splitContainerTablesMain.Panel1.ResumeLayout(false);
            this.splitContainerTablesMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTablesMain)).EndInit();
            this.splitContainerTablesMain.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panelTableProps.ResumeLayout(false);
            this.tabPageRelationships.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.ToolStripButton tsButtonConnect;
        private System.Windows.Forms.ToolStripSeparator tssSeparator1;
        private System.Windows.Forms.CheckedListBox ChkListTables;
        private System.Windows.Forms.PropertyGrid propGridAtribute;
        private System.Windows.Forms.Label labelTableProps;
        private System.Windows.Forms.CheckedListBox ChkListAttributes;
        private System.Windows.Forms.Label labelTableAttributes;
        private System.Windows.Forms.Label labelTablesFound;
        private System.Windows.Forms.Panel panelOptions;
        private System.Windows.Forms.PropertyGrid propGridTable;
        private System.Windows.Forms.Label labelAttribProps;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageTableSelections;
        private System.Windows.Forms.TabPage tabPageRelationships;
        private System.Windows.Forms.SplitContainer splitContainerTablesMain;
        private System.Windows.Forms.Splitter splitterTableProps;
        private System.Windows.Forms.Splitter splitterAttribs;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelTableProps;
        private System.Windows.Forms.Label labelTableDetails;
        private System.Windows.Forms.CheckBox chkAddNewFields;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnRelationshipRemove;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRelationshipAdd;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboRelationshipType;
        private System.Windows.Forms.ComboBox comboRelationshipSecondary;
        private System.Windows.Forms.ComboBox comboRelationshipPrimary;
        private System.Windows.Forms.Label labelRelationInstructions;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripLabel tsLabelConnString;
        private System.Windows.Forms.ToolStripTextBox tstxtConnectionString;
        private System.Windows.Forms.TabPage tabPageCommit;
        private System.Windows.Forms.CheckBox chkPublish;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox tsComboPrefix;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsButtonReload;
        private System.Windows.Forms.Label labelMainInstruct;
        private System.Windows.Forms.ListBox lstRelationships;
        private System.Windows.Forms.RichTextBox RichTextSummary;
    }
}
