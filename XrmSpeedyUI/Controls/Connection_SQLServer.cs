using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XrmSpeedyUI;

namespace XrmSpeedy.Controls
{
    public partial class Connection_SQLServer : UserControl
    {
        public Connection_SQLServer()
        {
            InitializeComponent();
        }

        private void chkConnectionIntegrated_CheckedChanged(object sender, EventArgs e)
        {
            if (chkConnectionIntegrated.Checked)
            {
                txtConnectionUsername.Clear();
                txtConnectionUsername.Enabled = false;
                txtConnectionPassword.Clear();
                txtConnectionPassword.Enabled = false;
            }
            else
            {
                txtConnectionUsername.Enabled = true;
                txtConnectionPassword.Enabled = true;
            }

            SetConnectionString();
        }

        private void SetConnectionString()
        {
            string conn = string.Empty;
            if (!chkConnectionIntegrated.Checked)
                conn = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}",
                    txtConnectionServer.Text.Trim(), txtConnectionDatabase.Text.Trim(), txtConnectionUsername.Text.Trim(), txtConnectionPassword.Text.Trim());
            else
                conn = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=SSPI;", txtConnectionServer.Text.Trim(),
                    txtConnectionDatabase.Text.Trim());

            frmMain parent = (frmMain)this.Parent.Parent;
            parent.ConnectionString = conn;
        }

        private void txtConnectionServer_Validated(object sender, EventArgs e)
        {
            SetConnectionString();
        }

        private void txtConnectionDatabase_Validated(object sender, EventArgs e)
        {
            SetConnectionString();
        }

        private void txtConnectionUsername_Validated(object sender, EventArgs e)
        {
            SetConnectionString();
        }

        private void txtConnectionPassword_Validated(object sender, EventArgs e)
        {
            SetConnectionString();
        }
    }
}
