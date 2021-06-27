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
    public partial class ActionChooser : Form
    {
        public ActionChooser()
        {
            InitializeComponent();
        }

        private void OnClosing(object sender, EventArgs e) 
        {
            MainFormHolder.myForm.Show();
            MainFormHolder.OpenMainForm = true;
        }

        private void ActionCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActionCB.SelectedIndex != -1)
            {
                MessageBox.Show(ActionCB.Text, "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CreatePaymentBTN_Click(object sender, EventArgs e)
        {
            if(UserIDTempStorage.UserID!=null && UserIDTempStorage.UserID.CompareTo("") != 0) 
            {
                String OrderID = "";
                String CheckOutPageUrl = "";
                if (GetPaymentCheckOutPage(ref OrderID, ref CheckOutPageUrl) == true)
                {
                    OrderIDTB.Text = OrderID;
                    CheckOutPageURLTB.Text = CheckOutPageUrl;
                }
                else
                {
                    MessageBox.Show("Failed to get check out page from server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else 
            {
                MessageBox.Show("You haven't login/register yet", "User ID not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VerifyPaymentBTN_Click(object sender, EventArgs e)
        {
            if ((OrderIDTB.Text != null && OrderIDTB.Text.CompareTo("") != 0) == true && (CheckOutPageURLTB.Text != null && CheckOutPageURLTB.Text.CompareTo("") != 0) == true)
            {
                String OrderID = OrderIDTB.Text;
                String ServerDirectoryID = "";
                if(VerifyPayment(OrderID,ref ServerDirectoryID) == true) 
                {
                    DirectoryIDTB.Text = ServerDirectoryID;
                    ReloadDirectoryIDArray();
                    MessageBox.Show("For support, please do contact merchant through his Session messaging application ID and make sure that you have confirmed shipment on Paypal buyer side...", "Crucial Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else 
                {
                    MessageBox.Show("Something went wrong...");
                }
            }
            else
            {
                MessageBox.Show("Please create a payment", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateRenewPaymentBTN_Click(object sender, EventArgs e)
        {
            if (UserIDTempStorage.UserID != null && UserIDTempStorage.UserID.CompareTo("") != 0)
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
            else
            {
                MessageBox.Show("You haven't login/register yet", "User ID not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if(Base64ServerChallenge!=null && Base64ServerChallenge.CompareTo("") != 0) 
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
                    if (RenewOrderIDTB.Text!=null && RenewOrderIDTB.Text.CompareTo("")!=0 && RenewServerDirectoryIDCB.Text!=null && RenewServerDirectoryIDCB.Text.CompareTo("")!=0 && RenewOrderIDTB.Text!=null && RenewOrderIDTB.Text.CompareTo("")!=0) 
                    {
                        OrderID = RenewOrderIDTB.Text;
                        ServerDirectoryID = RenewServerDirectoryIDCB.Text;
                        if (Directory.Exists(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + ServerDirectoryID) == true) 
                        {
                            if (RenewPayment(OrderID, ServerDirectoryID, Base64ServerChallengeByte) == true) 
                            {
                                MessageBox.Show("Successfully renewed","Succeed",MessageBoxButtons.OK,MessageBoxIcon.Information);
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
                        MessageBox.Show("Please check if renew checkout page ID and url is present/Renew User payment ID and Directory ID is present","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
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

        private void GoBTN_Click(object sender, EventArgs e)
        {
            if(IsOwnerCB.SelectedIndex!=-1 && IsOwnerCB.Text.CompareTo("Yes") == 0 && DirectoryIDComboBox.Text!=null && DirectoryIDComboBox.Text.CompareTo("")!=0) 
            {
                if(ActionCB.SelectedIndex!=-1) 
                {
                    DirectoryIDTempStorage.DirectoryID = DirectoryIDComboBox.Text;
                    if (ActionCB.SelectedIndex == 0) 
                    {
                        var NewForm = new OwnerUploadFile();
                        NewForm.Show();
                    }
                    else if (ActionCB.SelectedIndex == 1) 
                    {
                        var NewForm = new OwnerCheckFileContent();
                        NewForm.Show();
                    }
                    else if (ActionCB.SelectedIndex == 2)
                    {
                        var NewForm = new OwnerDecryptFileContent();
                        NewForm.Show();
                    }
                    else if (ActionCB.SelectedIndex == 3)
                    {
                        var NewForm = new OwnerDeleteFileContent();
                        NewForm.Show();
                    }
                    else if(ActionCB.SelectedIndex == 4) 
                    {
                        var NewForm = new OfflineBackupAllKeys();
                        NewForm.Show();
                    }
                }
                else 
                {
                    MessageBox.Show("Please choose an action", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else 
            {
                MessageBox.Show("Not supported","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void ActionChooser_Load(object sender, EventArgs e)
        {
            if (Directory.Exists(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\") == true) 
            {
                ReloadDirectoryIDArray();
            }
        }

        public Boolean GetPaymentCheckOutPage(ref String OrderID, ref String CheckOutPageUrl) 
        {
            CheckOutPageHolderModel PageHolder = new CheckOutPageHolderModel();
            Boolean CheckServerBoolean = true;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://{API URL}");
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
                        if (Result != null && Result.CompareTo("")!=0 && Result.Contains("Error")==false) 
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

        public Boolean VerifyPayment(String OrderID, ref String DirectoryID) 
        {
            Byte[] ClientECDSASK = new Byte[] { };
            Byte[] SharedSecret = new Byte[] { };
            Byte[] OrderIDByte = new Byte[] { };
            Byte[] NonceByte = new Byte[] { };
            Byte[] CipheredOrderIDByte = new Byte[] { };
            Byte[] CombinedCipheredOrderIDByte = new Byte[] { };
            Byte[] ETLSSignedCombinedCipheredOrderIDByte = new Byte[] { };
            Byte[] CipheredED25519PK = new Byte[] { };
            Byte[] CombinedCipheredED25519PK = new Byte[] { };
            Byte[] ETLSSignedCombinedCipheredED25519PK = new Byte[] { };
            Boolean CheckServerBoolean = true;
            RevampedKeyPair MyKeyPair = SodiumPublicKeyAuth.GenerateRevampedKeyPair();
            GCHandle MyGeneralGCHandle = new GCHandle();
            FileCreationModel DirectoryHolder = new FileCreationModel();
            String ETLSSessionID = "";
            ETLSSessionID = File.ReadAllText(Application.StartupPath + "\\Temp_Session\\" + "SessionID.txt");
            if (OrderID != null && OrderID.CompareTo("") != 0)
            {
                if (ETLSSessionID != null && ETLSSessionID.CompareTo("") != 0)
                {
                    ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionID + "\\" + "ECDSASK.txt");
                    SharedSecret = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionID + "\\" + "SharedSecret.txt");
                    OrderIDByte = Encoding.UTF8.GetBytes(OrderID);
                    NonceByte = SodiumSecretBox.GenerateNonce();
                    CipheredOrderIDByte = SodiumSecretBox.Create(OrderIDByte, NonceByte, SharedSecret);
                    CombinedCipheredOrderIDByte = NonceByte.Concat(CipheredOrderIDByte).ToArray();
                    ETLSSignedCombinedCipheredOrderIDByte = SodiumPublicKeyAuth.Sign(CombinedCipheredOrderIDByte, ClientECDSASK);
                    ClientECDSASK= File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionID + "\\" + "ECDSASK.txt");
                    NonceByte = new Byte[] { };
                    NonceByte = SodiumSecretBox.GenerateNonce();
                    CipheredED25519PK = SodiumSecretBox.Create(MyKeyPair.PublicKey, NonceByte, SharedSecret);
                    CombinedCipheredED25519PK = NonceByte.Concat(CipheredED25519PK).ToArray();
                    ETLSSignedCombinedCipheredED25519PK = SodiumPublicKeyAuth.Sign(CombinedCipheredED25519PK, ClientECDSASK);
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://{{API URL}}");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.GetAsync("CreateReceivePayment/CheckPayment?ClientPathID=" + ETLSSessionID + "&CipheredSignedOrderID=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredOrderIDByte)) + "&CipheredSignedED25519PK=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredED25519PK)));
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
                                if((Result == null || Result.CompareTo("") == 0) || (Result.Contains("Error") == true)) 
                                {
                                    MessageBox.Show(Result.Substring(1,Result.Length-2), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return false;
                                }
                                else 
                                {
                                    DirectoryHolder = JsonConvert.DeserializeObject<FileCreationModel>(Result);
                                    if(DirectoryHolder.Status!=null && DirectoryHolder.Status.CompareTo("") != 0) 
                                    {
                                        DirectoryID = DirectoryHolder.FolderID;
                                        if (Directory.Exists(Application.StartupPath + "\\Application_Data\\" + "\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data") == false)
                                        {
                                            Directory.CreateDirectory(Application.StartupPath + "\\Application_Data\\" + "\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data");
                                        }
                                        Directory.CreateDirectory(Application.StartupPath + "\\Application_Data\\" + "\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryHolder.FolderID);
                                        File.WriteAllBytes(Application.StartupPath + "\\Application_Data\\" + "\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryHolder.FolderID + "\\rootSK.txt", MyKeyPair.PrivateKey);
                                        File.WriteAllBytes(Application.StartupPath + "\\Application_Data\\" + "\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryHolder.FolderID + "\\rootPK.txt", MyKeyPair.PublicKey);
                                        MyGeneralGCHandle = GCHandle.Alloc(ClientECDSASK, GCHandleType.Pinned);
                                        SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), ClientECDSASK.Length);
                                        MyGeneralGCHandle.Free();
                                        MyGeneralGCHandle = GCHandle.Alloc(SharedSecret, GCHandleType.Pinned);
                                        SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), SharedSecret.Length);
                                        MyGeneralGCHandle.Free();
                                        MyKeyPair.Clear();
                                        return true;
                                    }
                                    else
                                    {
                                        MyGeneralGCHandle = GCHandle.Alloc(ClientECDSASK, GCHandleType.Pinned);
                                        SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), ClientECDSASK.Length);
                                        MyGeneralGCHandle.Free();
                                        MyGeneralGCHandle = GCHandle.Alloc(SharedSecret, GCHandleType.Pinned);
                                        SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), SharedSecret.Length);
                                        MyGeneralGCHandle.Free();
                                        MyKeyPair.Clear();
                                        return false;
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
                                MyKeyPair.Clear();
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
                            MyKeyPair.Clear();
                            return false;
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
                    MyKeyPair.Clear();
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
                MyKeyPair.Clear();
                return false;
            }
        }

        public void RequestChallenge(ref String Base64ServerChallenge) 
        {
            String AuthenticationType = "Renew Payment";
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
                                        Re_RequestRandomChallenge(ref Re_RequestRandomChallengeBoolean,ref Base64ServerChallenge);
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
            String AuthenticationType = "Renew Payment";
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

        public Boolean RenewPayment(String OrderID,String DirectoryID,Byte[] Challenge) 
        {
            if(OrderID!=null && OrderID.CompareTo("")!=0 && DirectoryID!=null && DirectoryID.CompareTo("")!=0 && Challenge.Length != 0) 
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
                Byte[] UserIDByte = new Byte[] { };
                Byte[] ETLSSignedUserIDByte = new Byte[] { };
                Byte[] UserSignedRandomChallenge = new Byte[] { };
                Byte[] ETLSSignedUserSignedRandomChallenge = new Byte[] { };
                Byte[] NonceByte = new Byte[] { };
                Byte[] CipheredNewDirectoryED25519PK = new Byte[] { };
                Byte[] CombinedCipheredNewDirectoryED25519PK = new Byte[] { };
                Byte[] ETLSSignedCombinedCipheredNewDirectoryED25519PK = new Byte[] { };
                GCHandle MyGeneralGCHandle = new GCHandle();
                Boolean ServerOnlineChecker = true;
                RevampedKeyPair MyKeyPair = SodiumPublicKeyAuth.GenerateRevampedKeyPair();
                OrderIDByte = Encoding.UTF8.GetBytes(OrderID);
                DirectoryIDByte = Encoding.UTF8.GetBytes(DirectoryID);
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
                        CipheredOrderIDByte = SodiumSecretBox.Create(OrderIDByte, NonceByte, SharedSecret);
                        CombinedCipheredOrderIDByte = NonceByte.Concat(CipheredOrderIDByte).ToArray();
                        ETLSSignedCombinedCipheredOrderIDByte = SodiumPublicKeyAuth.Sign(CombinedCipheredOrderIDByte, ClientECDSASK);
                        NonceByte = new Byte[] { };
                        NonceByte = SodiumSecretBox.GenerateNonce();
                        ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "ECDSASK.txt");
                        CipheredDirectoryIDByte = SodiumSecretBox.Create(DirectoryIDByte, NonceByte, SharedSecret);
                        CombinedCipheredDirectoryIDByte = NonceByte.Concat(CipheredDirectoryIDByte).ToArray();
                        ETLSSignedCombinedCipheredDirectoryIDByte = SodiumPublicKeyAuth.Sign(CombinedCipheredDirectoryIDByte, ClientECDSASK);
                        ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "ECDSASK.txt");
                        UserSignedRandomChallenge = SodiumPublicKeyAuth.Sign(Challenge, UserECDSASK);
                        ETLSSignedUserSignedRandomChallenge = SodiumPublicKeyAuth.Sign(UserSignedRandomChallenge, ClientECDSASK);
                        ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "ECDSASK.txt");
                        NonceByte = new Byte[] { };
                        NonceByte = SodiumSecretBox.GenerateNonce();
                        CipheredNewDirectoryED25519PK = SodiumSecretBox.Create(MyKeyPair.PublicKey, NonceByte, SharedSecret);
                        CombinedCipheredNewDirectoryED25519PK = NonceByte.Concat(CipheredNewDirectoryED25519PK).ToArray();
                        ETLSSignedCombinedCipheredNewDirectoryED25519PK = SodiumPublicKeyAuth.Sign(CombinedCipheredNewDirectoryED25519PK, ClientECDSASK);
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri("https://{API URL}");
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(
                                new MediaTypeWithQualityHeaderValue("application/json"));
                            var response = client.GetAsync("CreateReceivePayment/RenewPayment?ClientPathID=" + ETLSSessionIDStorage.ETLSID + "&SignedUserID=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserIDByte))  +"&CipheredSignedOrderID=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredOrderIDByte)) + "&CipheredSignedDirectoryID=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredDirectoryIDByte)) + "&SignedSignedRandomChallenge=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserSignedRandomChallenge)) + "&CipheredSignedED25519PK=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredNewDirectoryED25519PK)));
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
                                    if((Result==null || Result.CompareTo("") == 0)||(Result.Contains("Error")==true)) 
                                    {
                                        MessageBox.Show(Result.Substring(1, Result.Length - 2), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return false;
                                    }
                                    else
                                    {
                                        File.WriteAllBytes(Application.StartupPath + "\\Application_Data\\" + "\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\rootSK.txt", MyKeyPair.PrivateKey);
                                        File.WriteAllBytes(Application.StartupPath + "\\Application_Data\\" + "\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\rootPK.txt", MyKeyPair.PublicKey);
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

        public void ReloadDirectoryIDArray()
        {
            DirectoryIDComboBox.Items.Clear();
            RenewServerDirectoryIDCB.Items.Clear();
            String[] DirectoryIDFullPathArray = Directory.GetDirectories(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\");
            String[] DirectoryIDArray = new string[DirectoryIDFullPathArray.Length];
            int Count = 0;
            int RootDirectoryCount = 0;
            RootDirectoryCount = (Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\").Length;
            while (Count < DirectoryIDFullPathArray.Length)
            {
                DirectoryIDArray[Count] = DirectoryIDFullPathArray[Count].Remove(0, RootDirectoryCount);
                Count += 1;
            }
            DirectoryIDComboBox.Items.AddRange(DirectoryIDArray);
            RenewServerDirectoryIDCB.Items.AddRange(DirectoryIDArray);
        }
    }
}
