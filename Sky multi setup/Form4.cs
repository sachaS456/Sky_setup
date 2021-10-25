/*--------------------------------------------------------------------------------------------------------------------
 Copyright (C) 2021 Himber Sacha

 This program is free software: you can redistribute it and/or modify
 it under the +terms of the GNU General Public License as published by
 the Free Software Foundation, either version 2 of the License, or
 any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with this program.  If not, see https://www.gnu.org/licenses/gpl-2.0.html. 

--------------------------------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sky_multi_setup
{
    internal sealed partial class Form4 : Form
    {
        internal string Path;

        internal Form4()
        {
            InitializeComponent();

            if (Environment.Is64BitProcess == true)
            {
                label2.Text += Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\Sky multi";
                Path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\Sky multi";
            }
            else
            {
                label2.Text += Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + @"\Sky multi";
                Path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + @"\Sky multi";
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
                    Path = dialog.FileName + @"\Sky multi";
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
