///This project utilizes the Connection Controls for Microsoft Dynamics CRM 2011
///by Tanguy Touzard
///http://connectioncontrol.codeplex.com/

using CdsLoginControl;
using McTools.Xrm.Connection;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Tooling.CrmConnectControl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using XrmSpeedy;
using XrmSpeedy.Controls;

namespace XrmSpeedyUI
{
    public partial class frmMain : Form
    {
        public string ConnectionString { get; set; }
        
        
        CrmConnectionStatusBar ccsb;
        ConnectionManager cManager;
        IOrganizationService service;
        List<XRMSpeedyEntity> NewEntities = new List<XRMSpeedyEntity>();
        List<XRMSpeedyEntity> CreatedEntities = new List<XRMSpeedyEntity>();
        List<XRMSpeedyRelationship> NewRelationships = new List<XRMSpeedyRelationship>();
        List<XRMSpeedyRelationship> CreatedRelationships = new List<XRMSpeedyRelationship>();
        XRMSpeedyEntity SelectedEntity;
        XRMSpeedyField SelectedField;
        EntityMetadata[] ExistingEntityMetadata;
        SortedDictionary<string, string> RelationshipPrimaryEntities = new SortedDictionary<string, string>();
        SortedDictionary<string, string> RelationshipRelatedEntities = new SortedDictionary<string, string>();

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.cManager = new ConnectionManager(this);
            //this.cManager.ConnectionSucceed += new ConnectionManager.ConnectionSucceedEventHandler(cManager_ConnectionSucceed);
            //this.cManager.ConnectionFailed += new ConnectionManager.ConnectionFailedEventHandler(cManager_ConnectionFailed);
            this.cManager.StepChanged += new ConnectionManager.StepChangedEventHandler(cManager_StepChanged);
            //TODO: Remove dependency on CrmConnectionStatusBar since we are replacing the way this tool connects...using xrm tooling.
            ccsb = new CrmConnectionStatusBar(this.cManager);
            this.Controls.Add(ccsb);
        }
        
        void cManager_StepChanged(object sender, StepChangedEventArgs e)
        {
            this.ccsb.SetMessage(e.CurrentStep);
        }

        void ConnectionFailed()
        {
            this.ccsb.SetMessage("Failed to connect.  Please try again.");
            this.service = null;
        }

        void ConnectionSucceeded(CrmConnectionManager crmConnectionManager)
        {
            //TODO: Remove dependency on CrmConnectionStatusBar since we are replacing the way this tool connects...using xrm tooling.
            btnAuthenticate.Enabled = false;


            this.service = crmConnectionManager.CrmSvc;

            // HACK to get this working for now.  Need to clean this up.
            var connectionDetail = new ConnectionDetail();
            connectionDetail.OrganizationFriendlyName = crmConnectionManager.ConnectedOrgFriendlyName;
            this.ccsb.SetConnectionStatus(true, connectionDetail);
            this.ccsb.SetMessage(string.Empty);

            cboDatasource.Enabled = true;
            BindDropDownLists();
            GetEntityMetadata();
        }

        private void btnAuthenticate_Click(object sender, EventArgs e)
        {
            CdsLogin cdsLoginControl = new CdsLogin();
            cdsLoginControl.ConnectionToCdsCompleted += CdsLoginControl_ConnectionToCdsCompleted;

            // Show the dialog. 
            cdsLoginControl.ShowDialog();

            // Handle return. 
            if (cdsLoginControl.CrmConnectionMgr != null && cdsLoginControl.CrmConnectionMgr.CrmSvc != null && cdsLoginControl.CrmConnectionMgr.CrmSvc.IsReady)
            {
                ConnectionSucceeded(cdsLoginControl.CrmConnectionMgr);
            }   
            else
                MessageBox.Show("BadConnect");

            //if (this.service == null)
            //    this.cManager.AskForConnection("WhoAmI");
            //else
            //    WhoAmI();
        }

        private void CdsLoginControl_ConnectionToCdsCompleted(object sender, EventArgs e)
        {
            if (sender is CdsLogin)
            {
                ((CdsLogin)sender).Close();
            }
        }

        private void GetEntityMetadata()
        {
            CRM2011Organization org = new CRM2011Organization();
            ExistingEntityMetadata = org.GetEntityMetadata(service);
        }

        private void WhoAmI()
        {
            WhoAmIRequest request = new WhoAmIRequest();
            WhoAmIResponse response = (WhoAmIResponse)this.service.Execute(request);

            //TODO: Check Create Entity Privs
        }

        private void btnConnectDS_Click(object sender, EventArgs e)
        {
            switch (cboDatasource.SelectedItem.ToString())
            {
                case "SQL Server":
                    Connection_SQLServer cSQLServer = (Connection_SQLServer)grpConnection.Controls["cSQLServer"];
                    if (string.IsNullOrEmpty(((TextBox)cSQLServer.Controls["txtConnectionServer"]).Text))
                    {
                        MessageBox.Show("Please enter a server name", "Error");
                        return;
                    }
                    if (string.IsNullOrEmpty(((TextBox)cSQLServer.Controls["txtConnectionDatabase"]).Text))
                    {
                        MessageBox.Show("Please enter a database name", "Error");
                        return;
                    }
                    if (!((CheckBox)cSQLServer.Controls["chkConnectionIntegrated"]).Checked)
                    {
                        if (string.IsNullOrEmpty(((TextBox)cSQLServer.Controls["txtConnectionUsername"]).Text))
                        {
                            MessageBox.Show("Please enter a username", "Error");
                            return;
                        }
                    }
                    break;
                case "MS Access":
                    Connection_Access cAccess = (Connection_Access)grpConnection.Controls["cAccess"];
                    if (string.IsNullOrEmpty(((TextBox)cAccess.Controls["txtLocation"]).Text))
                    {
                        MessageBox.Show("Please enter a location", "Error");
                        return;
                    }
                    if (((CheckBox)cAccess.Controls["chkPassword"]).Checked)
                    {
                        if (string.IsNullOrEmpty(((TextBox)cAccess.Controls["txtConnectionPassword"]).Text))
                        {
                            MessageBox.Show("Please enter a password", "Error");
                            return;
                        }
                    }
                    break;
            }

            switch (cboDatasource.SelectedItem.ToString())
            {
                case "SQL Server":
                    SQLDBProvider sqlProvier = new SQLDBProvider(ConnectionString);
                    List<string> sqlTables = sqlProvier.GetTableNames();
                    if (sqlTables.Count() > 0)
                        DisplayTables(sqlTables);
                    Connection_SQLServer cSQLServer = (Connection_SQLServer)grpConnection.Controls["cSQLServer"];
                    cSQLServer.Controls["txtConnectionServer"].Enabled = false;
                    cSQLServer.Controls["txtConnectionDatabase"].Enabled = false;
                    cSQLServer.Controls["chkConnectionIntegrated"].Enabled = false;
                    cSQLServer.Controls["txtConnectionUsername"].Enabled = false;
                    cSQLServer.Controls["txtConnectionPassword"].Enabled = false;
                    chkOmitPrefixForRelationships.Enabled = false;
                    break;
                case "MS Access":
                    AccessDBProvider accessProvier = new AccessDBProvider(ConnectionString);
                    List<string> accessTables = accessProvier.GetTableNames();
                    if (accessTables.Count() > 0)
                        DisplayTables(accessTables);
                    Connection_Access cAccess = (Connection_Access)grpConnection.Controls["cAccess"];
                    cAccess.Controls["txtLocation"].Enabled = false;
                    cAccess.Controls["btnBrowse"].Enabled = false;
                    cAccess.Controls["chkPassword"].Enabled = false;
                    cAccess.Controls["txtConnectionPassword"].Enabled = false;
                    chkOmitPrefixForRelationships.Enabled = false;
                    break;
            }

            //Disable controls to prevent selecting a new datasource
            btnAuthenticate.Enabled = false;
            cboDatasource.Enabled = false;
            btnConnectDS.Enabled = false;
            btnRestart.Visible = true;
            ddlRelationshipType.Enabled = true;
        }

