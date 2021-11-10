using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using ASodium;
using PriSecFileStorageWeb.Helper;
using PriSecFileStorageWeb.Model;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace PriSecFileStorageWeb
{
    public class Program
    {
        private static SecureIDGenerator MySecureIDGenerator = new SecureIDGenerator();

        private static String RootDirectory = "";

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureKestrel(serverOptions =>
                    {
                        //If you are on either MacOS or Linux, you can change the port of
                        //4001 to whatever you want
                        //I am sorry but I don't really run ASP.Net Core application on
                        //Windows
                        //It's better you look for guide in
                        //https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/?view=aspnetcore-5.0
                        serverOptions.Listen(IPAddress.Parse("0.0.0.0"), 4001);
                    });
                    StartupFunction();
                });

        private static void StartupFunction()
        {
            //Get the application startup path
            RootDirectory = AppContext.BaseDirectory;
            if (Directory.Exists(RootDirectory + "Temp_Session") == false)
            {
                Directory.CreateDirectory(RootDirectory + "Temp_Session");
            }
            if (Directory.Exists(RootDirectory + "Temp_Data") == false)
            {
                Directory.CreateDirectory(RootDirectory + "Temp_Data");
            }
            if (Directory.Exists(RootDirectory + "Server_Directory_Data") == false) 
            {
                Directory.CreateDirectory(RootDirectory + "Server_Directory_Data");
            }
            if (Directory.Exists(RootDirectory + "Other_Directory_Data") == false)
            {
                Directory.CreateDirectory(RootDirectory + "Other_Directory_Data");
            }
            if (Directory.Exists(RootDirectory + "Error_Data") == false)
            {
                Directory.CreateDirectory(RootDirectory + "Error_Data");
            }
            if (Directory.Exists(RootDirectory + "Backup") == false)
            {
                Directory.CreateDirectory(RootDirectory + "Backup");
            }
            if (Directory.Exists(RootDirectory + "Import") == false)
            {
                Directory.CreateDirectory(RootDirectory + "Import");
            }
            if (Directory.Exists(RootDirectory + "Files_To_Encrypt") == false)
            {
                Directory.CreateDirectory(RootDirectory+"Files_To_Encrypt");
            }
            if (Directory.Exists(RootDirectory + "Decrypted_Files") == false)
            {
                Directory.CreateDirectory(RootDirectory + "Decrypted_Files");
            }
            if (Directory.GetFileSystemEntries(RootDirectory + "/Temp_Session/").Length == 0 || Directory.GetFileSystemEntries(RootDirectory + "/Temp_Session/").Length == 1)
            {
                CreateNewSession();
                Console.WriteLine("Warning: If you are not seeing 'Are you a file storage owner?', try to remove all data reside within 'Temp_Session' folder");
            }
            else
            {
                InitiateETLSDeletion();
                if (File.Exists(RootDirectory + "/Error_Data/InitiateHandShakeDeleteStatus.txt") == true)
                {
                    Console.WriteLine("Error: Please read the error data that exists in 'Error_Data' folder");
                }
                else
                {
                    DeleteETLSSession();
                    if (File.Exists(RootDirectory + "/Error_Data/DeleteHandShakeSessionIDStatus.txt") == true)
                    {
                        Console.WriteLine("Error: Please read the error data that exists in 'Error_Data' folder");
                    }
                }
            }
        }

        public static void CreateNewSession()
        {
            RootDirectory = AppContext.BaseDirectory;
            RevampedKeyPair SessionECDHKeyPair = SodiumPublicKeyBox.GenerateRevampedKeyPair();
            RevampedKeyPair SessionECDSAKeyPair = SodiumPublicKeyAuth.GenerateRevampedKeyPair();
            String MySession_ID = MySecureIDGenerator.GenerateUniqueString();
            ECDH_ECDSA_Models MyECDH_ECDSA_Models = new ECDH_ECDSA_Models();
            Boolean CheckServerOnline = true;
            Boolean CreateShareSecretStatus = true;
            Boolean CheckSharedSecretStatus = true;
            Byte[] ServerECDSAPKByte = new Byte[] { };
            Byte[] ServerECDHSPKByte = new Byte[] { };
            Byte[] ServerECDHPKByte = new Byte[] { };
            Byte[] SignedClientSessionECDHPKByte = new Byte[] { };
            Byte[] ClientSessionECDSAPKByte = new Byte[] { };
            Boolean VerifyBoolean = true;
            String SessionStatus = "";
            String ExceptionString = "";
            using (var InitializeHandShakeHttpclient = new HttpClient())
            {
                InitializeHandShakeHttpclient.BaseAddress = new Uri("https://mrchewitsoftware.com.my:5001/api/");
                InitializeHandShakeHttpclient.DefaultRequestHeaders.Accept.Clear();
                InitializeHandShakeHttpclient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                var response = InitializeHandShakeHttpclient.GetAsync("ECDH_ECDSA_TempSession/byID?ClientPathID=" + MySession_ID);
                try
                {
                    response.Wait();
                }
                catch
                {
                    CheckServerOnline = false;
                }
                if (CheckServerOnline == true)
                {
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();

                        var ECDH_ECDSA_Models_Result = readTask.Result;
                        MyECDH_ECDSA_Models = JsonConvert.DeserializeObject<ECDH_ECDSA_Models>(ECDH_ECDSA_Models_Result);
                        if (MyECDH_ECDSA_Models.ID_Checker_Message.CompareTo("You still can use the exact same client ID...") == 0 || MyECDH_ECDSA_Models.ID_Checker_Message.CompareTo("You have an exact client ID great~") == 0)
                        {
                            ServerECDSAPKByte = Convert.FromBase64String(MyECDH_ECDSA_Models.ECDSA_PK_Base64String);
                            ServerECDHSPKByte = Convert.FromBase64String(MyECDH_ECDSA_Models.ECDH_SPK_Base64String);
                            try
                            {
                                ServerECDHPKByte = SodiumPublicKeyAuth.Verify(ServerECDHSPKByte, ServerECDSAPKByte);
                            }
                            catch (Exception exception)
                            {
                                VerifyBoolean = false;
                                ExceptionString = exception.ToString();
                                SessionStatus += ExceptionString + Environment.NewLine;
                            }
                            if (VerifyBoolean == true)
                            {
                                ClientSessionECDSAPKByte = SessionECDSAKeyPair.PublicKey;
                                SignedClientSessionECDHPKByte = SodiumPublicKeyAuth.Sign(SessionECDHKeyPair.PublicKey, SessionECDSAKeyPair.PrivateKey);
                                CreateSharedSecret(ref CreateShareSecretStatus, MySession_ID, SignedClientSessionECDHPKByte, ClientSessionECDSAPKByte);
                                if (CreateShareSecretStatus == true)
                                {
                                    CheckSharedSecret(ref CheckSharedSecretStatus, MySession_ID, ServerECDHPKByte, SessionECDHKeyPair.PrivateKey, SessionECDSAKeyPair.PrivateKey);
                                    if (CheckSharedSecretStatus == true)
                                    {
                                        File.WriteAllText(RootDirectory + "/Temp_Data/" + "EstablishSharedSecret.txt", "Success");
                                    }
                                    else
                                    {
                                        File.WriteAllText(RootDirectory + "/Temp_Data/" + "EstablishSharedSecret.txt", "Shared Secret mismatch with the server");
                                    }
                                }
                                else
                                {
                                    File.WriteAllText(RootDirectory + "/Temp_Data/" + "EstablishSharedSecret.txt", "Unable to create shared secret");
                                }
                            }
                            else
                            {
                                File.WriteAllText(RootDirectory + "/Error_Data/FetchHandShakeSessionParameterStatus.txt", "Server's ECDH public key can't be verified with given ECDSA public key");
                            }
                            SodiumSecureMemory.SecureClearBytes(ServerECDSAPKByte);
                            SodiumSecureMemory.SecureClearBytes(ServerECDHSPKByte);
                            SodiumSecureMemory.SecureClearBytes(ServerECDHPKByte);
                            SodiumSecureMemory.SecureClearString(MyECDH_ECDSA_Models.ECDSA_PK_Base64String);
                            SodiumSecureMemory.SecureClearString(MyECDH_ECDSA_Models.ECDH_SPK_Base64String);
                        }
                        else
                        {
                            File.WriteAllText(RootDirectory + "/Error_Data/ID_Checker_Message.txt", MyECDH_ECDSA_Models.ID_Checker_Message);
                        }
                    }
                    else
                    {
                        File.WriteAllText(RootDirectory + "/Error_Data/FetchHandShakeSessionParameterStatus.txt", "Failed to get handshake parameters from server");
                    }
                }
                else
                {
                    Console.WriteLine("Error: The server is offline now please wait for awhile ..");
                }
            }
            SessionECDHKeyPair.Clear();
            SessionECDSAKeyPair.Clear();
        }

        public static void CreateSharedSecret(ref Boolean CheckBoolean, String MySession_ID, Byte[] SignedClientSessionECDHPKByte, Byte[] ClientSessionECDSAPKByte)
        {
            CheckBoolean = false;
            String SessionStatus = "";
            var CreateSharedSecretHttpClient = new HttpClient();
            CreateSharedSecretHttpClient.BaseAddress = new Uri("https://mrchewitsoftware.com.my:5001/api/");
            CreateSharedSecretHttpClient.DefaultRequestHeaders.Accept.Clear();
            CreateSharedSecretHttpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var newresponse = CreateSharedSecretHttpClient.GetAsync("ECDH_ECDSA_TempSession/ByHandshake?ClientPathID=" + MySession_ID + "&SECDHPK=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(SignedClientSessionECDHPKByte)) + "&ECDSAPK=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ClientSessionECDSAPKByte)));
            try
            {
                newresponse.Wait();
                var newresult = newresponse.Result;

                if (newresult.IsSuccessStatusCode)
                {
                    var newreadTask = newresult.Content.ReadAsStringAsync();
                    newreadTask.Wait();

                    SessionStatus += newreadTask.Result;

                    if (SessionStatus.Contains("Error") == true)
                    {
                        CheckBoolean = false;
                    }
                    else
                    {
                        CheckBoolean = true;
                    }
                }
                else
                {
                    Console.WriteLine("Error: Failed to fetch values from server");
                }
            }
            catch
            {
                Console.WriteLine("Error: The server is offline now please wait for awhile ..");
            }
        }

        public static void CheckSharedSecret(ref Boolean CheckBoolean, String MySession_ID, Byte[] ServerECDHPKByte, Byte[] SessionECDHPrivateKey, Byte[] SessionECDSAPrivateKey)
        {
            CheckBoolean = false;
            Boolean CheckServerOnline = true;
            String CheckSharedSecretStatus = "";
            Byte[] ValidationData = SodiumRNG.GetRandomBytes(128);
            //Test Data in Hexa Array => TestData = new Byte[] {0xFF,0xFF,0xFF};
            Byte[] TestData = new Byte[] { 255, 255, 255 };
            Byte[] SharedSecretByte = SodiumScalarMult.Mult(SessionECDHPrivateKey, ServerECDHPKByte, true);
            Byte[] NonceByte = SodiumSecretBox.GenerateNonce();
            Byte[] TestEncryptedData = SodiumSecretBox.Create(TestData, NonceByte, SharedSecretByte);
            var CheckSharedSecretHttpClient = new HttpClient();
            CheckSharedSecretHttpClient.BaseAddress = new Uri("https://mrchewitsoftware.com.my:5001/api/");
            CheckSharedSecretHttpClient.DefaultRequestHeaders.Accept.Clear();
            CheckSharedSecretHttpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var CheckSharedSecretHttpClientResponse = CheckSharedSecretHttpClient.GetAsync("ECDH_ECDSA_TempSession/BySharedSecret?ClientPathID=" + MySession_ID + "&CipheredData=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(TestEncryptedData)) + "&Nonce=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(NonceByte)) + "&RVData=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ValidationData)));
            try
            {
                CheckSharedSecretHttpClientResponse.Wait();
            }
            catch
            {
                CheckServerOnline = false;
            }
            if (CheckServerOnline == true)
            {
                var CheckSharedSecretHttpClientResponseResult = CheckSharedSecretHttpClientResponse.Result;

                if (CheckSharedSecretHttpClientResponseResult.IsSuccessStatusCode)
                {
                    var CheckSharedSecretHttpClientResponseResultReadTask = CheckSharedSecretHttpClientResponseResult.Content.ReadAsStringAsync();
                    CheckSharedSecretHttpClientResponseResultReadTask.Wait();

                    CheckSharedSecretStatus = CheckSharedSecretHttpClientResponseResultReadTask.Result;
                    if (CheckSharedSecretStatus.Contains("Error"))
                    {
                        CheckBoolean = false;
                    }
                    else
                    {
                        if (Directory.Exists(RootDirectory + "/Temp_Session/" + MySession_ID))
                        {
                            File.WriteAllBytes(RootDirectory + "/Temp_Session/" + MySession_ID + "/" + "SharedSecret.txt", SharedSecretByte);
                            File.WriteAllBytes(RootDirectory + "/Temp_Session/" + MySession_ID + "/" + "ECDSASK.txt", SessionECDSAPrivateKey);
                            File.WriteAllBytes(RootDirectory + "/Temp_Session/" + MySession_ID + "/" + "RVData.txt", ValidationData);
                        }
                        else
                        {
                            Directory.CreateDirectory(RootDirectory + "/Temp_Session/" + MySession_ID);
                            File.WriteAllBytes(RootDirectory + "/Temp_Session/" + MySession_ID + "/" + "SharedSecret.txt", SharedSecretByte);
                            File.WriteAllBytes(RootDirectory + "/Temp_Session/" + MySession_ID + "/" + "ECDSASK.txt", SessionECDSAPrivateKey);
                            File.WriteAllBytes(RootDirectory + "/Temp_Session/" + MySession_ID + "/" + "RVData.txt", ValidationData);
                            File.WriteAllText(RootDirectory + "/Temp_Session/" + "SessionID.txt", MySession_ID);
                        }
                        ETLSSessionIDStorage.ETLSID = MySession_ID;
                        CheckBoolean = true;
                    }
                }
                else
                {
                    Console.WriteLine("Error: Failed to fetch values from server");
                }
                SodiumSecureMemory.SecureClearBytes(SharedSecretByte);
            }
            else
            {
                Console.WriteLine("Error: The server is offline now please wait for awhile ..");
            }
        }

        public static void InitiateETLSDeletion()
        {
            StreamReader MyStreamReader = new StreamReader(RootDirectory + "/Temp_Session/" + "SessionID.txt");
            String Temp_Session_ID = MyStreamReader.ReadLine();
            MyStreamReader.Close();
            Boolean CheckServerOnline = true;
            using (var InitializeHandShakeHttpclient = new HttpClient())
            {
                InitializeHandShakeHttpclient.BaseAddress = new Uri("https://mrchewitsoftware.com.my:5001/api/");
                InitializeHandShakeHttpclient.DefaultRequestHeaders.Accept.Clear();
                InitializeHandShakeHttpclient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                var response = InitializeHandShakeHttpclient.GetAsync("ECDH_ECDSA_TempSession/InitiateDeletionOfETLS?ClientPathID=" + Temp_Session_ID);
                try
                {
                    response.Wait();
                }
                catch
                {
                    CheckServerOnline = false;
                }
                if (CheckServerOnline == true)
                {
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();

                        var Result = readTask.Result;

                        if (Result.Contains("Error") == true)
                        {
                            File.WriteAllText(RootDirectory + "/Error_Data/InitiateHandShakeDeleteStatus.txt", Result);
                        }
                    }
                    else
                    {
                        File.WriteAllText(RootDirectory + "/Error_Data/InitiateHandShakeDeleteStatus.txt", "Failed to get ETLS deletion initiation status from server");
                    }
                }
                else
                {
                    Console.WriteLine("Error: The server is offline now please wait for awhile ..");
                }
            }
        }

        public static void DeleteETLSSession()
        {
            Boolean ServerOnlineChecker = true;
            StreamReader MyStreamReader = new StreamReader(RootDirectory + "/Temp_Session/" + "SessionID.txt");
            String Temp_Session_ID = MyStreamReader.ReadLine();
            MyStreamReader.Close();
            Byte[] ClientECDSASKByte = new Byte[] { };
            Byte[] RandomData = File.ReadAllBytes(RootDirectory + "/Temp_Session/" + Temp_Session_ID + "/" + "RVData.txt");
            Byte[] SignedRandomData = new Byte[] { };
            String Status = "";
            if (Temp_Session_ID != null && Temp_Session_ID.CompareTo("") != 0)
            {
                using (var client = new HttpClient())
                {
                    ClientECDSASKByte = File.ReadAllBytes(RootDirectory + "/Temp_Session/" + Temp_Session_ID + "/ECDSASK.txt");
                    SignedRandomData = SodiumPublicKeyAuth.Sign(RandomData, ClientECDSASKByte, true);
                    client.BaseAddress = new Uri("https://mrchewitsoftware.com.my:5001/api/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync("ECDH_ECDSA_TempSession/DeleteByClientCryptographicID?ClientPathID=" + Temp_Session_ID + "&ValidationData=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(SignedRandomData)));
                    SodiumSecureMemory.SecureClearBytes(RandomData);
                    SodiumSecureMemory.SecureClearBytes(SignedRandomData);
                    try
                    {
                        response.Wait();
                    }
                    catch
                    {
                        ServerOnlineChecker = false;
                        Console.WriteLine("Error: The server is offline now please wait for awhile ..");
                    }
                    if (ServerOnlineChecker == true)
                    {
                        var result = response.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsStringAsync();
                            readTask.Wait();

                            Status = readTask.Result;

                            if (Status.Contains("Error"))
                            {
                                File.WriteAllText(RootDirectory + "/Error_Data/DeleteHandShakeSessionIDStatus.txt", Status);
                            }
                            else
                            {
                                Directory.Delete(RootDirectory + "/Temp_Session/" + Temp_Session_ID, true);
                                File.WriteAllText(RootDirectory + "/Temp_Session/" + "SessionID.txt", "");
                                CreateNewSession();
                            }
                        }
                        else
                        {
                            File.WriteAllText(RootDirectory + "/Error_Data/GeneralStatus.txt", "Something went wrong on server side... refer to any status text to find out..");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Error: Something's wrong .. did you delete the values in the text file but not the folder?");
            }
        }
    }
}
