using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AQWE.Core;

namespace AQWE
{
    public partial class frmSplash : Form
    {
        public frmSplash()
        {
            InitializeComponent();
        }

        private void tmrLoad_Tick(object sender, EventArgs e)
        {
            tmrLoad.Enabled = false;

            Logging.mainForm = new frmMain();
            Logging.mainForm.Show();

            this.Hide();
        }
    }
}
