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
    public partial class OwnerRenewPayment : Form
    {
        public OwnerRenewPayment()
        {
            InitializeComponent();
        }

        private void OwnerRenewPayment_Load(object sender, EventArgs e)
        {
            if (Directory.Exists(Application.StartupPath + "\\Application_Data\\Server_Directory_Data\\") == true)
            {
                ReloadDirectoryIDArray();
            }
        }

        private void CreateRenewPaymentBTN_Click(object sender, EventArgs e)
        {
            String OrderID = "";
            String CheckOutPageUrl = "";
            if (GetPaymentCheckOutPage(ref OrderID, ref CheckOutPageUrl) == true)
            {
                RenewOrderIDTB.Text = OrderID;
                RenewCheckOutPageURLTB.Text = CheckOutPageUrl;
            }
            else
            {
                MessageBox.Show("Failed to get check out page from server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RenewPaymentBTN_Click(object sender, EventArgs e)
        {
            String Base64ServerChallenge = "";
            String ServerDirectoryID = "";
            String OrderID = "";
            Byte[] Base64ServerChallengeByte = new Byte[] { };
            Boolean ConvertFromBase64String = true;
            RequestChallenge(ref Base64ServerChallenge);
            if (Base64ServerChallenge != null && Base64ServerChallenge.CompareTo("") != 0)
            {
                try
                {
                    Base64ServerChallengeByte = Convert.FromBase64String(Base64ServerChallenge);
                }
                catch
                {
                    ConvertFromBase64String = false;
                }
                if (ConvertFromBase64String == true)
                {
                    if (RenewOrderIDTB.Text != null && RenewOrderIDTB.Text.CompareTo("") != 0 && RenewServerDirectoryIDCB.Text != null && RenewServerDirectoryIDCB.Text.CompareTo("") != 0 && RenewOrderIDTB.Text != null && RenewOrderIDTB.Text.CompareTo("") != 0)
                    {
                        OrderID = RenewOrderIDTB.Text;
                        ServerDirectoryID = RenewServerDirectoryIDCB.Text;
                        if (Directory.Exists(Application.StartupPath + "\\Application_Data\\Server_Directory_Data\\" + ServerDirectoryID) == true)
                        {
                            if (RenewPayment(OrderID, ServerDirectoryID, Base64ServerChallengeByte) == true)
                            {
                                MessageBox.Show("Successfully renewed", "Succeed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Payment failed to renew", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("The directory ID that you typed does not exists in your local machine", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please check if renew checkout page ID and url is present/Renew User payment ID and Directory ID is present", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Error converting Base64 Server Challenge into byte array", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Something went wrong when fetching challenge from server..");
            }
        }

        public Boolean GetPaymentCheckOutPage(ref String OrderID, ref String CheckOutPageUrl)
        {
            CheckOutPageHolderModel PageHolder = new CheckOutPageHolderModel();
            Boolean CheckServerBoolean = true;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://mrchewitsoftware.com.my:5001/api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync("CreateReceivePayment/");
                try
                {
                    response.Wait();
                }
                catch
                {
                    CheckServerBoolean = false;
                }
                if (CheckServerBoolean == true)
                {
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();

                        var Result = readTask.Result;
                        if (Result != null && Result.CompareTo("") != 0 && Result.Contains("Error") == false)
                        {
                            PageHolder = JsonConvert.DeserializeObject<CheckOutPageHolderModel>(Result);
                            OrderID = PageHolder.PayPalOrderID;
                            CheckOutPageUrl = PageHolder.CheckOutPageUrl;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong with fetching values from server ...", "Request CheckOut Page Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Server is having some problems or offline...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public void RequestChallenge(ref String Base64ServerChallenge)
        {
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

        public Boolean RenewPayment(String OrderID, String DirectoryID, Byte[] Challenge)
        {
            if (OrderID != null && OrderID.CompareTo("") != 0 && DirectoryID != null && DirectoryID.CompareTo("") != 0 && Challenge.Length != 0)
            {
                Byte[] OrderIDByte = new Byte[] { };
                Byte[] CipheredOrderIDByte = new Byte[] { };
                Byte[] CombinedCipheredOrderIDByte = new Byte[] { };
                Byte[] ETLSSignedCombinedCipheredOrderIDByte = new Byte[] { };
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
                Byte[] ED25519PK = new Byte[] { };
                Byte[] SignedED25519PK = new Byte[] { };
                Byte[] MergedED25519PK = new Byte[] { };
                Byte[] CipheredNewDirectoryED25519PK = new Byte[] { };
                Byte[] CombinedCipheredNewDirectoryED25519PK = new Byte[] { };
                Byte[] ETLSSignedCombinedCipheredNewDirectoryED25519PK = new Byte[] { };
                Boolean ServerOnlineChecker = true;
                RevampedKeyPair MyKeyPair = SodiumPublicKeyAuth.GenerateRevampedKeyPair();
                OrderIDByte = Encoding.UTF8.GetBytes(OrderID);
                DirectoryIDByte = Encoding.UTF8.GetBytes(DirectoryID);
                if (ETLSSessionIDStorage.ETLSID.CompareTo("") != 0 && ETLSSessionIDStorage.ETLSID != null)
                {
                    ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "ECDSASK.txt");
                    UserECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryID + "\\rootSK.txt");
                    SharedSecret = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "SharedSecret.txt");
                    NonceByte = SodiumSecretBox.GenerateNonce();
                    CipheredOrderIDByte = SodiumSecretBox.Create(OrderIDByte, NonceByte, SharedSecret);
                    CombinedCipheredOrderIDByte = NonceByte.Concat(CipheredOrderIDByte).ToArray();
                    ETLSSignedCombinedCipheredOrderIDByte = SodiumPublicKeyAuth.Sign(CombinedCipheredOrderIDByte, ClientECDSASK);
                    NonceByte = SodiumSecretBox.GenerateNonce();
                    CipheredDirectoryIDByte = SodiumSecretBox.Create(DirectoryIDByte, NonceByte, SharedSecret);
                    CombinedCipheredDirectoryIDByte = NonceByte.Concat(CipheredDirectoryIDByte).ToArray();
                    ETLSSignedCombinedCipheredDirectoryIDByte = SodiumPublicKeyAuth.Sign(CombinedCipheredDirectoryIDByte, ClientECDSASK);
                    UserSignedRandomChallenge = SodiumPublicKeyAuth.Sign(Challenge, UserECDSASK, true);
                    ETLSSignedUserSignedRandomChallenge = SodiumPublicKeyAuth.Sign(UserSignedRandomChallenge, ClientECDSASK);
                    NonceByte = SodiumSecretBox.GenerateNonce();
                    ED25519PK = MyKeyPair.PublicKey;
                    SignedED25519PK = SodiumPublicKeyAuth.Sign(ED25519PK, MyKeyPair.PrivateKey);
                    MergedED25519PK = ED25519PK.Concat(SignedED25519PK).ToArray();
                    CipheredNewDirectoryED25519PK = SodiumSecretBox.Create(MergedED25519PK, NonceByte, SharedSecret, true);
                    CombinedCipheredNewDirectoryED25519PK = NonceByte.Concat(CipheredNewDirectoryED25519PK).ToArray();
                    ETLSSignedCombinedCipheredNewDirectoryED25519PK = SodiumPublicKeyAuth.Sign(CombinedCipheredNewDirectoryED25519PK, ClientECDSASK, true);
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://mrchewitsoftware.com.my:5001/api/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.GetAsync("CreateReceivePayment/RenewPayment?ClientPathID=" + ETLSSessionIDStorage.ETLSID + "&CipheredSignedOrderID=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredOrderIDByte)) + "&CipheredSignedDirectoryID=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredDirectoryIDByte)) + "&SignedSignedRandomChallenge=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserSignedRandomChallenge)) + "&CipheredSignedED25519PK=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredNewDirectoryED25519PK)));
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

                                var Result = readTask.Result;
                                if ((Result == null || Result.CompareTo("") == 0) || (Result.Contains("Error") == true))
                                {
                                    MessageBox.Show(Result.Substring(1, Result.Length - 2), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    MyKeyPair.Clear();
                                    return false;
                                }
                                else
                                {
                                    File.WriteAllBytes(Application.StartupPath + "\\Application_Data\\Server_Directory_Data\\" + DirectoryID + "\\rootSK.txt", MyKeyPair.PrivateKey);
                                    File.WriteAllBytes(Application.StartupPath + "\\Application_Data\\Server_Directory_Data\\" + DirectoryID + "\\rootPK.txt", MyKeyPair.PublicKey);
                                    MyKeyPair.Clear();
                                    return true;
                                }
                            }
                            else
                            {
                                MyKeyPair.Clear();
                                return false;
                            }
                        }
                        else
                        {
                            MyKeyPair.Clear();
                            return false;
                        }
                    }
                }
                else
                {
                    MyKeyPair.Clear();
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void ReloadDirectoryIDArray()
        {
            RenewServerDirectoryIDCB.Items.Clear();
            String[] DirectoryIDFullPathArray = Directory.GetDirectories(Application.StartupPath + "\\Application_Data\\Server_Directory_Data\\");
            String[] DirectoryIDArray = new string[DirectoryIDFullPathArray.Length];
            int Count = 0;
            int RootDirectoryCount = 0;
            RootDirectoryCount = (Application.StartupPath + "\\Application_Data\\Server_Directory_Data\\").Length;
            while (Count < DirectoryIDFullPathArray.Length)
            {
                DirectoryIDArray[Count] = DirectoryIDFullPathArray[Count].Remove(0, RootDirectoryCount);
                Count += 1;
            }
            RenewServerDirectoryIDCB.Items.AddRange(DirectoryIDArray);
        }
    }
}
