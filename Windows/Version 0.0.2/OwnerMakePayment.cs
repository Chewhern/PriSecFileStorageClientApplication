using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using Newtonsoft.Json;
using ASodium;
using PriSecFileStorageClient.Helper;
using PriSecFileStorageClient.Model;
using System.IO;

namespace PriSecFileStorageClient
{
    public partial class OwnerMakePayment : Form
    {
        public OwnerMakePayment()
        {
            InitializeComponent();
        }

        private void CreatePaymentBTN_Click(object sender, EventArgs e)
        {
            if (UserIDTempStorage.UserID != null && UserIDTempStorage.UserID.CompareTo("") != 0)
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
                if (VerifyPayment(OrderID, ref ServerDirectoryID) == true)
                {
                    DirectoryIDTB.Text = ServerDirectoryID;
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

        public Boolean VerifyPayment(String OrderID, ref String DirectoryID)
        {
            Byte[] ClientECDSASK = new Byte[] { };
            Byte[] SharedSecret = new Byte[] { };
            Byte[] OrderIDByte = new Byte[] { };
            Byte[] NonceByte = new Byte[] { };
            Byte[] CipheredOrderIDByte = new Byte[] { };
            Byte[] CombinedCipheredOrderIDByte = new Byte[] { };
            Byte[] ETLSSignedCombinedCipheredOrderIDByte = new Byte[] { };
            Byte[] ED25519PK = new Byte[] { };
            Byte[] SignedED25519PK = new Byte[] { };
            Byte[] MergedED25519PK = new Byte[] { };
            Byte[] CipheredED25519PK = new Byte[] { };
            Byte[] CombinedCipheredED25519PK = new Byte[] { };
            Byte[] ETLSSignedCombinedCipheredED25519PK = new Byte[] { };
            Boolean CheckServerBoolean = true;
            RevampedKeyPair MyKeyPair = SodiumPublicKeyAuth.GenerateRevampedKeyPair();
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
                    NonceByte = SodiumSecretBox.GenerateNonce();
                    ED25519PK = MyKeyPair.PublicKey;
                    SignedED25519PK = SodiumPublicKeyAuth.Sign(ED25519PK, MyKeyPair.PrivateKey);
                    MergedED25519PK = ED25519PK.Concat(SignedED25519PK).ToArray();
                    CipheredED25519PK = SodiumSecretBox.Create(MergedED25519PK, NonceByte, SharedSecret, true);
                    CombinedCipheredED25519PK = NonceByte.Concat(CipheredED25519PK).ToArray();
                    ETLSSignedCombinedCipheredED25519PK = SodiumPublicKeyAuth.Sign(CombinedCipheredED25519PK, ClientECDSASK, true);
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://mrchewitsoftware.com.my:5001/api/");
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
                                if ((Result == null || Result.CompareTo("") == 0) || (Result.Contains("Error") == true))
                                {
                                    MessageBox.Show(Result.Substring(1, Result.Length - 2), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return false;
                                }
                                else
                                {
                                    DirectoryHolder = JsonConvert.DeserializeObject<FileCreationModel>(Result);
                                    if (DirectoryHolder.Status != null && DirectoryHolder.Status.CompareTo("") != 0)
                                    {
                                        DirectoryID = DirectoryHolder.FolderID;
                                        if (Directory.Exists(Application.StartupPath + "\\Application_Data\\" + "\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data") == false)
                                        {
                                            Directory.CreateDirectory(Application.StartupPath + "\\Application_Data\\" + "\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data");
                                        }
                                        Directory.CreateDirectory(Application.StartupPath + "\\Application_Data\\" + "\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryHolder.FolderID);
                                        File.WriteAllBytes(Application.StartupPath + "\\Application_Data\\" + "\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryHolder.FolderID + "\\rootSK.txt", MyKeyPair.PrivateKey);
                                        File.WriteAllBytes(Application.StartupPath + "\\Application_Data\\" + "\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryHolder.FolderID + "\\rootPK.txt", MyKeyPair.PublicKey);
                                        MyKeyPair.Clear();
                                        return true;
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
                MyKeyPair.Clear();
                return false;
            }
        }
    }
}
