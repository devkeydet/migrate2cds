using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XrmSpeedyUI
{
    public partial class Browser : Form
    {
        public Browser(string url, string title)
        {
            InitializeComponent();
            this.Text += title;
            webBrowser1.Url = new Uri(url);
        }
    }
}
