using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using ASodium;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Threading;
using PriSecFileStorageClient.Model;
using PriSecFileStorageClient.Helper;

namespace PriSecFileStorageClient
{
    public partial class OwnerCheckFileContent : Form
    {
        private Thread CheckFileContentsThread;
        public OwnerCheckFileContent()
        {
            InitializeComponent();
        }

        private void GetFileContentCountBTN_Click(object sender, EventArgs e)
        {
            if(DirectoryIDTempStorage.DirectoryID!=null && DirectoryIDTempStorage.DirectoryID.CompareTo("")!=0 && UserIDTempStorage.UserID!=null && UserIDTempStorage.UserID.CompareTo("") != 0) 
            {
                if(EncryptedRandomFileNameCB.Text!=null && EncryptedRandomFileNameCB.Text.CompareTo("") != 0) 
                {
                    String ServerRandomFileName = EncryptedRandomFileNameCB.Text;
                    String DirectoryID = DirectoryIDTempStorage.DirectoryID;
                    String Base64Challenge = "";
                    Byte[] Base64ChallengeByte = new Byte[] { };
                    Boolean AbleToRequestChallenge = true;
                    int Count = 0;
                    RequestChallenge(ref AbleToRequestChallenge,ref Base64Challenge);
                    while (AbleToRequestChallenge == false) 
                    {
                        RequestChallenge(ref AbleToRequestChallenge, ref Base64Challenge);
                    }
                    if (Base64Challenge!=null && Base64Challenge.CompareTo("") != 0) 
                    {
                        Base64ChallengeByte = Convert.FromBase64String(Base64Challenge);
                        Count = GetFileContentCount(DirectoryID, ServerRandomFileName,Base64ChallengeByte);
                        if (Count != -1) 
                        {
                            FileContentCountTB.Text = Count.ToString();
                        }
                        else 
                        {
                            MessageBox.Show("Error getting file content count", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else 
                    {
                        MessageBox.Show("Error requesting challenge", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else 
                {
                    MessageBox.Show("Have you enter server random file name?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else 
            {
                MessageBox.Show("Have you enter server directory ID? Have you login/register yet?","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void CheckFileContentsBTN_Click(object sender, EventArgs e)
        {
            if(DirectoryIDTempStorage.DirectoryID!=null && DirectoryIDTempStorage.DirectoryID.CompareTo("")!=0 && EncryptedRandomFileNameCB.Text!=null && EncryptedRandomFileNameCB.Text.CompareTo("")!=0 && FileContentCountTB.Text!=null && FileContentCountTB.Text.CompareTo("") != 0) 
            {
                CheckFileContentsThread = new Thread(BackGroundCompareFileContent);
                CheckFileContentsThread.Start();
                CheckFileContentsBTN.Enabled = false;
            }
            else 
            {
                MessageBox.Show("Have you fetch the file content count from server?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RequestChallenge(ref Boolean Re_RequestChallengeBoolean,ref String Base64ServerChallenge)
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
                if (UserIDTempStorage.UserID != null && UserIDTempStorage.UserID.CompareTo("") != 0)
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
                                        SodiumSecureMemory.SecureClearBytes(ServerSignedRandomChallenge);
                                        SodiumSecureMemory.SecureClearBytes(ServerRandomChallenge);
                                        SodiumSecureMemory.SecureClearBytes(ServerED25519PK);
                                        Re_RequestChallengeBoolean = true;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Man in the middle spotted ..., proceeding with re-requesting random challenge from server...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public int GetFileContentCount(String DirectoryID,String RandomFileName, Byte[] RandomChallenge) 
        {
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
                if (UserIDTempStorage.UserID != null && UserIDTempStorage.UserID.CompareTo("") != 0)
                {
                    ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "ECDSASK.txt");
                    SharedSecret = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "SharedSecret.txt");
                    UserECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\" + "User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\rootSK.txt");
                    ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "ECDSASK.txt");
                    DirectoryIDByte = Encoding.UTF8.GetBytes(DirectoryID);
                    Nonce = SodiumSecretBox.GenerateNonce();
                    CipheredDirectoryIDByte = SodiumSecretBox.Create(DirectoryIDByte, Nonce, SharedSecret,true);
                    CombinedCipheredDirectoryIDByte = Nonce.Concat(CipheredDirectoryIDByte).ToArray();
                    ETLSSignedCombinedCipheredDirectoryIDByte = SodiumPublicKeyAuth.Sign(CombinedCipheredDirectoryIDByte, ClientECDSASK);
                    UserSignedRandomChallenge = SodiumPublicKeyAuth.Sign(RandomChallenge, UserECDSASK,true);
                    ETLSSignedUserSignedRandomChallenge = SodiumPublicKeyAuth.Sign(UserSignedRandomChallenge, ClientECDSASK);
                    RandomFileNameByte = Encoding.UTF8.GetBytes(RandomFileName);
                    ETLSSignedRandomFileNameByte = SodiumPublicKeyAuth.Sign(RandomFileNameByte, ClientECDSASK,true);
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://mrchewitsoftware.com.my:5001/api/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.GetAsync("OwnerUploadFiles/GetEndpointEncryptedFileContentCount?ClientPathID=" + ETLSSessionIDStorage.ETLSID + "&CipheredSignedDirectoryID=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredDirectoryIDByte))+"&SignedSignedRandomChallenge="+HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserSignedRandomChallenge))+"&SignedUniqueFileName="+HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedRandomFileNameByte)));
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
                                        MessageBox.Show(StringResult, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            else
            {
                return -1;
            }
        }

        public Byte[] GetFileContent(String DirectoryID, String RandomFileName, Byte[] RandomChallenge,int Count)
        {
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
                if (UserIDTempStorage.UserID != null && UserIDTempStorage.UserID.CompareTo("") != 0)
                {
                    ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "ECDSASK.txt");
                    SharedSecret = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "SharedSecret.txt");
                    UserECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\" + "User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\rootSK.txt");
                    DirectoryIDByte = Encoding.UTF8.GetBytes(DirectoryID);
                    Nonce = SodiumSecretBox.GenerateNonce();
                    CipheredDirectoryIDByte = SodiumSecretBox.Create(DirectoryIDByte, Nonce, SharedSecret,true);
                    CombinedCipheredDirectoryIDByte = Nonce.Concat(CipheredDirectoryIDByte).ToArray();
                    ETLSSignedCombinedCipheredDirectoryIDByte = SodiumPublicKeyAuth.Sign(CombinedCipheredDirectoryIDByte, ClientECDSASK);
                    ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "ECDSASK.txt");
                    UserSignedRandomChallenge = SodiumPublicKeyAuth.Sign(RandomChallenge, UserECDSASK,true);
                    ETLSSignedUserSignedRandomChallenge = SodiumPublicKeyAuth.Sign(UserSignedRandomChallenge, ClientECDSASK);
                    ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "ECDSASK.txt");
                    RandomFileNameByte = Encoding.UTF8.GetBytes(RandomFileName);
                    ETLSSignedRandomFileNameByte = SodiumPublicKeyAuth.Sign(RandomFileNameByte, ClientECDSASK,true);
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://mrchewitsoftware.com.my:5001/api/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.GetAsync("OwnerUploadFiles/GetEndpointEncryptedFile?ClientPathID=" + ETLSSessionIDStorage.ETLSID + "&CipheredSignedDirectoryID=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredDirectoryIDByte)) + "&SignedSignedRandomChallenge=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserSignedRandomChallenge)) + "&SignedUniqueFileName=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedRandomFileNameByte))+"&FileContentCount="+Count.ToString());
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
                                if (StringResult != null && StringResult.Contains("Error")==false)
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
            else
            {
                return new Byte[] { };
            }
        }

        public Byte[] GetLocalFileContent(String DirectoryID,String RandomFileName,int Count) 
        {
            try 
            {
                Byte[] ResultByte=File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryID + "\\Encrypted_Files\\" + RandomFileName + "\\FileContent" + Count.ToString() + ".txt");
                return ResultByte;
            }
            catch 
            {
                return new Byte[] { };
            }
        }

        public Boolean CompareLocalServerFileContent(Byte[] LocalFileContent,Byte[] ServerFileContent) 
        {
            return LocalFileContent.SequenceEqual(ServerFileContent);
        }

        public void BackGroundCompareFileContent() 
        {
            int LoopCount = 1;
            int Count = int.Parse(FileContentCountTB.Text);
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
                RequestChallenge(ref AbleToRequestChallenge,ref Base64Challenge);
                while (AbleToRequestChallenge == false) 
                {
                    RequestChallenge(ref AbleToRequestChallenge, ref Base64Challenge);
                }
                if (Base64Challenge!=null && Base64Challenge.CompareTo("") != 0) 
                {
                    Base64ChallengeByte = Convert.FromBase64String(Base64Challenge);
                    ServerFileContent = GetFileContent(DirectoryIDTempStorage.DirectoryID, EncryptedRandomFileNameCB.Text, Base64ChallengeByte, LoopCount);
                    LocalFileContent = GetLocalFileContent(DirectoryIDTempStorage.DirectoryID, EncryptedRandomFileNameCB.Text, LoopCount);
                    if(ServerFileContent.Length!=0 && LocalFileContent.Length != 0) 
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
                        CheckFileContentStatusLB.Items.Add(ResultString);
                    }
                    else 
                    {
                        EncounterError = true;
                        MessageBox.Show("Error when getting file content from either server or local side, aborting..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                }
                else 
                {
                    EncounterError = true;
                    MessageBox.Show("Error when requesting challenge from server, aborting..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("File content checking between local and server have been completed", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else 
            {
                MessageBox.Show("File content checking between local and server have failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            CheckFileContentsThread.Abort();
            CheckFileContentsBTN.Enabled = true;
        }

        private void EncryptedRandomFileNameCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EncryptedRandomFileNameCB.SelectedIndex != -1)
            {
                Byte[] OriginalFileNameByte = new Byte[] { };
                String FileName = "";
                Boolean CheckFileNameExists = true;
                try
                {
                    OriginalFileNameByte = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\Encrypted_Files\\" + EncryptedRandomFileNameCB.Text + "\\FileName.txt");
                }
                catch
                {
                    CheckFileNameExists = false;
                }
                if (CheckFileNameExists == true)
                {
                    FileName = Encoding.UTF8.GetString(OriginalFileNameByte);
                    MessageBox.Show("The file name from the selected random file name is " + FileName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Sorry system can't find the file name from the selected random file name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OwnerCheckFileContent_Load(object sender, EventArgs e)
        {
            String[] RandomFileIDFullPathArray = Directory.GetDirectories(Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\Encrypted_Files");
            String[] RandomFileIDArray = new string[RandomFileIDFullPathArray.Length];
            int Count = 0;
            int RootDirectoryCount = 0;
            RootDirectoryCount = (Application.StartupPath + "\\Application_Data\\User\\" + UserIDTempStorage.UserID + "\\Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\Encrypted_Files\\").Length;
            while (Count < RandomFileIDFullPathArray.Length)
            {
                RandomFileIDArray[Count] = RandomFileIDFullPathArray[Count].Remove(0, RootDirectoryCount);
                Count += 1;
            }
            EncryptedRandomFileNameCB.Items.AddRange(RandomFileIDArray);
        }
    }
}
