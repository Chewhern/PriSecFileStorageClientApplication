using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;
using System.IO;
using PriSecFileStorageClient.Helper;

namespace PriSecFileStorageClient
{
    public partial class UserImportInformationFromZip : Form
    {
        public UserImportInformationFromZip()
        {
            InitializeComponent();
        }

        private void ImportBTN_Click(object sender, EventArgs e)
        {
            ZipFileChooser.ShowDialog();
            if (ZipFileChooser.FileName != null && ZipFileChooser.FileName.CompareTo("") != 0)
            {
                if (ZipFileChooser.SafeFileName.Contains(".zip") == true)
                {
                    if (Directory.Exists(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Other_Directory_Data\\") == false)
                    {
                        Directory.CreateDirectory(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Other_Directory_Data\\");
                    }
                    ZipFile.ExtractToDirectory(ZipFileChooser.FileName, Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Other_Directory_Data\\");
                    MessageBox.Show("Information have been imported based on the types of keys/information", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("You haven't choose a zip file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("You haven't choose a file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
