/*--------------------------------------------------------------------------------------------------------------------
 Copyright (C) 2022 Himber Sacha

 This program is free software: you can redistribute it and/or modify
 it under the +terms of the GNU General Public License as published by
 the Free Software Foundation, either version 3 of the License, or
 any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with this program.  If not, see https://www.gnu.org/licenses/gpl-3.0.html. 

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

namespace Sky_multi_setup
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

            using (FileStream fileStream = new FileStream(Path + @"\SM.zip", FileMode.Create, FileAccess.Write))
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

            ZipFile.ExtractToDirectory(Path + @"\SM.zip", Path);
            SetValueProgressBar_Methode(80);
            SetTextLabel2_Methode("80%");

            System.IO.File.Delete(Path + @"\SM.zip");
            SetValueProgressBar_Methode(85);
            SetTextLabel2_Methode("85%");

            SetFile(Path + @"\", "Sky multi");
            SetValueProgressBar_Methode(90);
            SetTextLabel2_Methode("90%");

            RegistryKey classesKey;

            if (Environment.Is64BitProcess == true)
            {
                classesKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\", true);
                classesKey.CreateSubKey("Sky multi").SetValue("DisplayName", "Sky multi 3.1.1 (x64)");
                classesKey.CreateSubKey("Sky multi").SetValue("DisplayVersion", "3.1.1");
                classesKey.CreateSubKey("Sky multi").SetValue("DisplayIcon", '"' + Path + @"\Sky multi.exe" + '"' + ",0");
                classesKey.CreateSubKey("Sky multi").SetValue("HelpLink", "https://serie-sky.netlify.app");
                classesKey.CreateSubKey("Sky multi").SetValue("URLUpdateInfo", "https://serie-sky.netlify.app");
                classesKey.CreateSubKey("Sky multi").SetValue("URLInfoAbout", "https://serie-sky.netlify.app");
                classesKey.CreateSubKey("Sky multi").SetValue("UninstallString", Path + @"\Sky multi Uninstaller.exe");
                classesKey.CreateSubKey("Sky multi").SetValue("Publisher", "Himber Sacha");
                classesKey.CreateSubKey("Sky multi").SetValue("InstallLocation", Path);
                classesKey.CreateSubKey("Sky multi").SetValue("EstimatedSize", 457000, RegistryValueKind.DWord);
            }
            else
            {
                classesKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall", true);
                classesKey.CreateSubKey("Sky multi").SetValue("DisplayName", "Sky multi 3.1.1 (x86)");
                classesKey.CreateSubKey("Sky multi").SetValue("DisplayVersion", "3.1.1");
                classesKey.CreateSubKey("Sky multi").SetValue("DisplayIcon", '"' + Path + @"\Sky multi.exe" + '"' + ",0");
                classesKey.CreateSubKey("Sky multi").SetValue("HelpLink", "https://serie-sky.netlify.app");
                classesKey.CreateSubKey("Sky multi").SetValue("URLUpdateInfo", "https://serie-sky.netlify.app");
                classesKey.CreateSubKey("Sky multi").SetValue("URLInfoAbout", "https://serie-sky.netlify.app");
                classesKey.CreateSubKey("Sky multi").SetValue("UninstallString", Path + @"\Sky multi Uninstaller.exe");
                classesKey.CreateSubKey("Sky multi").SetValue("Publisher", "Himber Sacha");
                classesKey.CreateSubKey("Sky multi").SetValue("InstallLocation", Path);
                classesKey.CreateSubKey("Sky multi").SetValue("EstimatedSize", 432000, RegistryValueKind.DWord);
            }

            if (IconDesktop)
            {
                WshShell shell = new WshShell();
                IWshShortcut link = (IWshShortcut)shell.CreateShortcut(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Sky multi.lnk"));
                link.TargetPath = Path + @"\Sky multi.exe";
                //link.IconLocation = '"' + Path + @"\Sky multi.exe" + '"' + ",0";
                link.Save();
            }

            if (IconStartMenu)
            {
                WshShell shell = new WshShell();
                IWshShortcut link = (IWshShortcut)shell.CreateShortcut(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Sky multi.lnk"));
                link.TargetPath = Path + @"\Sky multi.exe";
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
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".mkv", App + ".mkv", "Video file mkv");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".avi", App + ".avi", "Video file avi");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".mp4", App + ".mp4", "Video file mp4");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".flv", App + ".flv", "Video file flv");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".m2ts", App + ".m2ts", "Video file m2ts");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".mov", App + ".mov", "Video fileo mov");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".mpeg", App + ".mpeg", "Video file mprg");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".mpeg1", App + ".mpeg1", "Video file mpeg1");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".mpeg2", App + ".mpeg2", "Video file mpeg2");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".mts", App + ".mts", "Video file mts");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".ogg", App + ".ogg", "Video file ogg");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".wmv", App + ".wmv", "Video file wmv");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".3g2", App + ".3g2", "Video file 3g2");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".3gp", App + ".3gp", "Video file 3gp");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".3gp2", App + ".3gp2", "Video file 3gp2");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".3gpp", App + ".3gpp", "Video file 3gpp");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".asf", App + ".asf", "Video file asf");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".bik", App + ".bik", "Video file bik");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".divx", App + ".divx", "Video file divx");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".drc", App + ".drc", "Video file drc");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".dv", App + ".dv", "Video file dv");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".dvr-ms", App + ".dvr-ms", "Video file dvr-ms");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".evo", App + ".evo", "Video file evo");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".f4v", App + ".f4v", "Video file f4v");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".gvi", App + ".gvi", "Video file gvi");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".gxv", App + ".gxv", "Video file gxv");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".m1v", App + ".m1v", "Video file m1v");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".m2t", App + ".m2t", "Video fileo m2t");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".m2v", App + ".m2v", "Video file m2v");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".m4v", App + ".m4v", "Video file m4v");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".mp2v", App + ".mp2v", "Video file mp2v");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".mp4v", App + ".mp4v", "Video file mp4v");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".mpa", App + ".mpa", "Video file mpa");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".mpe", App + ".mpe", "Video file mpe");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".mpeg4", App + ".mpeg4", "Video file mpeg4");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".mpg", App + ".mpg", "Video file mpg");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".mpv2", App + ".mpv2", "Video file mpv2");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".mtv", App + ".mtv", "Video file mtv");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".mxf", App + ".mxf", "Video file mxf");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".nsv", App + ".nsv", "Video file nsv");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".nuv", App + ".nuv", "Video file nuv");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".ogm", App + ".ogm", "Video file ogm");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".ogv", App + ".ogv", "Video file ogv");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".ogx", App + ".ogx", "Video file ogx");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".rec", App + ".rec", "Video file rec");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".rm", App + ".rm", "Video file rm");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".rmvb", App + ".rmvb", "Video file rmvb");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".rpl", App + ".rpl", "Video file rpl");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".thp", App + ".thp", "Video file thp");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".tod", App + ".tod", "Video file tod");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".tp", App + ".tp", "Video file tp");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".ts", App + ".ts", "Video file ts");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".tts", App + ".tts", "Video file tts");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".vob", App + ".vob", "Video file vob");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".vro", App + ".vro", "Video file vro");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".webm", App + ".webm", "Video file webm");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".wtv", App + ".wtv", "Video file wtv");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".xesc", App + ".xesc", "Video file xesc");

            // audio :
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".m4a", App + ".m4a", "Audio file m4a");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".mp3", App + ".mp3", "Audio file mp3");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".wav", App + ".wav", "Audio file wav");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".wma", App + ".wma", "Audio file wma");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".3ga", App + ".3ga", "Audio file 3ga");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".669", App + ".669", "Audio file 669");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".a52", App + ".a52", "Audio file a52");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".aac", App + ".aac", "Audio file aac");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".ac3", App + ".ac3", "Audio file ac3");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".adt", App + ".adt", "Audio file adt");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".adts", App + ".adts", "Audio file adts");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".aif", App + ".aif", "Audio file aif");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".aifc", App + ".aifc", "Audio file aifc");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".aiff", App + ".aiff", "Audio file aiff");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".amr", App + ".amr", "Audio file amr");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".aob", App + ".aob", "Audio file aob");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".ape", App + ".ape", "Audio file ape");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".au", App + ".au", "Audio file au");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".caf", App + ".caf", "Audio file caf");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".cda", App + ".cda", "Audio file cda");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".dts", App + ".dts", "Audio file dts");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".flac", App + ".flac", "Audio file flac");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".it", App + ".it", "Audio file it");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".m4p", App + ".m4p", "Audio file m4p");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".mid", App + ".mid", "Audio file mid");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".mka", App + ".mka", "Audio file mka");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".mlp", App + ".mlp", "Audio file mlp");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".mod", App + ".mod", "Audio file mod");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".mp1", App + ".mp1", "Audio file mp1");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".mp2", App + ".mp2", "Audio file mp2");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".mpc", App + ".mpc", "Audio file mpc");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".mpga", App + ".mpga", "Audio file mpga");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".oga", App + ".oga", "Audio file oga");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".oma", App + ".oma", "Audio file oma");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".opus", App + ".opus", "Audio file opus");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".qcp", App + ".qcp", "Audio file qcp");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".ra", App + ".ra", "Audio file ra");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".rmi", App + ".rmi", "Audio file rmi");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".s3m", App + ".s3m", "Audio file s3m");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".snd", App + ".snd", "Audio file snd");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".spx", App + ".spx", "Audio file spx");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".tta", App + ".tta", "Audio file tta");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".voc", App + ".voc", "Audio file voc");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".vqf", App + ".vqf", "Audio file vqf");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".w64", App + ".w64", "Audio file w64");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".wv", App + ".wv", "Audio file wv");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".xa", App + ".xa", "Audio file xa");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".xm", App + ".xm", "Audio file xm");

            // image

            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".png", App + ".png", "Image file png");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".jpg", App + ".jpg", "Image file jpg");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".gif", App + ".gif", "Image file gif");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".ico", App + ".ico", "Image file ico");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".tiff", App + ".tiff", "Image file tiff");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".tif", App + ".tif", "Image file tif");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".jpeg", App + ".jpeg", "Image file jpeg");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".bmp", App + ".bmp", "Image file bmp");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".raw", App + ".raw", "Image file raw");
            SetFileAssociation('"' + Emplacement + App + ".exe" + '"' + ",0", Emplacement + App, ".webp", App + ".webp", "Image file webp");
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