        private void cboDatasource_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Control c in grpConnection.Controls)
            {
                if (c is UserControl)
                    grpConnection.Controls.Remove(c);
            }

            if (cboDatasource.SelectedIndex == 0)
            {
                btnConnectDS.Enabled = false;
                return;
            }

            btnConnectDS.Enabled = true;

            switch (((ComboBox)sender).SelectedItem.ToString())
            {
                case "SQL Server":
                    Connection_SQLServer cSQLServer = new Connection_SQLServer();
                    cSQLServer.Name = "cSQLServer";
                    grpConnection.Controls.Add(cSQLServer);
                    cSQLServer.Location = new Point(172, 46);
                    break;
                case "MS Access":
                    Connection_Access cAccess = new Connection_Access();
                    cAccess.Name = "cAccess";
                    grpConnection.Controls.Add(cAccess);
                    cAccess.Location = new Point(172, 46);
                    break;
            }
        }

        private void DisplayTables(List<string> tables)
        {
            foreach (string table in tables)
            {
                clbTables.Items.Add(table);
                XRMSpeedyEntity newEntity = new XRMSpeedyEntity(table, txtPrefix.Text.Trim(), false);
                Dictionary<string, string> fields = null;

                switch (cboDatasource.SelectedItem.ToString())
                {
                    case "SQL Server":
                        fields = new SQLDBProvider(ConnectionString).GetFields(table);
                        break;
                    case "MS Access":
                        fields = new AccessDBProvider(ConnectionString).GetFields(table);
                        break;
                }

                foreach (KeyValuePair<string, string> field in fields)
                {
                    clbFields.Items.Add(field.Key);
                    string name = ValidateFieldSchemaName(field.Key, txtPrefix.Text.ToLower(), table);
                    switch (cboDatasource.SelectedItem.ToString())
                    {
                        case "SQL Server":
                            newEntity.Fields.Add(new XRMSpeedyField(name, new SQLDBProvider().DBTypeToCRMType(field.Value), txtPrefix.Text.ToLower(), false));
                            break;
                        case "MS Access":
                            newEntity.Fields.Add(new XRMSpeedyField(name, new AccessDBProvider().DBTypeToCRMType(field.Value), txtPrefix.Text.ToLower(), false));
                            break;
                    }
                }

                NewEntities.Add(newEntity);
            }

            CreateInitialRelationships(tables);
        }

        private string ValidateFieldSchemaName(string name, string prefix, string table)
        {
            if ((prefix + "_" + name).ToLower() == (prefix + "_" + table + "id").ToLower())
                return name + "2";
            else
                return name;
        }

        private void CreateInitialRelationships(List<string> tables)
        {
            foreach (string table in tables)
            {
                Dictionary<string, string> relationships = null;

                switch (cboDatasource.SelectedItem.ToString())
                {
                    case "SQL Server":
                        relationships = new SQLDBProvider(ConnectionString).GetRelationships(table);
                        break;
                    case "MS Access":
                        relationships = new AccessDBProvider(ConnectionString).GetRelationships(table);
                        break;
                }

                foreach (KeyValuePair<string, string> relationship in relationships)
                {
                    CreateRelationship(NewEntities.Where(n => n.OriginalTable == relationship.Key).FirstOrDefault().EntityMetadata.SchemaName,
                        NewEntities.Where(n => n.OriginalTable == relationship.Value).FirstOrDefault().EntityMetadata.SchemaName,
                        NewEntities.Where(n => n.OriginalTable == relationship.Key).FirstOrDefault().EntityMetadata.DisplayName.LocalizedLabels.Where(l => l.LanguageCode == 1033).FirstOrDefault().Label,
                        NewEntities.Where(n => n.OriginalTable == relationship.Value).FirstOrDefault().EntityMetadata.DisplayName.LocalizedLabels.Where(l => l.LanguageCode == 1033).FirstOrDefault().Label,
                        "1:N Relationship", CreateRelationshipSchemaName(NewEntities.Where(n => n.OriginalTable == relationship.Key).FirstOrDefault().EntityMetadata.SchemaName,
                        NewEntities.Where(n => n.OriginalTable == relationship.Value).FirstOrDefault().EntityMetadata.SchemaName));
                }
            }
        }

        private void clbTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (clbTables.SelectedItem != null)
            {
                XRMSpeedyEntity PreviousSelectedEntity = SelectedEntity;
                SelectedEntity = NewEntities.Find(c => c.OriginalTable == clbTables.Items[clbTables.SelectedIndex].ToString().Replace(" [In Relationship]", string.Empty));

                if (PreviousSelectedEntity == SelectedEntity)
                    return;

                if (clbTables.SelectedIndex != -1)
                {
                    grpEntity.Visible = true;
                    grpField.Visible = false;
                    GetTableEntity();
                    DisplayFields();
                }
                else
                {
                    grpEntity.Visible = false;
                    txtEntityDisplayName.Text = string.Empty;
                    txtEntityPluralName.Text = string.Empty;
                    txtEntityDescription.Text = string.Empty;
                }
            }
        }

        private void clbTables_ItemCheck(object sender, ItemCheckEventArgs e)
        {

            if (clbTables.Items[e.Index].ToString().Contains("[In Relationship]") && e.CurrentValue == CheckState.Checked)
            {
                e.NewValue = e.CurrentValue;
                return;
            }

            if (e.NewValue == CheckState.Unchecked)
                NewEntities.Find(c => c.OriginalTable == clbTables.Items[e.Index].ToString().Replace(" [In Relationship]", string.Empty)).Import = false;
            else
                NewEntities.Find(c => c.OriginalTable == clbTables.Items[e.Index].ToString().Replace(" [In Relationship]", string.Empty)).Import = true;

            if (NewEntities.Where(n => n.Import == true).Count() == 0)
            {
                btnCreateEntity.Enabled = false;
                chkAddNewFields.Enabled = false;
                chkPublish.Enabled = false;
            }
            else
            {
                btnCreateEntity.Enabled = true;
                chkAddNewFields.Enabled = true;
                chkPublish.Enabled = true;
            }

            ResetRelationshipFields();
        }

        private void GetTableEntity()
        {
            SelectedEntity = NewEntities.Find(c => c.OriginalTable == clbTables.SelectedItem.ToString().Replace(" [In Relationship]", string.Empty));
            if (SelectedEntity.EntityMetadata.SchemaName == null)
            {
                txtEntityDisplayName.Text = clbTables.SelectedItem.ToString();
                txtEntityPluralName.Text = clbTables.SelectedItem.ToString() + "s";
                txtPrimaryAttributeSize.Text = "100";
                chkShortPrimaryAttributeName.Checked = false;
            }
            else
            {
                txtEntityDisplayName.Text = SelectedEntity.EntityMetadata.DisplayName.LocalizedLabels.Where(l => l.LanguageCode == 1033).FirstOrDefault().Label;
                txtEntityPluralName.Text = SelectedEntity.EntityMetadata.DisplayCollectionName.LocalizedLabels.Where(l => l.LanguageCode == 1033).FirstOrDefault().Label;
                txtEntityDescription.Text = (SelectedEntity.EntityMetadata.Description != null) ?
                    SelectedEntity.EntityMetadata.Description.LocalizedLabels.Where(l => l.LanguageCode == 1033).FirstOrDefault().Label :
                    string.Empty;
                txtEntitySchemaName.Text = SelectedEntity.EntityMetadata.SchemaName.Split('_')[1];
                txtPrimaryAttributeSize.Text = SelectedEntity.PrimaryAttributeSize.ToString();
                chkShortPrimaryAttributeName.Checked = SelectedEntity.ShortPrimaryAttributeName;
            }
        }

        private void BindDropDownLists()
        {
            Dictionary<string, Microsoft.Xrm.Sdk.Metadata.StringFormat> stringFormatList = new Dictionary<string, Microsoft.Xrm.Sdk.Metadata.StringFormat>();
            stringFormatList.Add("E-mail", Microsoft.Xrm.Sdk.Metadata.StringFormat.Email);
            stringFormatList.Add("Text", Microsoft.Xrm.Sdk.Metadata.StringFormat.Text);
            stringFormatList.Add("Text Area", Microsoft.Xrm.Sdk.Metadata.StringFormat.TextArea);
            stringFormatList.Add("URL", Microsoft.Xrm.Sdk.Metadata.StringFormat.Url);
            stringFormatList.Add("Ticker Symbol", Microsoft.Xrm.Sdk.Metadata.StringFormat.TickerSymbol);
            ddlFieldSingleFormat.DataSource = stringFormatList.ToList();
            ddlFieldSingleFormat.ValueMember = "Value";
            ddlFieldSingleFormat.DisplayMember = "Key";
            ddlFieldSingleFormat.SelectedIndex = 1;

            Dictionary<string, Microsoft.Xrm.Sdk.Metadata.IntegerFormat> integerFormatList = new Dictionary<string, Microsoft.Xrm.Sdk.Metadata.IntegerFormat>();
            integerFormatList.Add("None", Microsoft.Xrm.Sdk.Metadata.IntegerFormat.None);
            integerFormatList.Add("Duration", Microsoft.Xrm.Sdk.Metadata.IntegerFormat.Duration);
            integerFormatList.Add("Time Zone", Microsoft.Xrm.Sdk.Metadata.IntegerFormat.TimeZone);
            integerFormatList.Add("Language", Microsoft.Xrm.Sdk.Metadata.IntegerFormat.Language);
            ddlFieldWholeFormat.DataSource = integerFormatList.ToList();
            ddlFieldWholeFormat.ValueMember = "Value";
            ddlFieldWholeFormat.DisplayMember = "Key";
            ddlFieldWholeFormat.SelectedIndex = 0;

            Dictionary<string, int> decimalPrecisionList = new Dictionary<string, int>();
            for (int i = 0; i < 11; i++)
            {
                decimalPrecisionList.Add(i.ToString(), i);
            }
            ddlFieldDecimalPrecision.DisplayMember = "Key";
            ddlFieldDecimalPrecision.ValueMember = "Value";
            ddlFieldDecimalPrecision.DataSource = decimalPrecisionList.ToList();
            ddlFieldDecimalPrecision.SelectedIndex = 2;

            Dictionary<string, int> floatPrecisionList = new Dictionary<string, int>();
            for (int i = 0; i < 6; i++)
            {
                floatPrecisionList.Add(i.ToString(), i);
            }
            ddlFieldFloatPrecision.DisplayMember = "Key";
            ddlFieldFloatPrecision.ValueMember = "Value";
            ddlFieldFloatPrecision.DataSource = floatPrecisionList.ToList();
            ddlFieldFloatPrecision.SelectedIndex = 2;

            Dictionary<string, Microsoft.Xrm.Sdk.Metadata.DateTimeFormat> dateFormatList = new Dictionary<string, Microsoft.Xrm.Sdk.Metadata.DateTimeFormat>();
            dateFormatList.Add("Date Only", DateTimeFormat.DateOnly);
            dateFormatList.Add("Date and Time", DateTimeFormat.DateAndTime);
            ddlFieldDateFormat.DataSource = dateFormatList.ToList();
            ddlFieldDateFormat.ValueMember = "Value";
            ddlFieldDateFormat.DisplayMember = "Key";
            ddlFieldDateFormat.SelectedIndex = 0;

            Dictionary<string, XRMSpeedyCurrencyValue> currencyFormatList = new Dictionary<string, XRMSpeedyCurrencyValue>();
            ICRM2011Organization org = new CRM2011Organization();
            int orgPrecision = org.GetOrganizationPricingDecimalPrecision(service);
            currencyFormatList.Add("Pricing Decimal Precision", new XRMSpeedyCurrencyValue(1, orgPrecision));
            currencyFormatList.Add("Currency Precision", new XRMSpeedyCurrencyValue(2, 4));
            currencyFormatList.Add("0", new XRMSpeedyCurrencyValue(0, 0));
            currencyFormatList.Add("1", new XRMSpeedyCurrencyValue(0, 1));
            currencyFormatList.Add("2", new XRMSpeedyCurrencyValue(0, 2));
            currencyFormatList.Add("3", new XRMSpeedyCurrencyValue(0, 3));
            currencyFormatList.Add("4", new XRMSpeedyCurrencyValue(0, 4));
            ddlFieldCurrencyPrecision.DisplayMember = "Key";
            ddlFieldCurrencyPrecision.ValueMember = "Value";
            ddlFieldCurrencyPrecision.DataSource = currencyFormatList.ToList();
            ddlFieldCurrencyPrecision.SelectedIndex = 1;
        }

        private void DisplayFields()
        {
            clbFields.Items.Clear();
            clbFields.Visible = true;

            foreach (XRMSpeedyField field in SelectedEntity.Fields)
            {
                clbFields.Items.Add(field.OriginalField, field.Import);
            }
        }

        private void HideFields()
        {
            clbFields.Visible = false;
            clbFields.Items.Clear();
        }

        private void DisplayFieldDetail()
        {
            XRMSpeedyField newField = SelectedEntity.Fields.Find(f => f.OriginalField == clbFields.SelectedItem.ToString());
            txtFieldDisplayName.Text = newField.AttributeMetadata.DisplayName.LocalizedLabels.Where(l => l.LanguageCode == 1033).FirstOrDefault().Label;
            txtFieldSchemaName.Text = newField.AttributeMetadata.SchemaName.Split('_')[1];
            ddlFieldType.SelectedIndex = GetFieldTypeIndex(newField.AttributeMetadata.AttributeType.Value);
            grpField.Visible = true;

            ValidateFieldSchemaName();
        }

        private void SetFieldByType(XRMSpeedyField field)
        {
            switch (field.AttributeMetadata.AttributeType)
            {
                case AttributeTypeCode.String:
                    DisplayFieldSpecific(grpFieldFormatSingle);
                    ddlFieldSingleFormat.SelectedValue = ((StringAttributeMetadata)field.AttributeMetadata).Format;
                    txtFieldSingleMaxLength.Text = ((StringAttributeMetadata)field.AttributeMetadata).MaxLength.ToString();
                    break;
                case AttributeTypeCode.Boolean:
                    DisplayFieldSpecific(grpFieldTwoOption);
                    txtFieldTwoOptionNoValue.Text = ((BooleanAttributeMetadata)field.AttributeMetadata).OptionSet.FalseOption.Label.LocalizedLabels
                        .Where(l => l.LanguageCode == 1033).FirstOrDefault().Label;
                    txtFieldTwoOptionYesValue.Text = ((BooleanAttributeMetadata)field.AttributeMetadata).OptionSet.TrueOption.Label.LocalizedLabels
                        .Where(l => l.LanguageCode == 1033).FirstOrDefault().Label;
                    ddlFieldTwoOptionDefaultValue.SelectedValue = (((BooleanAttributeMetadata)field.AttributeMetadata).DefaultValue == true) ? 0 : 1;
                    break;
                case AttributeTypeCode.Integer:
                    DisplayFieldSpecific(grpFieldWhole);
                    ddlFieldWholeFormat.SelectedValue = ((IntegerAttributeMetadata)field.AttributeMetadata).Format;
                    txtFieldWholeMinimum.Text = ((IntegerAttributeMetadata)field.AttributeMetadata).MinValue.ToString();
                    txtFieldWholeMaximum.Text = ((IntegerAttributeMetadata)field.AttributeMetadata).MaxValue.ToString();
                    break;
                case AttributeTypeCode.Double:
                    DisplayFieldSpecific(grpFieldFloat);
                    ddlFieldFloatPrecision.SelectedValue = ((DoubleAttributeMetadata)field.AttributeMetadata).Precision;
                    txtFieldFloatMinimum.Text = ((DoubleAttributeMetadata)field.AttributeMetadata).MinValue.Value.ToString("F" + ddlFieldFloatPrecision.SelectedValue.ToString());
                    txtFieldFloatMaximum.Text = ((DoubleAttributeMetadata)field.AttributeMetadata).MaxValue.Value.ToString("F" + ddlFieldFloatPrecision.SelectedValue.ToString());
                    break;
                case AttributeTypeCode.Decimal:
                    DisplayFieldSpecific(grpFieldDecimal);
                    ddlFieldDecimalPrecision.SelectedValue = ((DecimalAttributeMetadata)field.AttributeMetadata).Precision;
                    txtFieldDecimalMinimum.Text = ((DecimalAttributeMetadata)field.AttributeMetadata).MinValue.Value.ToString("F" + ddlFieldDecimalPrecision.SelectedValue.ToString());
                    txtFieldDecimalMaximum.Text = ((DecimalAttributeMetadata)field.AttributeMetadata).MaxValue.Value.ToString("F" + ddlFieldDecimalPrecision.SelectedValue.ToString());
                    break;
                case AttributeTypeCode.Money:
                    DisplayFieldSpecific(grpFieldCurrency);
                    XRMSpeedyCurrencyValue val = new XRMSpeedyCurrencyValue(((MoneyAttributeMetadata)field.AttributeMetadata).PrecisionSource.Value,
                        ((MoneyAttributeMetadata)field.AttributeMetadata).Precision.Value);
                    ddlFieldCurrencyPrecision.SelectedIndex = FindCurrencyPrecisionIndex(val);
                    txtFieldCurrencyMinimum.Text = decimal.Parse(((MoneyAttributeMetadata)field.AttributeMetadata).MinValue.ToString()).ToString("F" + val.Precision.ToString());
                    txtFieldCurrencyMaximum.Text = decimal.Parse(((MoneyAttributeMetadata)field.AttributeMetadata).MaxValue.ToString()).ToString("F" + val.Precision.ToString());
                    break;
                case AttributeTypeCode.Memo:
                    DisplayFieldSpecific(grpFieldMultiple);
                    txtFieldMemoMaxLength.Text = ((MemoAttributeMetadata)field.AttributeMetadata).MaxLength.ToString();
                    break;
                case AttributeTypeCode.DateTime:
                    DisplayFieldSpecific(grpFieldDate);
                    ddlFieldDateFormat.SelectedValue = ((DateTimeAttributeMetadata)field.AttributeMetadata).Format;
                    break;
            }
        }

        private int FindCurrencyPrecisionIndex(XRMSpeedyCurrencyValue val)
        {
            int result = 0;
            ICRM2011Organization org = new CRM2011Organization();
            if (val.PrecisionSource == 1 && val.Precision == org.GetOrganizationPricingDecimalPrecision(service))
                result = 0;
            if (val.PrecisionSource == 2 && val.Precision == 4)
                result = 1;
            if (val.PrecisionSource == 0 && val.Precision == 0)
                result = 2;
            if (val.PrecisionSource == 0 && val.Precision == 1)
                result = 3;
            if (val.PrecisionSource == 0 && val.Precision == 2)
                result = 4;
            if (val.PrecisionSource == 0 && val.Precision == 3)
                result = 5;
            if (val.PrecisionSource == 0 && val.Precision == 4)
                result = 6;

            return result;
        }

        private int GetFieldTypeIndex(AttributeTypeCode type)
        {
            int index = -1;

            switch (type)
            {
                case AttributeTypeCode.String:
                    index = 0;
                    break;
                case AttributeTypeCode.Boolean:
                    index = 1;
                    break;
                case AttributeTypeCode.Integer:
                    index = 2;
                    break;
                case AttributeTypeCode.Double:
                    index = 3;
                    break;
                case AttributeTypeCode.Decimal:
                    index = 4;
                    break;
                case AttributeTypeCode.Money:
                    index = 5;
                    break;
                case AttributeTypeCode.Memo:
                    index = 6;
                    break;
                case AttributeTypeCode.DateTime:
                    index = 7;
                    break;
            }

            return index;
        }

        private void HideFieldDetail()
        {
            grpField.Visible = false;
            txtFieldDisplayName.Text = string.Empty;
            txtFieldSchemaName.Text = string.Empty;
            ddlFieldType.SelectedIndex = -1;
        }

        private void ddlEntityOwnership_Leave(object sender, EventArgs e)
        {
            SelectedEntity.EntityMetadata.OwnershipType = (ddlEntityOwnership.SelectedItem.ToString() == "Organization") ?
                OwnershipTypes.OrganizationOwned : OwnershipTypes.UserOwned;
        }

        private void clbFields_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                SelectedEntity.Fields.Find(f => f.OriginalField == clbFields.Items[e.Index].ToString()).Import = true;
            }

            if (e.NewValue == CheckState.Unchecked)
            {
                string[] val = clbFields.Items[e.Index].ToString().Split(',');
                SelectedEntity.Fields.Find(f => f.OriginalField == clbFields.Items[e.Index].ToString()).Import = false;
            }
        }

        private void clbFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            XRMSpeedyField PreviousSelectedField = SelectedField;

            if (clbFields.SelectedItem != null)
            {
                SelectedField = SelectedEntity.Fields.Find(f => f.OriginalField == clbFields.SelectedItem.ToString());
                if (PreviousSelectedField == SelectedField && clbFields.Items.Count > 1)
                    return;

                if (clbFields.SelectedIndex != -1)
                {
                    SetFieldByType(SelectedField);
                    DisplayFieldDetail();
                }
                else
                    HideFieldDetail();
            }
        }

        private void DisplayFieldSpecific(GroupBox box)
        {
            foreach (Control c in grpField.Controls)
            {
                if (c is GroupBox)
                    c.Visible = false;
            }

            box.Visible = true;
            box.Parent = grpField;
            box.Location = new Point(6, 170);
        }

        private Microsoft.Xrm.Sdk.Metadata.IntegerFormat SetIntegerFormat(string inputFormat)
        {
            Microsoft.Xrm.Sdk.Metadata.IntegerFormat format = Microsoft.Xrm.Sdk.Metadata.IntegerFormat.None;
            switch (inputFormat)
            {
                case "None":
                    format = Microsoft.Xrm.Sdk.Metadata.IntegerFormat.None;
                    break;
                case "Duration":
                    format = Microsoft.Xrm.Sdk.Metadata.IntegerFormat.Duration;
                    break;
                case "Time Zone":
                    format = Microsoft.Xrm.Sdk.Metadata.IntegerFormat.TimeZone;
                    break;
                case "Language":
                    format = Microsoft.Xrm.Sdk.Metadata.IntegerFormat.Language;
                    break;
            }
            return format;
        }

        private Microsoft.Xrm.Sdk.Metadata.StringFormat SetStringFormat(string inputFormat)
        {
            Microsoft.Xrm.Sdk.Metadata.StringFormat format = Microsoft.Xrm.Sdk.Metadata.StringFormat.Text;
            switch (inputFormat)
            {
                case "E-mail":
                    format = Microsoft.Xrm.Sdk.Metadata.StringFormat.Email;
                    break;
                case "Text":
                    format = Microsoft.Xrm.Sdk.Metadata.StringFormat.Text;
                    break;
                case "Text Area":
                    format = Microsoft.Xrm.Sdk.Metadata.StringFormat.TextArea;
                    break;
                case "URL":
                    format = Microsoft.Xrm.Sdk.Metadata.StringFormat.Url;
                    break;
                case "Ticker Symbol":
                    format = Microsoft.Xrm.Sdk.Metadata.StringFormat.TickerSymbol;
                    break;
            }
            return format;
        }

        private Microsoft.Xrm.Sdk.Metadata.DateTimeFormat SetDateTimeFormat(string inputFormat)
        {
            Microsoft.Xrm.Sdk.Metadata.DateTimeFormat format = Microsoft.Xrm.Sdk.Metadata.DateTimeFormat.DateOnly;
            switch (inputFormat)
            {
                case "Date Only":
                    format = Microsoft.Xrm.Sdk.Metadata.DateTimeFormat.DateOnly;
                    break;
                case "Date and Time":
                    format = Microsoft.Xrm.Sdk.Metadata.DateTimeFormat.DateAndTime;
                    break;
            }
            return format;
        }

        private void ddlFieldSingleFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (clbFields.SelectedItem != null)
                ((StringAttributeMetadata)SelectedField.AttributeMetadata).Format =
                    (Microsoft.Xrm.Sdk.Metadata.StringFormat)ddlFieldSingleFormat.SelectedValue;
        }

        private void bgWorkerCreate_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            CreateEntities();
        }

        private void bgWorkerCreate_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (!chkPublish.Checked)
                btnDelete.Enabled = true;

            chkAddNewFields.Enabled = false;
            chkPublish.Enabled = false;

            grpTables.Enabled = true;
            grpFields.Enabled = true;
            grpRelationships.Enabled = true;
            grpOptions.Enabled = true;

            string Unpublished = "Items created and unpublished - If you want to rollback these updates you will have best results if you do not publish immediately or add any additional form elements or relationships";
            string Published = "Items created and published";

            pbSpinner.Visible = false;

            MessageBox.Show((chkPublish.Checked) ? Published : Unpublished);
        }

        private void CreateEntities()
        {
            ICRM2011Entity ent = new CRM2011Entity();
            ICRM2011FormXML fxml = new CRM2011FormXML();

            bool result = false;
            foreach (XRMSpeedyEntity entity in NewEntities.Where(n => n.Import == true))
            {
                result = ent.CreateEntity(service, entity, 1033);
                if (!result)
                {
                    MessageBox.Show("Error Creating Items");
                    return;
                }
            }

            CreatedEntities = NewEntities.Where(n => n.Import == true).ToList();

            foreach (XRMSpeedyRelationship relationship in NewRelationships)
            {
                switch (relationship.RelationshipType)
                {
                    case "1:N Relationship":
                        result = ent.CreateOneToMany(service, relationship, txtPrefix.Text.Trim(), 1033);
                        break;
                    case "N:N Relationship":
                        result = ent.CreateManyToMany(service, relationship, txtPrefix.Text.Trim(), 1033);
                        break;
                }

                if (!result)
                {
                    MessageBox.Show("Error Creating Relationships");
                    return;
                }
            }

            if (chkAddNewFields.Checked)
            {
                foreach (XRMSpeedyEntity entity in NewEntities.Where(n => n.Import == true))
                {
                    fxml.AddCustomFields(service, entity.EntityMetadata.SchemaName);
                }
            }

            CreatedRelationships = NewRelationships;

            if (chkPublish.Checked)
                ent.PublishEntities(service, CreatedEntities);
        }

        private void btnCreateEntity_Click(object sender, EventArgs e)
        {
            if (NewEntities.Where(n => n.Import == true).Count() == 0)
            {
                MessageBox.Show("Please select at least one table to import");
                return;
            }

            pbSpinner.Visible = true;
            btnCreateEntity.Enabled = false;
            grpConnection.Enabled = false;
            grpTables.Enabled = false;
            grpFields.Enabled = false;
            grpRelationships.Enabled = false;
            grpOptions.Enabled = false;

            bgWorkerCreate.RunWorkerAsync();
        }

        private void SetTwoOptionsDefault()
        {
            int i = ddlFieldTwoOptionDefaultValue.SelectedIndex;
            ddlFieldTwoOptionDefaultValue.Items.Clear();
            ddlFieldTwoOptionDefaultValue.Items.Add(txtFieldTwoOptionNoValue.Text.Trim());
            ddlFieldTwoOptionDefaultValue.Items.Add(txtFieldTwoOptionYesValue.Text.Trim());
            ddlFieldTwoOptionDefaultValue.SelectedIndex = i;
        }

        private int FindNumberDecimals(string input)
        {
            int i = input.IndexOf(".") + 1;
            return ((input.Length) - i);
        }

        private void ddlFieldType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            foreach (Control c in grpField.Controls)
            {
                if (c is GroupBox)
                    c.Visible = false;
            }

            SelectedField.AttributeMetadata = XRMSpeedyField.SetMetadataType(ddlFieldType.SelectedItem.ToString(), txtPrefix.Text.Trim().ToLower(),
                SelectedField.AttributeMetadata.DisplayName.LocalizedLabels.Where(l => l.LanguageCode == 1033).FirstOrDefault().Label);

            switch (ddlFieldType.SelectedItem.ToString())
            {
                case "Single Line of Text":
                    DisplayFieldSpecific(grpFieldFormatSingle);
                    break;
                case "Two Options":
                    DisplayFieldSpecific(grpFieldTwoOption);
                    break;
                case "Whole Number":
                    DisplayFieldSpecific(grpFieldWhole);
                    break;
                case "Floating Point Number":
                    DisplayFieldSpecific(grpFieldFloat);
                    break;
                case "Decimal Number":
                    DisplayFieldSpecific(grpFieldDecimal);
                    break;
                case "Currency":
                    DisplayFieldSpecific(grpFieldCurrency);
                    break;
                case "Multiple Lines of Text":
                    DisplayFieldSpecific(grpFieldMultiple);
                    break;
                case "Date and Time":
                    DisplayFieldSpecific(grpFieldDate);
                    break;
            }
        }

        private void ddlFieldWholeFormat_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ((IntegerAttributeMetadata)SelectedField.AttributeMetadata).Format
                       = (IntegerFormat)ddlFieldWholeFormat.SelectedValue;
            if (((IntegerAttributeMetadata)SelectedField.AttributeMetadata).Format == IntegerFormat.None)
                grpFieldWholeMinMax.Visible = true;
            else
                grpFieldWholeMinMax.Visible = false;
        }

        private void ddlFieldFloatPrecision_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int i = FindNumberDecimals(txtFieldFloatMinimum.Text.Trim().ToString());
            if (i < int.Parse(ddlFieldFloatPrecision.SelectedValue.ToString()))
            {
                txtFieldFloatMinimum.Text = decimal.Parse(txtFieldFloatMinimum.Text).ToString("F" + ddlFieldFloatPrecision.SelectedValue.ToString());
                txtFieldFloatMaximum.Text = decimal.Parse(txtFieldFloatMaximum.Text).ToString("F" + ddlFieldFloatPrecision.SelectedValue.ToString());
            }
            else
            {
                txtFieldFloatMinimum.Text = Math.Round(decimal.Parse(txtFieldFloatMinimum.Text), int.Parse(ddlFieldFloatPrecision.SelectedValue.ToString())).ToString();
                txtFieldFloatMaximum.Text = Math.Round(decimal.Parse(txtFieldFloatMaximum.Text), int.Parse(ddlFieldFloatPrecision.SelectedValue.ToString())).ToString();
            }
        }

        private void ddlFieldDecimalPrecision_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int i = FindNumberDecimals(txtFieldDecimalMinimum.Text.Trim().ToString());
            if (i < (int)ddlFieldDecimalPrecision.SelectedValue)
            {
                txtFieldDecimalMinimum.Text = decimal.Parse(txtFieldDecimalMinimum.Text).ToString("F" + ddlFieldDecimalPrecision.SelectedValue.ToString());
                txtFieldDecimalMaximum.Text = decimal.Parse(txtFieldDecimalMaximum.Text).ToString("F" + ddlFieldDecimalPrecision.SelectedValue.ToString());
            }
            else
            {
                txtFieldDecimalMinimum.Text = Math.Round(decimal.Parse(txtFieldDecimalMinimum.Text), (int)ddlFieldDecimalPrecision.SelectedValue).ToString();
                txtFieldDecimalMaximum.Text = Math.Round(decimal.Parse(txtFieldDecimalMaximum.Text), (int)ddlFieldDecimalPrecision.SelectedValue).ToString();
            }
        }

        private void ddlFieldCurrencyPrecision_SelectionChangeCommitted(object sender, EventArgs e)
        {
            XRMSpeedyCurrencyValue cValue = (XRMSpeedyCurrencyValue)ddlFieldCurrencyPrecision.SelectedValue;
            int i = FindNumberDecimals(txtFieldCurrencyMinimum.Text.Trim().ToString());
            if (i < cValue.Precision)
            {
                txtFieldCurrencyMinimum.Text = decimal.Parse(txtFieldCurrencyMinimum.Text).ToString("F" + cValue.Precision.ToString());
                txtFieldCurrencyMaximum.Text = decimal.Parse(txtFieldCurrencyMaximum.Text).ToString("F" + cValue.Precision.ToString());
            }
            else
            {
                txtFieldCurrencyMinimum.Text = Math.Round(decimal.Parse(txtFieldCurrencyMinimum.Text), cValue.Precision).ToString();
                txtFieldCurrencyMaximum.Text = Math.Round(decimal.Parse(txtFieldCurrencyMaximum.Text), cValue.Precision).ToString();
            }
        }

        private void bgWorkerDelete_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            DeleteEntities();
        }

        private void DeleteEntities()
        {
            ICRM2011Entity ent = new CRM2011Entity();

            if (chkAddNewFields.Checked)
            {
                CRM2011FormXML fxml = new CRM2011FormXML();
                foreach (XRMSpeedyEntity entity in CreatedEntities)
                {
                    fxml.RemoveCustomFields(service, entity.EntityMetadata.SchemaName);
                }
            }

            foreach (XRMSpeedyRelationship relationship in CreatedRelationships)
            {
                ent.DeleteRelationship(service, relationship);
            }

            foreach (XRMSpeedyEntity entity in CreatedEntities)
            {
                ent.DeleteEntity(service, entity);
            }
        }

        private void bgWorkerDelete_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            CreatedEntities = null;
            CreatedRelationships = null;
            btnCreateEntity.Enabled = true;
            btnDelete.Enabled = false;
            chkAddNewFields.Enabled = true;
            chkPublish.Enabled = true;
            grpConnection.Enabled = true;
            pbSpinner.Visible = false;

            MessageBox.Show("Entities and relationships have been successfully removed");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            pbSpinner.Visible = true;
            btnDelete.Enabled = false;

            bgWorkerDelete.RunWorkerAsync();
        }

        #region Validation

        private void txtPrefix_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string message = string.Empty;
            bool error = false;

            if (txtPrefix.Text.Trim().Length < 2)
            {
                message = "Prefix must have at least 2 characters";
                error = true;
            }

            if (txtPrefix.Text.StartsWith("mscrm"))
            {
                message = "Prefix cannot start with 'mscrm'";
                error = true;
            }

            Regex reg = new Regex("^\\d[^<]+");
            if (reg.Match(txtPrefix.Text.Trim()).Success)
            {
                message = "Prefix must start with a letter";
                error = true;
            }

            reg = new Regex("^[a-zA-Z0-9]*$");
            if (!reg.Match(txtPrefix.Text.Trim()).Success)
            {
                message = "Prefix can only contain alphanumeric characters";
                error = true;
            }

            if (error)
            {
                MessageBox.Show(message, "Error");
                txtPrefix.Text = "xrmspdy";
            }
        }

        private void txtPrefix_Validated(object sender, EventArgs e)
        {
            lblEntityPrefix.Text = txtPrefix.Text.Trim().ToLower() + "_";
            lblFieldPrefix.Text = txtPrefix.Text.Trim().ToLower() + "_";

            foreach (XRMSpeedyEntity entity in NewEntities)
            {
                string[] val = entity.EntityMetadata.SchemaName.Split('_');
                entity.EntityMetadata.SchemaName = txtPrefix.Text.Trim().ToLower() + "_" + val[1];

                foreach (XRMSpeedyField field in entity.Fields)
                {
                    string[] val2 = field.AttributeMetadata.SchemaName.Split('_');
                    field.AttributeMetadata.SchemaName = txtPrefix.Text.Trim().ToLower() + "_" + val2[1];
                }
            }

            foreach (XRMSpeedyRelationship relationship in NewRelationships)
            {
                string[] val3 = relationship.SchemaName.Split('_');
                relationship.SchemaName = txtPrefix.Text.Trim().ToLower();
                for (int i = 1; i < val3.Length; i++)
                {
                    relationship.SchemaName = relationship.SchemaName + "_" + val3[i];
                }

                if (relationship.PrimaryField != null)
                {
                    string[] val4 = relationship.PrimaryField.SchemaName.Split('_');
                    relationship.PrimaryField.SchemaName = txtPrefix.Text.Trim().ToLower();
                    for (int i = 1; i < val4.Length; i++)
                    {
                        relationship.PrimaryField.SchemaName = relationship.PrimaryField.SchemaName + "_" + val3[i];
                    }
                }
            }
        }

        private void txtEntityDisplayName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string message = string.Empty;
            bool error = false;

            if (txtEntityDisplayName.Text.Trim() == string.Empty)
            {
                message = "Display Name cannot blank";
                error = true;
            }

            if (error)
            {
                MessageBox.Show(message, "Error");
                SelectedEntity = NewEntities.Find(c => c.OriginalTable == clbTables.SelectedItem.ToString().Replace(" [In Relationship]", string.Empty));
                txtEntityDisplayName.Text = SelectedEntity.EntityMetadata.DisplayName.LocalizedLabels.Where(l => l.LanguageCode == 1033).FirstOrDefault().Label;
            }
        }

        private void txtEntityDisplayName_Validated(object sender, EventArgs e)
        {
            if (SelectedEntity.EntityMetadata.DisplayName == null)
                SelectedEntity.EntityMetadata.DisplayName = new Microsoft.Xrm.Sdk.Label(txtEntityDisplayName.Text.Trim(), 1033);
            else
                (SelectedEntity.EntityMetadata.DisplayName.LocalizedLabels.Where(l => l.LanguageCode == 1033).FirstOrDefault()).Label
                    = txtEntityDisplayName.Text.Trim();
        }

        private void txtEntityPluralName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string message = string.Empty;
            bool error = false;

            if (txtEntityPluralName.Text.Trim() == string.Empty)
            {
                message = "Plural Name cannot blank";
                error = true;
            }

            if (error)
            {
                MessageBox.Show(message, "Error");
                SelectedEntity = NewEntities.Find(c => c.OriginalTable == clbTables.SelectedItem.ToString().Replace(" [In Relationship]", string.Empty));
                txtEntityPluralName.Text = SelectedEntity.EntityMetadata.DisplayCollectionName.LocalizedLabels.Where(l => l.LanguageCode == 1033).FirstOrDefault().Label;
            }
        }

        private void txtEntityPluralName_Validated(object sender, EventArgs e)
        {
            if (SelectedEntity.EntityMetadata.DisplayCollectionName == null)
                SelectedEntity.EntityMetadata.DisplayCollectionName = new Microsoft.Xrm.Sdk.Label(txtEntityPluralName.Text.Trim(), 1033);
            else
                (SelectedEntity.EntityMetadata.DisplayCollectionName.LocalizedLabels.Where(l => l.LanguageCode == 1033).FirstOrDefault()).Label =
                    txtEntityPluralName.Text.Trim();
        }

        private void txtEntitySchemaName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string message = string.Empty;
            bool error = false;

            if (txtEntitySchemaName.Text.Trim() == string.Empty)
            {
                message = "Schema Name cannot blank";
                error = true;
            }

            Regex reg = new Regex("^[a-zA-Z0-9_]*$");
            if (!reg.Match(txtEntitySchemaName.Text.Trim()).Success)
            {
                message = "Schema name can only contain alphanumeric characters and underscore characters";
                error = true;
            }

            if (error)
            {
                MessageBox.Show(message, "Error");
                SelectedEntity = NewEntities.Find(c => c.OriginalTable == clbTables.SelectedItem.ToString().Replace(" [In Relationship]", string.Empty));
                txtEntitySchemaName.Text = SelectedEntity.EntityMetadata.SchemaName.Split('_')[1];
            }
        }

        private void txtEntitySchemaName_Validated(object sender, EventArgs e)
        {
            SelectedEntity.EntityMetadata.SchemaName = txtPrefix.Text.Trim() + "_" + txtEntityDisplayName.Text.Trim().ToLower();
        }

        private void txtEntityDescription_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEntityDescription.Text.Trim()))
                if (SelectedEntity.EntityMetadata.Description == null)
                    SelectedEntity.EntityMetadata.Description = new Microsoft.Xrm.Sdk.Label(txtEntityDescription.Text.Trim(), 1033);
                else
                    (SelectedEntity.EntityMetadata.Description.LocalizedLabels.Where(l => l.LanguageCode == 1033).FirstOrDefault()).Label
                        = txtEntityDescription.Text.Trim();
            else
                SelectedEntity.EntityMetadata.Description = null;
        }

        private void txtEntityPrimaryFieldSize_Validated(object sender, EventArgs e)
        {
            int fieldSize;
            int.TryParse(txtPrimaryAttributeSize.Text, out fieldSize);
            SelectedEntity.PrimaryAttributeSize = fieldSize;
        }

        private void txtEntityPrimaryFieldSize_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string message = string.Empty;
            bool error = false;

            int fieldSize;
            if (!int.TryParse(txtPrimaryAttributeSize.Text, out fieldSize) || fieldSize <= 0)
            {
                message = "Primary Field Size must be a number between 1 and 4000";
                error = true;
            }

            if (error)
            {
                MessageBox.Show(message, "Error");
                SelectedEntity = NewEntities.Find(c => c.OriginalTable == clbTables.SelectedItem.ToString().Replace(" [In Relationship]", string.Empty));
                txtPrimaryAttributeSize.Text = SelectedEntity.PrimaryAttributeSize.ToString();
            }
        }

        private void txtFieldDisplayName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string message = string.Empty;
            bool error = false;

            if (txtFieldDisplayName.Text.Trim() == string.Empty)
            {
                message = "Display Name cannot blank";
                error = true;
            }

            if (error)
            {
                MessageBox.Show(message, "Error");
                XRMSpeedyField newField = SelectedEntity.Fields.Find(f => f.OriginalField == clbFields.SelectedItem.ToString());
                txtFieldDisplayName.Text = newField.AttributeMetadata.DisplayName.LocalizedLabels.Where(l => l.LanguageCode == 1033).FirstOrDefault().Label;
            }
        }

        private void txtFieldDisplayName_Validated(object sender, EventArgs e)
        {
            if (SelectedField.AttributeMetadata.DisplayName == null)
                SelectedField.AttributeMetadata.DisplayName = new Microsoft.Xrm.Sdk.Label(txtFieldDisplayName.Text.Trim(), 1033);
            else
                (SelectedField.AttributeMetadata.DisplayName.LocalizedLabels.Where(l => l.LanguageCode == 1033).FirstOrDefault()).Label
                    = txtFieldDisplayName.Text.Trim();
        }

        private void txtFieldSchemaName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ValidateFieldSchemaName();
        }

        private void ValidateFieldSchemaName()
        {
            string message = string.Empty;
            bool error = false;
            bool exist = false;

            if (txtFieldSchemaName.Text.Trim() == string.Empty)
            {
                message = "Schema Name cannot blank";
                error = true;
            }

            Regex reg = new Regex("^[a-zA-Z0-9_]*$");
            if (!reg.Match(txtFieldSchemaName.Text.Trim()).Success)
            {
                message = "Schema Name can only contain alphanumeric characters and underscore characters";
                error = true;
            }

            if (((txtPrefix.Text.Trim() + "_" + txtFieldSchemaName.Text.Trim()).ToLower() == (SelectedEntity.EntityMetadata.SchemaName + "id").ToLower()) ||
                ((txtPrefix.Text.Trim() + "_" + txtFieldSchemaName.Text.Trim()).ToLower() == (SelectedEntity.EntityMetadata.SchemaName + "name").ToLower()))
            {
                message = "A field with the specified name already exists";
                error = true;
                exist = true;
            }

            if (error)
            {
                MessageBox.Show(message, "Error");
                XRMSpeedyField newField = SelectedEntity.Fields.Find(f => f.OriginalField == clbFields.SelectedItem.ToString());
                txtFieldSchemaName.Text = newField.AttributeMetadata.SchemaName.Split('_')[1];
                if (exist)
                {
                    txtFieldSchemaName.Text += "2";
                    SelectedField.AttributeMetadata.SchemaName = txtPrefix.Text.Trim().ToLower() + "_" + txtFieldSchemaName.Text.Trim().ToLower();
                }
            }
        }

        private void txtFieldSchemaName_Validated(object sender, EventArgs e)
        {
            SelectedField.AttributeMetadata.SchemaName = txtPrefix.Text.Trim().ToLower() + "_" + txtFieldSchemaName.Text.Trim().ToLower();
        }

        private void ddlFieldSingleFormat_Validated(object sender, EventArgs e)
        {
            ((StringAttributeMetadata)SelectedField.AttributeMetadata).Format =
                (Microsoft.Xrm.Sdk.Metadata.StringFormat)ddlFieldSingleFormat.SelectedValue;
        }

        private void txtFieldSingleMaxLength_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string message = string.Empty;
            bool error = false;

            if (txtFieldSingleMaxLength.Text.Trim() == string.Empty)
            {
                message = "Maximum Length cannot blank";
                error = true;
            }

            if (!error)
            {
                int number;
                bool result = int.TryParse(txtFieldSingleMaxLength.Text.Trim(), out number);
                if (!result)
                {
                    message = "Maximum Length must be numeric";
                    error = true;
                }

                if (!error)
                {
                    if (int.Parse(txtFieldSingleMaxLength.Text) < 1 || int.Parse(txtFieldSingleMaxLength.Text) > 4000)
                    {
                        message = "Maximum Length must be a number between 1 and 4,000";
                        error = true;
                    }
                }
            }

            if (error)
            {
                MessageBox.Show(message, "Error");
                txtFieldSingleMaxLength.Text = ((StringAttributeMetadata)SelectedField.AttributeMetadata).MaxLength.ToString();
            }
        }

        private void txtFieldSingleMaxLength_Validated(object sender, EventArgs e)
        {
            ((StringAttributeMetadata)SelectedField.AttributeMetadata).MaxLength = int.Parse(txtFieldSingleMaxLength.Text.Trim());
        }

        private void txtFieldMemoMaxLength_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string message = string.Empty;
            bool error = false;

            if (txtFieldMemoMaxLength.Text.Trim() == string.Empty)
            {
                message = "Maximum Length cannot blank";
                error = true;
            }

            int number;
            bool result = int.TryParse(txtFieldMemoMaxLength.Text.Trim(), out number);
            if (!result)
            {
                message = "You must enter a number between 1 and 1,048,576";
                error = true;
            }

            if (number < 1 || number > 1048576)
            {
                message = "You must enter a number between 1 and 1,048,576";
                error = true;
            }

            if (error)
            {
                MessageBox.Show(message, "Error");
                txtFieldMemoMaxLength.Text = ((MemoAttributeMetadata)SelectedField.AttributeMetadata).MaxLength.ToString();
            }
        }

        private void txtFieldMemoMaxLength_Validated(object sender, EventArgs e)
        {
            ((MemoAttributeMetadata)SelectedField.AttributeMetadata).MaxLength
                = int.Parse(txtFieldMemoMaxLength.Text);
        }

        private void ddlFieldDateFormat_Validated(object sender, EventArgs e)
        {
            ((DateTimeAttributeMetadata)SelectedField.AttributeMetadata).Format = (DateTimeFormat)ddlFieldDateFormat.SelectedValue;
        }

        private void ddlFieldCurrencyPrecision_Validated(object sender, EventArgs e)
        {
            XRMSpeedyCurrencyValue cValue = (XRMSpeedyCurrencyValue)ddlFieldCurrencyPrecision.SelectedValue;
            ((MoneyAttributeMetadata)SelectedField.AttributeMetadata).PrecisionSource = cValue.PrecisionSource.Value;
            ((MoneyAttributeMetadata)SelectedField.AttributeMetadata).Precision = cValue.Precision;
        }

        private void txtFieldCurrencyMinimum_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string message = string.Empty;
            bool error = false;

            XRMSpeedyCurrencyValue cValue = (XRMSpeedyCurrencyValue)ddlFieldCurrencyPrecision.SelectedValue;
            decimal number;
            bool result = decimal.TryParse(txtFieldCurrencyMinimum.Text.Trim(), out number);
            if (!result)
            {
                message = "You must enter a number between " + string.Format("{0:N" + cValue.Precision.ToString() + "}", -922337203685477) + " and " +
                    string.Format("{0:N" + cValue.Precision.ToString() + "}", 922337203685477);
                error = true;
            }

            if (!error)
            {
                if (decimal.Parse(txtFieldCurrencyMinimum.Text.Trim()) < -922337203685477 || decimal.Parse(txtFieldCurrencyMinimum.Text.Trim()) > 922337203685447)
                {
                    message = "You must enter a number between " + string.Format("{0:N" + cValue.Precision.ToString() + "}", -922337203685477) + " and " +
                    string.Format("{0:N" + cValue.Precision.ToString() + "}", 922337203685477);
                    error = true;
                }


                int i = FindNumberDecimals(txtFieldCurrencyMinimum.Text.Trim().ToString());
                if (i < cValue.Precision)
                {
                    txtFieldCurrencyMinimum.Text = decimal.Parse(txtFieldCurrencyMinimum.Text).ToString("F" + cValue.Precision.ToString());
                }

                if (number > decimal.Parse(txtFieldCurrencyMaximum.Text.Trim()))
                {
                    message = "Minimum value cannot be greater than maximum value";
                    error = true;
                }
            }

            if (error)
            {
                MessageBox.Show(message, "Error");
                txtFieldCurrencyMinimum.Text = ((MoneyAttributeMetadata)SelectedField.AttributeMetadata).MinValue.Value.ToString("F" + cValue.Precision.ToString());
            }
        }

        private void txtFieldCurrencyMinimum_Validated(object sender, EventArgs e)
        {
            ((MoneyAttributeMetadata)SelectedField.AttributeMetadata).MinValue
                = double.Parse(txtFieldCurrencyMinimum.Text.Trim());
            XRMSpeedyCurrencyValue cValue = (XRMSpeedyCurrencyValue)ddlFieldCurrencyPrecision.SelectedValue;
            txtFieldCurrencyMinimum.Text = decimal.Parse(((MoneyAttributeMetadata)SelectedField.AttributeMetadata).MinValue.ToString()).ToString("F" + cValue.Precision.ToString());
        }

        private void txtFieldCurrencyMaximum_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string message = string.Empty;
            bool error = false;

            XRMSpeedyCurrencyValue cValue = (XRMSpeedyCurrencyValue)ddlFieldCurrencyPrecision.SelectedValue;
            decimal number;
            bool result = decimal.TryParse(txtFieldCurrencyMaximum.Text.Trim(), out number);
            if (!result)
            {
                message = "You must enter a number between " + string.Format("{0:N" + cValue.Precision.ToString() + "}", -922337203685477) + " and " +
                    string.Format("{0:N" + cValue.Precision.ToString() + "}", 922337203685477);
                error = true;
            }

            if (!error)
            {
                if (decimal.Parse(txtFieldCurrencyMaximum.Text.Trim()) < -922337203685477 || decimal.Parse(txtFieldCurrencyMaximum.Text.Trim()) > 922337203685447)
                {
                    message = "You must enter a number between " + string.Format("{0:N" + cValue.Precision.ToString() + "}", -922337203685477) + " and " +
                    string.Format("{0:N" + cValue.Precision.ToString() + "}", 922337203685477);
                    error = true;
                }

                int i = FindNumberDecimals(txtFieldCurrencyMaximum.Text.Trim().ToString());
                if (i < cValue.Precision)
                {
                    txtFieldCurrencyMaximum.Text = decimal.Parse(txtFieldCurrencyMaximum.Text).ToString("F" + cValue.Precision.ToString());
                }

                if (decimal.Parse(txtFieldCurrencyMinimum.Text.Trim()) > number)
                {
                    message = "Minimum value cannot be greater than maximum value";
                    error = true;
                }
            }

            if (error)
            {
                MessageBox.Show(message, "Error");
                txtFieldCurrencyMaximum.Text = ((MoneyAttributeMetadata)SelectedField.AttributeMetadata).MaxValue.Value.ToString("F" + cValue.Precision.ToString());
            }
        }

        private void txtFieldCurrencyMaximum_Validated(object sender, EventArgs e)
        {
            ((MoneyAttributeMetadata)SelectedField.AttributeMetadata).MaxValue
                = double.Parse(txtFieldCurrencyMaximum.Text.Trim());
            XRMSpeedyCurrencyValue cValue = (XRMSpeedyCurrencyValue)ddlFieldCurrencyPrecision.SelectedValue;
            txtFieldCurrencyMaximum.Text = decimal.Parse(((MoneyAttributeMetadata)SelectedField.AttributeMetadata).MaxValue.ToString()).ToString("F" + cValue.Precision.ToString());
        }

        private void ddlFieldDecimalPrecision_Validated(object sender, EventArgs e)
        {
            ((DecimalAttributeMetadata)SelectedField.AttributeMetadata).Precision
                    = (int)ddlFieldDecimalPrecision.SelectedValue;
        }

        private void txtFieldDecimalMinimum_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string message = string.Empty;
            bool error = false;

            decimal number;
            bool result = decimal.TryParse(txtFieldDecimalMinimum.Text.Trim(), out number);
            if (!result)
            {
                message = "You must enter a number between " + string.Format("{0:N" + ddlFieldDecimalPrecision.SelectedValue.ToString() + "}", -100000000000) + " and " +
                    string.Format("{0:N" + ddlFieldDecimalPrecision.SelectedValue.ToString() + "}", 100000000000);
                error = true;
            }

            if (!error)
            {
                if (decimal.Parse(txtFieldDecimalMinimum.Text.Trim()) < -100000000000 || decimal.Parse(txtFieldDecimalMinimum.Text.Trim()) > 100000000000)
                {
                    message = "You must enter a number between " + string.Format("{0:N" + ddlFieldDecimalPrecision.SelectedValue.ToString() + "}", -100000000000) + " and " +
                        string.Format("{0:N" + ddlFieldDecimalPrecision.SelectedValue.ToString() + "}", 100000000000);
                    error = true;
                }

                int i = FindNumberDecimals(txtFieldDecimalMinimum.Text.Trim().ToString());
                if (i < int.Parse(ddlFieldDecimalPrecision.SelectedValue.ToString()))
                {
                    txtFieldDecimalMinimum.Text = decimal.Parse(txtFieldDecimalMinimum.Text).ToString("F" + ddlFieldDecimalPrecision.SelectedValue.ToString());
                }

                if (number > decimal.Parse(txtFieldDecimalMaximum.Text.Trim()))
                {
                    message = "Minimum value cannot be greater than maximum value";
                    error = true;
                }
            }

            if (error)
            {
                MessageBox.Show(message, "Error");
                txtFieldDecimalMinimum.Text = ((DecimalAttributeMetadata)SelectedField.AttributeMetadata).MinValue.Value.ToString("F" + ddlFieldDecimalPrecision.SelectedValue.ToString());
            }
        }

        private void txtFieldDecimalMinimum_Validated(object sender, EventArgs e)
        {
            ((DecimalAttributeMetadata)SelectedField.AttributeMetadata).MinValue
                = decimal.Parse(txtFieldDecimalMinimum.Text.Trim());
            txtFieldDecimalMinimum.Text = decimal.Parse(txtFieldDecimalMinimum.Text.Trim()).ToString("F" + ddlFieldDecimalPrecision.SelectedValue.ToString());
        }

        private void txtFieldDecimalMaximum_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string message = string.Empty;
            bool error = false;

            decimal number;
            bool result = decimal.TryParse(txtFieldDecimalMaximum.Text.Trim(), out number);
            if (!result)
            {
                message = "You must enter a number between " + string.Format("{0:N" + ddlFieldDecimalPrecision.SelectedValue.ToString() + "}", -100000000000) + " and " +
                    string.Format("{0:N" + ddlFieldDecimalPrecision.SelectedValue.ToString() + "}", 100000000000);
                error = true;
            }

            if (!error)
            {
                if (decimal.Parse(txtFieldDecimalMaximum.Text.Trim()) < -100000000000 || decimal.Parse(txtFieldDecimalMaximum.Text.Trim()) > 100000000000)
                {
                    message = "You must enter a number between " + string.Format("{0:N" + ddlFieldDecimalPrecision.SelectedValue.ToString() + "}", -100000000000) + " and " +
                        string.Format("{0:N" + ddlFieldDecimalPrecision.SelectedValue.ToString() + "}", 100000000000);
                    error = true;
                }

                int i = FindNumberDecimals(txtFieldDecimalMaximum.Text.Trim().ToString());
                if (i < int.Parse(ddlFieldDecimalPrecision.SelectedValue.ToString()))
                {
                    txtFieldDecimalMaximum.Text = decimal.Parse(txtFieldDecimalMaximum.Text).ToString("F" + ddlFieldDecimalPrecision.SelectedValue.ToString());
                }

                if (decimal.Parse(txtFieldDecimalMinimum.Text.Trim()) > number)
                {
                    message = "Minimum value cannot be greater than maximum value";
                    error = true;
                }
            }

            if (error)
            {
                MessageBox.Show(message, "Error");
                txtFieldDecimalMaximum.Text = ((DecimalAttributeMetadata)SelectedField.AttributeMetadata).MaxValue.Value.ToString("F" + ddlFieldDecimalPrecision.SelectedValue.ToString());
            }
        }

        private void txtFieldDecimalMaximum_Validated(object sender, EventArgs e)
        {
            ((DecimalAttributeMetadata)SelectedField.AttributeMetadata).MaxValue
                = decimal.Parse(txtFieldDecimalMaximum.Text.Trim());
            txtFieldDecimalMaximum.Text = decimal.Parse(txtFieldDecimalMaximum.Text.Trim()).ToString("F" + ddlFieldDecimalPrecision.SelectedValue.ToString());
        }

        private void ddlFieldWholeFormat_Validated(object sender, EventArgs e)
        {
            ((IntegerAttributeMetadata)SelectedField.AttributeMetadata).Format
                    = (IntegerFormat)ddlFieldWholeFormat.SelectedValue;
            if (((IntegerAttributeMetadata)SelectedField.AttributeMetadata).Format == IntegerFormat.None)
            {
                if (!string.IsNullOrEmpty(txtFieldWholeMinimum.Text))
                    ((IntegerAttributeMetadata)SelectedField.AttributeMetadata).MinValue = int.Parse(txtFieldWholeMinimum.Text.Trim());
                if (!string.IsNullOrEmpty(txtFieldWholeMaximum.Text))
                    ((IntegerAttributeMetadata)SelectedField.AttributeMetadata).MaxValue = int.Parse(txtFieldWholeMaximum.Text.Trim());
            }
            else
            {
                ((IntegerAttributeMetadata)SelectedField.AttributeMetadata).MinValue = null;
                ((IntegerAttributeMetadata)SelectedField.AttributeMetadata).MaxValue = null;
            }
        }

        private void txtFieldWholeMinimum_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string message = string.Empty;
            bool error = false;

            int number;
            bool result = int.TryParse(txtFieldWholeMinimum.Text.Trim(), out number);

            if (!result)
            {
                message = "You must enter a number between -2,147,483,648 and 2,147,483,647";
                error = true;
            }

            if (number < -2147483648 || number > 2147483647)
            {
                message = "You must enter a number between -2,147,483,648 and 2,147,483,647";
                error = true;
            }

            if (!error)
            {
                if (number > int.Parse(txtFieldWholeMaximum.Text.Trim()))
                {
                    message = "Minimum value cannot be greater than maximum value";
                    error = true;
                }
            }

            if (error)
            {
                MessageBox.Show(message, "Error");
                txtFieldWholeMinimum.Text = ((IntegerAttributeMetadata)SelectedField.AttributeMetadata).MinValue.ToString();
            }
        }

        private void txtFieldWholeMinimum_Validated(object sender, EventArgs e)
        {
            ((IntegerAttributeMetadata)SelectedField.AttributeMetadata).MinValue
                = int.Parse(txtFieldWholeMinimum.Text.Trim());
        }

        private void txtFieldWholeMaximum_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string message = string.Empty;
            bool error = false;

            int number;
            bool result = int.TryParse(txtFieldWholeMaximum.Text.Trim(), out number);

            if (!result)
            {
                message = "You must enter a number between -2,147,483,648 and 2,147,483,647";
                error = true;
            }

            if (number < -2147483648 || number > 2147483647)
            {
                message = "You must enter a number between -2,147,483,648 and 2,147,483,647";
                error = true;
            }

            if (!error)
            {
                if (int.Parse(txtFieldWholeMinimum.Text.Trim()) > number)
                {
                    message = "Minimum value cannot be greater than maximum value";
                    error = true;
                }
            }

            if (error)
            {
                MessageBox.Show(message, "Error");
                txtFieldWholeMaximum.Text = ((IntegerAttributeMetadata)SelectedField.AttributeMetadata).MaxValue.ToString();
            }
        }

        private void txtFieldWholeMaximum_Validated(object sender, EventArgs e)
        {
            ((IntegerAttributeMetadata)SelectedField.AttributeMetadata).MaxValue
               = int.Parse(txtFieldWholeMaximum.Text.Trim());
        }

        private void txtFieldTwoOptionNoValue_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string message = string.Empty;
            bool error = false;

            if (string.IsNullOrEmpty(txtFieldTwoOptionNoValue.Text))
            {
                message = "Enter a value for this label";
                error = true;
            }

            if (error)
            {
                MessageBox.Show(message, "Error");
                txtFieldTwoOptionNoValue.Text = ((BooleanAttributeMetadata)SelectedField.AttributeMetadata).OptionSet.FalseOption.
                    Label.LocalizedLabels.Where(l => l.LanguageCode == 1033).FirstOrDefault().Label;
            }
        }

        private void txtFieldTwoOptionNoValue_Validated(object sender, EventArgs e)
        {
            ((BooleanAttributeMetadata)SelectedField.AttributeMetadata).OptionSet.FalseOption.
                Label.LocalizedLabels.Where(l => l.LanguageCode == 1033).FirstOrDefault().Label = txtFieldTwoOptionNoValue.Text.Trim();
            SetTwoOptionsDefault();
        }

        private void txtFieldTwoOptionYesValue_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string message = string.Empty;
            bool error = false;

            if (string.IsNullOrEmpty(txtFieldTwoOptionYesValue.Text))
            {
                message = "Enter a value for this label";
                error = true;
            }

            if (error)
            {
                MessageBox.Show(message, "Error");
                txtFieldTwoOptionYesValue.Text = ((BooleanAttributeMetadata)SelectedField.AttributeMetadata).OptionSet.TrueOption.
                    Label.LocalizedLabels.Where(l => l.LanguageCode == 1033).FirstOrDefault().Label;
            }
        }

        private void txtFieldTwoOptionYesValue_Validated(object sender, EventArgs e)
        {
            ((BooleanAttributeMetadata)SelectedField.AttributeMetadata).OptionSet.TrueOption.
                Label.LocalizedLabels.Where(l => l.LanguageCode == 1033).FirstOrDefault().Label = txtFieldTwoOptionYesValue.Text.Trim();
            SetTwoOptionsDefault();
        }

        private void ddlFieldTwoOptionDefaultValue_Validated(object sender, EventArgs e)
        {
            ((BooleanAttributeMetadata)SelectedField.AttributeMetadata).DefaultValue =
                (ddlFieldTwoOptionDefaultValue.SelectedIndex == 0) ? false : true;
        }

        private void ddlFieldFloatPrecision_Validated(object sender, EventArgs e)
        {
            ((DoubleAttributeMetadata)SelectedField.AttributeMetadata).Precision
                = int.Parse(ddlFieldFloatPrecision.SelectedValue.ToString());
        }

        private void txtFieldFloatMinimum_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string message = string.Empty;
            bool error = false;

            float number;
            bool result = float.TryParse(txtFieldFloatMinimum.Text.Trim(), out number);
            if (!result)
            {
                message = "You must enter a number between " + string.Format("{0:N" + ddlFieldFloatPrecision.SelectedValue.ToString() + "}", -100000000000) + " and " +
                    string.Format("{0:N" + ddlFieldFloatPrecision.SelectedValue.ToString() + "}", 100000000000);
                error = true;
            }

            if (!error)
            {
                if (float.Parse(txtFieldFloatMinimum.Text.Trim()) < -100000000000 || float.Parse(txtFieldFloatMinimum.Text.Trim()) > 100000000000)
                {
                    message = "You must enter a number between " + string.Format("{0:N" + ddlFieldFloatPrecision.SelectedValue.ToString() + "}", -100000000000) + " and " +
                        string.Format("{0:N" + ddlFieldFloatPrecision.SelectedValue.ToString() + "}", 100000000000);
                    error = true;
                }

                int i = FindNumberDecimals(txtFieldFloatMinimum.Text.Trim().ToString());
                if (i < int.Parse(ddlFieldFloatPrecision.SelectedValue.ToString()))
                {
                    txtFieldFloatMinimum.Text = decimal.Parse(txtFieldFloatMinimum.Text).ToString("F" + ddlFieldFloatPrecision.SelectedValue.ToString());
                }

                if (number > float.Parse(txtFieldFloatMaximum.Text.Trim()))
                {
                    message = "Minimum value cannot be greater than maximum value";
                    error = true;
                }
            }

            if (error)
            {
                MessageBox.Show(message, "Error");
                txtFieldFloatMinimum.Text = ((DoubleAttributeMetadata)SelectedField.AttributeMetadata).MinValue.Value.ToString("F" + ddlFieldFloatPrecision.SelectedValue.ToString());
            }
        }

        private void txtFieldFloatMinimum_Validated(object sender, EventArgs e)
        {
            ((DoubleAttributeMetadata)SelectedField.AttributeMetadata).MinValue
                = double.Parse(txtFieldFloatMinimum.Text.Trim());
            txtFieldFloatMinimum.Text = double.Parse(txtFieldFloatMinimum.Text.Trim()).ToString("F" + ddlFieldFloatPrecision.SelectedValue.ToString());
        }

        private void txtFieldFloatMaximum_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string message = string.Empty;
            bool error = false;

            float number;
            bool result = float.TryParse(txtFieldFloatMaximum.Text.Trim(), out number);
            if (!result)
            {
                message = "You must enter a number between " + string.Format("{0:N" + ddlFieldFloatPrecision.SelectedValue.ToString() + "}", -100000000000) + " and " +
                    string.Format("{0:N" + ddlFieldFloatPrecision.SelectedValue.ToString() + "}", 100000000000);
                error = true;
            }

            if (!error)
            {
                if (float.Parse(txtFieldFloatMaximum.Text.Trim()) < -100000000000 || float.Parse(txtFieldFloatMaximum.Text.Trim()) > 100000000000)
                {
                    message = "You must enter a number between " + string.Format("{0:N" + ddlFieldFloatPrecision.SelectedValue.ToString() + "}", -100000000000) + " and " +
                        string.Format("{0:N" + ddlFieldFloatPrecision.SelectedValue.ToString() + "}", 100000000000);
                    error = true;
                }

                int i = FindNumberDecimals(txtFieldFloatMaximum.Text.Trim().ToString());
                if (i < int.Parse(ddlFieldFloatPrecision.SelectedValue.ToString()))
                {
                    txtFieldFloatMaximum.Text = decimal.Parse(txtFieldFloatMaximum.Text).ToString("F" + ddlFieldFloatPrecision.SelectedValue.ToString());
                }

                if (float.Parse(txtFieldFloatMinimum.Text.Trim()) > number)
                {
                    message = "Minimum value cannot be greater than maximum value";
                    error = true;
                }
            }

            if (error)
            {
                MessageBox.Show(message, "Error");
                txtFieldFloatMaximum.Text = ((DoubleAttributeMetadata)SelectedField.AttributeMetadata).MaxValue.Value.ToString("F" + ddlFieldFloatPrecision.SelectedValue.ToString());
            }
        }

        private void txtFieldFloatMaximum_Validated(object sender, EventArgs e)
        {
            ((DoubleAttributeMetadata)SelectedField.AttributeMetadata).MaxValue
                = double.Parse(txtFieldFloatMaximum.Text.Trim());
            txtFieldFloatMaximum.Text = double.Parse(txtFieldFloatMaximum.Text.Trim()).ToString("F" + ddlFieldFloatPrecision.SelectedValue.ToString());
        }

        private void chkShortPrimaryAttributeName_CheckedChanged(object sender, EventArgs e)
        {
            SelectedEntity.ShortPrimaryAttributeName
                        = chkShortPrimaryAttributeName.Checked;
        }

        #endregion

        #region Relationships

        private void btnRestart_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void ResetRelationshipFields()
        {
            ddlRelationshipType.SelectedIndex = -1;
            ddlRelationshipPrimary.Enabled = false;
            ddlRelationshipPrimary.SelectedIndex = -1;
            ddlRelationshipSecondary.Enabled = false;
            ddlRelationshipSecondary.SelectedIndex = -1;
            btnRelationshipAdd.Enabled = false;
            chkOmitPrefixForRelationships.Enabled = true;
        }

        private void ddlRelationshipType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRelationshipType.SelectedIndex < 1)
            {
                ResetRelationshipFields();
                return;
            }

            ddlRelationshipPrimary.Enabled = true;
            ddlRelationshipSecondary.Enabled = true;

            BindRelationshipLists();

            SetRelationshipAddButtonState();
        }

        private void BindRelationshipLists()
        {
            RelationshipPrimaryEntities = new SortedDictionary<string, string>();
            RelationshipRelatedEntities = new SortedDictionary<string, string>();
            ddlRelationshipPrimary.DataSource = null;
            ddlRelationshipSecondary.DataSource = null;

            RelationshipPrimaryEntities.Add(string.Empty, string.Empty);
            RelationshipRelatedEntities.Add(string.Empty, string.Empty);

            if (ddlRelationshipType.SelectedItem.ToString() == "1:N Relationship")
            {
                foreach (EntityMetadata ent in ExistingEntityMetadata.Where(ent => ent.CanBePrimaryEntityInRelationship.Value == true))
                {
                    RelationshipPrimaryEntities.Add(GetEntityDisplayName(ent.LogicalName), ent.LogicalName);
                }

                foreach (EntityMetadata ent in ExistingEntityMetadata.Where(ent => ent.CanBeRelatedEntityInRelationship.Value == true))
                {
                    RelationshipRelatedEntities.Add(GetEntityDisplayName(ent.LogicalName), ent.LogicalName);
                }

                foreach (XRMSpeedyEntity ent in NewEntities)
                {
                    if (ent.Import)
                    {
                        RelationshipPrimaryEntities.Add(ent.EntityMetadata.DisplayName.LocalizedLabels.Where(l => l.LanguageCode == 1033).FirstOrDefault().Label,
                            ent.EntityMetadata.SchemaName);

                        RelationshipRelatedEntities.Add(ent.EntityMetadata.DisplayName.LocalizedLabels.Where(l => l.LanguageCode == 1033).FirstOrDefault().Label,
                            ent.EntityMetadata.SchemaName);
                    }
                }

                ddlRelationshipPrimary.DataSource = RelationshipPrimaryEntities.ToList();
                ddlRelationshipSecondary.DataSource = RelationshipRelatedEntities.ToList();
            }

            if (ddlRelationshipType.SelectedItem.ToString() == "N:N Relationship")
            {
                foreach (EntityMetadata ent in ExistingEntityMetadata.Where(ent => ent.CanBeInManyToMany.Value == true))
                {
                    RelationshipPrimaryEntities.Add(GetEntityDisplayName(ent.LogicalName), ent.LogicalName);
                }

                foreach (XRMSpeedyEntity ent in NewEntities)
                {
                    if (ent.Import)
                    {
                        RelationshipPrimaryEntities.Add(ent.EntityMetadata.DisplayName.LocalizedLabels.Where(l => l.LanguageCode == 1033).FirstOrDefault().Label,
                            ent.EntityMetadata.SchemaName);
                    }
                }

                ddlRelationshipPrimary.DataSource = RelationshipPrimaryEntities.ToList();
                ddlRelationshipSecondary.DataSource = RelationshipPrimaryEntities.ToList();
            }

            ddlRelationshipPrimary.DisplayMember = "Key";
            ddlRelationshipPrimary.ValueMember = "Value";
            ddlRelationshipPrimary.SelectedIndex = -1;
            ddlRelationshipSecondary.DisplayMember = "Key";
            ddlRelationshipSecondary.ValueMember = "Value";
            ddlRelationshipSecondary.SelectedIndex = -1;
        }

        private void SetRelationshipAddButtonState()
        {
            if (ddlRelationshipType.SelectedIndex > 0 && ddlRelationshipPrimary.SelectedIndex > 0 && ddlRelationshipSecondary.SelectedIndex > 0)
                btnRelationshipAdd.Enabled = true;
            else
                btnRelationshipAdd.Enabled = false;
        }

        private void ddlRelationshipPrimary_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SetRelationshipAddButtonState();
        }

        private void ddlRelationshipSecondary_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SetRelationshipAddButtonState();
        }

        private void lstRelationships_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstRelationships.SelectedIndex != -1)
                btnRelationshipRemove.Enabled = true;
            else
                btnRelationshipRemove.Enabled = false;
        }

        private void btnRelationshipAdd_Click(object sender, EventArgs e)
        {
            CreateRelationship(ddlRelationshipPrimary.SelectedValue.ToString(),
                ddlRelationshipSecondary.SelectedValue.ToString(), GetEntityDisplayName(ddlRelationshipPrimary.SelectedValue.ToString()),
                GetEntityDisplayName(ddlRelationshipSecondary.SelectedValue.ToString()), ddlRelationshipType.SelectedItem.ToString(),
                CreateRelationshipSchemaName(ddlRelationshipPrimary.SelectedValue.ToString(), ddlRelationshipSecondary.SelectedValue.ToString()));

            ddlRelationshipType.SelectedIndex = -1;
            ddlRelationshipPrimary.SelectedIndex = -1;
            ddlRelationshipSecondary.SelectedIndex = -1;
            btnRelationshipAdd.Enabled = false;
        }

        private void CreateRelationship(string entity1, string entity2, string entity1Display, string entity2Display, string relationshipType, string schemaName)
        {
            XRMSpeedyRelationship relationship = new XRMSpeedyRelationship(entity1, entity2, entity1Display, entity2Display, relationshipType, schemaName, txtPrefix.Text.Trim().ToLower());

            NewRelationships.Add(relationship);
            BindRelationshipList();

            foreach (XRMSpeedyEntity entity in NewEntities)
            {
                if (entity.EntityMetadata.SchemaName == relationship.Entity1 || entity.EntityMetadata.SchemaName == relationship.Entity2)
                {
                    int x = clbTables.FindString(entity.OriginalTable);
                    clbTables.Items[x] = entity.OriginalTable + " [In Relationship]";

                    entity.Import = true;
                }
            }

            for (int i = 0; i < clbTables.Items.Count; i++)
            {
                if (clbTables.Items[i].ToString().Contains("[In Relationship]"))
                {
                    clbTables.SetItemChecked(i, true);
                }
            }
        }

        private string CreateRelationshipSchemaName(string entity1, string entity2)
        {
            return chkOmitPrefixForRelationships.Checked
              ? string.Concat(entity1, "_", entity2)
              : string.Concat(txtPrefix.Text.Trim(), "_", entity1, "_", entity2);
        }

        private string GetEntityDisplayName(string logicalName)
        {
            string name = string.Empty;
            EntityMetadata em = ExistingEntityMetadata.Where(e => e.LogicalName == logicalName).FirstOrDefault();
            if (em != null)
            {
                LocalizedLabel label = em.DisplayName.LocalizedLabels.Where(l => l.LanguageCode == 1033).FirstOrDefault();
                if (label != null)
                    name = label.Label;
            }

            if (name == string.Empty)
            {
                name = NewEntities.Where(e => e.EntityMetadata.SchemaName == logicalName).FirstOrDefault().EntityMetadata.DisplayName
                    .LocalizedLabels.Where(l => l.LanguageCode == 1033).FirstOrDefault().Label;
            }

            return name;
        }

        private void btnRelationshipRemove_Click(object sender, EventArgs e)
        {
            string item = lstRelationships.SelectedItem.ToString();
            string[] itemArr = item.Split('|');
            XRMSpeedyRelationship relationship = NewRelationships.Where(r => r.Entity1Display == itemArr[0].Trim() &&
                                                          r.Entity2Display == itemArr[2].Trim() &&
                                                          r.RelationshipType == itemArr[1].Trim()).FirstOrDefault();

            NewRelationships.Remove(relationship);

            BindRelationshipList();
            btnRelationshipRemove.Enabled = false;

            foreach (XRMSpeedyEntity entity in NewEntities.Where(ent => ent.Import == true))
            {
                int g = NewRelationships.Where(r => r.Entity1 == entity.EntityMetadata.SchemaName || r.Entity2 == entity.EntityMetadata.SchemaName).Count();
                if (g == 0)
                {
                    if (entity.EntityMetadata.SchemaName == relationship.Entity1 || entity.EntityMetadata.SchemaName == relationship.Entity2)
                    {
                        int x = clbTables.FindString(entity.OriginalTable + " [In Relationship]");
                        clbTables.Items[x] = entity.OriginalTable;
                    }
                }
            }
        }

        private void BindRelationshipList()
        {
            lstRelationships.Items.Clear();
            foreach (XRMSpeedyRelationship r in NewRelationships)
            {
                lstRelationships.Items.Add(string.Concat(r.Entity1Display, " | ", r.RelationshipType, " | ", r.Entity2Display));
            }
        }

        #endregion
    }
}
