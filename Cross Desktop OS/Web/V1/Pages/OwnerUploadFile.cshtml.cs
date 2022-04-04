using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using ASodium;
using PriSecFileStorageWeb.Model;
using PriSecFileStorageWeb.Helper;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Threading;

namespace PriSecFileStorageWeb.Pages
{
    public class OwnerUploadFileModel : PageModel
    {
        public String[] ListOfFileNames = new String[] { };
        public String Current_Directory_ID = "";
        public String Storage_Size_In_String = "";
        public String Chosen_File_Name = "";
        public String File_Size_In_String = "";
        public String Random_File_Name = "";

        private SecureIDGenerator IDGenerator = new SecureIDGenerator();

        public void OnGet()
        {
            if (DirectoryIDTempStorage.DirectoryID != null && DirectoryIDTempStorage.DirectoryID.CompareTo("") != 0)
            {
                Current_Directory_ID = DirectoryIDTempStorage.DirectoryID;
                ReloadFiles();
                GetFileStorageUsedSize(ref Storage_Size_In_String);
            }
        }

        public void OnPostReadFileSize() 
        {
            if (DirectoryIDTempStorage.DirectoryID != null && DirectoryIDTempStorage.DirectoryID.CompareTo("") != 0)
            {
                Current_Directory_ID = DirectoryIDTempStorage.DirectoryID;
                ReloadFiles();
                GetFileStorageUsedSize(ref Storage_Size_In_String);
            }
            String RootDirectory = AppContext.BaseDirectory;
            String Chosen_File_Name = Request.Form["File_Name"].ToString();
            if (Chosen_File_Name.CompareTo("") != 0) 
            {
                int FileSize = System.IO.File.ReadAllBytes(RootDirectory + "/Files_To_Encrypt/" + Chosen_File_Name).Length;
                File_Size_In_String = FileSize.ToString();
                this.Chosen_File_Name = Chosen_File_Name;
            }
        }

