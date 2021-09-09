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
using System.Web;
using Newtonsoft.Json;

namespace PriSecFileStorageClient
{
    public partial class OwnerRemoveUser : Form
    {
        public OwnerRemoveUser()
        {
            InitializeComponent();
        }

        private void OwnerRemoveUser_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Please take note, this is an online usage", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RemoveAccessBTN_Click(object sender, EventArgs e)
        {
            if (AccessIDTB.Text!=null && AccessIDTB.Text.CompareTo("")!=0)
            {
                String Base64Challenge = "";
                Boolean AbleToRequestChallenge = true;
                Byte[] Base64ChallengeByte = new Byte[] { };
                String AccessID = AccessIDTB.Text;
                String Result = "";
                RequestChallenge(ref AbleToRequestChallenge, ref Base64Challenge);
                while (AbleToRequestChallenge == false)
                {
                    RequestChallenge(ref AbleToRequestChallenge, ref Base64Challenge);
                }
                Base64ChallengeByte = Convert.FromBase64String(Base64Challenge);
                Result = RemoveUserFunction(DirectoryIDTempStorage.DirectoryID, AccessID, Base64ChallengeByte);
                if (Result.CompareTo("") != 0)
                {
                    AccessIDTB.Text = "";
                    MessageBox.Show(Result);
                }
            }
            else
            {
                MessageBox.Show("Allowed User Access ID must not be null/Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("You have not yet login/register ....", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("You have not yet establish an ephemeral TLS session with the server..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public String RemoveUserFunction(String DirectoryID, String AccessID, Byte[] RandomChallenge)
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
            Byte[] AccessIDByte = new Byte[] { };
            Byte[] CipheredAccessIDByte = new Byte[] { };
            Byte[] CombinedCipheredAccessIDByte = new Byte[] { };
            Byte[] ETLSSignedCombinedCipheredAccessIDByte = new Byte[] { };
            Boolean ServerOnlineChecker = true;
            if (ETLSSessionIDStorage.ETLSID.CompareTo("") != 0 && ETLSSessionIDStorage.ETLSID != null)
            {
                if (UserIDTempStorage.UserID != null && UserIDTempStorage.UserID.CompareTo("") != 0)
                {
                    ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "ECDSASK.txt");
                    SharedSecret = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "SharedSecret.txt");
                    UserECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\" + "User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\rootSK.txt");
                    DirectoryIDByte = Encoding.UTF8.GetBytes(DirectoryID);
                    Nonce = SodiumSecretBox.GenerateNonce();
                    CipheredDirectoryIDByte = SodiumSecretBox.Create(DirectoryIDByte, Nonce, SharedSecret);
                    CombinedCipheredDirectoryIDByte = Nonce.Concat(CipheredDirectoryIDByte).ToArray();
                    ETLSSignedCombinedCipheredDirectoryIDByte = SodiumPublicKeyAuth.Sign(CombinedCipheredDirectoryIDByte, ClientECDSASK);
                    UserSignedRandomChallenge = SodiumPublicKeyAuth.Sign(RandomChallenge, UserECDSASK, true);
                    ETLSSignedUserSignedRandomChallenge = SodiumPublicKeyAuth.Sign(UserSignedRandomChallenge, ClientECDSASK);
                    Nonce = SodiumSecretBox.GenerateNonce();
                    AccessIDByte = Encoding.UTF8.GetBytes(AccessID);
                    CipheredAccessIDByte = SodiumSecretBox.Create(AccessIDByte, Nonce, SharedSecret, true);
                    CombinedCipheredAccessIDByte = Nonce.Concat(CipheredAccessIDByte).ToArray();
                    ETLSSignedCombinedCipheredAccessIDByte = SodiumPublicKeyAuth.Sign(CombinedCipheredAccessIDByte, ClientECDSASK, true);
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://mrchewitsoftware.com.my:5001/api/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.GetAsync("OwnerAddUser/RemovePK?ClientPathID=" + ETLSSessionIDStorage.ETLSID + "&CipheredSignedDirectoryID=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredDirectoryIDByte)) + "&SignedSignedRandomChallenge=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserSignedRandomChallenge)) + "&CipheredSignedAnotherUserID=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredAccessIDByte)));
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
            else
            {
                return "";
            }
        }
    }
}
