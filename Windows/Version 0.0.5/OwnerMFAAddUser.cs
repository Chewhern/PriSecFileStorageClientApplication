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
using PriSecFileStorageClient.Model;
using PriSecFileStorageClient.Helper;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace PriSecFileStorageClient
{
    public partial class OwnerMFAAddUser : Form
    {
        public OwnerMFAAddUser()
        {
            InitializeComponent();
        }

        private void OwnerMFAAddUser_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Please take note, this is an online usage", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void GiveAccessBTN_Click(object sender, EventArgs e)
        {
            if (MergedED25519PKSTB.Text.CompareTo("") != 0 && FirstMFADeviceIDTB.Text.CompareTo("")!=0 && SecondMFADeviceIDTB.Text.CompareTo("")!=0)
            {
                String Base64Challenge = "";
                Boolean AbleToRequestChallenge = true;
                Byte[] Base64ChallengeByte = new Byte[] { };
                Byte[] MergedED25519PK = new Byte[] { };
                Boolean AbleToConvertFromBase64 = true;
                String Result = "";
                try
                {
                    MergedED25519PK = Convert.FromBase64String(MergedED25519PKSTB.Text);
                }
                catch
                {
                    MessageBox.Show("Please pass in a correct format of ED25519 PK", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    AbleToConvertFromBase64 = false;
                }
                if (AbleToConvertFromBase64 == true)
                {
                    RequestChallenge(ref AbleToRequestChallenge, ref Base64Challenge);
                    while (AbleToRequestChallenge == false)
                    {
                        RequestChallenge(ref AbleToRequestChallenge, ref Base64Challenge);
                    }
                    Base64ChallengeByte = Convert.FromBase64String(Base64Challenge);
                    Result = AddUserFunction(DirectoryIDTempStorage.DirectoryID, MergedED25519PK, Base64ChallengeByte,FirstMFADeviceIDTB.Text,SecondMFADeviceIDTB.Text);
                    if (Result.CompareTo("") != 0)
                    {
                        Result = Result.Remove(0, 4);
                        AccessIDTB.Text = Result;
                        StorageIDTB.Text = DirectoryIDTempStorage.DirectoryID;
                        if (AdditionalNoteTB.Text != null && AdditionalNoteTB.Text.CompareTo("") != 0)
                        {
                            if (Directory.Exists(Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\Other_User") == false)
                            {
                                Directory.CreateDirectory(Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\Other_User");
                            }
                            if (Directory.Exists(Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\Other_User\\" + Result) == false)
                            {
                                Directory.CreateDirectory(Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\Other_User\\" + Result);
                            }
                            File.WriteAllText(Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\Other_User\\" + Result + "\\Note.txt", AdditionalNoteTB.Text);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("You must get merged ED25519 PKs from the user and type in MFA device IDs", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public String AddUserFunction(String DirectoryID, Byte[] MergedED25519PK, Byte[] RandomChallenge, String FirstMFADeviceID, String SecondMFADeviceID)
        {
            Byte[] ClientECDSASK = new Byte[] { };
            Byte[] UserECDSASK = new Byte[] { };
            Byte[] SharedSecret = new Byte[] { };
            Byte[] DirectoryIDByte = new Byte[] { };
            Byte[] CipheredDirectoryIDByte = new Byte[] { };
            Byte[] CombinedCipheredDirectoryIDByte = new Byte[] { };
            Byte[] ETLSSignedCombinedCipheredDirectoryIDByte = new Byte[] { };
            Byte[] UserSignedRandomChallenge = new Byte[] { };
            Byte[] ETLSSignedUserSignedRandomChallenge = new Byte[] { };
            Byte[] Nonce = new Byte[] { };
            Byte[] CipheredMergedED25519PK = new Byte[] { };
            Byte[] CombinedCipheredMergedED25519PK = new Byte[] { };
            Byte[] ETLSSignedCombinedCipheredMergedED25519PK = new Byte[] { };
            Boolean ServerOnlineChecker = true;
            if (ETLSSessionIDStorage.ETLSID.CompareTo("") != 0 && ETLSSessionIDStorage.ETLSID != null)
            {
                ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "ECDSASK.txt");
                SharedSecret = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "SharedSecret.txt");
                UserECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryID + "\\rootSK.txt");
                DirectoryIDByte = Encoding.UTF8.GetBytes(DirectoryID);
                Nonce = SodiumSecretBox.GenerateNonce();
                CipheredDirectoryIDByte = SodiumSecretBox.Create(DirectoryIDByte, Nonce, SharedSecret);
                CombinedCipheredDirectoryIDByte = Nonce.Concat(CipheredDirectoryIDByte).ToArray();
                ETLSSignedCombinedCipheredDirectoryIDByte = SodiumPublicKeyAuth.Sign(CombinedCipheredDirectoryIDByte, ClientECDSASK);
                UserSignedRandomChallenge = SodiumPublicKeyAuth.Sign(RandomChallenge, UserECDSASK, true);
                ETLSSignedUserSignedRandomChallenge = SodiumPublicKeyAuth.Sign(UserSignedRandomChallenge, ClientECDSASK);
                Nonce = SodiumSecretBox.GenerateNonce();
                CipheredMergedED25519PK = SodiumSecretBox.Create(MergedED25519PK, Nonce, SharedSecret, true);
                CombinedCipheredMergedED25519PK = Nonce.Concat(CipheredMergedED25519PK).ToArray();
                ETLSSignedCombinedCipheredMergedED25519PK = SodiumPublicKeyAuth.Sign(CombinedCipheredMergedED25519PK, ClientECDSASK, true);
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://mrchewitsoftware.com.my:5001/api/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync("MFAOwnerAddUser/UploadPK?ClientPathID=" + ETLSSessionIDStorage.ETLSID + "&CipheredSignedDirectoryID=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredDirectoryIDByte)) + "&SignedSignedRandomChallenge=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserSignedRandomChallenge)) + "&CipheredSignedED25519PK=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredMergedED25519PK))+"&FirstMFADeviceID="+FirstMFADeviceID+"&SecondMFADeviceID="+SecondMFADeviceID);
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
                        return "";
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
                                if (StringResult.Contains("Error") == true)
                                {
                                    MessageBox.Show(StringResult, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return "";
                                }
                                else
                                {
                                    return StringResult;
                                }
                            }
                            else
                            {
                                return "";
                            }
                        }
                        else
                        {
                            return "";
                        }
                    }
                }
            }
            else
            {
                return "";
            }
        }
    }
}
