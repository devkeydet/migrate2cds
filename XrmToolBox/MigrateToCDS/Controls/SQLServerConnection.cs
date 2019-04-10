using System;
using System.Windows.Forms;

namespace CDSTools.UserControls
{
    public partial class SQLServerConnection : UserControl, IProviderControl
    {

        public event EventHandler ProviderChanged;
        public IDbProvider Provider { get; private set; }
        public string ConnectionString { get; set; }
        public SQLServerConnection()
        {
            InitializeComponent();
        }

        private void chkConnectionIntegrated_CheckedChanged(object sender, EventArgs e)
        {
            var isChecked = chkConnectionIntegrated.Checked;
            if (isChecked)
            {
                txtConnectionUsername.Clear();
                txtConnectionPassword.Clear();
            }

            txtConnectionUsername.Enabled = isChecked;
            txtConnectionPassword.Enabled = isChecked;

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

            if (string.IsNullOrEmpty(txtConnectionServer.Text))
            {
                errorMgr.SetError(txtConnectionServer, "Please enter a server name");
                hasError = true;
            }
            if (string.IsNullOrEmpty(txtConnectionDatabase.Text))
            {
                errorMgr.SetError(txtConnectionDatabase, "Please enter a database name");
                hasError = true;
            }
            if (!chkConnectionIntegrated.Checked)
            {
                if (string.IsNullOrEmpty(txtConnectionUsername.Text))
                {
                    errorMgr.SetError(txtConnectionUsername, "Please enter a username");
                    hasError = true;
                }
            }

            if (!hasError)
            {
                string conn = $"Data Source={txtConnectionServer.Text.Trim()};Initial Catalog={txtConnectionDatabase.Text.Trim()}";
                if (!chkConnectionIntegrated.Checked)
                {
                    conn += $";Persist Security Info=True;User ID={txtConnectionUsername.Text.Trim()};Password={txtConnectionPassword.Text.Trim()}";
                }
                else
                {
                    conn += $";Integrated Security=SSPI;";
                }
                ConnectionString = conn;
            }

            ProviderChanged.Invoke(this, new EventArgs());
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
                return new SQLDBProvider(ConnectionString);
            }
        }

        private void SQLServerConnect_EnabledChanged(object sender, EventArgs e)
        {
            chkConnectionIntegrated.Enabled =
            txtConnectionDatabase.Enabled =
            txtConnectionServer.Enabled =
            txtConnectionUsername.Enabled =
            txtConnectionPassword.Enabled = Enabled;

            var isChecked = chkConnectionIntegrated.Checked;
            txtConnectionUsername.Enabled = (Enabled) ? isChecked: Enabled;
            txtConnectionPassword.Enabled = (Enabled) ? isChecked : Enabled;
        }
    }
}
