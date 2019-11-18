using CDSTools.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CDSTools
{
    public partial class Connect : Form
    {
        public IDbProvider Provider { get; set; }

        public Connect()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            var providerType = comboDSType.SelectedItem?.ToString();
            var ctl = Controls[providerType];

            if (providerType != null)
            {
                Provider = ((IProviderControl)ctl).GetProvider();
            }

            if (Provider != null) {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Provider = null;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Connect_ProviderChanged(object sender, EventArgs e)
        {
            Provider = ((IProviderControl)sender).GetProvider();

            buttonOK.Enabled = (Provider != null);
        }

        private void ComboDSType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var providerType = comboDSType.SelectedItem?.ToString();

            if (providerType != null)
            {
                var ctl = this.Controls[providerType];
                ctl.BringToFront();
                ctl.Visible = true;
                ctl.Enabled = true;
            }
        }

        private void Connect_Load(object sender, EventArgs e)
        {
            // set up the controls for the DB provider types
            // TODO - better way to configure this.  if we add more providers, we are hosed

            SQLServerConnect.Name = SQLDBProvider.Name;
            SQLServerConnect.Enabled = false;

            AccessConnect.Name = AccessDBProvider.Name;
            AccessConnect.Enabled = false;

            comboDSType.Items.Clear();
            comboDSType.Items.AddRange( new string[] { AccessDBProvider.Name, SQLDBProvider.Name });

            comboDSType.SelectedIndex = 0;
        }

        private void GetProvider() {

            var providerType = comboDSType.SelectedItem?.ToString();
            var ctl = Controls[providerType];

            if (providerType != null)
            {
                Provider = ((IProviderControl)ctl).GetProvider();
            }
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            GetProvider();

            Provider.TestConnect();

        }
    }
}
