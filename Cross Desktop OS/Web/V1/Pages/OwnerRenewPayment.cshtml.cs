using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PriSecFileStorageWeb.Model;
using PriSecFileStorageWeb.Helper;
using ASodium;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace PriSecFileStorageWeb.Pages
{
    public class OwnerRenewPaymentModel : PageModel
    {
        public String[] ListOfStorageIDs = new String[] { };

        public void OnGet()
        {
            ReloadDirectory();
        }

        //Create Payment
        public void OnPost() 
        {
            ReloadDirectory();
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

        public void OnPostVPayment() 
        {
            ReloadDirectory();
            String RootDirectory = AppContext.BaseDirectory;
            String Base64ServerChallenge = "";
            String OrderID = Request.Form["Order_ID"].ToString();
            String CheckOutPageUrl = Request.Form["Payment_URL"].ToString();
            String ServerDirectoryID = Request.Form["Storage_ID"].ToString();
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
                    if (OrderID != null && OrderID.CompareTo("") != 0 && OrderID != null && ServerDirectoryID.CompareTo("") != 0)
                    {
                        if (Directory.Exists(RootDirectory + "/Server_Directory_Data/" + ServerDirectoryID) == true)
                        {
                            if (RenewPayment(OrderID, ServerDirectoryID, Base64ServerChallengeByte) == true)
                            {
                                ViewData["Order_ID"] = OrderID;
                                ViewData["CheckOutPageURL"] = CheckOutPageUrl;
                                ViewData["Storage_ID"] = ServerDirectoryID;
                                ViewData["Status"]="Succeed:_Successfully_renewed";
                            }
                            else
                            {
                                ViewData["Status"] = "Failed:_Payment_failed_to_renew";
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error: The directory ID that you typed does not exists in your local machine");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: Please check if renew checkout url and order ID is present");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Error converting Base64 Server Challenge into byte array");
                }
            }
            else
            {
                Console.WriteLine("Something went wrong when fetching challenge from server..");
            }
        }

        public void ReloadDirectory() 
        {
            String RootDirectory = AppContext.BaseDirectory;
            String[] DirectoryIDFullPathArray = Directory.GetDirectories(RootDirectory + "/Server_Directory_Data/");
            String[] DirectoryIDArray = new string[DirectoryIDFullPathArray.Length];
            int Count = 0;
            int RootDirectoryCount = 0;
            RootDirectoryCount = (RootDirectory + "/Server_Directory_Data/").Length;
            while (Count < DirectoryIDFullPathArray.Length)
            {
                DirectoryIDArray[Count] = DirectoryIDFullPathArray[Count].Remove(0, RootDirectoryCount);
                Count += 1;
            }
            ListOfStorageIDs = DirectoryIDArray;
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
                                Console.WriteLine(MyLoginModels.RequestStatus);
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
                                    Console.WriteLine("Error: Man in the middle spotted ..., proceeding with re-requesting random challenge from server...");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error: Something went wrong with fetching values from server ...");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: Server is now offline...");
                    }
                }
            }
            else
            {
                Console.WriteLine("Error: You have not yet establish an ephemeral TLS session with the server..");
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

        public Boolean RenewPayment(String OrderID, String DirectoryID, Byte[] Challenge)
        {
            String RootDirectory = AppContext.BaseDirectory;
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
                    ClientECDSASK = System.IO.File.ReadAllBytes(RootDirectory + "/Temp_Session/" + ETLSSessionIDStorage.ETLSID + "/" + "ECDSASK.txt");
                    UserECDSASK = System.IO.File.ReadAllBytes(RootDirectory +  "/Server_Directory_Data/" + DirectoryID + "/rootSK.txt");
                    SharedSecret = System.IO.File.ReadAllBytes(RootDirectory + "/Temp_Session/" + ETLSSessionIDStorage.ETLSID + "/" + "SharedSecret.txt");
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
                        var response = client.GetAsync("CreateReceivePayment/RenewPayment?ClientPathID=" + ETLSSessionIDStorage.ETLSID + "&CipheredSignedOrderID=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredOrderIDByte)) + "&CipheredSignedDirectoryID=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredDirectoryIDByte)) + "&SignedSignedRandomChallenge=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserSignedRandomChallenge)) + "&CipheredSignedED25519PK=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredNewDirectoryED25519PK)));
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
                                    Console.WriteLine(Result.Substring(1, Result.Length - 2));
                                    MyKeyPair.Clear();
                                    return false;
                                }
                                else
                                {
                                    System.IO.File.WriteAllBytes(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/rootSK.txt", MyKeyPair.PrivateKey);
                                    System.IO.File.WriteAllBytes(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/rootPK.txt", MyKeyPair.PublicKey);
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
    }
}
