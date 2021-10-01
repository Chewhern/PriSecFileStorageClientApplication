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
                    try 
                    {
                        ZipFile.ExtractToDirectory(ZipFileChooser.FileName, Application.StartupPath + "\\Application_Data\\Other_Directory_Data\\");
                        MessageBox.Show("Information have been imported based on the types of keys/information", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch(Exception exception)
                    {
                        MessageBox.Show("Encounter some error during import, please read the error message to know what you should do next", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MessageBox.Show(exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
