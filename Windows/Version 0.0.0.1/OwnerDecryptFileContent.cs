﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using ASodium;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;

namespace PriSecFileStorageClient
{
    public partial class OwnerDecryptFileContent : Form
    {
        private Thread DecryptFileContentsThread;

        public OwnerDecryptFileContent()
        {
            InitializeComponent();
        }

        private void OwnerDecryptFileContent_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }

        private void DecryptFetchedFileContentsBTN_Click(object sender, EventArgs e)
        {
            DecryptFileContentsThread = new Thread(BackGroundDecryptFileContent);
            DecryptFileContentsThread.Start();
        }

        private void GetFileContentCountBTN_Click(object sender, EventArgs e)
        {
            if (DirectoryIDTempStorage.DirectoryID != null && DirectoryIDTempStorage.DirectoryID.CompareTo("") != 0 && UserIDTempStorage.UserID != null && UserIDTempStorage.UserID.CompareTo("") != 0)
            {
                if (ServerRandomFileNameTB.Text != null && ServerRandomFileNameTB.Text.CompareTo("") != 0)
                {
                    String ServerRandomFileName = ServerRandomFileNameTB.Text;
                    String DirectoryID = DirectoryIDTempStorage.DirectoryID;
                    String Base64Challenge = "";
                    Byte[] Base64ChallengeByte = new Byte[] { };
                    int Count = 0;
                    RequestChallenge(ref Base64Challenge);
                    if (Base64Challenge != null && Base64Challenge.CompareTo("") != 0)
                    {
                        Base64ChallengeByte = Convert.FromBase64String(Base64Challenge);
                        Count = GetFileContentCount(DirectoryID, ServerRandomFileName, Base64ChallengeByte);
                        if (Count != -1)
                        {
                            FileContentCountTB.Text = Count.ToString();
                        }
                        else
                        {
                            MessageBox.Show("Error getting file content count", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error requesting challenge", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Have you enter server random file name?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Have you enter server directory ID? Have you login/register yet?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public int GetFileContentCount(String DirectoryID, String RandomFileName, Byte[] RandomChallenge)
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
            Boolean TryParseStringToInt = true;
            int Count = 0;
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
                        client.BaseAddress = new Uri("https://{link to API}");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.GetAsync("OwnerUploadFiles/GetEndpointEncryptedFileContentCount?ClientPathID=" + ETLSSessionIDStorage.ETLSID + "&SignedUserID=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserIDByte)) + "&CipheredSignedDirectoryID=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredDirectoryIDByte)) + "&SignedSignedRandomChallenge=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserSignedRandomChallenge)) + "&SignedUniqueFileName=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedRandomFileNameByte)));
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
                            return -1;
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
                                    try
                                    {
                                        Count = int.Parse(StringResult);
                                    }
                                    catch
                                    {
                                        TryParseStringToInt = false;
                                    }
                                    if (TryParseStringToInt == true)
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
                                        return Count;
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
                                        return -1;
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
                                    return -1;
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
                                return -1;
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
                    return -1;
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
                return -1;
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
                        client.BaseAddress = new Uri("https://{link to API}");
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
                        client.BaseAddress = new Uri("https://{link to API}");
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

        public Byte[] GetFileContent(String DirectoryID, String RandomFileName, Byte[] RandomChallenge, int Count)
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
            Boolean TryParseBase64StringToBytes = true;
            Byte[] ResultByte = new Byte[] { };
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
                        client.BaseAddress = new Uri("https://{link to API}");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.GetAsync("OwnerUploadFiles/GetEndpointEncryptedFile?ClientPathID=" + ETLSSessionIDStorage.ETLSID + "&SignedUserID=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserIDByte)) + "&CipheredSignedDirectoryID=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredDirectoryIDByte)) + "&SignedSignedRandomChallenge=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserSignedRandomChallenge)) + "&SignedUniqueFileName=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedRandomFileNameByte)) + "&FileContentCount=" + Count.ToString());
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
                            return new Byte[] { };
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
                                    try
                                    {
                                        ResultByte = Convert.FromBase64String(StringResult);
                                    }
                                    catch
                                    {
                                        TryParseBase64StringToBytes = false;
                                    }
                                    if (TryParseBase64StringToBytes == true)
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
                                        return ResultByte;
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
                                        return new Byte[] { };
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
                                    return new Byte[] { };
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
                                return new Byte[] { };
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
                    return new Byte[] { };
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
                return new Byte[] { };
            }
        }

        public void BackGroundDecryptFileContent()
        {
            int LoopCount = 1;
            int Count = int.Parse(FileContentCountTB.Text);
            String Base64Challenge = "";
            Byte[] Base64ChallengeByte = new Byte[] { };
            Byte[] ServerFileContent = new Byte[] { };
            Byte[] ED25519PK = new Byte[] { };
            Byte[] VerifiedServerFileContent = new Byte[] { };
            Byte[] CipherTextWithMAC = new Byte[] { };
            Byte[] DecryptedFileBytes = new Byte[] { };
            Byte[] Key = new Byte[] { };
            Byte[] Nonce = new Byte[] { };
            Byte[] OriginalFileNameByte = new Byte[] { };
            String FileName = "";
            Boolean EncounterError = false;
            Boolean CheckED25519PKExists = true;
            Boolean VerifyFileContent = true;
            Boolean CheckKeyExists = true;
            Boolean CheckFileNameExists = true;
            Boolean DecryptChecker = true;
            GCHandle MyGeneralGCHandle = new GCHandle();
            FileStream PlainTextFileStream;
            try
            {
                OriginalFileNameByte = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\Encrypted_Files\\" + ServerRandomFileNameTB.Text + "\\FileName.txt");
            }
            catch
            {
                CheckFileNameExists = false;
            }
            if (CheckFileNameExists == true) 
            {
                FileName = Encoding.UTF8.GetString(OriginalFileNameByte);
                if (Directory.Exists(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\Decrypted_Files") == false)
                {
                    Directory.CreateDirectory(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\Decrypted_Files");
                }
                PlainTextFileStream = File.OpenWrite(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\Decrypted_Files\\" + FileName);
                while (LoopCount <= Count)
                {
                    DecryptProgressBar.Maximum = Count;
                    DecryptProgressBar.Step = 1;
                    RequestChallenge(ref Base64Challenge);
                    if (Base64Challenge != null && Base64Challenge.CompareTo("") != 0)
                    {
                        Base64ChallengeByte = Convert.FromBase64String(Base64Challenge);
                        ServerFileContent = GetFileContent(DirectoryIDTempStorage.DirectoryID, ServerRandomFileNameTB.Text, Base64ChallengeByte, LoopCount);
                        if (ServerFileContent.Length != 0)
                        {
                            try 
                            {
                                Key = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\Encrypted_Files\\" + ServerRandomFileNameTB.Text + "\\Key\\" + "Key" + LoopCount.ToString() + ".txt");
                            }
                            catch 
                            {
                                CheckKeyExists = false;
                                break;
                            }
                            try 
                            {
                                ED25519PK = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\Encrypted_Files\\" + ServerRandomFileNameTB.Text + "\\ED25519PK\\" + "PK" + LoopCount.ToString() + ".txt");
                            }
                            catch 
                            {
                                CheckED25519PKExists = false;
                                break;
                            }
                            try 
                            {
                                VerifiedServerFileContent = SodiumPublicKeyAuth.Verify(ServerFileContent, ED25519PK);
                            }
                            catch 
                            {
                                VerifyFileContent = false;
                                break;
                            }
                            Nonce = new Byte[SodiumSecretBox.GenerateNonce().Length];
                            CipherTextWithMAC = new Byte[VerifiedServerFileContent.Length - Nonce.Length];
                            Array.Copy(VerifiedServerFileContent, Nonce, Nonce.Length);
                            Array.Copy(VerifiedServerFileContent,Nonce.Length,CipherTextWithMAC,0,CipherTextWithMAC.Length);
                            try 
                            {
                                DecryptedFileBytes = SodiumSecretBox.Open(CipherTextWithMAC, Nonce, Key);
                            }
                            catch 
                            {
                                DecryptChecker = false;
                                break;
                            }
                            PlainTextFileStream.Write(DecryptedFileBytes, 0, DecryptedFileBytes.Length);
                            DecryptedFileBytes = new Byte[] { };
                            MyGeneralGCHandle = GCHandle.Alloc(Key, GCHandleType.Pinned);
                            SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), Key.Length);
                            MyGeneralGCHandle.Free();
                            MyGeneralGCHandle = GCHandle.Alloc(ED25519PK, GCHandleType.Pinned);
                            SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), ED25519PK.Length);
                            MyGeneralGCHandle.Free();
                            Key = new Byte[] { };
                            ED25519PK = new Byte[] { };
                            if(CheckKeyExists==true && CheckED25519PKExists==true && VerifyFileContent==true && DecryptChecker == true) 
                            {
                                DecryptProgressBar.PerformStep();
                            }
                        }
                        else
                        {
                            EncounterError = true;
                            MessageBox.Show("Error when getting file content from server side, aborting..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    }
                    else
                    {
                        EncounterError = true;
                        MessageBox.Show("Error when requesting challenge from server, aborting..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                    LoopCount += 1;
                }
                PlainTextFileStream.Close();
                if (EncounterError==false && CheckKeyExists == true && CheckED25519PKExists == true && VerifyFileContent == true && DecryptChecker == true) 
                {
                    MessageBox.Show("Decryption of file have been done", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else 
                {
                    MessageBox.Show("Could not decrypt file properly..,aborting..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else 
            {
                MessageBox.Show("Can't find file name.., decryption of file could not done properly, aborting..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DecryptFileContentsThread.Abort();
        }
    }
}
