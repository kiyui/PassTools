using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IWshRuntimeLibrary;

namespace Installer
{
    public partial class Installer : Form
    {
        public Installer()
        {
            InitializeComponent();
        }

        private void Installer_Load(object sender, EventArgs e)
        {
            var ME = new Installer();
            Console.WriteLine("Installing Pass Tools.");
            string installPath = "C:\\PassTools";
            string installerPath = Directory.GetCurrentDirectory();
            Console.WriteLine(installerPath.ToString());
            string executablePath = installerPath + "\\PassTools.exe";
            string iconPath = installerPath + "\\Icon.ico";
            string passtoolsPath = "C:\\PassTools\\PassTools.exe";
            string desktopDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string startDir = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
            string shortcutStartLocation = System.IO.Path.Combine(startDir, "Pass Tools" + ".lnk");
            string shortcutDesktopLocation = System.IO.Path.Combine(desktopDir, "Pass Tools" + ".lnk");
            pbProgress.Value = 10;
            //Check if PassTools exists
            if (System.IO.File.Exists(executablePath) == false)
            {
                string reason = "Executable is missing!";
                MessageBox.Show(reason, "Installation Failed");
                installFailed(reason);
                return;
            }
            //Checks if there is a previous installation
            pbProgress.Value = 20;
            if (Directory.Exists(installPath) == true)
            {
                DialogResult deleteOld;
                deleteOld = MessageBox.Show("Remove previous Pass Tools installation?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (deleteOld == DialogResult.No)
                {
                    string reason = "Installation aborted.";
                    MessageBox.Show(reason, "Installation Failed");
                    installFailed(reason);
                    return;
                }
                else 
                {
                    Directory.Delete(installPath, true);
                    Directory.CreateDirectory(installPath);
                }
            }
            else 
            {
                Directory.CreateDirectory(installPath);
            }
            //Install!
            pbProgress.Value = 30;
            System.IO.File.Copy(executablePath, passtoolsPath);
            WshShell startShell = new WshShell();
            pbProgress.Value = 40;
            try
            {
                IWshShortcut startShortcut = (IWshShortcut)startShell.CreateShortcut(shortcutStartLocation);
                startShortcut.Description = "Pass Tools";
                startShortcut.IconLocation = iconPath;
                startShortcut.TargetPath = passtoolsPath;
                startShortcut.Save();
                pbProgress.Value = 50;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            try 
            {
                IWshShortcut desktopShortcut = (IWshShortcut)startShell.CreateShortcut(shortcutDesktopLocation);
                desktopShortcut.Description = "Pass Tools";
                desktopShortcut.IconLocation = iconPath;
                desktopShortcut.TargetPath = passtoolsPath;
                desktopShortcut.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            txtInstall.Text = "Installation succeded";
            pbProgress.Value = 100;
        }

        private void installFailed(string reason)
        {
            txtInstall.Text = reason;
            pbProgress.ForeColor = Color.Red;
            pbProgress.Refresh();
            pbProgress.Value = 100;
        }
    }
}
