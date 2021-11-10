using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PriSecFileStorageWeb.Model;
using ASodium;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace PriSecFileStorageWeb.Pages
{
    public class OwnerMakePaymentModel : PageModel
    {
        public void OnGet()
        {
        }

        //Create PayPal CheckOutPage
        public void OnPost() 
        {
            String OrderID = "";
            String CheckOutPageUrl = "";
            if (GetPaymentCheckOutPage(ref OrderID, ref CheckOutPageUrl) == true)
            {
                ViewData["Order_ID"] = OrderID;
                ViewData["CheckOutPageURL"] = CheckOutPageUrl;
            }
            else
            {
                Console.WriteLine("Error: Failed to get check out page from server");
            }
        }

        //Verify payment on a certain paypal checkoutpage and create directories and cryptographic information on local device.
        public void OnPostVPayment() 
        {
            String OrderID = Request.Form["Order_ID"].ToString();
            String CheckOutPageUrl = Request.Form["Payment_URL"].ToString();
            String ServerDirectoryID = "";
            if ((OrderID != null && OrderID.CompareTo("") != 0) == true && (CheckOutPageUrl!= null && CheckOutPageUrl.CompareTo("") != 0) == true)
            {                
                if (VerifyPayment(OrderID, ref ServerDirectoryID) == true)
                {
                    ViewData["Storage_ID"] = ServerDirectoryID;
                    ViewData["Order_ID"] = OrderID;
                    ViewData["CheckOutPageURL"] = CheckOutPageUrl;
                    Console.WriteLine("Crucial Information: For support, please do contact merchant through his Session messaging application ID and make sure that you have confirmed shipment on Paypal buyer side...");
                }
                else
                {
                    Console.WriteLine("Something went wrong...");
                }
            }
            else
            {
                Console.WriteLine("Error: Please create a payment");
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
                        Console.WriteLine("Request CheckOut Page Error: Something went wrong with fetching values from server ...");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Error: Server is having some problems or offline...");
                    return false;
                }
            }
        }

        public Boolean VerifyPayment(String OrderID, ref String DirectoryID)
        {
            String RootDirectory = AppContext.BaseDirectory;
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
            ETLSSessionID = System.IO.File.ReadAllText(RootDirectory + "/Temp_Session/" + "SessionID.txt");
            if (OrderID != null && OrderID.CompareTo("") != 0)
            {
                if (ETLSSessionID != null && ETLSSessionID.CompareTo("") != 0)
                {
                    ClientECDSASK = System.IO.File.ReadAllBytes(RootDirectory + "/Temp_Session/" + ETLSSessionID + "/" + "ECDSASK.txt");
                    SharedSecret = System.IO.File.ReadAllBytes(RootDirectory + "/Temp_Session/" + ETLSSessionID + "/" + "SharedSecret.txt");
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
                        var response = client.GetAsync("CreateReceivePayment/CheckPayment?ClientPathID=" + ETLSSessionID + "&CipheredSignedOrderID=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredOrderIDByte)) + "&CipheredSignedED25519PK=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredED25519PK)));
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
                                    Console.WriteLine(Result.Substring(1, Result.Length - 2));
                                    return false;
                                }
                                else
                                {
                                    DirectoryHolder = JsonConvert.DeserializeObject<FileCreationModel>(Result);
                                    if (DirectoryHolder.Status != null && DirectoryHolder.Status.CompareTo("") != 0)
                                    {
                                        DirectoryID = DirectoryHolder.FolderID;
                                        Directory.CreateDirectory(RootDirectory + "/Server_Directory_Data/" + DirectoryHolder.FolderID);
                                        System.IO.File.WriteAllBytes(RootDirectory + "/Server_Directory_Data/" + DirectoryHolder.FolderID + "/rootSK.txt", MyKeyPair.PrivateKey);
                                        System.IO.File.WriteAllBytes(RootDirectory + "/Server_Directory_Data/" + DirectoryHolder.FolderID + "/rootPK.txt", MyKeyPair.PublicKey);
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
