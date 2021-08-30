using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;

namespace Sky_multi_Uninstaller
{
    internal sealed partial class Form1 : Form
    {
        internal Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegistryKey key;
            if (Environment.Is64BitProcess)
            {
                key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Sky note", false); // x64
            }
            else
            {
                key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Sky note", false); // x86
            }

            foreach (string i in Directory.EnumerateFileSystemEntries(key.GetValue("InstallLocation").ToString())) 
            {
                if (File.Exists(i) && i != Application.ExecutablePath)
                {
                    File.Delete(i);
                }
                else if (Directory.Exists(i))
                {
                    Directory.Delete(i, true);
                }
            }

            key.Close();
            key.Dispose();
            if (Environment.Is64BitProcess)
            {
                key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", true); // x64
            }
            else
            {
                key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall", true); // x86
            }
            key.DeleteSubKey("Sky note");

            key.Close();
            key.Dispose();
            key = Registry.ClassesRoot;
            key.DeleteSubKeyTree("Sky note.txt");
            key.OpenSubKey(".txt", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky note.log");
            key.OpenSubKey(".log", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky note.ini");
            key.OpenSubKey(".ini", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky note.bat");
            key.OpenSubKey(".bat", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky note.html");
            key.OpenSubKey(".html", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky note.css");
            key.OpenSubKey(".css", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky note.xml");
            key.OpenSubKey(".xml", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky note.tag");
            key.OpenSubKey(".tag", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky note.prx");
            key.OpenSubKey(".prx", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky note.bin");
            key.OpenSubKey(".bin", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky note.old");
            key.OpenSubKey(".old", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky note.psd1");
            key.OpenSubKey(".psd1", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky note.ps1");
            key.OpenSubKey(".ps1", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky note.psm1");
            key.OpenSubKey(".psm1", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky note.plist");
            key.OpenSubKey(".plist", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky note.yml");
            key.OpenSubKey(".yml", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky note.asar");
            key.OpenSubKey(".asar", true).SetValue(string.Empty, string.Empty);            

            if (File.Exists(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Sky note.lnk")))
            {
                File.Delete(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Sky note.lnk"));
            }

            if (File.Exists(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Sky note.lnk")))
            {
                File.Delete(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Sky note.lnk"));
            }

            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Sky note"))
            {
                Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Sky note", true);
            }

            Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
