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
    public class OwnerDecryptFileContentModel : PageModel
    {
        public String Current_Directory_ID = "";
        public String[] ListOfFileNames = new String[] { };
        public String Chosen_File_Name = "";
        public String File_Name = "";
        public String File_Content_Count = "";

        public void OnGet()
        {
            if (DirectoryIDTempStorage.DirectoryID!=null && DirectoryIDTempStorage.DirectoryID.CompareTo("") != 0) 
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
                Console.WriteLine("Error: You haven't choose either a random file or purchased storage ID");
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
            String SelectedRandomFileName = Request.Form["Current_File_Name"].ToString();
            String FileNameString = Request.Form["File_Name"].ToString();
            String FileContentCountString = Request.Form["File_Content_Count"].ToString();
            String SEAlgorithmString = Request.Form["SEAlgorithm"].ToString();
            Boolean IsAnyAlgorithmChecked = (SEAlgorithmString.CompareTo("1")==0)||(SEAlgorithmString.CompareTo("2")==0)||(SEAlgorithmString.CompareTo("3")==0);
            if (SelectedRandomFileName.CompareTo("")!=0 && FileNameString.CompareTo("") != 0 && FileContentCountString.CompareTo("") != 0 && IsAnyAlgorithmChecked==true)
            {
                File_Name = FileNameString;
                Chosen_File_Name = SelectedRandomFileName;
                File_Content_Count = FileContentCountString;
                int FileContentCount = int.Parse(FileContentCountString);
                int Loop = 1;
                String RandomFileName = SelectedRandomFileName;
                Boolean IsXSalsa20Poly1305 = SEAlgorithmString.CompareTo("1")==0;
                Boolean IsXChaCha20Poly1305 = SEAlgorithmString.CompareTo("2")==0;
                Boolean IsAES256HAGCM = SEAlgorithmString.CompareTo("3")==0;
                var progress = new Progress<int>(percent =>
                {
                    Console.WriteLine(Loop.ToString() + " part out of " + FileContentCount.ToString() + " parts have been decrypted");
                    Loop += 1;
                });

                await Task.Run(() => BackGroundDecryptFileContent(progress, FileContentCount, RandomFileName, IsXSalsa20Poly1305, IsXChaCha20Poly1305, IsAES256HAGCM));
            }
            else
            {
                Console.WriteLine("Information:"+Environment.NewLine+
                    "Please check the amount of file parts need to be fetched and decrypted"
                    + Environment.NewLine +
                    "Please choose a random file name");
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
                    var response = client.GetAsync("OwnerUploadFiles/GetEndpointEncryptedFile?ClientPathID=" + ETLSSessionIDStorage.ETLSID + "&SignedUserID=" + "&CipheredSignedDirectoryID=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredDirectoryIDByte)) + "&SignedSignedRandomChallenge=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserSignedRandomChallenge)) + "&SignedUniqueFileName=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedRandomFileNameByte)) + "&FileContentCount=" + Count.ToString());
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
                            if (StringResult != null)
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
                                    Console.WriteLine(StringResult);
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

        public void BackGroundDecryptFileContent(IProgress<int> progress, int FileCount, String RandomFileName, Boolean IsXSalsa20Poly1305, Boolean IsXChaCha20Poly1305, Boolean IsAES256HAGCM)
        {
            String RootDirectory = AppContext.BaseDirectory;
            int LoopCount = 1;
            int Count = FileCount;
            String Base64Challenge = "";
            Byte[] Base64ChallengeByte = new Byte[] { };
            Byte[] ServerFileContent = new Byte[] { };
            Byte[] ED25519PK = new Byte[] { };
            Byte[] VerifiedServerFileContent = new Byte[] { };
            Byte[] CipherTextWithMAC = new Byte[] { };
            Byte[] DecryptedFileBytes = new Byte[] { };
            Byte[] Key = new Byte[] { };
            Byte[] Nonce = new Byte[] { };
            Byte[] OriginalFileNameByte = new Byte[] { };
            String FileName = "";
            Boolean EncounterError = false;
            Boolean CheckED25519PKExists = true;
            Boolean VerifyFileContent = true;
            Boolean CheckKeyExists = true;
            Boolean CheckFileNameExists = true;
            Boolean DecryptChecker = true;
            Boolean AbleToRequestChallenge = true;
            FileStream PlainTextFileStream;
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
                PlainTextFileStream = System.IO.File.OpenWrite(RootDirectory + "/Decrypted_Files/" + FileName);
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
                        if (ServerFileContent.Length != 0)
                        {
                            try
                            {
                                Key = System.IO.File.ReadAllBytes(RootDirectory + "/Server_Directory_Data/" + DirectoryIDTempStorage.DirectoryID + "/Encrypted_Files/" + RandomFileName + "/Key/" + "Key" + LoopCount.ToString() + ".txt");
                            }
                            catch
                            {
                                CheckKeyExists = false;
                                break;
                            }
                            try
                            {
                                ED25519PK = System.IO.File.ReadAllBytes(RootDirectory + "/Server_Directory_Data/" + DirectoryIDTempStorage.DirectoryID + "/Encrypted_Files/" + RandomFileName + "/ED25519PK/" + "PK" + LoopCount.ToString() + ".txt");
                            }
                            catch
                            {
                                CheckED25519PKExists = false;
                                break;
                            }
                            try
                            {
                                VerifiedServerFileContent = SodiumPublicKeyAuth.Verify(ServerFileContent, ED25519PK);
                            }
                            catch
                            {
                                VerifyFileContent = false;
                                break;
                            }
                            Nonce = new Byte[SodiumSecretBox.GenerateNonce().Length];
                            CipherTextWithMAC = new Byte[VerifiedServerFileContent.Length - Nonce.Length];
                            Array.Copy(VerifiedServerFileContent, Nonce, Nonce.Length);
                            Array.Copy(VerifiedServerFileContent, Nonce.Length, CipherTextWithMAC, 0, CipherTextWithMAC.Length);
                            try
                            {
                                if (IsXSalsa20Poly1305 == true)
                                {
                                    DecryptedFileBytes = SodiumSecretBox.Open(CipherTextWithMAC, Nonce, Key, true);
                                }
                                if (IsXChaCha20Poly1305 == true)
                                {
                                    DecryptedFileBytes = SodiumSecretBoxXChaCha20Poly1305.Open(CipherTextWithMAC, Nonce, Key, true);
                                }
                                if (IsAES256HAGCM == true)
                                {
                                    if (SodiumSecretAeadAES256GCM.IsAES256GCMAvailable() == true)
                                    {
                                        DecryptedFileBytes = SodiumSecretAeadAES256GCM.Decrypt(CipherTextWithMAC, Nonce, Key, null, null, true);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error: Your device does not support AES256 GCM");
                                    }
                                }
                            }
                            catch
                            {
                                DecryptChecker = false;
                                break;
                            }
                            PlainTextFileStream.Write(DecryptedFileBytes, 0, DecryptedFileBytes.Length);
                            DecryptedFileBytes = new Byte[] { };
                            SodiumSecureMemory.SecureClearBytes(ED25519PK);
                            Key = new Byte[] { };
                            ED25519PK = new Byte[] { };
                            if (CheckKeyExists == true && CheckED25519PKExists == true && VerifyFileContent == true && DecryptChecker == true)
                            {
                                if (progress != null)
                                {
                                    progress.Report(LoopCount);
                                }
                            }
                        }
                        else
                        {
                            EncounterError = true;
                            Console.WriteLine("Error when getting file content from server side, aborting..");
                            break;
                        }
                    }
                    else
                    {
                        EncounterError = true;
                        Console.WriteLine("Error when requesting challenge from server, aborting..");
                        break;
                    }
                    LoopCount += 1;
                }
                PlainTextFileStream.Close();
                if (EncounterError == false && CheckKeyExists == true && CheckED25519PKExists == true && VerifyFileContent == true && DecryptChecker == true)
                {
                    Console.WriteLine("Succeed: Decryption of file have been done");
                }
                else
                {
                    Console.WriteLine("Error: Could not decrypt file properly..,aborting..");
                }
            }
            else
            {
                Console.WriteLine("Error: Can't find file name.., decryption of file could not done properly, aborting..");
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
