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
                key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Sky multi", false); // x64
            }
            else
            {
                key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Sky multi", false); // x86
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
            key.DeleteSubKey("Sky multi");

            key.Close();
            key.Dispose();
            key = Registry.ClassesRoot;
            key.DeleteSubKeyTree("Sky multi.mkv");
            key.OpenSubKey(".mkv", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.avi");
            key.OpenSubKey(".avi", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.mp4");
            key.OpenSubKey(".mp4", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.flv");
            key.OpenSubKey(".flv", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.m2ts");
            key.OpenSubKey(".m2ts", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.mov");
            key.OpenSubKey(".mov", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.mpeg");
            key.OpenSubKey(".mpeg", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.mpeg1");
            key.OpenSubKey(".mpeg1", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.mpeg2");
            key.OpenSubKey(".mpeg2", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.mts");
            key.OpenSubKey(".mts", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.ogg");
            key.OpenSubKey(".ogg", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.wmv");
            key.OpenSubKey(".wmv", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.3g2");
            key.OpenSubKey(".3g2", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.3gp");
            key.OpenSubKey(".3gp", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.3gp2");
            key.OpenSubKey(".3gp2", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.3gpp");
            key.OpenSubKey(".3gpp", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.asf");
            key.OpenSubKey(".asf", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.bik");
            key.OpenSubKey(".bik", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.divx");
            key.OpenSubKey(".divx", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.drc");
            key.OpenSubKey(".drc", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.dv");
            key.OpenSubKey(".dv", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.dvr-ms");
            key.OpenSubKey(".dvr-ms", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.evo");
            key.OpenSubKey(".evo", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.f4v");
            key.OpenSubKey(".f4v", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.gvi");
            key.OpenSubKey(".gvi", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.gxv");
            key.OpenSubKey(".gxv", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.m1v");
            key.OpenSubKey(".m1v", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.m2t");
            key.OpenSubKey(".m2t", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.m2v");
            key.OpenSubKey(".m2v", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.m4v");
            key.OpenSubKey(".m4v", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.mpa");
            key.OpenSubKey(".mpa", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.mpe");
            key.OpenSubKey(".mpe", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.mpeg4");
            key.OpenSubKey(".mpeg4", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.mpg");
            key.OpenSubKey(".mpg", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.mpv2");
            key.OpenSubKey(".mpv2", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.mp2v");
            key.OpenSubKey(".mp2v", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.mp4v");
            key.OpenSubKey(".mp4v", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.mtv");
            key.OpenSubKey(".mtv", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.mxf");
            key.OpenSubKey(".mxf", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.nsv");
            key.OpenSubKey(".nsv", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.nuv");
            key.OpenSubKey(".nuv", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.ogm");
            key.OpenSubKey(".ogm", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.ogv");
            key.OpenSubKey(".ogv", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.ogx");
            key.OpenSubKey(".ogx", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.rec");
            key.OpenSubKey(".rec", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.rm");
            key.OpenSubKey(".rm", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.rmvb");
            key.OpenSubKey(".rmvb", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.rpl");
            key.OpenSubKey(".rpl", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.thp");
            key.OpenSubKey(".thp", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.tod");
            key.OpenSubKey(".tod", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.tp");
            key.OpenSubKey(".tp", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.ts");
            key.OpenSubKey(".ts", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.tts");
            key.OpenSubKey(".tts", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.vob");
            key.OpenSubKey(".vob", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.vro");
            key.OpenSubKey(".vro", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.webm");
            key.OpenSubKey(".webm", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.wtv");
            key.OpenSubKey(".wtv", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.xesc");
            key.OpenSubKey(".xesc", true).SetValue(string.Empty, string.Empty);

            key.DeleteSubKeyTree("Sky multi.m4a");
            key.OpenSubKey(".m4a", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.mp3");
            key.OpenSubKey(".mp3", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.wav");
            key.OpenSubKey(".wav", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.wma");
            key.OpenSubKey(".wma", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.3ga");
            key.OpenSubKey(".3ga", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.669");
            key.OpenSubKey(".669", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.a52");
            key.OpenSubKey(".a52", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.aac");
            key.OpenSubKey(".aac", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.ac3");
            key.OpenSubKey(".ac3", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.adt");
            key.OpenSubKey(".adt", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.adts");
            key.OpenSubKey(".adts", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.aif");
            key.OpenSubKey(".aif", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.aifc");
            key.OpenSubKey(".aifc", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.aiff");
            key.OpenSubKey(".aiff", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.amr");
            key.OpenSubKey(".amr", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.aob");
            key.OpenSubKey(".aob", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.ape");
            key.OpenSubKey(".ape", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.au");
            key.OpenSubKey(".au", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.caf");
            key.OpenSubKey(".caf", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.cda");
            key.OpenSubKey(".cda", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.dts");
            key.OpenSubKey(".dts", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.flac");
            key.OpenSubKey(".flac", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.it");
            key.OpenSubKey(".it", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.m4p");
            key.OpenSubKey(".m4p", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.mid");
            key.OpenSubKey(".mid", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.mka");
            key.OpenSubKey(".mka", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.mlp");
            key.OpenSubKey(".mlp", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.mod");
            key.OpenSubKey(".mod", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.mp1");
            key.OpenSubKey(".mp1", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.mp2");
            key.OpenSubKey(".mp2", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.mpc");
            key.OpenSubKey(".mpc", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.mpga");
            key.OpenSubKey(".mpga", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.oga");
            key.OpenSubKey(".oga", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.oma");
            key.OpenSubKey(".oma", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.opus");
            key.OpenSubKey(".opus", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.qcp");
            key.OpenSubKey(".qcp", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.ra");
            key.OpenSubKey(".ra", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.rmi");
            key.OpenSubKey(".rmi", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.s3m");
            key.OpenSubKey(".s3m", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.snd");
            key.OpenSubKey(".snd", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.spx");
            key.OpenSubKey(".spx", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.tta");
            key.OpenSubKey(".tta", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.voc");
            key.OpenSubKey(".voc", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.vqf");
            key.OpenSubKey(".vqf", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.w64");
            key.OpenSubKey(".w64", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.wv");
            key.OpenSubKey(".wv", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.xa");
            key.OpenSubKey(".xa", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.xm");
            key.OpenSubKey(".xm", true).SetValue(string.Empty, string.Empty);

            // image
            key.DeleteSubKeyTree("Sky multi.bmp");
            key.OpenSubKey(".bmp", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.gif");
            key.OpenSubKey(".gif", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.ico");
            key.OpenSubKey(".ico", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.jpeg");
            key.OpenSubKey(".jpeg", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.jpg");
            key.OpenSubKey(".jpg", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.png");
            key.OpenSubKey(".png", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.raw");
            key.OpenSubKey(".raw", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.tiff");
            key.OpenSubKey(".tiff", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.tif");
            key.OpenSubKey(".tif", true).SetValue(string.Empty, string.Empty);
            key.DeleteSubKeyTree("Sky multi.webp");
            key.OpenSubKey(".webp", true).SetValue(string.Empty, string.Empty);

            if (File.Exists(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Sky multi.lnk")))
            {
                File.Delete(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Sky multi.lnk"));
            }

            if (File.Exists(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Sky multi.lnk")))
            {
                File.Delete(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Sky multi.lnk"));
            }

            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Sky multi"))
            {
                Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Sky multi", true);
            }

            Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
