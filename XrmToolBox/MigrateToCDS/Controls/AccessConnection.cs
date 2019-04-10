using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace CDSTools.UserControls
{
    public partial class AccessConnection : UserControl, IProviderControl
    {
        public event EventHandler ProviderChanged;

        public string ConnectionString { get; set;  }

        public AccessConnection()
        {
            InitializeComponent();
        }

        private void chkPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkPassword.Checked)
            {
                errorMgr.SetError(txtPassword, null);
                txtPassword.Clear();
                txtPassword.Enabled = false;
            }
            else
            {
                txtPassword.Enabled = true;
            }

            SetConnStringAndProvider();
        }

        /// <summary>
        /// Set up the connection string and provider based on the current selections 
        /// </summary>
        private void SetConnStringAndProvider()
        {
            ConnectionString = null;
            errorMgr.Clear();

            bool hasError = false;
            if (string.IsNullOrEmpty(txtLocation.Text))
            {
                errorMgr.SetError(txtPassword, "Please select an Acess Database file");
                hasError = true;
            }
            if (chkPassword.Checked)
            {
                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    errorMgr.SetError(txtPassword, "Please enter a Password");
                    hasError = true;
                }
            }

            if (!hasError) {

                string conn = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={txtLocation.Text.Trim()}";

                if (chkPassword.Checked)
                {
                    conn += $";Jet OLEDB:Database Password={txtPassword.Text.Trim()};";
                }
                else
                {
                    conn += $";Persist Security Info=False;";
                }

                ConnectionString = conn;
            }

            ProviderChanged.Invoke(this, new EventArgs());
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDlg.Filter = "Access 2007-2019 (.accdb)|*.accdb|Access 2000-2003(.mdb)|*.mdb";
            DialogResult result = openFileDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtLocation.Text = openFileDlg.FileName;
                SetConnStringAndProvider();
            }
        }

        private void lblRuntime_Click(object sender, EventArgs e)
        {
            var sInfo = new ProcessStartInfo("https://www.microsoft.com/en-us/download/details.aspx?id=13255&displaylang=en");
            Process.Start(sInfo);
        }

        public string GetConnectionString()
        {
            return ConnectionString;
        }

        public IDbProvider GetProvider()
        {
            if (ConnectionString == null)
            {
                return null;
            }
            else
            {
                return new AccessDBProvider(ConnectionString);
            }
        }

        private void AccessConnection_EnabledChanged(object sender, EventArgs e)
        {
            chkPassword.Enabled =
            btnBrowse.Enabled = 
            txtLocation.Enabled = Enabled;

            txtPassword.Enabled = (Enabled) ? chkPassword.Checked : Enabled;
            errorMgr.SetError(txtPassword, null);
        }
    }
}
