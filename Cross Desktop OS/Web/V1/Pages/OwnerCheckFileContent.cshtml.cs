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
    public class OwnerCheckFileContentModel : PageModel
    {
        public String Current_Directory_ID = "";
        public String[] ListOfFileNames = new String[] { };
        public String Chosen_File_Name = "";
        public String File_Name = "";
        public String File_Content_Count = "";
        public String Status = "";

        public void OnGet()
        {
            if (DirectoryIDTempStorage.DirectoryID != null && DirectoryIDTempStorage.DirectoryID.CompareTo("") != 0)
            {
                Current_Directory_ID = DirectoryIDTempStorage.DirectoryID;
                ReloadFiles();
            }
        }

        public void OnPostGetFileContentCount() 
        {
            if (DirectoryIDTempStorage.DirectoryID != null && DirectoryIDTempStorage.DirectoryID.CompareTo("") != 0)
            {
                Current_Directory_ID = DirectoryIDTempStorage.DirectoryID;
                ReloadFiles();
            }
            String RandomFileName = Request.Form["Random_File_Name"].ToString();
            String CurrentDirectoryID = Request.Form["Storage_ID"].ToString();
            String RootDirectory = AppContext.BaseDirectory;
            if(RandomFileName.CompareTo("")!=0 && CurrentDirectoryID.CompareTo("") != 0) 
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
                String ServerRandomFileName = RandomFileName;
                String DirectoryID = DirectoryIDTempStorage.DirectoryID;
                String Base64Challenge = "";
                Byte[] Base64ChallengeByte = new Byte[] { };
                Boolean AbleToRequestChallenge = true;
                int Count = 0;
                RequestChallenge(ref AbleToRequestChallenge, ref Base64Challenge);
                while (AbleToRequestChallenge == false)
                {
                    RequestChallenge(ref AbleToRequestChallenge, ref Base64Challenge);
                }
                if (Base64Challenge != null && Base64Challenge.CompareTo("") != 0)
                {
                    Base64ChallengeByte = Convert.FromBase64String(Base64Challenge);
                    Count = GetFileContentCount(DirectoryID, ServerRandomFileName, Base64ChallengeByte);
                    if (Count != -1)
                    {
                        File_Content_Count = Count.ToString();
                        Status = "The_file_content_count_on_server_have_been_retrieved";
                    }
                    else
                    {
                        Console.WriteLine("Error getting file content count");
                    }
                }
                else
                {
                    Console.WriteLine("Error requesting challenge");
                }
            }
            else 
            {
                Status = "You_haven't_choose_either_a_random_file_or_purchased_storage ID";
            }
            Chosen_File_Name = RandomFileName;
        }

        public async void OnPost() 
        {
            if (DirectoryIDTempStorage.DirectoryID != null && DirectoryIDTempStorage.DirectoryID.CompareTo("") != 0)
            {
                Current_Directory_ID = DirectoryIDTempStorage.DirectoryID;
                ReloadFiles();
            }
            String RandomFileName = Request.Form["Current_File_Name"].ToString();
            String CurrentDirectoryID = Request.Form["Storage_ID"].ToString();
            String FileName = Request.Form["File_Name"].ToString();
            String FileContentCountString = Request.Form["File_Content_Count"].ToString();
            int FileContentCount = 0;
            int Loop = 1;
            File_Name = FileName;
            Chosen_File_Name = RandomFileName;
            File_Content_Count = FileContentCountString;
            if (RandomFileName.CompareTo("") != 0 && CurrentDirectoryID.CompareTo("") != 0 && FileContentCountString.CompareTo("")!=0)
            {
                FileContentCount = int.Parse(FileContentCountString);
                var progress = new Progress<String>(percent =>
                {
                    Console.WriteLine(Loop.ToString() + " part out of "+FileContentCount.ToString()+" parts have been compared");
                    Console.WriteLine(percent);
                    Loop += 1;
                });
                await Task.Run(() => BackGroundCompareFileContent(progress, FileContentCount ,RandomFileName));
            }
            else 
            {
                Status = "You_haven't_choose_either_a_random_file_or_purchased_storage_ID_or_get_the_file_content_count_from_server";
            }
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

        public int GetFileContentCount(String DirectoryID, String RandomFileName, Byte[] RandomChallenge)
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
            Boolean TryParseStringToInt = true;
            int Count = 0;
            if (ETLSSessionIDStorage.ETLSID.CompareTo("") != 0 && ETLSSessionIDStorage.ETLSID != null)
            {
                ClientECDSASK = System.IO.File.ReadAllBytes(RootDirectory + "/Temp_Session/" + ETLSSessionIDStorage.ETLSID + "/" + "ECDSASK.txt");
                SharedSecret = System.IO.File.ReadAllBytes(RootDirectory + "/Temp_Session/" + ETLSSessionIDStorage.ETLSID + "/" + "SharedSecret.txt");
                UserECDSASK = System.IO.File.ReadAllBytes(RootDirectory  + "/Server_Directory_Data/" + DirectoryID + "/rootSK.txt");
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
                    var response = client.GetAsync("OwnerUploadFiles/GetEndpointEncryptedFileContentCount?ClientPathID=" + ETLSSessionIDStorage.ETLSID + "&CipheredSignedDirectoryID=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredDirectoryIDByte)) + "&SignedSignedRandomChallenge=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserSignedRandomChallenge)) + "&SignedUniqueFileName=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedRandomFileNameByte)));
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
                        return -1;
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
                                try
                                {
                                    Count = int.Parse(StringResult);
                                }
                                catch
                                {
                                    TryParseStringToInt = false;
                                }
                                if (TryParseStringToInt == true)
                                {
                                    return Count;
                                }
                                else
                                {
                                    Console.WriteLine(StringResult);
                                    return -1;
                                }
                            }
                            else
                            {
                                return -1;
                            }
                        }
                        else
                        {
                            return -1;
                        }
                    }
                }
            }
            else
            {
                return -1;
            }
        }

        public Byte[] GetFileContent(String DirectoryID, String RandomFileName, Byte[] RandomChallenge, int Count)
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
            Boolean TryParseBase64StringToBytes = true;
            Byte[] ResultByte = new Byte[] { };
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
                    var response = client.GetAsync("OwnerUploadFiles/GetEndpointEncryptedFile?ClientPathID=" + ETLSSessionIDStorage.ETLSID + "&CipheredSignedDirectoryID=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredDirectoryIDByte)) + "&SignedSignedRandomChallenge=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserSignedRandomChallenge)) + "&SignedUniqueFileName=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedRandomFileNameByte)) + "&FileContentCount=" + Count.ToString());
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
                        return new Byte[] { };
                    }
                    else
                    {
                        var result = response.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsStringAsync();
                            readTask.Wait();

                            var StringResult = readTask.Result;
                            if (StringResult != null && StringResult.Contains("Error") == false)
                            {
                                StringResult = StringResult.Substring(1, StringResult.Length - 2);
                                try
                                {
                                    ResultByte = Convert.FromBase64String(StringResult);
                                }
                                catch
                                {
                                    TryParseBase64StringToBytes = false;
                                }
                                if (TryParseBase64StringToBytes == true)
                                {
                                    return ResultByte;
                                }
                                else
                                {
                                    return new Byte[] { };
                                }
                            }
                            else
                            {
                                return new Byte[] { };
                            }
                        }
                        else
                        {
                            return new Byte[] { };
                        }
                    }
                }
            }
            else
            {
                return new Byte[] { };
            }
        }

        public Byte[] GetLocalFileContent(String DirectoryID, String RandomFileName, int Count)
        {
            String RootDirectory = AppContext.BaseDirectory;
            try
            {
                Byte[] ResultByte = System.IO.File.ReadAllBytes(RootDirectory + "/Server_Directory_Data/" + DirectoryID + "/Encrypted_Files/" + RandomFileName + "/FileContent" + Count.ToString() + ".txt");
                return ResultByte;
            }
            catch
            {
                return new Byte[] { };
            }
        }

        public Boolean CompareLocalServerFileContent(Byte[] LocalFileContent, Byte[] ServerFileContent)
        {
            return LocalFileContent.SequenceEqual(ServerFileContent);
        }

        public void BackGroundCompareFileContent(IProgress<String> progress, int FileCount ,String RandomFileName)
        {
            int LoopCount = 1;
            int Count = FileCount;
            String Base64Challenge = "";
            Byte[] Base64ChallengeByte = new Byte[] { };
            Byte[] ServerFileContent = new Byte[] { };
            Byte[] LocalFileContent = new Byte[] { };
            Boolean CompareResult = true;
            Boolean AbleToRequestChallenge = true;
            String ResultString = "";
            Boolean EncounterError = false;
            while (LoopCount <= Count)
            {
                Thread.Sleep(100);
                RequestChallenge(ref AbleToRequestChallenge, ref Base64Challenge);
                while (AbleToRequestChallenge == false)
                {
                    RequestChallenge(ref AbleToRequestChallenge, ref Base64Challenge);
                }
                if (Base64Challenge != null && Base64Challenge.CompareTo("") != 0)
                {
                    Base64ChallengeByte = Convert.FromBase64String(Base64Challenge);
                    ServerFileContent = GetFileContent(DirectoryIDTempStorage.DirectoryID, RandomFileName, Base64ChallengeByte, LoopCount);
                    LocalFileContent = GetLocalFileContent(DirectoryIDTempStorage.DirectoryID, RandomFileName, LoopCount);
                    if (ServerFileContent.Length != 0 && LocalFileContent.Length != 0)
                    {
                        CompareResult = CompareLocalServerFileContent(LocalFileContent, ServerFileContent);
                        if (CompareResult == true)
                        {
                            ResultString = "Match";
                        }
                        else
                        {
                            ResultString = "Not Match";
                        }
                        if (progress != null)
                        {
                            progress.Report(ResultString);
                        }
                    }
                    else
                    {
                        EncounterError = true;
                        Console.WriteLine("Error when getting file content from either server or local side, aborting..");
                        break;
                    }
                }
                else
                {
                    EncounterError = true;
                    Console.WriteLine("Error when requesting challenge from server, aborting..");
                    break;
                }
                Base64Challenge = "";
                Base64ChallengeByte = new Byte[] { };
                ServerFileContent = new Byte[] { };
                LocalFileContent = new Byte[] { };
                CompareResult = true;
                ResultString = "";
                LoopCount += 1;
            }
            if (EncounterError == false)
            {
                Console.WriteLine("Succeed: File content checking between local and server have been completed");
            }
            else
            {
                Console.WriteLine("Error: File content checking between local and server have failed");
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
