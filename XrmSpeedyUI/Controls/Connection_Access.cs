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
    public partial class Connection_Access : UserControl
    {
        public Connection_Access()
        {
            InitializeComponent();
        }

        private void chkPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkPassword.Checked)
            {
                txtConnectionPassword.Clear();
                txtConnectionPassword.Enabled = false;
            }
            else
            {
                txtConnectionPassword.Enabled = true;
            }

            SetConnectionString();
        }

        private void SetConnectionString()
        {
            string conn = string.Empty;
            if (!chkPassword.Checked)
                conn = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Jet OLEDB:Database Password={1};",
                    txtLocation.Text.Trim(), txtConnectionPassword.Text.Trim());
            else
                conn = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Persist Security Info=False;",
                    txtLocation.Text.Trim());

            frmMain parent = (frmMain)this.Parent.Parent;
            parent.ConnectionString = conn;
        }

        private void txtLocationServer_Validated(object sender, EventArgs e)
        {
            SetConnectionString();
        }

        private void txtConnectionPassword_Validated(object sender, EventArgs e)
        {
            SetConnectionString();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Access 2007-2013 (.accdb)|*.accdb|Access 2000-2003(.mdb)|*.mdb";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtLocation.Text = openFileDialog1.FileName;
                SetConnectionString();
            }
        }

        private void lblRuntime_Click(object sender, EventArgs e)
        {
            //Access 2007 Download: Access Runtime
            Browser b = new Browser("http://www.microsoft.com/en-us/download/details.aspx?displaylang=en&id=4438",
                "Access 2007 Download: Access Runtime");
            b.Show();
        }
    }
}