        public async void OnPostEncryptFile() 
        {
            if (DirectoryIDTempStorage.DirectoryID != null && DirectoryIDTempStorage.DirectoryID.CompareTo("") != 0)
            {
                Current_Directory_ID = DirectoryIDTempStorage.DirectoryID;
                ReloadFiles();
                GetFileStorageUsedSize(ref Storage_Size_In_String);
            }
            String FileSizeInString = Request.Form["File_Size"].ToString();
            int FileSize = 0;
            String Current_File_Name = Request.Form["Current_File_Name"].ToString();
            String XSalsa20Poly1305String = Request.Form["SEAlgorithm"].ToString();
            String XChaCha20Poly1305String = Request.Form["SEAlgorithm"].ToString();
            String AES256GCMString = Request.Form["SEAlgorithm"].ToString();
            Boolean IsAnyChecked = (XSalsa20Poly1305String.CompareTo("1") == 0) || (XChaCha20Poly1305String.CompareTo("2") == 0) || (AES256GCMString.CompareTo("3") == 0);
            Boolean IsXSalsa20Poly1305Checked = true;
            Boolean IsXChaCha20Poly1305Checked = true;
            Boolean IsAES256GCMChecked = true;
            String RootDirectory = AppContext.BaseDirectory;
            if (Current_File_Name.CompareTo("") != 0 && FileSizeInString.CompareTo("")!=0 && DirectoryIDTempStorage.DirectoryID.CompareTo("")!=0 && IsAnyChecked==true)
            {
                File_Size_In_String = FileSizeInString;
                this.Chosen_File_Name = Current_File_Name;
                FileSize = int.Parse(FileSizeInString);
                IsXSalsa20Poly1305Checked = (XSalsa20Poly1305String.CompareTo("1") == 0);
                IsXChaCha20Poly1305Checked = (XChaCha20Poly1305String.CompareTo("2") == 0);
                IsAES256GCMChecked = (AES256GCMString.CompareTo("3") == 0);
                String DirectoryID = DirectoryIDTempStorage.DirectoryID;
                String FileName = Current_File_Name;
                String RandomFileName = IDGenerator.GenerateUniqueString();
                RevampedKeyPair MyKeyPair = SodiumPublicKeyAuth.GenerateRevampedKeyPair();
                Byte[] Nonce = SodiumSecretBox.GenerateNonce();
                Byte[] XChaCha20Poly1305Nonce = SodiumSecretBoxXChaCha20Poly1305.GenerateNonce();
                Byte[] AES256GCMNonce = SodiumSecretAeadAES256GCM.GeneratePublicNonce();
                Byte[] Key = SodiumSecretBox.GenerateKey();
                Byte[] FileBytes = new Byte[] { };
                Byte[] EncryptedFileBytes = new Byte[] { };
                Byte[] CombinedEncryptedFileBytes = new Byte[] { };
                Byte[] LocalSignedCombinedEncryptedFileBytes = new Byte[] { };
                Byte[] UserECDSASK = new Byte[] { };
                int FileCount = 0;
                int ActualFileCount = 0;
                int ActualEncryptionCount = 0;
                int EncryptionCount = 1;
                if (FileSize <= 1073741824)
                {
                    if (Directory.Exists(RootDirectory + "/Server_Directory_Data/" + DirectoryID) == true)
                    {
                        if (Directory.Exists(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files") == false)
                        {
                            Directory.CreateDirectory(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files");
                        }
                        if (RandomFileName.Length > 45)
                        {
                            RandomFileName = RandomFileName.Substring(0, 20);
                        }
                        if (Directory.Exists(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName) == true)
                        {
                            while (Directory.Exists(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName) == true)
                            {
                                RandomFileName = IDGenerator.GenerateUniqueString();
                                if (RandomFileName.Length > 45)
                                {
                                    RandomFileName = RandomFileName.Substring(0, 20);
                                }
                            }
                            Directory.CreateDirectory(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName);
                        }
                        else
                        {
                            Directory.CreateDirectory(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName);
                        }
                        Random_File_Name = RandomFileName;
                        if (Directory.Exists(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName + "/ED25519SK") == false)
                        {
                            Directory.CreateDirectory(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName + "/ED25519SK");
                        }
                        if (Directory.Exists(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName + "/ED25519PK") == false)
                        {
                            Directory.CreateDirectory(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName + "/ED25519PK");
                        }
                        if (Directory.Exists(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName + "/Key") == false)
                        {
                            Directory.CreateDirectory(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName + "/Key");
                        }
                        System.IO.File.WriteAllBytes(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName + "/ED25519SK/" + "SK1.txt", MyKeyPair.PrivateKey);
                        System.IO.File.WriteAllBytes(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName + "/ED25519PK/" + "PK1.txt", MyKeyPair.PublicKey);
                        System.IO.File.WriteAllBytes(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName + "/Key/" + "Key1.txt", Key);
                        MyKeyPair.Clear();
                        if (FileSize <= FileBytesChunkClass.MaximumChunksFileBytesLength)
                        {
                            Key = System.IO.File.ReadAllBytes(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName + "/Key/" + "Key1.txt");
                            UserECDSASK = System.IO.File.ReadAllBytes(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName + "/ED25519SK/" + "SK1.txt");
                            FileBytes = System.IO.File.ReadAllBytes(RootDirectory + "/Files_To_Encrypt/"+Current_File_Name);
                            if (IsXSalsa20Poly1305Checked == true)
                            {
                                EncryptedFileBytes = SodiumSecretBox.Create(FileBytes, Nonce, Key, true);
                                CombinedEncryptedFileBytes = Nonce.Concat(EncryptedFileBytes).ToArray();
                                LocalSignedCombinedEncryptedFileBytes = SodiumPublicKeyAuth.Sign(CombinedEncryptedFileBytes, UserECDSASK, true);
                                System.IO.File.WriteAllBytes(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName + "/FileName.txt", Encoding.UTF8.GetBytes(FileName));
                                FileCount = Directory.GetFileSystemEntries(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName).Length;
                                ActualFileCount = FileCount - 4;
                                ActualFileCount += 1;
                                System.IO.File.WriteAllBytes(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName + "/FileContent" + ActualFileCount.ToString() + ".txt", LocalSignedCombinedEncryptedFileBytes);
                                Console.WriteLine("Encryption of file done");
                            }
                            if (IsXChaCha20Poly1305Checked == true)
                            {
                                EncryptedFileBytes = SodiumSecretBoxXChaCha20Poly1305.Create(FileBytes, XChaCha20Poly1305Nonce, Key, true);
                                CombinedEncryptedFileBytes = XChaCha20Poly1305Nonce.Concat(EncryptedFileBytes).ToArray();
                                LocalSignedCombinedEncryptedFileBytes = SodiumPublicKeyAuth.Sign(CombinedEncryptedFileBytes, UserECDSASK, true);
                                System.IO.File.WriteAllBytes(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName + "/FileName.txt", Encoding.UTF8.GetBytes(FileName));
                                FileCount = Directory.GetFileSystemEntries(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName).Length;
                                ActualFileCount = FileCount - 4;
                                ActualFileCount += 1;
                                System.IO.File.WriteAllBytes(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName + "/FileContent" + ActualFileCount.ToString() + ".txt", LocalSignedCombinedEncryptedFileBytes);
                                Console.WriteLine("Encryption of file done");
                            }
                            if (IsAES256GCMChecked == true)
                            {
                                if (SodiumSecretAeadAES256GCM.IsAES256GCMAvailable() == true)
                                {
                                    EncryptedFileBytes = SodiumSecretAeadAES256GCM.Encrypt(FileBytes, AES256GCMNonce, Key, null, null, true);
                                    CombinedEncryptedFileBytes = AES256GCMNonce.Concat(EncryptedFileBytes).ToArray();
                                    LocalSignedCombinedEncryptedFileBytes = SodiumPublicKeyAuth.Sign(CombinedEncryptedFileBytes, UserECDSASK, true);
                                    System.IO.File.WriteAllBytes(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName + "/FileName.txt", Encoding.UTF8.GetBytes(FileName));
                                    FileCount = Directory.GetFileSystemEntries(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName).Length;
                                    ActualFileCount = FileCount - 4;
                                    ActualFileCount += 1;
                                    System.IO.File.WriteAllBytes(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName + "/FileContent" + ActualFileCount.ToString() + ".txt", LocalSignedCombinedEncryptedFileBytes);
                                    Console.WriteLine("Encryption of file done");
                                }
                                else
                                {
                                    Console.WriteLine("Error: Your device does not support AES256 GCM");
                                }
                            }
                        }
                        else
                        {
                            if (FileSize % FileBytesChunkClass.MaximumChunksFileBytesLength != 0)
                            {
                                ActualEncryptionCount = (FileSize / FileBytesChunkClass.MaximumChunksFileBytesLength) + 1;
                            }
                            else
                            {
                                ActualEncryptionCount = FileSize / FileBytesChunkClass.MaximumChunksFileBytesLength;
                            }
                            var progress = new Progress<int>(percent =>
                            {
                                Console.WriteLine(EncryptionCount.ToString()+" out of "+ActualEncryptionCount+" parts have been encrypted");
                                EncryptionCount += 1;
                            });
                            await Task.Run(() => BackGroundEncrypt(progress,IsXSalsa20Poly1305Checked,IsXChaCha20Poly1305Checked,IsAES256GCMChecked,FileSize,FileName,RandomFileName));
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: you haven't have a directory that rented from the provider");
                    }
                }
                else
                {
                    Console.WriteLine("Error: File Size must not be greater than 1 GB");
                }
            }
        }

        public async void OnPost() 
        {
            if (DirectoryIDTempStorage.DirectoryID != null && DirectoryIDTempStorage.DirectoryID.CompareTo("") != 0)
            {
                Current_Directory_ID = DirectoryIDTempStorage.DirectoryID;
                ReloadFiles();
                GetFileStorageUsedSize(ref Storage_Size_In_String);
            }
            String FileSizeInString = Request.Form["File_Size"].ToString();
            String RandomFileName = Request.Form["Random_File_Name"].ToString();
            String Current_File_Name = Request.Form["Current_File_Name"].ToString();
            String RootDirectory = AppContext.BaseDirectory;
            if (RandomFileName.CompareTo("") != 0)
            {
                File_Size_In_String = FileSizeInString;
                this.Chosen_File_Name = Current_File_Name;
                Random_File_Name = RandomFileName;
                int FilesCount = Directory.GetFiles(RootDirectory + "/Server_Directory_Data/" + DirectoryIDTempStorage.DirectoryID + "/Encrypted_Files/" + RandomFileName).Length;
                int ActualFileCount = FilesCount - 1;
                int Count = 1;
                var progress = new Progress<int>(percent =>
                {
                    Console.WriteLine(Count.ToString() + " part out of "+ActualFileCount.ToString()+" parts have been uploaded");
                    Count += 1;
                });
                await Task.Run(() => BackGroundUpload(progress,RandomFileName));
                GetFileStorageUsedSize(ref Storage_Size_In_String);
            }
            else
            {
                Console.WriteLine("Error: You have not yet encrypt a file");
            }
        }

        public void BackGroundUpload(IProgress<int> progress,String Random_File_Name)
        {
            if (ETLSSessionIDStorage.ETLSID != null && ETLSSessionIDStorage.ETLSID.CompareTo("") != 0)
            {
                String RootDirectory = AppContext.BaseDirectory;
                String DirectoryID = DirectoryIDTempStorage.DirectoryID;
                String RandomFileName = Random_File_Name;
                int FileCount = 0;
                int ActualFileCount = 0;
                int LoopCount = 1;
                String Base64Challenge = "";
                Byte[] Challenge = new Byte[] { };
                Boolean AbleToRequestChallenge = true;
                Boolean RequestChallengeErrorChecking = false;
                Boolean UploadFileErrorChecking = false;
                Boolean TempBoolean = false;
                if (DirectoryID != null && DirectoryID.CompareTo("") != 0 && RandomFileName != null && RandomFileName.CompareTo("") != 0)
                {
                    if (Directory.Exists(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName) == true)
                    {
                        FileCount = Directory.GetFileSystemEntries(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName).Length;
                        ActualFileCount = FileCount - 4;
                        while (LoopCount <= ActualFileCount)
                        {
                            Thread.Sleep(100);
                            RequestChallenge(ref AbleToRequestChallenge, ref Base64Challenge);
                            while (AbleToRequestChallenge == false)
                            {
                                RequestChallenge(ref AbleToRequestChallenge, ref Base64Challenge);
                            }
                            if (Base64Challenge != null && Base64Challenge.CompareTo("") != 0)
                            {
                                Challenge = Convert.FromBase64String(Base64Challenge);
                                TempBoolean = UploadFileAsync(DirectoryID, RandomFileName ,Challenge, LoopCount).Result;
                                if (TempBoolean == false && UploadFileErrorChecking == false)
                                {
                                    UploadFileErrorChecking = true;
                                    break;
                                }
                            }
                            else
                            {
                                if (RequestChallengeErrorChecking == false)
                                {
                                    RequestChallengeErrorChecking = true;
                                    break;
                                }
                            }
                            if (UploadFileErrorChecking == false && RequestChallengeErrorChecking == false)
                            {
                                if (progress != null)
                                {
                                    progress.Report(LoopCount);
                                }
                            }
                            Base64Challenge = "";
                            Challenge = new Byte[] { };
                            LoopCount += 1;
                        }
                        if (RequestChallengeErrorChecking == true)
                        {
                            Console.WriteLine("Error requesting challenge from server");
                        }
                        if (UploadFileErrorChecking == true)
                        {
                            Console.WriteLine("Error uploading file to server");
                        }
                        if (UploadFileErrorChecking == false && RequestChallengeErrorChecking == false)
                        {
                            Console.WriteLine("File successfully uploaded to server by chunks/parts, please keep your key safe and private");
                            Console.WriteLine("For the encrypted file that you uploaded, please note down the corresponding file name that links to random file name sent to server.", "Crucial Information");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: Directory ID and Random file name does not exists..");
                    }
                }
                else
                {
                    Console.WriteLine("Error: You have not yet purchase the file storage");
                }
            }
            else
            {
                Console.WriteLine("Error: You have not yet establish an ephemeral TLS session");
            }
        }

        public void BackGroundEncrypt(IProgress<int> progress,Boolean IsXSalsa20Poly1305,Boolean IsXChaCha20Poly1305,Boolean IsAES256GCM,int File_Size,String File_Name,String Random_File_Name)
        {
            int FileSize = File_Size;
            String RootDirectory = AppContext.BaseDirectory;
            String DirectoryID = DirectoryIDTempStorage.DirectoryID;
            String FileName = File_Name;
            String RandomFileName = Random_File_Name;
            Boolean HasRemainder = false;
            Boolean GeneralChecker = true;
            Byte[] Nonce = SodiumSecretBox.GenerateNonce();
            Byte[] XChaCha20Poly1305Nonce = SodiumSecretBoxXChaCha20Poly1305.GenerateNonce();
            Byte[] AES256GCMNonce = SodiumSecretAeadAES256GCM.GeneratePublicNonce();
            Byte[] Key = new Byte[] { };
            Byte[] FileBytes = new Byte[] { };
            Byte[] EncryptedFileBytes = new Byte[] { };
            Byte[] CombinedEncryptedFileBytes = new Byte[] { };
            Byte[] SignedCombinedEncryptedFileBytes = new Byte[] { };
            Byte[] UserECDSASK = new Byte[] { };
            Byte[] UserECDSAPK = new Byte[] { };
            FileStream MessageFileStream = System.IO.File.OpenRead(RootDirectory + "/Files_To_Encrypt/" + FileName);
            int Count = 0;
            int Remainder = 0;
            int LoopCount = 1;
            int EndOfFile = 0;
            int FileCount = 0;
            int ActualFileCount = 0;
            RevampedKeyPair MyKeyPair = SodiumPublicKeyAuth.GenerateRevampedKeyPair();
            if (Directory.Exists(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files") == false)
            {
                Directory.CreateDirectory(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files");
            }
            Count = FileSize / FileBytesChunkClass.MaximumChunksFileBytesLength;
            Remainder = FileSize % FileBytesChunkClass.MaximumChunksFileBytesLength;
            if (Remainder != 0)
            {
                HasRemainder = true;
                Count += 1;
            }
            System.IO.File.WriteAllBytes(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName + "/FileName.txt", Encoding.UTF8.GetBytes(FileName));
            FileCount = Directory.GetFileSystemEntries(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName).Length;
            ActualFileCount = FileCount - 4;
            while (LoopCount <= Count)
            {
                Thread.Sleep(100);
                if (LoopCount == 1)
                {
                    Key = System.IO.File.ReadAllBytes(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName + "/Key/Key1.txt");
                    UserECDSASK = System.IO.File.ReadAllBytes(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName + "/ED25519SK/SK1.txt");
                    UserECDSAPK = System.IO.File.ReadAllBytes(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName + "/ED25519PK/PK1.txt");
                }
                else
                {
                    Key = SodiumSecretBox.GenerateKey();
                    UserECDSASK = MyKeyPair.PrivateKey;
                    UserECDSAPK = MyKeyPair.PublicKey;
                }
                if (HasRemainder == true)
                {
                    if (LoopCount == 1)
                    {
                        if (Count == 1)
                        {
                            FileBytes = new Byte[Remainder];
                            EncryptedFileBytes = new Byte[] { };
                            CombinedEncryptedFileBytes = new Byte[] { };
                            EndOfFile = MessageFileStream.Read(FileBytes, 0, FileBytes.Length);
                            if (IsXSalsa20Poly1305 == true)
                            {
                                EncryptedFileBytes = SodiumSecretBox.Create(FileBytes, Nonce, Key);
                                CombinedEncryptedFileBytes = Nonce.Concat(EncryptedFileBytes).ToArray();
                            }
                            if (IsXChaCha20Poly1305 == true)
                            {
                                EncryptedFileBytes = SodiumSecretBoxXChaCha20Poly1305.Create(FileBytes, XChaCha20Poly1305Nonce, Key);
                                CombinedEncryptedFileBytes = XChaCha20Poly1305Nonce.Concat(EncryptedFileBytes).ToArray();
                            }
                            if (IsAES256GCM == true)
                            {
                                if (SodiumSecretAeadAES256GCM.IsAES256GCMAvailable() == true)
                                {
                                    EncryptedFileBytes = SodiumSecretAeadAES256GCM.Encrypt(FileBytes, AES256GCMNonce, Key);
                                    CombinedEncryptedFileBytes = AES256GCMNonce.Concat(EncryptedFileBytes).ToArray();
                                }
                                else
                                {
                                    GeneralChecker = false;
                                    break;
                                }
                            }
                            SignedCombinedEncryptedFileBytes = SodiumPublicKeyAuth.Sign(CombinedEncryptedFileBytes, UserECDSASK);
                        }
                        else
                        {
                            FileBytes = new Byte[FileBytesChunkClass.MaximumChunksFileBytesLength];
                            EncryptedFileBytes = new Byte[] { };
                            CombinedEncryptedFileBytes = new Byte[] { };
                            EndOfFile = MessageFileStream.Read(FileBytes, 0, FileBytes.Length);
                            if (IsXSalsa20Poly1305 == true)
                            {
                                EncryptedFileBytes = SodiumSecretBox.Create(FileBytes, Nonce, Key);
                                CombinedEncryptedFileBytes = Nonce.Concat(EncryptedFileBytes).ToArray();
                            }
                            if (IsXChaCha20Poly1305 == true)
                            {
                                EncryptedFileBytes = SodiumSecretBoxXChaCha20Poly1305.Create(FileBytes, XChaCha20Poly1305Nonce, Key);
                                CombinedEncryptedFileBytes = XChaCha20Poly1305Nonce.Concat(EncryptedFileBytes).ToArray();
                            }
                            if (IsAES256GCM == true)
                            {
                                if (SodiumSecretAeadAES256GCM.IsAES256GCMAvailable() == true)
                                {
                                    EncryptedFileBytes = SodiumSecretAeadAES256GCM.Encrypt(FileBytes, AES256GCMNonce, Key);
                                    CombinedEncryptedFileBytes = AES256GCMNonce.Concat(EncryptedFileBytes).ToArray();
                                }
                                else
                                {
                                    GeneralChecker = false;
                                    break;
                                }
                            }
                            SignedCombinedEncryptedFileBytes = SodiumPublicKeyAuth.Sign(CombinedEncryptedFileBytes, UserECDSASK);
                        }
                    }
                    else
                    {
                        Nonce = new Byte[] { };
                        XChaCha20Poly1305Nonce = new Byte[] { };
                        AES256GCMNonce = new Byte[] { };
                        Nonce = SodiumSecretBox.GenerateNonce();
                        XChaCha20Poly1305Nonce = SodiumSecretBoxXChaCha20Poly1305.GenerateNonce();
                        AES256GCMNonce = SodiumSecretAeadAES256GCM.GeneratePublicNonce();
                        if (LoopCount == Count)
                        {
                            FileBytes = new Byte[Remainder];
                            EncryptedFileBytes = new Byte[] { };
                            CombinedEncryptedFileBytes = new Byte[] { };
                            EndOfFile = MessageFileStream.Read(FileBytes, 0, FileBytes.Length);
                            if (IsXSalsa20Poly1305 == true)
                            {
                                EncryptedFileBytes = SodiumSecretBox.Create(FileBytes, Nonce, Key);
                                CombinedEncryptedFileBytes = Nonce.Concat(EncryptedFileBytes).ToArray();
                            }
                            if (IsXChaCha20Poly1305 == true)
                            {
                                EncryptedFileBytes = SodiumSecretBoxXChaCha20Poly1305.Create(FileBytes, XChaCha20Poly1305Nonce, Key);
                                CombinedEncryptedFileBytes = XChaCha20Poly1305Nonce.Concat(EncryptedFileBytes).ToArray();
                            }
                            if (IsAES256GCM == true)
                            {
                                if (SodiumSecretAeadAES256GCM.IsAES256GCMAvailable() == true)
                                {
                                    EncryptedFileBytes = SodiumSecretAeadAES256GCM.Encrypt(FileBytes, AES256GCMNonce, Key);
                                    CombinedEncryptedFileBytes = AES256GCMNonce.Concat(EncryptedFileBytes).ToArray();
                                }
                                else
                                {
                                    GeneralChecker = false;
                                    break;
                                }
                            }
                            SignedCombinedEncryptedFileBytes = SodiumPublicKeyAuth.Sign(CombinedEncryptedFileBytes, UserECDSASK);
                        }
                        else
                        {
                            FileBytes = new Byte[FileBytesChunkClass.MaximumChunksFileBytesLength];
                            EncryptedFileBytes = new Byte[] { };
                            CombinedEncryptedFileBytes = new Byte[] { };
                            EndOfFile = MessageFileStream.Read(FileBytes, 0, FileBytes.Length);
                            if (IsXSalsa20Poly1305 == true)
                            {
                                EncryptedFileBytes = SodiumSecretBox.Create(FileBytes, Nonce, Key);
                                CombinedEncryptedFileBytes = Nonce.Concat(EncryptedFileBytes).ToArray();
                            }
                            if (IsXChaCha20Poly1305 == true)
                            {
                                EncryptedFileBytes = SodiumSecretBoxXChaCha20Poly1305.Create(FileBytes, XChaCha20Poly1305Nonce, Key);
                                CombinedEncryptedFileBytes = XChaCha20Poly1305Nonce.Concat(EncryptedFileBytes).ToArray();
                            }
                            if (IsAES256GCM == true)
                            {
                                if (SodiumSecretAeadAES256GCM.IsAES256GCMAvailable() == true)
                                {
                                    EncryptedFileBytes = SodiumSecretAeadAES256GCM.Encrypt(FileBytes, AES256GCMNonce, Key);
                                    CombinedEncryptedFileBytes = AES256GCMNonce.Concat(EncryptedFileBytes).ToArray();
                                }
                                else
                                {
                                    GeneralChecker = false;
                                    break;
                                }
                            }
                            SignedCombinedEncryptedFileBytes = SodiumPublicKeyAuth.Sign(CombinedEncryptedFileBytes, UserECDSASK);
                        }
                    }
                }
                else
                {
                    FileBytes = new Byte[FileBytesChunkClass.MaximumChunksFileBytesLength];
                    EncryptedFileBytes = new Byte[] { };
                    CombinedEncryptedFileBytes = new Byte[] { };
                    if (LoopCount == 1)
                    {
                        EndOfFile = MessageFileStream.Read(FileBytes, 0, FileBytes.Length);
                        if (IsXSalsa20Poly1305 == true)
                        {
                            EncryptedFileBytes = SodiumSecretBox.Create(FileBytes, Nonce, Key);
                            CombinedEncryptedFileBytes = Nonce.Concat(EncryptedFileBytes).ToArray();
                        }
                        if (IsXChaCha20Poly1305 == true)
                        {
                            EncryptedFileBytes = SodiumSecretBoxXChaCha20Poly1305.Create(FileBytes, XChaCha20Poly1305Nonce, Key);
                            CombinedEncryptedFileBytes = XChaCha20Poly1305Nonce.Concat(EncryptedFileBytes).ToArray();
                        }
                        if (IsAES256GCM == true)
                        {
                            if (SodiumSecretAeadAES256GCM.IsAES256GCMAvailable() == true)
                            {
                                EncryptedFileBytes = SodiumSecretAeadAES256GCM.Encrypt(FileBytes, AES256GCMNonce, Key);
                                CombinedEncryptedFileBytes = AES256GCMNonce.Concat(EncryptedFileBytes).ToArray();
                            }
                            else
                            {
                                GeneralChecker = false;
                                break;
                            }
                        }
                        SignedCombinedEncryptedFileBytes = SodiumPublicKeyAuth.Sign(CombinedEncryptedFileBytes, UserECDSASK);
                    }
                    else
                    {
                        Nonce = new Byte[] { };
                        XChaCha20Poly1305Nonce = new Byte[] { };
                        AES256GCMNonce = new Byte[] { };
                        Nonce = SodiumSecretBox.GenerateNonce();
                        XChaCha20Poly1305Nonce = SodiumSecretBoxXChaCha20Poly1305.GenerateNonce();
                        AES256GCMNonce = SodiumSecretAeadAES256GCM.GeneratePublicNonce();
                        EndOfFile = MessageFileStream.Read(FileBytes, 0, FileBytes.Length);
                        if (IsXSalsa20Poly1305 == true)
                        {
                            EncryptedFileBytes = SodiumSecretBox.Create(FileBytes, Nonce, Key);
                            CombinedEncryptedFileBytes = Nonce.Concat(EncryptedFileBytes).ToArray();
                        }
                        if (IsXChaCha20Poly1305 == true)
                        {
                            EncryptedFileBytes = SodiumSecretBoxXChaCha20Poly1305.Create(FileBytes, XChaCha20Poly1305Nonce, Key);
                            CombinedEncryptedFileBytes = XChaCha20Poly1305Nonce.Concat(EncryptedFileBytes).ToArray();
                        }
                        if (IsAES256GCM == true)
                        {
                            if (SodiumSecretAeadAES256GCM.IsAES256GCMAvailable() == true)
                            {
                                EncryptedFileBytes = SodiumSecretAeadAES256GCM.Encrypt(FileBytes, AES256GCMNonce, Key);
                                CombinedEncryptedFileBytes = AES256GCMNonce.Concat(EncryptedFileBytes).ToArray();
                            }
                            else
                            {
                                GeneralChecker = false;
                                break;
                            }
                        }
                        SignedCombinedEncryptedFileBytes = SodiumPublicKeyAuth.Sign(CombinedEncryptedFileBytes, UserECDSASK);
                    }
                }
                if (progress != null)
                {
                    progress.Report(LoopCount);
                }
                ActualFileCount += 1;
                System.IO.File.WriteAllBytes(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName + "/FileContent" + ActualFileCount.ToString() + ".txt", SignedCombinedEncryptedFileBytes);
                System.IO.File.WriteAllBytes(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName + "/Key/Key" + LoopCount.ToString() + ".txt", Key);
                System.IO.File.WriteAllBytes(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName + "/ED25519SK/SK" + LoopCount.ToString() + ".txt", UserECDSASK);
                System.IO.File.WriteAllBytes(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName + "/ED25519PK/PK" + LoopCount.ToString() + ".txt", UserECDSAPK);
                SignedCombinedEncryptedFileBytes = new Byte[] { };
                SodiumSecureMemory.SecureClearBytes(Key);
                SodiumSecureMemory.SecureClearBytes(UserECDSASK);
                SodiumSecureMemory.SecureClearBytes(UserECDSAPK);
                MyKeyPair.Clear();
                MyKeyPair = SodiumPublicKeyAuth.GenerateRevampedKeyPair();
                LoopCount += 1;
            }
            if (GeneralChecker == true)
            {
                System.IO.File.WriteAllBytes(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName + "/FileName.txt", Encoding.UTF8.GetBytes(FileName));
            }
            MessageFileStream.Close();
        }

        public void GetFileStorageUsedSize(ref String Result)
        {
            Boolean ServerOnlineChecker = true;
            int FileStorageSize = 0;
            Decimal FileStorageSizeInKB = 0;
            Decimal FileStorageSizeInMB = 0;
            if (ETLSSessionIDStorage.ETLSID.CompareTo("") != 0 && ETLSSessionIDStorage.ETLSID != null)
            {
                using (var client = new HttpClient())
                {
                    if (APIIPAddressHelper.HasSet == true)
                    {
                        client.BaseAddress = new Uri(APIIPAddressHelper.IPAddress);
                    }
                    else
                    {
                        client.BaseAddress = new Uri("https://mrchewitsoftware.com.my:5001/api/");
                    }
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync("OwnerUploadFiles/GetOwnerFolderSize?FolderID=" + DirectoryIDTempStorage.DirectoryID);
                    try
                    {
                        response.Wait();
                    }
                    catch
                    {
                        ServerOnlineChecker = false;
                    }
                    if (ServerOnlineChecker == true)
                    {
                        var result = response.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsStringAsync();
                            readTask.Wait();

                            var StringResult = readTask.Result;
                            if (StringResult.Contains("Error"))
                            {
                                Console.WriteLine(StringResult);
                            }
                            else
                            {
                                StringResult = StringResult.Substring(1, StringResult.Length - 2);
                                FileStorageSize = int.Parse(StringResult);
                                FileStorageSizeInKB = FileStorageSize / 1024;
                                FileStorageSizeInMB = FileStorageSize / 1048576;
                                Result = StringResult + " bytes" + Environment.NewLine + FileStorageSizeInKB.ToString() + " kb" + Environment.NewLine + FileStorageSizeInMB.ToString() + " mb";
                            }
                        }
                        else
                        {
                            Console.WriteLine("Something went wrong with fetching values from server ...");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Server is now offline...");
                    }
                }
            }
            else
            {
                Console.WriteLine("You have not yet establish an ephemeral TLS session with the server..");
            }
        }

        public void RequestChallenge(ref Boolean Re_RequestRandomChallengeBoolean, ref String Base64ServerChallenge)
        {
            Re_RequestRandomChallengeBoolean = false;
            Byte[] ServerRandomChallenge = new Byte[] { };
            Byte[] ServerSignedRandomChallenge = new Byte[] { };
            Byte[] ServerED25519PK = new Byte[] { };
            Boolean ServerOnlineChecker = true;
            Boolean CheckServerED25519PK = true;
            LoginModels MyLoginModels = new LoginModels();
            if (ETLSSessionIDStorage.ETLSID.CompareTo("") != 0 && ETLSSessionIDStorage.ETLSID != null)
            {
                using (var client = new HttpClient())
                {
                    if (APIIPAddressHelper.HasSet == true)
                    {
                        client.BaseAddress = new Uri(APIIPAddressHelper.IPAddress);
                    }
                    else
                    {
                        client.BaseAddress = new Uri("https://mrchewitsoftware.com.my:5001/api/");
                    }
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync("Login/");
                    try
                    {
                        response.Wait();
                    }
                    catch
                    {
                        ServerOnlineChecker = false;
                    }
                    if (ServerOnlineChecker == true)
                    {
                        var result = response.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsStringAsync();
                            readTask.Wait();

                            var LoginModelsResult = readTask.Result;
                            MyLoginModels = JsonConvert.DeserializeObject<LoginModels>(LoginModelsResult);
                            if (MyLoginModels.RequestStatus.Contains("Error"))
                            {
                                Console.WriteLine(MyLoginModels.RequestStatus);
                            }
                            else
                            {
                                ServerSignedRandomChallenge = Convert.FromBase64String(MyLoginModels.SignedRandomChallengeBase64String);
                                ServerED25519PK = Convert.FromBase64String(MyLoginModels.ServerECDSAPKBase64String);
                                try
                                {
                                    ServerRandomChallenge = SodiumPublicKeyAuth.Verify(ServerSignedRandomChallenge, ServerED25519PK);
                                }
                                catch
                                {
                                    CheckServerED25519PK = false;
                                }
                                if (CheckServerED25519PK == true)
                                {
                                    Base64ServerChallenge = Convert.ToBase64String(ServerRandomChallenge);
                                    Re_RequestRandomChallengeBoolean = true;
                                    SodiumSecureMemory.SecureClearBytes(ServerSignedRandomChallenge);
                                    SodiumSecureMemory.SecureClearBytes(ServerRandomChallenge);
                                    SodiumSecureMemory.SecureClearBytes(ServerED25519PK);
                                }
                                else
                                {
                                    Console.WriteLine("Man in the middle spotted ..., proceeding with re-requesting random challenge from server...");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Something went wrong with fetching values from server ...");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Server is now offline...");
                    }
                }
            }
            else
            {
                Console.WriteLine("You have not yet establish an ephemeral TLS session with the server..");
            }
        }

        public async Task<bool> UploadFileAsync(String DirectoryID, String Random_File_Name ,Byte[] Challenge, int Count)
        {
            String RootDirectory = AppContext.BaseDirectory;
            String UniqueFileName = Random_File_Name;
            Byte[] UniqueFileNameByte = Encoding.UTF8.GetBytes(UniqueFileName);
            Byte[] ETLSSignedUniqueFileNameByte = new Byte[] { };
            Byte[] CipheredSignedFileContent = new Byte[] { };
            Byte[] DirectoryIDByte = new Byte[] { };
            Byte[] CipheredDirectoryIDByte = new Byte[] { };
            Byte[] CombinedCipheredDirectoryIDByte = new Byte[] { };
            Byte[] ETLSSignedCombinedCipheredDirectoryIDByte = new Byte[] { };
            Byte[] ClientECDSASK = new Byte[] { };
            Byte[] UserECDSASK = new Byte[] { };
            Byte[] SharedSecret = new Byte[] { };
            Byte[] UserSignedRandomChallenge = new Byte[] { };
            Byte[] ETLSSignedUserSignedRandomChallenge = new Byte[] { };
            Byte[] NonceByte = new Byte[] { };
            DirectoryIDByte = Encoding.UTF8.GetBytes(DirectoryID);
            String JSONBodyString = "";
            UploadFilesModel FilesModel = new UploadFilesModel();
            if (ETLSSessionIDStorage.ETLSID.CompareTo("") != 0 && ETLSSessionIDStorage.ETLSID != null)
            {
                ClientECDSASK = System.IO.File.ReadAllBytes(RootDirectory + "/Temp_Session/" + ETLSSessionIDStorage.ETLSID + "/" + "ECDSASK.txt");
                UserECDSASK = System.IO.File.ReadAllBytes(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/rootSK.txt");
                SharedSecret = System.IO.File.ReadAllBytes(RootDirectory + "/Temp_Session/" + ETLSSessionIDStorage.ETLSID + "/" + "SharedSecret.txt");
                NonceByte = SodiumSecretBox.GenerateNonce();
                CipheredDirectoryIDByte = SodiumSecretBox.Create(DirectoryIDByte, NonceByte, SharedSecret, true);
                CombinedCipheredDirectoryIDByte = NonceByte.Concat(CipheredDirectoryIDByte).ToArray();
                ETLSSignedCombinedCipheredDirectoryIDByte = SodiumPublicKeyAuth.Sign(CombinedCipheredDirectoryIDByte, ClientECDSASK);
                ETLSSignedUniqueFileNameByte = SodiumPublicKeyAuth.Sign(UniqueFileNameByte, ClientECDSASK);
                CipheredSignedFileContent = System.IO.File.ReadAllBytes(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + UniqueFileName + "/FileContent" + Count.ToString() + ".txt");
                UserSignedRandomChallenge = SodiumPublicKeyAuth.Sign(Challenge, UserECDSASK, true);
                ETLSSignedUserSignedRandomChallenge = SodiumPublicKeyAuth.Sign(UserSignedRandomChallenge, ClientECDSASK, true);
                FilesModel.ClientPathID = ETLSSessionIDStorage.ETLSID;
                FilesModel.CipheredSignedDirectoryID = System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredDirectoryIDByte));
                FilesModel.SignedSignedRandomChallenge = System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserSignedRandomChallenge));
                FilesModel.SignedUniqueFileName = System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUniqueFileNameByte));
                FilesModel.CipheredSignedFileContent = Convert.ToBase64String(CipheredSignedFileContent);
                JSONBodyString = JsonConvert.SerializeObject(FilesModel);
                StringContent PostRequestData = new StringContent(JSONBodyString, Encoding.UTF8, "application/json");
                using (var client = new HttpClient())
                {
                    if (APIIPAddressHelper.HasSet == true)
                    {
                        client.BaseAddress = new Uri(APIIPAddressHelper.IPAddress);
                    }
                    else
                    {
                        client.BaseAddress = new Uri("https://mrchewitsoftware.com.my:5001/api/");
                    }
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await client.PostAsync("OwnerUploadFiles", PostRequestData);

                    if (response.IsSuccessStatusCode == true)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        if (result != null)
                        {
                            result = result.Substring(1, result.Length - 2);
                            if (result.CompareTo("Successed: File has been uploaded..") == 0)
                            {
                                return true;
                            }
                            else
                            {
                                Console.WriteLine(result);
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        public void ReloadFiles()
        {
            String RootDirectory = AppContext.BaseDirectory;
            String[] FileNamesFullPathArray = Directory.GetFiles(RootDirectory + "/Files_To_Encrypt/");
            String[] FileNamesArray = new string[FileNamesFullPathArray.Length];
            int Count = 0;
            int RootDirectoryCount = 0;
            RootDirectoryCount = (RootDirectory + "/Files_To_Encrypt/").Length;
            while (Count < FileNamesFullPathArray.Length)
            {
                FileNamesArray[Count] = FileNamesFullPathArray[Count].Remove(0, RootDirectoryCount);
                Count += 1;
            }
            ListOfFileNames = FileNamesArray;
        }
    }
}
