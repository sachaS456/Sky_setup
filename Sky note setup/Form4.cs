using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sky_note_setup
{
    internal sealed partial class Form4 : Form
    {
        internal string Path;

        internal Form4()
        {
            InitializeComponent();

            if (Environment.Is64BitProcess == true)
            {
                label2.Text += Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\Sky note";
                Path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\Sky note";
            }
            else
            {
                label2.Text += Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + @"\Sky note";
                Path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + @"\Sky note";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (FolderSelectDialog dialog = new FolderSelectDialog())
            {
                if (dialog.Show() == true)
                {
                    Path = dialog.FileName + @"\Sky note";
                    label2.Text = "File path : " + Path;
                    dialog.Dispose();
                }
            }
        }

        internal bool IconDesktop
        {
            get
            {
                return checkBox1.Checked;
            }
        }

        internal bool IconStartMenu
        {
            get
            {
                return checkBox2.Checked;
            }
        }
    }
}
