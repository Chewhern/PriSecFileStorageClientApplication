using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.IO.Compression;
using ASodium;
using PriSecFileStorageWeb.Helper;

namespace PriSecFileStorageWeb.Pages
{
    public class OfflineBackupAllKeysModel : PageModel
    {
        public String Status = "";
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            if (DirectoryIDTempStorage.DirectoryID == null) 
            {
                Status = "Error:Please_choose_any_purchased_storage_ID";
            }
            else 
            {
                String BackupOptionString = Request.Form["BackupOption"].ToString();
                Boolean IsAnyChecked = (BackupOptionString.CompareTo("1") == 0) || (BackupOptionString.CompareTo("2") == 0) || (BackupOptionString.CompareTo("3") == 0);
                String ZipName = Request.Form["Zip_Name"].ToString();
                String CheckBoxOptionString = Request.Form["CheckBoxOption"].ToString();
                Boolean IsSelectExportSignatureKey = false;
                if (IsAnyChecked == true && ZipName != null && ZipName.CompareTo("") != 0)
                {
                    if (CheckBoxOptionString != null && CheckBoxOptionString.CompareTo("Yes") == 0)
                    {
                        IsSelectExportSignatureKey = true;
                    }
                    else
                    {
                        IsSelectExportSignatureKey = false;
                    }
                    String RootDirectory = AppContext.BaseDirectory;
                    Boolean IsIndex0 = BackupOptionString.CompareTo("1") == 0;
                    Boolean IsIndex1 = BackupOptionString.CompareTo("2") == 0;
                    String DirectoryID = DirectoryIDTempStorage.DirectoryID;
                    Byte[] SK = new Byte[] { };
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
                    if (IsIndex0==true)
                    {
                        ZipFile.CreateFromDirectory(RootDirectory + "/Server_Directory_Data/", RootDirectory + "/Backup/" + ZipName + ".zip");
                        
                    }
                    else if (IsIndex1==true)
                    {
                        GetAllSubDirectories(ref SubDirectories);
                        SubDirectoryRootCount = (RootDirectory + "/Server_Directory_Data/" + DirectoryIDTempStorage.DirectoryID + "/Encrypted_Files/").Length;
                        if (Directory.Exists(RootDirectory + "/Temp_Data/" + "Server_Directory_Data") == false)
                        {
                            Directory.CreateDirectory(RootDirectory + "/Temp_Data/" + "Server_Directory_Data");
                        }
                        if (Directory.Exists(RootDirectory + "/Temp_Data/" + "Server_Directory_Data/" + DirectoryID) == false)
                        {
                            Directory.CreateDirectory(RootDirectory + "/Temp_Data/" + "Server_Directory_Data/" + DirectoryID);
                        }
                        if (Directory.Exists(RootDirectory + "/Temp_Data/" + "Server_Directory_Data/" + DirectoryID + "/Encrypted_Files") == false)
                        {
                            Directory.CreateDirectory(RootDirectory + "/Temp_Data/" + "Server_Directory_Data/" + DirectoryID + "/Encrypted_Files");
                        }
                        while (LoopCount < SubDirectories.Length)
                        {
                            FilesAmount = Directory.GetFiles(SubDirectories[LoopCount] + "/").Length;
                            SubDirectoryName = SubDirectories[LoopCount].Remove(0, SubDirectoryRootCount);
                            if (Directory.Exists(RootDirectory + "/Temp_Data/" + "Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + SubDirectoryName) == false)
                            {
                                Directory.CreateDirectory(RootDirectory + "/Temp_Data/" + "Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + SubDirectoryName);
                            }
                            while (KeysLoopCount < FilesAmount)
                            {
                                if (Directory.Exists(RootDirectory + "/Temp_Data/" + "Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + SubDirectoryName + "/ED25519PK") == false)
                                {
                                    Directory.CreateDirectory(RootDirectory + "/Temp_Data/" + "Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + SubDirectoryName + "/ED25519PK");
                                }
                                PK = System.IO.File.ReadAllBytes(SubDirectories[LoopCount] + "/ED25519PK/PK" + KeysLoopCount.ToString() + ".txt");
                                System.IO.File.WriteAllBytes(RootDirectory + "/Temp_Data/" + "Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + SubDirectoryName + "/ED25519PK/PK" + KeysLoopCount.ToString() + ".txt", PK);
                                SodiumSecureMemory.SecureClearBytes(PK);
                                KeysLoopCount += 1;
                            }
                            if (IsSelectExportSignatureKey == true)
                            {
                                KeysLoopCount = 1;
                                while (KeysLoopCount < FilesAmount)
                                {
                                    if (Directory.Exists(RootDirectory + "/Temp_Data/" + "Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + SubDirectoryName + "/ED25519SK/") == false)
                                    {
                                        Directory.CreateDirectory(RootDirectory + "/Temp_Data/" + "Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + SubDirectoryName + "/ED25519SK/");
                                    }
                                    SK = System.IO.File.ReadAllBytes(SubDirectories[LoopCount] + "/ED25519SK/SK" + KeysLoopCount.ToString() + ".txt");
                                    System.IO.File.WriteAllBytes(RootDirectory + "/Temp_Data/" + "Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + SubDirectoryName + "/ED25519SK/SK" + KeysLoopCount.ToString() + ".txt", SK);
                                    SodiumSecureMemory.SecureClearBytes(SK);
                                    KeysLoopCount += 1;
                                }
                            }
                            KeysLoopCount = 1;
                            while (KeysLoopCount < FilesAmount)
                            {
                                if (Directory.Exists(RootDirectory + "/Temp_Data/" + "Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + SubDirectoryName + "/Key/") == false)
                                {
                                    Directory.CreateDirectory(RootDirectory + "/Temp_Data/" + "Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + SubDirectoryName + "/Key/");
                                }
                                Key = System.IO.File.ReadAllBytes(SubDirectories[LoopCount] + "/Key/Key" + KeysLoopCount.ToString() + ".txt");
                                System.IO.File.WriteAllBytes(RootDirectory + "/Temp_Data/" + "Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + SubDirectoryName + "/Key/Key" + KeysLoopCount.ToString() + ".txt", Key);
                                SodiumSecureMemory.SecureClearBytes(Key);
                                KeysLoopCount += 1;
                            }
                            KeysLoopCount = 1;
                            LoopCount += 1;
                        }
                        ZipFile.CreateFromDirectory(RootDirectory + "/Temp_Data/" + "Server_Directory_Data/", RootDirectory + "/Backup/" + ZipName + ".zip");
                        Directory.Delete(RootDirectory + "/Temp_Data/" + "Server_Directory_Data/",true);
                    }
                    else
                    {
                        GetAllSubDirectories(ref SubDirectories);
                        SubDirectoryRootCount = (RootDirectory + "/Server_Directory_Data/" + DirectoryIDTempStorage.DirectoryID + "/Encrypted_Files/").Length;
                        while (LoopCount < SubDirectories.Length)
                        {
                            FileNameByte = System.IO.File.ReadAllBytes(SubDirectories[LoopCount] + "/FileName.txt");
                            SubDirectoryName = SubDirectories[LoopCount].Remove(0, SubDirectoryRootCount);
                            if (Directory.Exists(RootDirectory + "/Temp_Data/" + "Server_Directory_Data/") == false)
                            {
                                Directory.CreateDirectory(RootDirectory + "/Temp_Data/" + "Server_Directory_Data/");
                            }
                            if (Directory.Exists(RootDirectory + "/Temp_Data/" + "Server_Directory_Data/" + DirectoryID) == false)
                            {
                                Directory.CreateDirectory(RootDirectory + "/Temp_Data/" + "Server_Directory_Data/" + DirectoryID);
                            }
                            if (Directory.Exists(RootDirectory + "/Temp_Data/" + "Server_Directory_Data/" + DirectoryID + "/Encrypted_Files") == false)
                            {
                                Directory.CreateDirectory(RootDirectory + "/Temp_Data/" + "Server_Directory_Data/" + DirectoryID + "/Encrypted_Files");
                            }
                            if (Directory.Exists(RootDirectory + "/Temp_Data/" + "Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + SubDirectoryName) == false)
                            {
                                Directory.CreateDirectory(RootDirectory + "/Temp_Data/" + "Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + SubDirectoryName);
                            }
                            System.IO.File.WriteAllBytes(RootDirectory + "/Temp_Data/" + "Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + SubDirectoryName + "/FileName.txt", FileNameByte);
                            SodiumSecureMemory.SecureClearBytes(FileNameByte);
                            LoopCount += 1;
                            FilesCount += 1;
                        }
                        ZipFile.CreateFromDirectory(RootDirectory + "/Temp_Data/" + "Server_Directory_Data/", RootDirectory + "/Backup/" + ZipName + ".zip");
                        Directory.Delete(RootDirectory + "/Temp_Data/" + "Server_Directory_Data/", true);
                    }
                    Status = "Succeed:Local_backup_has_been_made";
                }
                else
                {
                    Status = "Error:Please_choose_a_backup_option_or_type_a_file_name_for_Zip";
                }
            }            
        }

        private void GetAllSubDirectories(ref String[] SubDirectories)
        {
            String RootDirectory = AppContext.BaseDirectory;
            SubDirectories = Directory.GetDirectories(RootDirectory + "/Server_Directory_Data/" + DirectoryIDTempStorage.DirectoryID + "/Encrypted_Files/");
        }
    }
}
