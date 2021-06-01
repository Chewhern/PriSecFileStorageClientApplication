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
using System.Runtime.InteropServices;
using ASodium;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using Newtonsoft.Json;

namespace PriSecFileStorageClient
{
    public partial class OwnerDeleteFileContent : Form
    {
        public OwnerDeleteFileContent()
        {
            InitializeComponent();
        }

        private void OwnerDeleteFileContent_Load(object sender, EventArgs e)
        {
            String[] RandomFileIDFullPathArray = Directory.GetDirectories(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\Encrypted_Files");
            String[] RandomFileIDArray = new string[RandomFileIDFullPathArray.Length];
            int Count = 0;
            int RootDirectoryCount = 0;
            RootDirectoryCount = (Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\Encrypted_Files\\").Length;
            while (Count < RandomFileIDFullPathArray.Length)
            {
                RandomFileIDArray[Count] = RandomFileIDFullPathArray[Count].Remove(0, RootDirectoryCount);
                Count += 1;
            }
            EncryptedRandomFileNameCB.Items.AddRange(RandomFileIDArray);
        }

        private void EncryptedRandomFileNameCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EncryptedRandomFileNameCB.SelectedIndex != -1)
            {
                Byte[] OriginalFileNameByte = new Byte[] { };
                String FileName = "";
                Boolean CheckFileNameExists = true;
                try
                {
                    OriginalFileNameByte = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\Encrypted_Files\\" + EncryptedRandomFileNameCB.Text + "\\FileName.txt");
                }
                catch
                {
                    CheckFileNameExists = false;
                }
                if (CheckFileNameExists == true)
                {
                    FileName = Encoding.UTF8.GetString(OriginalFileNameByte);
                    MessageBox.Show("The file name from the selected random file name is " + FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Sorry system can't find the file name from the selected random file name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DeleteFileContentBTN_Click(object sender, EventArgs e)
        {
            if(DirectoryIDTempStorage.DirectoryID!=null && DirectoryIDTempStorage.DirectoryID.CompareTo("")!=0 && EncryptedRandomFileNameCB.Text!=null && EncryptedRandomFileNameCB.Text.CompareTo("") != 0) 
            {
                String Base64Challenge = "";
                Byte[] Base64ChallengeByte = new Byte[] { };
                Boolean DeleteStatus = true;
                RequestChallenge(ref Base64Challenge);
                if(Base64Challenge!=null && Base64Challenge.CompareTo("") != 0) 
                {
                    Base64ChallengeByte = Convert.FromBase64String(Base64Challenge);
                    DeleteStatus = DeleteFileContent(DirectoryIDTempStorage.DirectoryID, EncryptedRandomFileNameCB.Text,Base64ChallengeByte);
                    if (DeleteStatus == true) 
                    {
                        MessageBox.Show("Successfully deleted directory from both server and client", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error deleting directory from both server and client","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                }
                else 
                {
                    MessageBox.Show("Error requesting challenge from server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else 
            {
                MessageBox.Show("Please enter the file name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                        client.BaseAddress = new Uri("https://{API URL}");
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
                        client.BaseAddress = new Uri("https://{API URL}");
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

        public Boolean DeleteFileContent(String DirectoryID, String RandomFileName, Byte[] RandomChallenge)
        {
            Byte[] ClientECDSASK = new Byte[] { };
            Byte[] UserECDSASK = new Byte[] { };
            Byte[] SharedSecret = new Byte[] { };
            Byte[] DirectoryIDByte = new Byte[] { };
            Byte[] CipheredDirectoryIDByte = new Byte[] { };
            Byte[] CombinedCipheredDirectoryIDByte = new Byte[] { };
            Byte[] ETLSSignedCombinedCipheredDirectoryIDByte = new Byte[] { };
            Byte[] RandomFileNameByte = new Byte[] { };
            Byte[] ETLSSignedRandomFileNameByte = new Byte[] { };
            Byte[] UserSignedRandomChallenge = new Byte[] { };
            Byte[] ETLSSignedUserSignedRandomChallenge = new Byte[] { };
            Byte[] UserIDByte = new Byte[] { };
            Byte[] ETLSSignedUserIDByte = new Byte[] { };
            Byte[] Nonce = new Byte[] { };
            GCHandle MyGeneralGCHandle = new GCHandle();
            Boolean ServerOnlineChecker = true;
            if (ETLSSessionIDStorage.ETLSID.CompareTo("") != 0 && ETLSSessionIDStorage.ETLSID != null)
            {
                if (UserIDTempStorage.UserID != null && UserIDTempStorage.UserID.CompareTo("") != 0)
                {
                    UserIDByte = Encoding.UTF8.GetBytes(UserIDTempStorage.UserID);
                    ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "ECDSASK.txt");
                    ETLSSignedUserIDByte = SodiumPublicKeyAuth.Sign(UserIDByte, ClientECDSASK);
                    SharedSecret = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "SharedSecret.txt");
                    UserECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\" + "User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\rootSK.txt");
                    ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "ECDSASK.txt");
                    DirectoryIDByte = Encoding.UTF8.GetBytes(DirectoryID);
                    Nonce = SodiumSecretBox.GenerateNonce();
                    CipheredDirectoryIDByte = SodiumSecretBox.Create(DirectoryIDByte, Nonce, SharedSecret);
                    CombinedCipheredDirectoryIDByte = Nonce.Concat(CipheredDirectoryIDByte).ToArray();
                    ETLSSignedCombinedCipheredDirectoryIDByte = SodiumPublicKeyAuth.Sign(CombinedCipheredDirectoryIDByte, ClientECDSASK);
                    ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "ECDSASK.txt");
                    UserSignedRandomChallenge = SodiumPublicKeyAuth.Sign(RandomChallenge, UserECDSASK);
                    ETLSSignedUserSignedRandomChallenge = SodiumPublicKeyAuth.Sign(UserSignedRandomChallenge, ClientECDSASK);
                    ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "ECDSASK.txt");
                    RandomFileNameByte = Encoding.UTF8.GetBytes(RandomFileName);
                    ETLSSignedRandomFileNameByte = SodiumPublicKeyAuth.Sign(RandomFileNameByte, ClientECDSASK);
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://{API URL}");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.GetAsync("OwnerUploadFiles/DeleteEndpointEncryptedFile?ClientPathID=" + ETLSSessionIDStorage.ETLSID + "&SignedUserID=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserIDByte)) + "&CipheredSignedDirectoryID=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredDirectoryIDByte)) + "&SignedSignedRandomChallenge=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserSignedRandomChallenge)) + "&SignedUniqueFileName=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedRandomFileNameByte)));
                        try
                        {
                            response.Wait();
                        }
                        catch
                        {
                            ServerOnlineChecker = false;
                        }
                        if (ServerOnlineChecker == false)
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
                        else
                        {
                            var result = response.Result;
                            if (result.IsSuccessStatusCode)
                            {
                                var readTask = result.Content.ReadAsStringAsync();
                                readTask.Wait();

                                var StringResult = readTask.Result;
                                if (StringResult != null)
                                {
                                    StringResult = StringResult.Substring(1, StringResult.Length - 2);
                                    if (StringResult.CompareTo("Successed: File successfully deleted") == 0) 
                                    {
                                        Directory.Delete(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + RandomFileName,true);
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
                                        MessageBox.Show(StringResult, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
}
