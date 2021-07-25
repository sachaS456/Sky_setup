using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Sky_multi_setup
{
    internal sealed partial class Form1 : Form
    {
        internal Form1()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new Form2().ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e) // button refuse
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e) // button accept
        {
            this.DialogResult = DialogResult.OK;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process process = new Process();
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.FileName = "https://www.gnu.org/licenses/old-licenses/gpl-2.0.html#SEC1";
            process.Start();
            process.Close();
            process = null;
        }
    }
}
