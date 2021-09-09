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
using PriSecFileStorageClient.Helper;

namespace PriSecFileStorageClient
{
    public partial class UserOfflineBackupAllKeys : Form
    {
        public UserOfflineBackupAllKeys()
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
                Byte[] SPK = new Byte[] { };
                Byte[] PK = new Byte[] { };
                Byte[] Key = new Byte[] { };
                Byte[] FileNameByte = new Byte[] { };
                int FilesAmount = 0;
                int LoopCount = 0;
                int KeysLoopCount = 1;
                String[] SubDirectories = new String[] { };
                String SubDirectoryName = "";
                int SubDirectoryRootCount = 0;
                int FilesCount = 1;
                if (ZipNameTB.Text != null && ZipNameTB.Text.CompareTo("") != 0 && DirectoryIDTempStorage.DirectoryID != null && DirectoryIDTempStorage.DirectoryID.CompareTo("") != 0)
                {
                    if (KeyTypesComboBox.SelectedIndex == 0)
                    {
                        ZipFile.CreateFromDirectory(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Other_Directory_Data\\", SelectedPath + "\\" + ZipNameTB.Text + ".zip");
                        MessageBox.Show("The corresponding directory data have been zipped to a zip with corresponding path " + SelectedPath + "\\" + ZipNameTB.Text + ".zip", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (KeyTypesComboBox.SelectedIndex == 1)
                    {
                        GetAllSubDirectories(ref SubDirectories);
                        SubDirectoryRootCount = (Application.StartupPath + "\\Application_Data\\" + "\\User\\" + UserIDTempStorage.UserID + "\\Other_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\Encrypted_Files\\").Length;
                        if (Directory.Exists(SelectedPath + "\\Other_Directory_Data\\") == false)
                        {
                            Directory.CreateDirectory(SelectedPath + "\\Other_Directory_Data\\");
                        }
                        if (Directory.Exists(SelectedPath + "\\Other_Directory_Data\\" + DirectoryID) == false)
                        {
                            Directory.CreateDirectory(SelectedPath + "\\Other_Directory_Data\\" + DirectoryID);
                        }
                        if (Directory.Exists(SelectedPath + "\\Other_Directory_Data\\" + DirectoryID + "\\Encrypted_Files") == false)
                        {
                            Directory.CreateDirectory(SelectedPath + "\\Other_Directory_Data\\" + DirectoryID + "\\Encrypted_Files");
                        }
                        while (LoopCount < SubDirectories.Length)
                        {
                            FilesAmount = Directory.GetFiles(SubDirectories[LoopCount] + "\\").Length;
                            SubDirectoryName = SubDirectories[LoopCount].Remove(0, SubDirectoryRootCount);
                            if (Directory.Exists(SelectedPath + "\\Other_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + SubDirectoryName) == false)
                            {
                                Directory.CreateDirectory(SelectedPath + "\\Other_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + SubDirectoryName);
                            }
                            while (KeysLoopCount < FilesAmount)
                            {
                                if (Directory.Exists(SelectedPath + "\\Other_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + SubDirectoryName + "\\ED25519PK\\") == false)
                                {
                                    Directory.CreateDirectory(SelectedPath + "\\Other_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + SubDirectoryName + "\\ED25519PK\\");
                                }
                                PK = File.ReadAllBytes(SubDirectories[LoopCount] + "\\ED25519PK\\PK" + KeysLoopCount.ToString() + ".txt");
                                File.WriteAllBytes(SelectedPath + "\\Other_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + SubDirectoryName + "\\ED25519PK\\PK" + KeysLoopCount.ToString() + ".txt", PK);
                                SodiumSecureMemory.SecureClearBytes(PK);
                                KeysLoopCount += 1;
                            }
                            KeysLoopCount = 1;
                            while (KeysLoopCount < FilesAmount)
                            {
                                if (Directory.Exists(SelectedPath + "\\Other_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + SubDirectoryName + "\\Key\\") == false)
                                {
                                    Directory.CreateDirectory(SelectedPath + "\\Other_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + SubDirectoryName + "\\Key\\");
                                }
                                Key = File.ReadAllBytes(SubDirectories[LoopCount] + "\\Key\\Key" + KeysLoopCount.ToString() + ".txt");
                                File.WriteAllBytes(SelectedPath + "\\Other_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + SubDirectoryName + "\\Key\\Key" + KeysLoopCount.ToString() + ".txt", Key);
                                SodiumSecureMemory.SecureClearBytes(Key);
                                KeysLoopCount += 1;
                            }
                            KeysLoopCount = 1;
                            LoopCount += 1;
                        }
                        ZipFile.CreateFromDirectory(SelectedPath + "\\Other_Directory_Data\\", SelectedPath + "\\" + ZipNameTB.Text + ".zip");
                        MessageBox.Show("The file encryption keys have been zipped to a zip with corresponding path " + SelectedPath + "\\" + ZipNameTB.Text + ".zip", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        GetAllSubDirectories(ref SubDirectories);
                        SubDirectoryRootCount = (Application.StartupPath + "\\Application_Data\\" + "\\User\\" + UserIDTempStorage.UserID + "\\Other_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\Encrypted_Files\\").Length;
                        while (LoopCount < SubDirectories.Length)
                        {
                            FileNameByte = File.ReadAllBytes(SubDirectories[LoopCount] + "\\FileName.txt");
                            SubDirectoryName = SubDirectories[LoopCount].Remove(0, SubDirectoryRootCount);
                            if (Directory.Exists(SelectedPath + "\\Other_Directory_Data\\") == false)
                            {
                                Directory.CreateDirectory(SelectedPath + "\\Other_Directory_Data\\");
                            }
                            if (Directory.Exists(SelectedPath + "\\Other_Directory_Data\\" + DirectoryID) == false)
                            {
                                Directory.CreateDirectory(SelectedPath + "\\Other_Directory_Data\\" + DirectoryID);
                            }
                            if (Directory.Exists(SelectedPath + "\\Other_Directory_Data\\" + DirectoryID + "\\Encrypted_Files") == false)
                            {
                                Directory.CreateDirectory(SelectedPath + "\\Other_Directory_Data\\" + DirectoryID + "\\Encrypted_Files");
                            }
                            if (Directory.Exists(SelectedPath + "\\Other_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + SubDirectoryName) == false)
                            {
                                Directory.CreateDirectory(SelectedPath + "\\Other_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + SubDirectoryName);
                            }
                            File.WriteAllBytes(SelectedPath + "\\Other_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + SubDirectoryName + "\\FileName.txt", FileNameByte);
                            SodiumSecureMemory.SecureClearBytes(FileNameByte);
                            LoopCount += 1;
                            FilesCount += 1;
                        }
                        ZipFile.CreateFromDirectory(SelectedPath + "\\Other_Directory_Data\\", SelectedPath + "\\" + ZipNameTB.Text + ".zip");
                        MessageBox.Show("The file name and its extension have been zipped to a zip with corresponding path " + SelectedPath + "\\" + ZipNameTB.Text + ".zip", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("You haven't write a name for the zip file and you haven't made/renew any payment yet", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("You haven't choose a folder/directory", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetAllSubDirectories(ref String[] SubDirectories)
        {
            SubDirectories = Directory.GetDirectories(Application.StartupPath + "\\Application_Data\\" + "\\User\\" + UserIDTempStorage.UserID + "\\Other_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\Encrypted_Files\\");
        }
    }
}
