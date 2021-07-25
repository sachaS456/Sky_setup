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
