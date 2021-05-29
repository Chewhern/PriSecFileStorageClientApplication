using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using ASodium;
using System.IO;
using System.Runtime.InteropServices;
using System.Web;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PriSecFileStorageClient
{
    public partial class OwnerUploadFile : Form
    {
        private SecureIDGenerator IDGenerator = new SecureIDGenerator();
        private Thread EncryptionThread;
        private Thread UploadThread;


        public OwnerUploadFile()
        {
            InitializeComponent();
        }



        private void OwnerUploadFile_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }

        private void UploadServerBTN_Click(object sender, EventArgs e)
        {
            UploadThread = new Thread(BackGroundUpload);
            UploadThread.Start();
        }

        private void ChooseFileBTN_Click(object sender, EventArgs e)
        {
            PlainTextFileChooserDialog.ShowDialog();
        }

        private void PlainTextFileChooserDialog_FileOk(object sender, CancelEventArgs e)
        {
            FileNameWithPathTB.Text = PlainTextFileChooserDialog.FileName;
            int FileSize = File.ReadAllBytes(PlainTextFileChooserDialog.FileName).Length;
            FileSizeTB.Text = FileSize.ToString();
        }

        private void EncryptBTN_Click(object sender, EventArgs e)
        {
            if(FileSizeTB.Text!=null && FileSizeTB.Text.CompareTo("") != 0 && DirectoryIDTempStorage.DirectoryID!=null && DirectoryIDTempStorage.DirectoryID.CompareTo("")!=0 && UserIDTempStorage.UserID!=null && UserIDTempStorage.UserID.CompareTo("")!=0) 
            {                
                int FileSize = int.Parse(FileSizeTB.Text);
                String DirectoryID = DirectoryIDTempStorage.DirectoryID;
                String FileName = PlainTextFileChooserDialog.SafeFileName;
                String RandomFileName = IDGenerator.GenerateUniqueString();
                RevampedKeyPair MyKeyPair = SodiumPublicKeyAuth.GenerateRevampedKeyPair();
                Byte[] Nonce = SodiumSecretBox.GenerateNonce();
                Byte[] Key = SodiumSecretBox.GenerateKey();
                Byte[] FileBytes = new Byte[] { };
                Byte[] EncryptedFileBytes = new Byte[] { };
                Byte[] CombinedEncryptedFileBytes = new Byte[] { };
                Byte[] LocalSignedCombinedEncryptedFileBytes = new Byte[] { };
                Byte[] UserECDSASK = new Byte[] { };
                int FileCount = 0;
                int ActualFileCount = 0;
                GCHandle MyGeneralGCHandle = new GCHandle();
                EncryptionThread = new Thread(BackGroundEncrypt);
                if (FileSize<= 1073741824) 
                {
                    if(Directory.Exists(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID) == true) 
                    {
                        if(Directory.Exists(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files") == false) 
                        {
                            Directory.CreateDirectory(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files");
                        }
                        if (RandomFileName.Length > 45) 
                        {
                            RandomFileName = RandomFileName.Substring(0, 20);
                        }
                        if(Directory.Exists(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + RandomFileName) == true) 
                        {
                            while(Directory.Exists(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + RandomFileName) == true) 
                            {
                                RandomFileName = IDGenerator.GenerateUniqueString();
                                if (RandomFileName.Length > 45)
                                {
                                    RandomFileName = RandomFileName.Substring(0, 20);
                                }
                            }
                            Directory.CreateDirectory(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + RandomFileName);
                        }
                        else 
                        {
                            Directory.CreateDirectory(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + RandomFileName);
                        }
                        RandomFileNameTB.Text = RandomFileName;
                        if (Directory.Exists(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + RandomFileName+ "\\ED25519SK") ==false) 
                        {
                            Directory.CreateDirectory(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + RandomFileName + "\\ED25519SK");
                        }
                        if (Directory.Exists(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + RandomFileName + "\\ED25519PK")==false) 
                        {
                            Directory.CreateDirectory(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + RandomFileName + "\\ED25519PK");
                        }
                        if (Directory.Exists(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + RandomFileName + "\\Key")==false) 
                        {
                            Directory.CreateDirectory(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + RandomFileName + "\\Key");
                        }
                        File.WriteAllBytes(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + RandomFileName + "\\ED25519SK\\" +"SK1.txt", MyKeyPair.PrivateKey);
                        File.WriteAllBytes(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + RandomFileName + "\\ED25519PK\\" +"PK1.txt", MyKeyPair.PublicKey);
                        File.WriteAllBytes(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + RandomFileName + "\\Key\\"+"Key1.txt", Key);
                        MyKeyPair.Clear();
                        MyGeneralGCHandle = GCHandle.Alloc(Key, GCHandleType.Pinned);
                        SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), Key.Length);
                        MyGeneralGCHandle.Free();
                        if (FileSize <= FileBytesChunkClass.MaximumChunksFileBytesLength)
                        {
                            Key = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + RandomFileName + "\\Key\\Key1.txt");
                            UserECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + RandomFileName + "\\ED25519SK\\SK1.txt");
                            FileBytes = File.ReadAllBytes(PlainTextFileChooserDialog.FileName);
                            EncryptedFileBytes = SodiumSecretBox.Create(FileBytes, Nonce, Key);
                            CombinedEncryptedFileBytes = Nonce.Concat(EncryptedFileBytes).ToArray();
                            LocalSignedCombinedEncryptedFileBytes = SodiumPublicKeyAuth.Sign(CombinedEncryptedFileBytes, UserECDSASK);
                            File.WriteAllBytes(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + RandomFileName + "\\FileName.txt",Encoding.UTF8.GetBytes(FileName));
                            FileCount = Directory.GetFiles(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + RandomFileName).Length;
                            ActualFileCount = FileCount - 4;
                            ActualFileCount += 1;
                            File.WriteAllBytes(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + RandomFileName + "\\FileContent"+ActualFileCount.ToString()+".txt", LocalSignedCombinedEncryptedFileBytes);
                            EncryptionProgressBar.Value = 100;
                            MyGeneralGCHandle = GCHandle.Alloc(Key, GCHandleType.Pinned);
                            SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), Key.Length);
                            MyGeneralGCHandle.Free();
                        }
                        else
                        {
                            EncryptionThread.Start();
                            MyGeneralGCHandle = GCHandle.Alloc(Key, GCHandleType.Pinned);
                            SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), Key.Length);
                            MyGeneralGCHandle.Free();
                        }
                    }
                    else 
                    {
                        MessageBox.Show("Error you haven't have a directory that rented from the provider","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                }
                else 
                {
                    MessageBox.Show("File Size must not be greater than 1 GB", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else 
            {
                MessageBox.Show("You haven't login or choose a file yet","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        public void BackGroundUpload()
        {
            if (ETLSSessionIDStorage.ETLSID != null && ETLSSessionIDStorage.ETLSID.CompareTo("") != 0)
            {
                if (UserIDTempStorage.UserID != null && UserIDTempStorage.UserID.CompareTo("") != 0)
                {
                    String DirectoryID = DirectoryIDTempStorage.DirectoryID;
                    String RandomFileName = RandomFileNameTB.Text;
                    int FileCount = 0;
                    int ActualFileCount = 0;
                    int LoopCount = 1;
                    String Base64Challenge = "";
                    Byte[] Challenge = new Byte[] { };
                    Boolean RequestChallengeErrorChecking = false;
                    Boolean UploadFileErrorChecking = false;
                    Boolean TempBoolean = false;
                    if (DirectoryID != null && DirectoryID.CompareTo("") != 0 && RandomFileName != null && RandomFileName.CompareTo("") != 0)
                    {
                        if (Directory.Exists(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + RandomFileName) == true)
                        {
                            FileCount = Directory.GetFileSystemEntries(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + RandomFileName).Length;
                            ActualFileCount = FileCount - 4;
                            UploadProgressBar.Maximum = ActualFileCount;
                            UploadProgressBar.Step = 1;
                            while (LoopCount <= ActualFileCount)
                            {
                                RequestChallenge(ref Base64Challenge);
                                if (Base64Challenge != null && Base64Challenge.CompareTo("") != 0)
                                {
                                    Challenge = Convert.FromBase64String(Base64Challenge);
                                    TempBoolean = UploadFileAsync(DirectoryID, Challenge, LoopCount).Result;
                                    if (TempBoolean==false && UploadFileErrorChecking == false)
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
                                    UploadProgressBar.PerformStep();
                                }
                                Base64Challenge = "";
                                Challenge = new Byte[] { };
                                LoopCount += 1;
                            }
                            if (RequestChallengeErrorChecking == true)
                            {
                                MessageBox.Show("Error requesting challenge from server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            if (UploadFileErrorChecking == true)
                            {
                                MessageBox.Show("Error uploading file to server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            if (UploadFileErrorChecking == false && RequestChallengeErrorChecking == false)
                            {
                                MessageBox.Show("File successfully uploaded to server by chunks/parts, please keep your key safe and private", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                MessageBox.Show("For the encrypted file that you uploaded, please note down the corresponding file name that links to random file name sent to server.", "Crucial Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Directory ID and Random file name does not exists..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("You have not yet register/login an account", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("You have not yet register/login an account", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("You have not yet establish an ephemeral TLS session", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            UploadThread.Abort();
        }

        public void BackGroundEncrypt() 
        {
            int FileSize = int.Parse(FileSizeTB.Text);
            String DirectoryID = DirectoryIDTempStorage.DirectoryID;
            String FileName = PlainTextFileChooserDialog.SafeFileName;
            String RandomFileName = RandomFileNameTB.Text;
            Boolean HasRemainder = false;
            Byte[] Nonce = SodiumSecretBox.GenerateNonce();
            Byte[] Key = new Byte[] { };
            Byte[] FileBytes = new Byte[] { };
            Byte[] EncryptedFileBytes = new Byte[] { };
            Byte[] CombinedEncryptedFileBytes = new Byte[] { };
            Byte[] SignedCombinedEncryptedFileBytes = new Byte[] { };
            Byte[] UserECDSASK = new Byte[] { };
            Byte[] UserECDSAPK = new Byte[] { };
            GCHandle MyGeneralGCHandle = new GCHandle();
            FileStream MessageFileStream = File.OpenRead(PlainTextFileChooserDialog.FileName);
            int Count = 0;
            int Remainder = 0;
            int LoopCount = 1;
            int EndOfFile = 0;
            int FileCount = 0;
            int ActualFileCount = 0;
            RevampedKeyPair MyKeyPair = SodiumPublicKeyAuth.GenerateRevampedKeyPair();
            if (Directory.Exists(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files") == false)
            {
                Directory.CreateDirectory(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files");
            }
            Count = FileSize / FileBytesChunkClass.MaximumChunksFileBytesLength;
            Remainder = FileSize % FileBytesChunkClass.MaximumChunksFileBytesLength;
            if (Remainder != 0)
            {
                HasRemainder = true;
                Count += 1;
            }
            EncryptionProgressBar.Maximum = Count;
            EncryptionProgressBar.Step = 1;
            File.WriteAllBytes(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + RandomFileName + "\\FileName.txt", Encoding.UTF8.GetBytes(FileName));
            FileCount = Directory.GetFileSystemEntries(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + RandomFileName).Length;
            ActualFileCount = FileCount - 4;
            while (LoopCount <= Count)
            {
                if (LoopCount == 1) 
                {
                    Key = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + RandomFileName + "\\Key\\Key1.txt");
                    UserECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + RandomFileName + "\\ED25519SK\\SK1.txt");
                    UserECDSAPK = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + RandomFileName + "\\ED25519PK\\PK1.txt");
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
                            EncryptedFileBytes = SodiumSecretBox.Create(FileBytes, Nonce, Key);
                            CombinedEncryptedFileBytes = Nonce.Concat(EncryptedFileBytes).ToArray();
                            SignedCombinedEncryptedFileBytes = SodiumPublicKeyAuth.Sign(CombinedEncryptedFileBytes, UserECDSASK);
                        }
                        else
                        {
                            FileBytes = new Byte[FileBytesChunkClass.MaximumChunksFileBytesLength];
                            EncryptedFileBytes = new Byte[] { };
                            CombinedEncryptedFileBytes = new Byte[] { };
                            EndOfFile = MessageFileStream.Read(FileBytes, 0, FileBytes.Length);
                            EncryptedFileBytes = SodiumSecretBox.Create(FileBytes, Nonce, Key);
                            CombinedEncryptedFileBytes = Nonce.Concat(EncryptedFileBytes).ToArray();
                            SignedCombinedEncryptedFileBytes = SodiumPublicKeyAuth.Sign(CombinedEncryptedFileBytes, UserECDSASK);
                        }
                    }
                    else
                    {
                        if (LoopCount == Count)
                        {
                            FileBytes = new Byte[Remainder];
                            EncryptedFileBytes = new Byte[] { };
                            CombinedEncryptedFileBytes = new Byte[] { };
                            EndOfFile = MessageFileStream.Read(FileBytes, 0, FileBytes.Length);
                            EncryptedFileBytes = SodiumSecretBox.Create(FileBytes, Nonce, Key);
                            CombinedEncryptedFileBytes = Nonce.Concat(EncryptedFileBytes).ToArray();
                            SignedCombinedEncryptedFileBytes = SodiumPublicKeyAuth.Sign(CombinedEncryptedFileBytes, UserECDSASK);
                        }
                        else
                        {
                            FileBytes = new Byte[FileBytesChunkClass.MaximumChunksFileBytesLength];
                            EncryptedFileBytes = new Byte[] { };
                            CombinedEncryptedFileBytes = new Byte[] { };
                            EndOfFile = MessageFileStream.Read(FileBytes, 0, FileBytes.Length);
                            EncryptedFileBytes = SodiumSecretBox.Create(FileBytes, Nonce, Key);
                            CombinedEncryptedFileBytes = Nonce.Concat(EncryptedFileBytes).ToArray();
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
                        EncryptedFileBytes = SodiumSecretBox.Create(FileBytes, Nonce, Key);
                        CombinedEncryptedFileBytes = Nonce.Concat(EncryptedFileBytes).ToArray();
                        SignedCombinedEncryptedFileBytes = SodiumPublicKeyAuth.Sign(CombinedEncryptedFileBytes, UserECDSASK);
                    }
                    else
                    {
                        Nonce = SodiumSecretBox.GenerateNonce();
                        EndOfFile = MessageFileStream.Read(FileBytes, 0, FileBytes.Length);
                        EncryptedFileBytes = SodiumSecretBox.Create(FileBytes, Nonce, Key);
                        CombinedEncryptedFileBytes = Nonce.Concat(EncryptedFileBytes).ToArray();
                        SignedCombinedEncryptedFileBytes = SodiumPublicKeyAuth.Sign(CombinedEncryptedFileBytes, UserECDSASK);
                    }
                }
                ActualFileCount += 1;
                File.WriteAllBytes(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + RandomFileName + "\\FileContent" + ActualFileCount.ToString() + ".txt", SignedCombinedEncryptedFileBytes);
                File.WriteAllBytes(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + RandomFileName + "\\Key\\Key"+LoopCount.ToString()+".txt",Key);
                File.WriteAllBytes(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + RandomFileName + "\\ED25519SK\\SK" +LoopCount.ToString() + ".txt", UserECDSASK);
                File.WriteAllBytes(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + RandomFileName + "\\ED25519PK\\PK" + LoopCount.ToString() + ".txt", UserECDSAPK);
                EncryptionProgressBar.PerformStep();
                SignedCombinedEncryptedFileBytes = new Byte[] { };
                MyGeneralGCHandle = GCHandle.Alloc(Key, GCHandleType.Pinned);
                SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), Key.Length);
                MyGeneralGCHandle.Free();
                MyKeyPair.Clear();
                MyKeyPair = SodiumPublicKeyAuth.GenerateRevampedKeyPair();
                LoopCount += 1;
            }
            File.WriteAllBytes(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + RandomFileName + "\\FileName.txt", Encoding.UTF8.GetBytes(FileName));
            RandomFileNameTB.Text = RandomFileName;
            MyGeneralGCHandle = GCHandle.Alloc(Key, GCHandleType.Pinned);
            SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), Key.Length);
            MyGeneralGCHandle.Free();
            MessageFileStream.Close();
            EncryptionThread.Abort();
        }

        public void RequestChallenge(ref String Base64ServerChallenge)
        {
            String AuthenticationType = "Miscellaneous";
            Byte[] ServerRandomChallenge = new Byte[] { };
            Byte[] ServerSignedRandomChallenge = new Byte[] { };
            Byte[] ServerED25519PK = new Byte[] { };
            Byte[] UserIDByte = new Byte[] { };
            Byte[] ETLSSignedUserIDByte = new Byte[] { };
            Byte[] AuthenticationTypeByte = new Byte[] { };
            Byte[] ETLSSignedAuthenticationTypeByte = new Byte[] { };
            Byte[] ClientECDSASK = new Byte[] { };
            Boolean ServerOnlineChecker = true;
            Boolean CheckServerED25519PK = true;
            Boolean Re_RequestRandomChallengeBoolean = true;
            LoginModels MyLoginModels = new LoginModels();
            GCHandle MyGeneralGCHandle;
            if (ETLSSessionIDStorage.ETLSID.CompareTo("") != 0 && ETLSSessionIDStorage.ETLSID != null)
            {
                if (UserIDTempStorage.UserID != null && UserIDTempStorage.UserID.CompareTo("") != 0)
                {
                    UserIDByte = Encoding.UTF8.GetBytes(UserIDTempStorage.UserID);
                    AuthenticationTypeByte = Encoding.UTF8.GetBytes(AuthenticationType);
                    ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "ECDSASK.txt");
                    ETLSSignedUserIDByte = SodiumPublicKeyAuth.Sign(UserIDByte, ClientECDSASK);
                    ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "ECDSASK.txt");
                    ETLSSignedAuthenticationTypeByte = SodiumPublicKeyAuth.Sign(AuthenticationTypeByte, ClientECDSASK);
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("{API Base Url}");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.GetAsync("Login/RequestBy?ClientPathID=" + ETLSSessionIDStorage.ETLSID + "&SignedUserID=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserIDByte)) + "&SignedAuthenticationType=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedAuthenticationTypeByte)));
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
                                    MessageBox.Show("Something went wrong when requesting random challenge...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    MessageBox.Show("Request Error Message: " + MyLoginModels.RequestStatus, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                                        MyGeneralGCHandle = GCHandle.Alloc(ClientECDSASK, GCHandleType.Pinned);
                                        SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), ClientECDSASK.Length);
                                        MyGeneralGCHandle.Free();
                                        MyGeneralGCHandle = GCHandle.Alloc(ServerSignedRandomChallenge, GCHandleType.Pinned);
                                        SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), ServerSignedRandomChallenge.Length);
                                        MyGeneralGCHandle.Free();
                                        MyGeneralGCHandle = GCHandle.Alloc(ServerRandomChallenge, GCHandleType.Pinned);
                                        SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), ServerRandomChallenge.Length);
                                        MyGeneralGCHandle.Free();
                                        MyGeneralGCHandle = GCHandle.Alloc(ServerED25519PK, GCHandleType.Pinned);
                                        SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), ServerED25519PK.Length);
                                        MyGeneralGCHandle.Free();
                                        MyGeneralGCHandle = GCHandle.Alloc(ETLSSignedUserIDByte, GCHandleType.Pinned);
                                        SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), ETLSSignedUserIDByte.Length);
                                        MyGeneralGCHandle.Free();
                                        MyGeneralGCHandle = GCHandle.Alloc(ETLSSignedAuthenticationTypeByte, GCHandleType.Pinned);
                                        SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), ETLSSignedAuthenticationTypeByte.Length);
                                        MyGeneralGCHandle.Free();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Man in the middle spotted ..., proceeding with re-requesting random challenge from server...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        Re_RequestRandomChallenge(ref Re_RequestRandomChallengeBoolean, ref Base64ServerChallenge);
                                        while (Re_RequestRandomChallengeBoolean == false)
                                        {
                                            Re_RequestRandomChallenge(ref Re_RequestRandomChallengeBoolean, ref Base64ServerChallenge);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Something went wrong with fetching values from server ...", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Server is now offline...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You have not yet login/register ....", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("You have not yet establish an ephemeral TLS session with the server..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Re_RequestRandomChallenge(ref Boolean RefBoolean, ref String Base64ServerChallenge)
        {
            String AuthenticationType = "Miscellaneous";
            Byte[] ServerRandomChallenge = new Byte[] { };
            Byte[] ServerSignedRandomChallenge = new Byte[] { };
            Byte[] ServerED25519PK = new Byte[] { };
            Byte[] UserIDByte = new Byte[] { };
            Byte[] ETLSSignedUserIDByte = new Byte[] { };
            Byte[] AuthenticationTypeByte = new Byte[] { };
            Byte[] ETLSSignedAuthenticationTypeByte = new Byte[] { };
            Byte[] ClientECDSASK = new Byte[] { };
            Boolean ServerOnlineChecker = true;
            Boolean CheckServerED25519PK = true;
            LoginModels MyLoginModels = new LoginModels();
            GCHandle MyGeneralGCHandle;
            if (ETLSSessionIDStorage.ETLSID.CompareTo("") != 0 && ETLSSessionIDStorage.ETLSID != null)
            {
                if (UserIDTempStorage.UserID != null && UserIDTempStorage.UserID.CompareTo("") != 0)
                {
                    UserIDByte = Encoding.UTF8.GetBytes(UserIDTempStorage.UserID);
                    AuthenticationTypeByte = Encoding.UTF8.GetBytes(AuthenticationType);
                    ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "ECDSASK.txt");
                    ETLSSignedUserIDByte = SodiumPublicKeyAuth.Sign(UserIDByte, ClientECDSASK);
                    ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "ECDSASK.txt");
                    ETLSSignedAuthenticationTypeByte = SodiumPublicKeyAuth.Sign(AuthenticationTypeByte, ClientECDSASK);
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("{API Base Url}");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.GetAsync("Login/RequestBy?ClientPathID=" + ETLSSessionIDStorage.ETLSID + "&SignedUserID=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserIDByte)) + "&SignedAuthenticationType=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedAuthenticationTypeByte)));
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
                                    MessageBox.Show("Something went wrong when requesting random challenge...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    MessageBox.Show("Request Error Message: " + MyLoginModels.RequestStatus, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                                        MyGeneralGCHandle = GCHandle.Alloc(ClientECDSASK, GCHandleType.Pinned);
                                        SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), ClientECDSASK.Length);
                                        MyGeneralGCHandle.Free();
                                        MyGeneralGCHandle = GCHandle.Alloc(ServerSignedRandomChallenge, GCHandleType.Pinned);
                                        SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), ServerSignedRandomChallenge.Length);
                                        MyGeneralGCHandle.Free();
                                        MyGeneralGCHandle = GCHandle.Alloc(ServerRandomChallenge, GCHandleType.Pinned);
                                        SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), ServerRandomChallenge.Length);
                                        MyGeneralGCHandle.Free();
                                        MyGeneralGCHandle = GCHandle.Alloc(ServerED25519PK, GCHandleType.Pinned);
                                        SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), ServerED25519PK.Length);
                                        MyGeneralGCHandle.Free();
                                        MyGeneralGCHandle = GCHandle.Alloc(ETLSSignedUserIDByte, GCHandleType.Pinned);
                                        SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), ETLSSignedUserIDByte.Length);
                                        MyGeneralGCHandle.Free();
                                        MyGeneralGCHandle = GCHandle.Alloc(ETLSSignedAuthenticationTypeByte, GCHandleType.Pinned);
                                        SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), ETLSSignedAuthenticationTypeByte.Length);
                                        MyGeneralGCHandle.Free();
                                        RefBoolean = true;
                                    }
                                    else
                                    {
                                        RefBoolean = false;
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Something went wrong with fetching values from server ...", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Server is now offline...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You have not yet login/register...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("You have not yet establish an ephemeral TLS session with the server..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async Task<bool> UploadFileAsync(String DirectoryID,Byte[] Challenge,int Count)
        {
            if (RandomFileNameTB.Text!=null && RandomFileNameTB.Text.CompareTo("")!=0 && DirectoryID != null && DirectoryID.CompareTo("") != 0 && Challenge.Length != 0 && Count!=0)
            {
                String UniqueFileName = RandomFileNameTB.Text;
                Byte[] UniqueFileNameByte = Encoding.UTF8.GetBytes(UniqueFileName);
                Byte[] ETLSSignedUniqueFileNameByte = new Byte[] { };
                Byte[] CipheredSignedFileContent = new Byte[] { };
                Byte[] ETLSSignedCipheredSignedFileContent = new Byte[] { };
                Byte[] DirectoryIDByte = new Byte[] { };
                Byte[] CipheredDirectoryIDByte = new Byte[] { };
                Byte[] CombinedCipheredDirectoryIDByte = new Byte[] { };
                Byte[] ETLSSignedCombinedCipheredDirectoryIDByte = new Byte[] { };
                Byte[] ClientECDSASK = new Byte[] { };
                Byte[] UserECDSASK = new Byte[] { };
                Byte[] SharedSecret = new Byte[] { };
                Byte[] UserIDByte = new Byte[] { };
                Byte[] ETLSSignedUserIDByte = new Byte[] { };
                Byte[] UserSignedRandomChallenge = new Byte[] { };
                Byte[] ETLSSignedUserSignedRandomChallenge = new Byte[] { };
                Byte[] NonceByte = new Byte[] { };
                GCHandle MyGeneralGCHandle = new GCHandle();
                DirectoryIDByte = Encoding.UTF8.GetBytes(DirectoryID);
                String JSONBodyString = "";
                UploadFilesModel FilesModel = new UploadFilesModel();
                if (ETLSSessionIDStorage.ETLSID.CompareTo("") != 0 && ETLSSessionIDStorage.ETLSID != null)
                {
                    if (UserIDTempStorage.UserID != null && UserIDTempStorage.UserID.CompareTo("") != 0)
                    {
                        ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "ECDSASK.txt");
                        UserECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\" + "User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\rootSK.txt");
                        SharedSecret = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "SharedSecret.txt");
                        UserIDByte = Encoding.UTF8.GetBytes(UserIDTempStorage.UserID);
                        ETLSSignedUserIDByte = SodiumPublicKeyAuth.Sign(UserIDByte, ClientECDSASK);
                        NonceByte = SodiumSecretBox.GenerateNonce();
                        ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "ECDSASK.txt");
                        CipheredDirectoryIDByte = SodiumSecretBox.Create(DirectoryIDByte, NonceByte, SharedSecret);
                        CombinedCipheredDirectoryIDByte = NonceByte.Concat(CipheredDirectoryIDByte).ToArray();
                        ETLSSignedCombinedCipheredDirectoryIDByte = SodiumPublicKeyAuth.Sign(CombinedCipheredDirectoryIDByte, ClientECDSASK);
                        ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "ECDSASK.txt");
                        ETLSSignedUniqueFileNameByte = SodiumPublicKeyAuth.Sign(UniqueFileNameByte, ClientECDSASK);
                        ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "ECDSASK.txt");
                        CipheredSignedFileContent = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + UniqueFileName + "\\FileContent" + Count.ToString() + ".txt");
                        ETLSSignedCipheredSignedFileContent = SodiumPublicKeyAuth.Sign(CipheredSignedFileContent, ClientECDSASK);
                        ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "ECDSASK.txt");
                        UserSignedRandomChallenge = SodiumPublicKeyAuth.Sign(Challenge, UserECDSASK);
                        ETLSSignedUserSignedRandomChallenge = SodiumPublicKeyAuth.Sign(UserSignedRandomChallenge, ClientECDSASK);
                        FilesModel.ClientPathID = ETLSSessionIDStorage.ETLSID;
                        FilesModel.SignedUserID = HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserIDByte));
                        FilesModel.CipheredSignedDirectoryID = HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredDirectoryIDByte));
                        FilesModel.SignedSignedRandomChallenge = HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserSignedRandomChallenge));
                        FilesModel.SignedUniqueFileName = HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUniqueFileNameByte));
                        FilesModel.SignedCipheredSignedFileContent = HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCipheredSignedFileContent));
                        JSONBodyString = JsonConvert.SerializeObject(FilesModel);
                        StringContent PostRequestData = new StringContent(JSONBodyString, Encoding.UTF8, "application/json");
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri("{API Base Url}");
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(
                                new MediaTypeWithQualityHeaderValue("application/json"));
                            var response = await client.PostAsync("OwnerUploadFiles", PostRequestData);
                            
                            if (response.IsSuccessStatusCode == true)
                            {
                                var result = response.Content.ReadAsStringAsync().Result;
                                if (result!=null) 
                                {
                                    result = result.Substring(1, result.Length - 2);
                                    if (result.CompareTo("Successed: File has been uploaded..") ==0) 
                                    {
                                        MyGeneralGCHandle = GCHandle.Alloc(ClientECDSASK, GCHandleType.Pinned);
                                        SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), ClientECDSASK.Length);
                                        MyGeneralGCHandle.Free();
                                        MyGeneralGCHandle = GCHandle.Alloc(SharedSecret, GCHandleType.Pinned);
                                        SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), SharedSecret.Length);
                                        MyGeneralGCHandle.Free();
                                        MyGeneralGCHandle = GCHandle.Alloc(UserECDSASK, GCHandleType.Pinned);
                                        SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), UserECDSASK.Length);
                                        MyGeneralGCHandle.Free();
                                        return true;
                                    }
                                    else 
                                    {
                                        MessageBox.Show(result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        MyGeneralGCHandle = GCHandle.Alloc(ClientECDSASK, GCHandleType.Pinned);
                                        SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), ClientECDSASK.Length);
                                        MyGeneralGCHandle.Free();
                                        MyGeneralGCHandle = GCHandle.Alloc(SharedSecret, GCHandleType.Pinned);
                                        SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), SharedSecret.Length);
                                        MyGeneralGCHandle.Free();
                                        MyGeneralGCHandle = GCHandle.Alloc(UserECDSASK, GCHandleType.Pinned);
                                        SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), UserECDSASK.Length);
                                        MyGeneralGCHandle.Free();
                                        return false;
                                    }
                                }
                                else 
                                {
                                    MyGeneralGCHandle = GCHandle.Alloc(ClientECDSASK, GCHandleType.Pinned);
                                    SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), ClientECDSASK.Length);
                                    MyGeneralGCHandle.Free();
                                    MyGeneralGCHandle = GCHandle.Alloc(SharedSecret, GCHandleType.Pinned);
                                    SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), SharedSecret.Length);
                                    MyGeneralGCHandle.Free();
                                    MyGeneralGCHandle = GCHandle.Alloc(UserECDSASK, GCHandleType.Pinned);
                                    SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), UserECDSASK.Length);
                                    MyGeneralGCHandle.Free();
                                    return false;
                                }
                            }
                            else
                            {
                                MyGeneralGCHandle = GCHandle.Alloc(ClientECDSASK, GCHandleType.Pinned);
                                SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), ClientECDSASK.Length);
                                MyGeneralGCHandle.Free();
                                MyGeneralGCHandle = GCHandle.Alloc(SharedSecret, GCHandleType.Pinned);
                                SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), SharedSecret.Length);
                                MyGeneralGCHandle.Free();
                                MyGeneralGCHandle = GCHandle.Alloc(UserECDSASK, GCHandleType.Pinned);
                                SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), UserECDSASK.Length);
                                MyGeneralGCHandle.Free();
                                return false;
                            }
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
            else
            {
                return false;
            }
        }
    }
}
