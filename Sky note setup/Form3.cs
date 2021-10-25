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
using System.IO;
using System.IO.Compression;
using Microsoft.Win32;
using IWshRuntimeLibrary;
using System.Threading;

namespace Sky_note_setup
{
    internal sealed partial class Form3 : Form
    {
        private string Path;
        private bool IconDesktop;
        private bool IconStartMenu;

        private delegate void SetTextLabel(string Text);
        private delegate void SetValueProgressBar(int value);

        internal Form3(string Path, bool IconDesktop, bool IconStartMenu)
        {
            InitializeComponent();
            button1.Enabled = false;

            this.Path = Path;
            this.IconDesktop = IconDesktop;
            this.IconStartMenu = IconStartMenu;

            Thread thread = new Thread(Installation);
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
        }

        private void SetTextLabel2(string Text)
        {
            label2.Text = Text;
        }

        private void SetTextLabel2_Methode(string Text)
        {
            if (label2.InvokeRequired)
            {
                label2.Invoke(new SetTextLabel(SetTextLabel2), Text);
            }
            else
            {
                SetTextLabel2(Text);
            }
        }

        private void SetValueProgressBar1(int value)
        {
            progressBar1.Value = value;
        }

        private void SetValueProgressBar_Methode(int value)
        {
            if (progressBar1.InvokeRequired)
            {
                progressBar1.Invoke(new SetValueProgressBar(SetValueProgressBar1), value);
            }
            else
            {
                SetValueProgressBar1(value);
            }
        }

        public static MemoryStream GetMemoryStream(ComponentResourceManager resourceManager, String name)
        {
            object resource = resourceManager.GetObject(name);

            if (resource is byte[])
            {
                return new MemoryStream((byte[])resource);
            }
            else
            {
                throw new System.InvalidCastException("The specified resource is not a binary resource.");
            }
        }

        private void Installation()
        {
            Thread.Sleep(1000);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileApp));
            SetValueProgressBar_Methode(1);
            SetTextLabel2_Methode("1%");

            if (Directory.Exists(Path))
            {
                try
                {
                    Directory.Delete(Path, true);
                }
                catch
                {
                    MessageBox.Show("An error occured!");
                }
            }

            Directory.CreateDirectory(Path);
            SetValueProgressBar_Methode(7);
            SetTextLabel2_Methode("7%");

            using (FileStream fileStream = new FileStream(Path + @"\SN.zip", FileMode.Create, FileAccess.Write))
            {
                if (Environment.Is64BitProcess == true)
                {
                    //resources.GetStream("x64").CopyTo(fileStream);
                    GetMemoryStream(resources, "x64").CopyTo(fileStream);
                }
                else
                {
                    //resources.GetStream("x86").CopyTo(fileStream);
                    GetMemoryStream(resources, "x86").CopyTo(fileStream);
                }

                fileStream.Close();
            }

            SetValueProgressBar_Methode(40);
            SetTextLabel2_Methode("40%");

            ZipFile.ExtractToDirectory(Path + @"\SN.zip", Path);
            SetValueProgressBar_Methode(80);
            SetTextLabel2_Methode("80%");

            System.IO.File.Delete(Path + @"\SN.zip");
            SetValueProgressBar_Methode(85);
            SetTextLabel2_Methode("85%");

