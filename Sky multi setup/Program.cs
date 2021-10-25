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
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Sky_multi_setup
{
    public static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        public static int Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //MessageBox.Show("Unvailable for the moment!", "Sky multi Setup", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //return 0;

            if (new Form1().ShowDialog() == DialogResult.OK)
            {
                if (Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Sky multi", false) == null &&
                    Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Sky multi", false) == null)
                {
                    Form4 InstallationSettingsDialog = new Form4();

                    if (InstallationSettingsDialog.ShowDialog() == DialogResult.OK)
                    {
                        new Form3(InstallationSettingsDialog.Path, InstallationSettingsDialog.IconDesktop, InstallationSettingsDialog.IconStartMenu).ShowDialog();
                    }
                }
                else
                {
                    string InstallationPath;

                    if (Environment.Is64BitProcess)
                    {
                        InstallationPath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Sky multi", false).GetValue("InstallLocation").ToString();
                    }
                    else
                    {
                        InstallationPath = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Sky multi", false).GetValue("InstallLocation").ToString();
                    }

                    new Form3(InstallationPath, false, false).ShowDialog();
                }
            }
                
            return 0;
        }
    }
}
