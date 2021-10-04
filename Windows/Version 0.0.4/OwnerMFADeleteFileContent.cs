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
using ASodium;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using Newtonsoft.Json;
using PriSecFileStorageClient.Helper;
using PriSecFileStorageClient.Model;

namespace PriSecFileStorageClient
{
    public partial class OwnerMFADeleteFileContent : Form
    {
        public OwnerMFADeleteFileContent()
        {
            InitializeComponent();
        }

        private void OwnerMFADeleteFileContent_Load(object sender, EventArgs e)
        {
            ReloadEncryptedFile();
            GetFileStorageUsedSize();
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
                    OriginalFileNameByte = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\Encrypted_Files\\" + EncryptedRandomFileNameCB.Text + "\\FileName.txt");
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
            if (DirectoryIDTempStorage.DirectoryID.CompareTo("") != 0 && EncryptedRandomFileNameCB.Text.CompareTo("") != 0 && FirstMFADeviceIDTB.Text.CompareTo("")!=0 && SecondMFADeviceIDTB.Text.CompareTo("")!=0)
            {
                String Base64Challenge = "";
                Byte[] Base64ChallengeByte = new Byte[] { };
                Boolean DeleteStatus = true;
                Boolean AbleToRequestChallenge = true;
                RequestChallenge(ref AbleToRequestChallenge, ref Base64Challenge);
                while (AbleToRequestChallenge == false)
                {
                    RequestChallenge(ref AbleToRequestChallenge, ref Base64Challenge);
                }
                if (Base64Challenge != null && Base64Challenge.CompareTo("") != 0)
                {
                    Base64ChallengeByte = Convert.FromBase64String(Base64Challenge);
                    DeleteStatus = DeleteFileContent(DirectoryIDTempStorage.DirectoryID, EncryptedRandomFileNameCB.Text, Base64ChallengeByte,FirstMFADeviceIDTB.Text,SecondMFADeviceIDTB.Text);
                    if (DeleteStatus == true)
                    {
                        GetFileStorageUsedSize();
                        MessageBox.Show("Successfully deleted directory from both server and client", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ReloadEncryptedFile();
                    }
                    else
                    {
                        MessageBox.Show("Error deleting directory from both server and client", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                                    Re_RequestRandomChallengeBoolean = true;
                                    SodiumSecureMemory.SecureClearBytes(ServerSignedRandomChallenge);
                                    SodiumSecureMemory.SecureClearBytes(ServerRandomChallenge);
                                    SodiumSecureMemory.SecureClearBytes(ServerED25519PK);
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
                MessageBox.Show("You have not yet establish an ephemeral TLS session with the server..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Boolean DeleteFileContent(String DirectoryID, String RandomFileName, Byte[] RandomChallenge, String FirstMFADeviceID, String SecondMFADeviceID)
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
            Boolean ServerOnlineChecker = true;
            if (ETLSSessionIDStorage.ETLSID.CompareTo("") != 0 && ETLSSessionIDStorage.ETLSID != null)
            {
                ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "ECDSASK.txt");
                SharedSecret = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "SharedSecret.txt");
                UserECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryID + "\\rootSK.txt");
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
                    var response = client.GetAsync("MFAOwnerUploadFiles/DeleteEndpointEncryptedFile?ClientPathID=" + ETLSSessionIDStorage.ETLSID + "&CipheredSignedDirectoryID=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredDirectoryIDByte)) + "&SignedSignedRandomChallenge=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserSignedRandomChallenge)) + "&SignedUniqueFileName=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedRandomFileNameByte))+"&FirstMFADeviceID="+FirstMFADeviceID+"&SecondMFADeviceID="+SecondMFADeviceID);
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
                                    Directory.Delete(Application.StartupPath + "\\Application_Data\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + RandomFileName, true);
                                    return true;
                                }
                                else
                                {
                                    MessageBox.Show(StringResult, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            else
            {
                return false;
            }
        }

        public void GetFileStorageUsedSize()
        {
            Boolean ServerOnlineChecker = true;
            int FileStorageSize = 0;
            Decimal FileStorageSizeInKB = 0;
            Decimal FileStorageSizeInMB = 0;
            if (ETLSSessionIDStorage.ETLSID.CompareTo("") != 0 && ETLSSessionIDStorage.ETLSID != null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://mrchewitsoftware.com.my:5001/api/");
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
                                MessageBox.Show("Something went wrong when requesting random challenge...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                MessageBox.Show(StringResult, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                StringResult = StringResult.Substring(1, StringResult.Length - 2);
                                FileStorageSize = int.Parse(StringResult);
                                FileStorageSizeInKB = FileStorageSize / 1024;
                                FileStorageSizeInMB = FileStorageSize / 1048576;
                                FileStorageSizeTB.Text = "";
                                FileStorageSizeTB.Text = StringResult + " bytes" + Environment.NewLine + FileStorageSizeInKB.ToString() + " kb" + Environment.NewLine + FileStorageSizeInMB.ToString() + " mb";
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
                MessageBox.Show("You have not yet establish an ephemeral TLS session with the server..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ReloadEncryptedFile()
        {
            String[] RandomFileIDFullPathArray = Directory.GetDirectories(Application.StartupPath + "\\Application_Data\\Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\Encrypted_Files");
            String[] RandomFileIDArray = new string[RandomFileIDFullPathArray.Length];
            int Count = 0;
            int RootDirectoryCount = 0;
            RootDirectoryCount = (Application.StartupPath + "\\Application_Data\\Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\Encrypted_Files\\").Length;
            while (Count < RandomFileIDFullPathArray.Length)
            {
                RandomFileIDArray[Count] = RandomFileIDFullPathArray[Count].Remove(0, RootDirectoryCount);
                Count += 1;
            }
            EncryptedRandomFileNameCB.Items.AddRange(RandomFileIDArray);
        }
    }
}
