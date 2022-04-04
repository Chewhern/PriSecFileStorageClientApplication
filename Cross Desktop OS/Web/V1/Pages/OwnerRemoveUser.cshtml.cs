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
    public class OwnerRemoveUserModel : PageModel
    {
        public String[] ListOfStorageIDs = new String[] { };
        public String[] ListOfAccessIDs = new String[] { };

        public void OnGet()
        {
            ReloadDirectory();
            ReloadAllowedUser();
        }

        public void OnPost() 
        {
            ReloadDirectory();
            ReloadAllowedUser();
            String AccessID = Request.Form["Access_ID"].ToString();
            String Directory_ID = Request.Form["Storage_ID"].ToString();
            if (AccessID.CompareTo("") != 0 && Directory_ID.CompareTo("")!=0)
            {
                String Base64Challenge = "";
                Boolean AbleToRequestChallenge = true;
                Byte[] Base64ChallengeByte = new Byte[] { };
                String Result = "";
                RequestChallenge(ref AbleToRequestChallenge, ref Base64Challenge);
                while (AbleToRequestChallenge == false)
                {
                    RequestChallenge(ref AbleToRequestChallenge, ref Base64Challenge);
                }
                Base64ChallengeByte = Convert.FromBase64String(Base64Challenge);
                Result = RemoveUserFunction(Directory_ID, AccessID, Base64ChallengeByte);
                if (Result.CompareTo("") != 0)
                {
                    ReloadAllowedUser();
                    ViewData["Status"] = "Succeed:_Successfully_removed_the_user_from_your_storage";
                }
                ViewData["Current_Storage_ID"] = Directory_ID;
                ViewData["Current_Access_ID"] = AccessID;
            }
            else
            {
                ViewData["Status"] = "Allowed_User_Access_ID_and_Storage_ID_must_not_be_null/Empty";
                ViewData["Current_Storage_ID"] = Directory_ID;
                ViewData["Current_Access_ID"] = AccessID;
            }
        }

        public void OnPostReadRemarks() 
        {
            ReloadDirectory();
            ReloadAllowedUser();
            String RootDirectory = AppContext.BaseDirectory;
            String AccessID = Request.Form["Access_ID"].ToString();
            String Directory_ID = Request.Form["Storage_ID"].ToString();
            String Remarks = "";
            if (AccessID.CompareTo("") != 0 && Directory_ID.CompareTo("") != 0)
            {
                if (System.IO.File.Exists(RootDirectory + "/Server_Directory_Data/" + Directory_ID + "/Other_User/" + AccessID + "/Note.txt") == true)
                {
                    Remarks= System.IO.File.ReadAllText(RootDirectory + "/Server_Directory_Data/" + Directory_ID + "/Other_User/" + AccessID + "/Note.txt");
                    ViewData["Remarks"] = Remarks;
                }
            }
            ViewData["Current_Storage_ID"] = Directory_ID;
            ViewData["Current_Access_ID"] = AccessID;
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


        public void ReloadAllowedUser()
        {
            String[] DirectoryID = ListOfStorageIDs;
            String[] AllowedUserID = new String[] { };
            String Base64Challenge = "";
            Byte[] RandomChallenge = new Byte[] { };
            Boolean RequestChallengeBoolean = true;
            int LoopCount = 0;
            while (LoopCount < DirectoryID.Length)
            {
                RequestChallenge(ref RequestChallengeBoolean, ref Base64Challenge);
                while (Base64Challenge.CompareTo("") == 0)
                {
                    RequestChallenge(ref RequestChallengeBoolean, ref Base64Challenge);
                }
                RandomChallenge = Convert.FromBase64String(Base64Challenge);
                AllowedUserID = AllowedUserID.Concat(ViewUserFunction(DirectoryID[LoopCount], RandomChallenge)).ToArray();
                LoopCount += 1;
            }
            ListOfAccessIDs = AllowedUserID;
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
                    if (APIIPAddressHelper.HasSet == true)
                    {
                        client.BaseAddress = new Uri(APIIPAddressHelper.IPAddress);
                    }
                    else
                    {
                        client.BaseAddress = new Uri("https://mrchewitsoftware.com.my:5001/api/");
                    }
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

        public String RemoveUserFunction(String DirectoryID, String AccessID, Byte[] RandomChallenge)
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
            Byte[] AccessIDByte = new Byte[] { };
            Byte[] CipheredAccessIDByte = new Byte[] { };
            Byte[] CombinedCipheredAccessIDByte = new Byte[] { };
            Byte[] ETLSSignedCombinedCipheredAccessIDByte = new Byte[] { };
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
                AccessIDByte = Encoding.UTF8.GetBytes(AccessID);
                CipheredAccessIDByte = SodiumSecretBox.Create(AccessIDByte, Nonce, SharedSecret, true);
                CombinedCipheredAccessIDByte = Nonce.Concat(CipheredAccessIDByte).ToArray();
                ETLSSignedCombinedCipheredAccessIDByte = SodiumPublicKeyAuth.Sign(CombinedCipheredAccessIDByte, ClientECDSASK, true);
                using (var client = new HttpClient())
                {
                    if (APIIPAddressHelper.HasSet == true)
                    {
                        client.BaseAddress = new Uri(APIIPAddressHelper.IPAddress);
                    }
                    else
                    {
                        client.BaseAddress = new Uri("https://mrchewitsoftware.com.my:5001/api/");
                    }
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync("OwnerAddUser/RemovePK?ClientPathID=" + ETLSSessionIDStorage.ETLSID + "&CipheredSignedDirectoryID=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredDirectoryIDByte)) + "&SignedSignedRandomChallenge=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserSignedRandomChallenge)) + "&CipheredSignedAnotherUserID=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredAccessIDByte)));
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

        public String[] ViewUserFunction(String DirectoryID, Byte[] RandomChallenge)
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
            Boolean ServerOnlineChecker = true;
            AllowedUserListModel MyAllowedUserList = new AllowedUserListModel();
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
                using (var client = new HttpClient())
                {
                    if (APIIPAddressHelper.HasSet == true)
                    {
                        client.BaseAddress = new Uri(APIIPAddressHelper.IPAddress);
                    }
                    else
                    {
                        client.BaseAddress = new Uri("https://mrchewitsoftware.com.my:5001/api/");
                    }
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync("OwnerAddUser/GetAllowedUsers?ClientPathID=" + ETLSSessionIDStorage.ETLSID + "&CipheredSignedDirectoryID=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredDirectoryIDByte)) + "&SignedSignedRandomChallenge=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserSignedRandomChallenge)));
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
                        return new String[] { };
                    }
                    else
                    {
                        var result = response.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsStringAsync();
                            readTask.Wait();

                            var StringResult = readTask.Result;
                            MyAllowedUserList = JsonConvert.DeserializeObject<AllowedUserListModel>(StringResult);

                            if (MyAllowedUserList != null)
                            {
                                if (MyAllowedUserList.Status.Contains("Error") == true)
                                {
                                    Console.WriteLine(MyAllowedUserList.Status);
                                    return new String[] { };
                                }
                                else
                                {
                                    return MyAllowedUserList.AllowedUserID;
                                }
                            }
                            else
                            {
                                return new String[] { };
                            }
                        }
                        else
                        {
                            return new String[] { };
                        }
                    }
                }
            }
            else
            {
                return new String[] { };
            }
        }
    }
}
