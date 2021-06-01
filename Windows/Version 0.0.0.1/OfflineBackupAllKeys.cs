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
using ASodium;
using System.Runtime.InteropServices;

namespace PriSecFileStorageClient
{
    public partial class OfflineBackupAllKeys : Form
    {
        public OfflineBackupAllKeys()
        {
            InitializeComponent();
        }

        private void BackupBTN_Click(object sender, EventArgs e)
        {
            BackupDirectorySelector.ShowDialog();
            if (BackupDirectorySelector.SelectedPath != null && BackupDirectorySelector.SelectedPath.CompareTo("") != 0 && KeyTypesComboBox.SelectedIndex != -1)
            {
                String SelectedPath = BackupDirectorySelector.SelectedPath;
                String DirectoryID = DirectoryIDTempStorage.DirectoryID;
                String UserID = UserIDTempStorage.UserID;
                Byte[] SK = new Byte[] { };
                Byte[] PK = new Byte[] { };
                GCHandle MyGeneralGCHandle = new GCHandle();
                if (KeyTypesComboBox.SelectedIndex == 0 && ZipNameTB.Text!=null && ZipNameTB.Text.CompareTo("")!=0)
                {
                    ZipFile.CreateFromDirectory(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID, SelectedPath + "\\" + ZipNameTB.Text + ".zip");
                    MessageBox.Show("The corresponding directory data have been zipped to a zip with corresponding path " + SelectedPath + "\\" + ZipNameTB.Text + ".zip", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (KeyTypesComboBox.SelectedIndex == 1)
                {
                    SK = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\" + "\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\rootSK.txt");
                    PK = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\" + "\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\rootPK.txt");
                    if (Directory.Exists(SelectedPath + "\\" + DirectoryID) == false)
                    {
                        Directory.CreateDirectory(SelectedPath + "\\" + DirectoryID);
                    }
                    File.WriteAllBytes(SelectedPath + "\\" + DirectoryID + "\\rootSK.txt", SK);
                    File.WriteAllBytes(SelectedPath + "\\" + DirectoryID + "\\rootPK.txt", PK);
                    MyGeneralGCHandle = GCHandle.Alloc(SK, GCHandleType.Pinned);
                    SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), SK.Length);
                    MyGeneralGCHandle.Free();
                    MyGeneralGCHandle = GCHandle.Alloc(PK, GCHandleType.Pinned);
                    SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), PK.Length);
                    MyGeneralGCHandle.Free();
                    MessageBox.Show("Due to some technical stuffs and difficulty, the system can only help you to copy the 2 keys that can prove server directory ownership towards server to a new folder " + SelectedPath + "\\" + DirectoryID, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if(KeyTypesComboBox.SelectedIndex == 2 && ZipNameTB.Text != null && ZipNameTB.Text.CompareTo("") != 0)
                {
                    ZipFile.CreateFromDirectory(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files", SelectedPath + "\\"+ZipNameTB.Text+".zip");
                    MessageBox.Show("The file encryption keys have been zipped to a zip with corresponding path " + SelectedPath + "\\" + ZipNameTB.Text + ".zip", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("You haven't choose a folder/directory", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
