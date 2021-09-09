using System;
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
using PriSecFileStorageClient.Helper;
using PriSecFileStorageClient.Model;

namespace PriSecFileStorageClient
{
    public partial class UserDecryptFileContent : Form
    {
        private Thread DecryptFileContentsThread;

        public UserDecryptFileContent()
        {
            InitializeComponent();
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
                    OriginalFileNameByte = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Other_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\Encrypted_Files\\" + EncryptedRandomFileNameCB.Text + "\\FileName.txt");
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

        private void UserDecryptFileContent_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            String[] RandomFileIDFullPathArray = Directory.GetDirectories(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Other_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\Encrypted_Files");
            String[] RandomFileIDArray = new string[RandomFileIDFullPathArray.Length];
            int Count = 0;
            int RootDirectoryCount = 0;
            RootDirectoryCount = (Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Other_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\Encrypted_Files\\").Length;
            while (Count < RandomFileIDFullPathArray.Length)
            {
                RandomFileIDArray[Count] = RandomFileIDFullPathArray[Count].Remove(0, RootDirectoryCount);
                Count += 1;
            }
            EncryptedRandomFileNameCB.Items.AddRange(RandomFileIDArray);
        }

        private void DecryptFetchedFileContentsBTN_Click(object sender, EventArgs e)
        {
            DecryptFolderSelector.ShowDialog();
            DecryptFileContentsThread = new Thread(BackGroundDecryptFileContent);
            DecryptFileContentsThread.Start();
        }

        private void GetFileContentCountBTN_Click(object sender, EventArgs e)
        {
            if (DirectoryIDTempStorage.DirectoryID != null && DirectoryIDTempStorage.DirectoryID.CompareTo("") != 0 && UserIDTempStorage.UserID != null && UserIDTempStorage.UserID.CompareTo("") != 0)
            {
                if (EncryptedRandomFileNameCB.Text != null && EncryptedRandomFileNameCB.Text.CompareTo("") != 0)
                {
                    String ServerRandomFileName = EncryptedRandomFileNameCB.Text;
                    String DirectoryID = DirectoryIDTempStorage.DirectoryID;
                    String Base64Challenge = "";
                    Byte[] Base64ChallengeByte = new Byte[] { };
                    Boolean AbleToRequestChallenge = true;
                    int Count = 0;
                    RequestChallenge(ref AbleToRequestChallenge, ref Base64Challenge);
                    while (AbleToRequestChallenge == false)
                    {
                        RequestChallenge(ref AbleToRequestChallenge, ref Base64Challenge);
                    }
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
            Byte[] Nonce = new Byte[] { };
            String AccessID = "";
            Byte[] AccessIDByte = new Byte[] { };
            Byte[] CipheredAccessIDByte = new Byte[] { };
            Byte[] CombinedCipheredAccessIDByte = new Byte[] { };
            Byte[] ETLSSignedCombinedCipheredAccessIDByte = new Byte[] { };
            Boolean ServerOnlineChecker = true;
            Boolean TryParseStringToInt = true;
            int Count = 0;
            if (ETLSSessionIDStorage.ETLSID.CompareTo("") != 0 && ETLSSessionIDStorage.ETLSID != null)
            {
                if (UserIDTempStorage.UserID != null && UserIDTempStorage.UserID.CompareTo("") != 0)
                {
                    ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "ECDSASK.txt");
                    SharedSecret = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "SharedSecret.txt");
                    UserECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\" + "User\\" + UserIDTempStorage.UserID + "\\Other_Directory_Data\\" + DirectoryID + "\\SK.txt");
                    AccessID = File.ReadAllText(Application.StartupPath + "\\Application_Data\\" + "User\\" + UserIDTempStorage.UserID + "\\Other_Directory_Data\\" + DirectoryID + "\\AccessID.txt");
                    AccessIDByte = Encoding.UTF8.GetBytes(AccessID);
                    Nonce = SodiumSecretBox.GenerateNonce();
                    CipheredAccessIDByte = SodiumSecretBox.Create(AccessIDByte, Nonce, SharedSecret);
                    CombinedCipheredAccessIDByte = Nonce.Concat(CipheredAccessIDByte).ToArray();
                    ETLSSignedCombinedCipheredAccessIDByte = SodiumPublicKeyAuth.Sign(CombinedCipheredAccessIDByte, ClientECDSASK);
                    DirectoryIDByte = Encoding.UTF8.GetBytes(DirectoryID);
                    Nonce = SodiumSecretBox.GenerateNonce();
                    CipheredDirectoryIDByte = SodiumSecretBox.Create(DirectoryIDByte, Nonce, SharedSecret, true);
                    CombinedCipheredDirectoryIDByte = Nonce.Concat(CipheredDirectoryIDByte).ToArray();
                    ETLSSignedCombinedCipheredDirectoryIDByte = SodiumPublicKeyAuth.Sign(CombinedCipheredDirectoryIDByte, ClientECDSASK);
                    UserSignedRandomChallenge = SodiumPublicKeyAuth.Sign(RandomChallenge, UserECDSASK, true);
                    ETLSSignedUserSignedRandomChallenge = SodiumPublicKeyAuth.Sign(UserSignedRandomChallenge, ClientECDSASK);
                    RandomFileNameByte = Encoding.UTF8.GetBytes(RandomFileName);
                    ETLSSignedRandomFileNameByte = SodiumPublicKeyAuth.Sign(RandomFileNameByte, ClientECDSASK, true);
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://mrchewitsoftware.com.my:5001/api/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.GetAsync("UserFileOperation/GetEndpointEncryptedFileContentCount?ClientPathID=" + ETLSSessionIDStorage.ETLSID + "&CipheredSignedDirectoryID=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredDirectoryIDByte)) + "&SignedSignedRandomChallenge=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserSignedRandomChallenge)) + "&SignedUniqueFileName=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedRandomFileNameByte))+"&CipheredSignedAnotherUserID="+HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredAccessIDByte)));
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
                                        return Count;
                                    }
                                    else
                                    {
                                        return -1;
                                    }
                                }
                                else
                                {
                                    return -1;
                                }
                            }
                            else
                            {
                                return -1;
                            }
                        }
                    }
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return -1;
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
                if (UserIDTempStorage.UserID != null && UserIDTempStorage.UserID.CompareTo("") != 0)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://mrchewitsoftware.com.my:5001/api/");
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
                                        SodiumSecureMemory.SecureClearBytes(ServerSignedRandomChallenge);
                                        SodiumSecureMemory.SecureClearBytes(ServerRandomChallenge);
                                        SodiumSecureMemory.SecureClearBytes(ServerED25519PK);
                                        Re_RequestRandomChallengeBoolean = true;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Man in the middle spotted ..., proceeding with re-requesting random challenge from server...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            Byte[] Nonce = new Byte[] { };
            String AccessID = "";
            Byte[] AccessIDByte = new Byte[] { };
            Byte[] CipheredAccessIDByte = new Byte[] { };
            Byte[] CombinedCipheredAccessIDByte = new Byte[] { };
            Byte[] ETLSSignedCombinedCipheredAccessIDByte = new Byte[] { };
            Boolean ServerOnlineChecker = true;
            Boolean TryParseBase64StringToBytes = true;
            Byte[] ResultByte = new Byte[] { };
            if (ETLSSessionIDStorage.ETLSID.CompareTo("") != 0 && ETLSSessionIDStorage.ETLSID != null)
            {
                if (UserIDTempStorage.UserID != null && UserIDTempStorage.UserID.CompareTo("") != 0)
                {
                    ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "ECDSASK.txt");
                    SharedSecret = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "SharedSecret.txt");
                    UserECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\" + "User\\" + UserIDTempStorage.UserID + "\\Other_Directory_Data\\" + DirectoryID + "\\SK.txt");
                    Nonce = SodiumSecretBox.GenerateNonce();
                    AccessID = File.ReadAllText(Application.StartupPath + "\\Application_Data\\" + "User\\" + UserIDTempStorage.UserID + "\\Other_Directory_Data\\" + DirectoryID + "\\AccessID.txt");
                    AccessIDByte = Encoding.UTF8.GetBytes(AccessID);
                    CipheredAccessIDByte = SodiumSecretBox.Create(AccessIDByte, Nonce, SharedSecret);
                    CombinedCipheredAccessIDByte = Nonce.Concat(CipheredAccessIDByte).ToArray();
                    ETLSSignedCombinedCipheredAccessIDByte = SodiumPublicKeyAuth.Sign(CombinedCipheredAccessIDByte, ClientECDSASK);
                    DirectoryIDByte = Encoding.UTF8.GetBytes(DirectoryID);
                    Nonce = SodiumSecretBox.GenerateNonce();
                    CipheredDirectoryIDByte = SodiumSecretBox.Create(DirectoryIDByte, Nonce, SharedSecret, true);
                    CombinedCipheredDirectoryIDByte = Nonce.Concat(CipheredDirectoryIDByte).ToArray();
                    ETLSSignedCombinedCipheredDirectoryIDByte = SodiumPublicKeyAuth.Sign(CombinedCipheredDirectoryIDByte, ClientECDSASK);
                    ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "ECDSASK.txt");
                    UserSignedRandomChallenge = SodiumPublicKeyAuth.Sign(RandomChallenge, UserECDSASK, true);
                    ETLSSignedUserSignedRandomChallenge = SodiumPublicKeyAuth.Sign(UserSignedRandomChallenge, ClientECDSASK);
                    ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "ECDSASK.txt");
                    RandomFileNameByte = Encoding.UTF8.GetBytes(RandomFileName);
                    ETLSSignedRandomFileNameByte = SodiumPublicKeyAuth.Sign(RandomFileNameByte, ClientECDSASK, true);
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://mrchewitsoftware.com.my:5001/api/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.GetAsync("UserFileOperation/GetEndpointEncryptedFile?ClientPathID=" + ETLSSessionIDStorage.ETLSID + "&SignedUserID=" + "&CipheredSignedDirectoryID=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredDirectoryIDByte)) + "&SignedSignedRandomChallenge=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserSignedRandomChallenge)) + "&SignedUniqueFileName=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedRandomFileNameByte)) + "&FileContentCount=" + Count.ToString() + "&CipheredSignedAnotherUserID=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredAccessIDByte)));
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
                                        return ResultByte;
                                    }
                                    else
                                    {
                                        MessageBox.Show(StringResult, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return new Byte[] { };
                                    }
                                }
                                else
                                {
                                    return new Byte[] { };
                                }
                            }
                            else
                            {
                                return new Byte[] { };
                            }
                        }
                    }
                }
                else
                {
                    return new Byte[] { };
                }
            }
            else
            {
                return new Byte[] { };
            }
        }

        public void BackGroundDecryptFileContent()
        {
            if (DecryptFolderSelector.SelectedPath != null && DecryptFolderSelector.SelectedPath.CompareTo("") != 0)
            {
                String DecryptFolderPath = DecryptFolderSelector.SelectedPath;
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
                Boolean AbleToRequestChallenge = true;
                GCHandle MyGeneralGCHandle = new GCHandle();
                FileStream PlainTextFileStream;
                try
                {
                    OriginalFileNameByte = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Other_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\Encrypted_Files\\" + EncryptedRandomFileNameCB.Text + "\\FileName.txt");
                }
                catch
                {
                    CheckFileNameExists = false;
                }
                if (CheckFileNameExists == true)
                {
                    FileName = Encoding.UTF8.GetString(OriginalFileNameByte);
                    if (DecryptFolderPath.Substring(DecryptFolderPath.Length - 1).CompareTo("\\") != 0)
                    {
                        PlainTextFileStream = File.OpenWrite(DecryptFolderPath + "\\" + FileName);
                    }
                    else
                    {
                        PlainTextFileStream = File.OpenWrite(DecryptFolderPath + FileName);
                    }
                    while (LoopCount <= Count)
                    {
                        DecryptProgressBar.Maximum = Count;
                        DecryptProgressBar.Step = 1;
                        RequestChallenge(ref AbleToRequestChallenge, ref Base64Challenge);
                        while (AbleToRequestChallenge == false)
                        {
                            RequestChallenge(ref AbleToRequestChallenge, ref Base64Challenge);
                        }
                        if (Base64Challenge != null && Base64Challenge.CompareTo("") != 0)
                        {
                            Base64ChallengeByte = Convert.FromBase64String(Base64Challenge);
                            ServerFileContent = GetFileContent(DirectoryIDTempStorage.DirectoryID, EncryptedRandomFileNameCB.Text, Base64ChallengeByte, LoopCount);
                            if (ServerFileContent.Length != 0)
                            {
                                try
                                {
                                    Key = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Other_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\Encrypted_Files\\" + EncryptedRandomFileNameCB.Text + "\\Key\\" + "Key" + LoopCount.ToString() + ".txt");
                                }
                                catch
                                {
                                    CheckKeyExists = false;
                                    break;
                                }
                                try
                                {
                                    ED25519PK = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Other_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\Encrypted_Files\\" + EncryptedRandomFileNameCB.Text + "\\ED25519PK\\" + "PK" + LoopCount.ToString() + ".txt");
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
                                Array.Copy(VerifiedServerFileContent, Nonce.Length, CipherTextWithMAC, 0, CipherTextWithMAC.Length);
                                try
                                {
                                    if (DefaultRB.Checked == true)
                                    {
                                        DecryptedFileBytes = SodiumSecretBox.Open(CipherTextWithMAC, Nonce, Key, true);
                                    }
                                    if (XChaCha20Poly1305RB.Checked == true)
                                    {
                                        DecryptedFileBytes = SodiumSecretBoxXChaCha20Poly1305.Open(CipherTextWithMAC, Nonce, Key, true);
                                    }
                                    if (AES256GCMRB.Checked == true)
                                    {
                                        if (SodiumSecretAeadAES256GCM.IsAES256GCMAvailable() == true)
                                        {
                                            DecryptedFileBytes = SodiumSecretAeadAES256GCM.Decrypt(CipherTextWithMAC, Nonce, Key, null, null, true);
                                        }
                                        else
                                        {
                                            MessageBox.Show("Your device does not support AES256 GCM", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                    }
                                }
                                catch
                                {
                                    DecryptChecker = false;
                                    break;
                                }
                                PlainTextFileStream.Write(DecryptedFileBytes, 0, DecryptedFileBytes.Length);
                                DecryptedFileBytes = new Byte[] { };
                                MyGeneralGCHandle = GCHandle.Alloc(ED25519PK, GCHandleType.Pinned);
                                SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), ED25519PK.Length);
                                MyGeneralGCHandle.Free();
                                Key = new Byte[] { };
                                ED25519PK = new Byte[] { };
                                if (CheckKeyExists == true && CheckED25519PKExists == true && VerifyFileContent == true && DecryptChecker == true)
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
                    if (EncounterError == false && CheckKeyExists == true && CheckED25519PKExists == true && VerifyFileContent == true && DecryptChecker == true)
                    {
                        MessageBox.Show("Decryption of file have been done", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(EncounterError.ToString());
                        MessageBox.Show(CheckKeyExists.ToString());
                        MessageBox.Show(CheckED25519PKExists.ToString());
                        MessageBox.Show(VerifyFileContent.ToString());
                        MessageBox.Show(DecryptChecker.ToString());
                        MessageBox.Show("Could not decrypt file properly..,aborting..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Can't find file name.., decryption of file could not done properly, aborting..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("You must choose a folder to let the system know where to put the decrypted file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DecryptFileContentsThread.Abort();
        }
    }
}
