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
using System.Threading;

namespace PriSecFileStorageWeb.Pages
{
    public class OwnerDeleteFileContentModel : PageModel
    {
        public String Current_Directory_ID = "";
        public String[] ListOfFileNames = new String[] { };
        public String Chosen_File_Name = "";
        public String File_Name = "";
        public String Status = "";
        public String Storage_Size_In_String = "";

        public void OnGet()
        {
            if (DirectoryIDTempStorage.DirectoryID != null && DirectoryIDTempStorage.DirectoryID.CompareTo("") != 0)
            {
                Current_Directory_ID = DirectoryIDTempStorage.DirectoryID;
                ReloadFiles();
                GetFileStorageUsedSize(ref Storage_Size_In_String);
            }
        }

        public void OnPostGetFileName()
        {
            if (DirectoryIDTempStorage.DirectoryID != null && DirectoryIDTempStorage.DirectoryID.CompareTo("") != 0)
            {
                Current_Directory_ID = DirectoryIDTempStorage.DirectoryID;
                ReloadFiles();
                GetFileStorageUsedSize(ref Storage_Size_In_String);
            }
            String RandomFileName = Request.Form["Random_File_Name"].ToString();
            String CurrentDirectoryID = Request.Form["Storage_ID"].ToString();
            String RootDirectory = AppContext.BaseDirectory;
            if (RandomFileName.CompareTo("") != 0 && CurrentDirectoryID.CompareTo("") != 0)
            {
                Byte[] OriginalFileNameByte = new Byte[] { };
                String FileName = "";
                Boolean CheckFileNameExists = true;
                try
                {
                    OriginalFileNameByte = System.IO.File.ReadAllBytes(RootDirectory + "/Server_Directory_Data/" + DirectoryIDTempStorage.DirectoryID + "/Encrypted_Files/" + RandomFileName + "/FileName.txt");
                }
                catch
                {
                    CheckFileNameExists = false;
                }
                if (CheckFileNameExists == true)
                {
                    FileName = Encoding.UTF8.GetString(OriginalFileNameByte);
                    File_Name = FileName;
                }
                else
                {
                    Console.WriteLine("Error: Sorry system can't find the file name from the selected random file name");
                }
            }
            else
            {
                Console.WriteLine("Error: You haven't choose either a random file or purchased storage ID");
            }
            Chosen_File_Name = RandomFileName;
        }

        public void OnPost() 
        {
            if (DirectoryIDTempStorage.DirectoryID != null && DirectoryIDTempStorage.DirectoryID.CompareTo("") != 0)
            {
                Current_Directory_ID = DirectoryIDTempStorage.DirectoryID;
                ReloadFiles();
            }
            String RandomFileName = Request.Form["Current_File_Name"].ToString();
            if (DirectoryIDTempStorage.DirectoryID != null && DirectoryIDTempStorage.DirectoryID.CompareTo("") != 0 && RandomFileName.CompareTo("")!=0)
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
                    DeleteStatus = DeleteFileContent(DirectoryIDTempStorage.DirectoryID, RandomFileName, Base64ChallengeByte);
                    if (DeleteStatus == true)
                    {
                        Status="Successfully_deleted_directory_from_both_server_and_client";
                        ReloadFiles();
                    }
                    else
                    {
                        Status="Error_deleting_directory_from_both_server_and_client";
                    }
                }
                else
                {
                    Console.WriteLine("Error requesting challenge from server");
                }
            }
            else
            {
                Console.WriteLine("Error: Please choose the a random file");
            }
            GetFileStorageUsedSize(ref Storage_Size_In_String);
        }

        public void RequestChallenge(ref Boolean Re_RequestChallengeBoolean, ref String Base64ServerChallenge)
        {
            Re_RequestChallengeBoolean = false;
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
                                    SodiumSecureMemory.SecureClearBytes(ServerSignedRandomChallenge);
                                    SodiumSecureMemory.SecureClearBytes(ServerRandomChallenge);
                                    SodiumSecureMemory.SecureClearBytes(ServerED25519PK);
                                    Re_RequestChallengeBoolean = true;
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

        public Boolean DeleteFileContent(String DirectoryID, String RandomFileName, Byte[] RandomChallenge)
        {
            String RootDirectory = AppContext.BaseDirectory;
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
                ClientECDSASK = System.IO.File.ReadAllBytes(RootDirectory + "/Temp_Session/" + ETLSSessionIDStorage.ETLSID + "/" + "ECDSASK.txt");
                SharedSecret = System.IO.File.ReadAllBytes(RootDirectory + "/Temp_Session/" + ETLSSessionIDStorage.ETLSID + "/" + "SharedSecret.txt");
                UserECDSASK = System.IO.File.ReadAllBytes(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/rootSK.txt");
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
                    var response = client.GetAsync("OwnerUploadFiles/DeleteEndpointEncryptedFile?ClientPathID=" + ETLSSessionIDStorage.ETLSID + "&CipheredSignedDirectoryID=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredDirectoryIDByte)) + "&SignedSignedRandomChallenge=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserSignedRandomChallenge)) + "&SignedUniqueFileName=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedRandomFileNameByte)));
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
                                    Directory.Delete(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName, true);
                                    return true;
                                }
                                else
                                {
                                    Console.WriteLine(StringResult);
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

        public void GetFileStorageUsedSize(ref String Result)
        {
            Boolean ServerOnlineChecker = true;
            int FileStorageSize = 0;
            Decimal FileStorageSizeInKB = 0;
            Decimal FileStorageSizeInMB = 0;
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
                                Console.WriteLine(StringResult);
                            }
                            else
                            {
                                StringResult = StringResult.Substring(1, StringResult.Length - 2);
                                FileStorageSize = int.Parse(StringResult);
                                FileStorageSizeInKB = FileStorageSize / 1024;
                                FileStorageSizeInMB = FileStorageSize / 1048576;
                                Result = StringResult + " bytes" + Environment.NewLine + FileStorageSizeInKB.ToString() + " kb" + Environment.NewLine + FileStorageSizeInMB.ToString() + " mb";
                            }
                        }
                        else
                        {
                            Console.WriteLine("Something went wrong with fetching values from server ...");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Server is now offline...");
                    }
                }
            }
            else
            {
                Console.WriteLine("You have not yet establish an ephemeral TLS session with the server..");
            }
        }

        public void ReloadFiles()
        {
            String RootDirectory = AppContext.BaseDirectory;
            String[] FileNamesFullPathArray = Directory.GetDirectories(RootDirectory + "/Server_Directory_Data/" + DirectoryIDTempStorage.DirectoryID + "/Encrypted_Files/");
            String[] FileNamesArray = new string[FileNamesFullPathArray.Length];
            int Count = 0;
            int RootDirectoryCount = 0;
            RootDirectoryCount = (RootDirectory + "/Server_Directory_Data/" + DirectoryIDTempStorage.DirectoryID + "/Encrypted_Files/").Length;
            while (Count < FileNamesFullPathArray.Length)
            {
                FileNamesArray[Count] = FileNamesFullPathArray[Count].Remove(0, RootDirectoryCount);
                Count += 1;
            }
            ListOfFileNames = FileNamesArray;
        }
    }
}
