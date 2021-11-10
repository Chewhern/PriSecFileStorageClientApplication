using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using ASodium;
using PriSecFileStorageWeb.Model;
using PriSecFileStorageWeb.Helper;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace PriSecFileStorageWeb.Pages
{
    public class OwnerAddUserModel : PageModel
    {
        public String[] ListOfStorageIDs = new String[] { };

        public void OnGet()
        {
            ReloadDirectory();
        }

        public void OnPost() 
        {
            String RootDirectory = AppContext.BaseDirectory;
            String Merged_PK = Request.Form["Merged_PK"].ToString();
            String Storage_ID = Request.Form["Storage_ID"].ToString();
            String Remarks = Request.Form["Remarks"].ToString();
            String Access_ID = "";
            ReloadDirectory();
            if (Merged_PK.CompareTo("") != 0 && Storage_ID.CompareTo("")!=0)
            {
                String Base64Challenge = "";
                Boolean AbleToRequestChallenge = true;
                Byte[] Base64ChallengeByte = new Byte[] { };
                Byte[] MergedED25519PK = new Byte[] { };
                Boolean AbleToConvertFromBase64 = true;
                String Result = "";
                try
                {
                    MergedED25519PK = Convert.FromBase64String(Merged_PK);
                }
                catch
                {
                    Console.WriteLine("Error: Please pass in a correct format of ED25519 PK");
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
                    Result = AddUserFunction(Storage_ID, MergedED25519PK, Base64ChallengeByte);
                    if (Result.CompareTo("") != 0)
                    {
                        Result = Result.Remove(0, 4);
                        Access_ID = Result;
                        ViewData["Current_Storage_ID"] = Storage_ID;
                        ViewData["Access_ID"] = Access_ID;
                        if (Remarks.CompareTo("") != 0)
                        {
                            if (Directory.Exists(RootDirectory + "/Server_Directory_Data/" + Storage_ID + "/Other_User") == false)
                            {
                                Directory.CreateDirectory(RootDirectory + "/Server_Directory_Data/" + Storage_ID + "/Other_User");
                            }
                            if (Directory.Exists(RootDirectory + "/Server_Directory_Data/" + Storage_ID + "/Other_User/" + Result) == false)
                            {
                                Directory.CreateDirectory(RootDirectory + "/Server_Directory_Data/" + Storage_ID + "/Other_User/" + Result);
                            }
                            System.IO.File.WriteAllText(RootDirectory + "/Server_Directory_Data/" + Storage_ID + "/Other_User/" + Result + "/Note.txt", Remarks);
                            ViewData["Remarks"] = Remarks;
                        }
                        ViewData["Status"] = "Succeed:User_have_been_added_to_permission_list";
                    }
                }
            }
            else
            {
                ViewData["Current_Storage_ID"] = Storage_ID;
                ViewData["Status"] = "Error:You_must_get_merged_ED25519_PKs_from_the_user";
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
                                    Re_RequestRandomChallengeBoolean = true;
                                    SodiumSecureMemory.SecureClearBytes(ServerSignedRandomChallenge);
                                    SodiumSecureMemory.SecureClearBytes(ServerRandomChallenge);
                                    SodiumSecureMemory.SecureClearBytes(ServerED25519PK);
                                }
                                else
                                {
                                    Console.WriteLine("Man in the middle spotted ..., proceeding with re-requesting random challenge from server...");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Registration Error: Something went wrong with fetching values from server ...");
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

        public String AddUserFunction(String DirectoryID, Byte[] MergedED25519PK, Byte[] RandomChallenge)
        {
            String RootDirectory = AppContext.BaseDirectory;
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
                ClientECDSASK = System.IO.File.ReadAllBytes(RootDirectory + "/Temp_Session/" + ETLSSessionIDStorage.ETLSID + "/" + "ECDSASK.txt");
                SharedSecret = System.IO.File.ReadAllBytes(RootDirectory + "/Temp_Session/" + ETLSSessionIDStorage.ETLSID + "/" + "SharedSecret.txt");
                UserECDSASK = System.IO.File.ReadAllBytes(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/rootSK.txt");
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
                    var response = client.GetAsync("OwnerAddUser/UploadPK?ClientPathID=" + ETLSSessionIDStorage.ETLSID + "&CipheredSignedDirectoryID=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredDirectoryIDByte)) + "&SignedSignedRandomChallenge=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserSignedRandomChallenge)) + "&CipheredSignedED25519PK=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredMergedED25519PK)));
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
                                    Console.WriteLine(StringResult);
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
    }
}