            SetFile(Path + @"\", "Sky note");
            SetValueProgressBar_Methode(90);
            SetTextLabel2_Methode("90%");

            RegistryKey classesKey;

            if (Environment.Is64BitProcess == true)
            {
                classesKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\", true);
                classesKey.CreateSubKey("Sky note").SetValue("DisplayName", "Sky note 3.0.0 (x64)");
                classesKey.CreateSubKey("Sky note").SetValue("DisplayVersion", "3.0.0");
                classesKey.CreateSubKey("Sky note").SetValue("DisplayIcon", '"' + Path + @"\Sky note.exe" + '"' + ",0");
                classesKey.CreateSubKey("Sky note").SetValue("HelpLink", "https://serie-sky.netlify.app");
                classesKey.CreateSubKey("Sky note").SetValue("URLUpdateInfo", "https://serie-sky.netlify.app");
                classesKey.CreateSubKey("Sky note").SetValue("URLInfoAbout", "https://serie-sky.netlify.app");
                classesKey.CreateSubKey("Sky note").SetValue("UninstallString", Path + @"\Sky note Uninstaller.exe");
                classesKey.CreateSubKey("Sky note").SetValue("Publisher", "Himber Sacha");
                classesKey.CreateSubKey("Sky note").SetValue("InstallLocation", Path);
                classesKey.CreateSubKey("Sky note").SetValue("EstimatedSize", 150000, RegistryValueKind.DWord);
            }
            else
            {
                classesKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall", true);
                classesKey.CreateSubKey("Sky note").SetValue("DisplayName", "Sky note 3.0.0 (x86)");
                classesKey.CreateSubKey("Sky note").SetValue("DisplayVersion", "3.0.0");
                classesKey.CreateSubKey("Sky note").SetValue("DisplayIcon", '"' + Path + @"\Sky note.exe" + '"' + ",0");
                classesKey.CreateSubKey("Sky note").SetValue("HelpLink", "https://serie-sky.netlify.app");
                classesKey.CreateSubKey("Sky note").SetValue("URLUpdateInfo", "https://serie-sky.netlify.app");
                classesKey.CreateSubKey("Sky note").SetValue("URLInfoAbout", "https://serie-sky.netlify.app");
                classesKey.CreateSubKey("Sky note").SetValue("UninstallString", Path + @"\Sky note Uninstaller.exe");
                classesKey.CreateSubKey("Sky note").SetValue("Publisher", "Himber Sacha");
                classesKey.CreateSubKey("Sky note").SetValue("InstallLocation", Path);
                classesKey.CreateSubKey("Sky note").SetValue("EstimatedSize", 138000, RegistryValueKind.DWord);
            }


            classesKey.Close();
            classesKey = Registry.ClassesRoot.OpenSubKey(".txt", true);
            classesKey.CreateSubKey("ShellNew").SetValue("ItemName", Path + @"\Sky note.exe");
            classesKey.CreateSubKey("ShellNew").SetValue("NullFile", string.Empty);
            classesKey.CreateSubKey(@"Sky note\command").SetValue(string.Empty, Path + @"\Sky note.exe");
            classesKey.Close();

            if (IconDesktop)
            {
                WshShell shell = new WshShell();
                IWshShortcut link = (IWshShortcut)shell.CreateShortcut(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Sky note.lnk"));
                link.TargetPath = Path + @"\Sky note.exe";
                //link.IconLocation = '"' + Path + @"\Sky multi.exe" + '"' + ",0";
                link.Save();
            }

            if (IconStartMenu)
            {
                WshShell shell = new WshShell();
                IWshShortcut link = (IWshShortcut)shell.CreateShortcut(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Sky note.lnk"));
                link.TargetPath = Path + @"\Sky note.exe";
                //link.IconLocation = '"' + Path + @"\Sky multi.exe" + '"' + ",0";
                link.Save();
            }

            SetValueProgressBar_Methode(100);
            SetTextLabel2_Methode("100%");
            button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void SetFile(string Emplacement, string App)
        {
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".txt", App + ".txt", "Text file");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".log", App + ".log", "Log file");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".ini", App + ".ini", "File config");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".bat", App + ".bat", "Command file");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".html", App + ".html", "Html file");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".css", App + ".css", "css file");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".xml", App + ".xml", "xml file");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".tag", App + ".tag", "tag file");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".prx", App + ".prx", "prx file");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".bin", App + ".bin", "bin file");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".old", App + ".old", "old file");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".psd1", App + ".psd1", "data file");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".ps1", App + ".ps1", "Script file");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".psm1", App + ".psm1", "Script Module File ");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".plist", App + ".plist", "plist file");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".yml", App + ".yml", "yml file");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".asar", App + ".asar", "asar file");
        }

        private void SetFileAssociation(string icon, string application, string extension, string progId, string description)
        {
            RegistryKey classesKey = Registry.CurrentUser.OpenSubKey(@"Software\Classes", true);
            classesKey.CreateSubKey(extension).SetValue(string.Empty, progId);
            RegistryKey progKey = classesKey.CreateSubKey(progId);

            if (description != null && description != string.Empty)
            {
                progKey.SetValue(string.Empty, description);
            }

            if (icon != null && icon != string.Empty)
            {
                progKey.CreateSubKey("DefaultIcon").SetValue(string.Empty, icon);
            }

            progKey.CreateSubKey(@"Shell\Open\Command").SetValue(string.Empty, '"' + application + ".exe" + '"' + " " + '"' + "%1" + '"');
        }
    }
}
