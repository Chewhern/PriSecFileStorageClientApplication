using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ASodium;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using PriSecFileStorageClient.Helper;
using PriSecFileStorageClient.Model;

namespace PriSecFileStorageClient
{
    public partial class OwnerAddMFADevice : Form
    {
        public OwnerAddMFADevice()
        {
            InitializeComponent();
        }

        private void OwnerAddMFADevice_Load(object sender, EventArgs e)
        {
            if (Directory.Exists(Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\MFA_Device\\")==true) 
            {
                int MFADeviceCount = 0;
                MFADeviceCount = Directory.GetDirectories(Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\MFA_Device\\").Length;
                if (MFADeviceCount > 1 || MFADeviceCount==1) 
                {
                    MessageBox.Show("Privacy based MFA wouldn't work if there exists more than 1 MFA device data stored in this device","MFA Device Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            else 
            {
                Directory.CreateDirectory(Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\MFA_Device");
            }
        }

        private void CheckBTN_Click(object sender, EventArgs e)
        {
            int MFADeviceCount = 0;
            GetMFADeviceCount(ref MFADeviceCount);
            MFADeviceCountTB.Text = MFADeviceCount.ToString();
        }

        private void AddDeviceBTN_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\MFA_Device\\") == true)
            {
                SecureIDGenerator MyIDGenerator = new SecureIDGenerator();
                String MFADeviceID = MyIDGenerator.GenerateUniqueString();
                if (MFADeviceID.Length > 24) 
                {
                    MFADeviceID = MFADeviceID.Substring(0, 24);
                }
                String OtherMFADeviceID = "";
                int MFADeviceRootDirectoryCount = 0;
                int MFADeviceCount = 0;
                Boolean AddMFADevice = true;
                String Base64RandomChallenge = "";
                Boolean RequestChallengeBoolean = true;
                Byte[] RandomChallenge = new Byte[] { };
                Byte[] ED25519PK = new Byte[] { };
                Byte[] SignedED25519PK = new Byte[] { };
                Byte[] MergedED25519PK = new Byte[] { };
                RevampedKeyPair MyKeyPair = SodiumPublicKeyAuth.GenerateRevampedKeyPair();
                MFADeviceCount = Directory.GetDirectories(Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\MFA_Device\\").Length;
                if (MFADeviceCount <= 1)
                {
                    RequestChallenge(ref RequestChallengeBoolean, ref Base64RandomChallenge);
                    while (Base64RandomChallenge.CompareTo("") == 0) 
                    {
                        RequestChallenge(ref RequestChallengeBoolean, ref Base64RandomChallenge);
                    }
                    RandomChallenge = Convert.FromBase64String(Base64RandomChallenge);
                    ED25519PK = MyKeyPair.PublicKey;
                    SignedED25519PK = SodiumPublicKeyAuth.Sign(ED25519PK, MyKeyPair.PrivateKey);
                    MergedED25519PK = ED25519PK.Concat(SignedED25519PK).ToArray();
                    if (MFADeviceCount == 0) 
                    {
                        AddMFADevice = UploadFirstMFADevicePK(DirectoryIDTempStorage.DirectoryID, MFADeviceID,RandomChallenge,MergedED25519PK);
                    }
                    else 
                    {
                        MFADeviceRootDirectoryCount = (Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\MFA_Device\\").Length;
                        OtherMFADeviceID = Directory.GetDirectories(Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\MFA_Device\\")[0];
                        OtherMFADeviceID = OtherMFADeviceID.Remove(0,MFADeviceRootDirectoryCount);
                        AddMFADevice = UploadSecondMFADevicePK(DirectoryIDTempStorage.DirectoryID, OtherMFADeviceID, MFADeviceID, RandomChallenge, MergedED25519PK);
                    }
                    if (AddMFADevice == true) 
                    {
                        if (Directory.Exists(Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\MFA_Device") == false) 
                        {
                            Directory.CreateDirectory(Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\MFA_Device");
                        }
                        if(Directory.Exists(Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\MFA_Device\\" + MFADeviceID) == false) 
                        {
                            Directory.CreateDirectory(Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\MFA_Device\\" + MFADeviceID);
                        }
                        File.WriteAllBytes(Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\MFA_Device\\" + MFADeviceID+"\\SK.txt",MyKeyPair.PrivateKey);
                        File.WriteAllBytes(Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\MFA_Device\\" + MFADeviceID + "\\PK.txt", MyKeyPair.PublicKey);
                        if(MFARemarksTB.Text!=null && MFARemarksTB.Text.CompareTo("") != 0) 
                        {
                            File.WriteAllText(Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\MFA_Device\\" + MFADeviceID + "\\Note.txt", MFARemarksTB.Text);
                        }
                        int ServerMFADeviceCount = 0;
                        GetMFADeviceCount(ref ServerMFADeviceCount);
                        MFADeviceCountTB.Text = ServerMFADeviceCount.ToString();
                        MFADeviceIDTB.Text = MFADeviceID;
                        MessageBox.Show("MFA Device has been added", "MFA Device", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else 
                {
                    MessageBox.Show("Privacy based MFA wouldn't work if there exists more than 2 MFA device data stored in this device", "MFA Device Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void SendMFADeviceBTN_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\MFA_Device\\") == true)
            {
                if(DeviceMFADeviceIDTB.Text.CompareTo("")!=0 && MergedED25519PKTB.Text.CompareTo("") != 0) 
                {
                    String MFADeviceID = DeviceMFADeviceIDTB.Text;
                    String OtherMFADeviceID = "";
                    int MFADeviceRootDirectoryCount = 0;
                    int MFADeviceCount = 0;
                    Boolean AddMFADevice = true;
                    String Base64RandomChallenge = "";
                    Boolean RequestChallengeBoolean = true;
                    Boolean AbleToConvertFromBase64 = true;
                    Byte[] RandomChallenge = new Byte[] { };
                    Byte[] MergedED25519PK = new Byte[] { };
                    MFADeviceCount = Directory.GetDirectories(Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\MFA_Device\\").Length;
                    if (MFADeviceCount >= 1)
                    {
                        RequestChallenge(ref RequestChallengeBoolean, ref Base64RandomChallenge);
                        while (Base64RandomChallenge.CompareTo("") == 0)
                        {
                            RequestChallenge(ref RequestChallengeBoolean, ref Base64RandomChallenge);
                        }
                        RandomChallenge = Convert.FromBase64String(Base64RandomChallenge);
                        try 
                        {
                            MergedED25519PK = Convert.FromBase64String(MergedED25519PKTB.Text);
                        }
                        catch 
                        {
                            AbleToConvertFromBase64 = false;
                        }
                        if (AbleToConvertFromBase64 == true) 
                        {
                            MFADeviceRootDirectoryCount = (Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\MFA_Device\\").Length;
                            OtherMFADeviceID = Directory.GetDirectories(Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\MFA_Device\\")[0];
                            OtherMFADeviceID = OtherMFADeviceID.Remove(0, MFADeviceRootDirectoryCount);
                            AddMFADevice = UploadSecondMFADevicePK(DirectoryIDTempStorage.DirectoryID, OtherMFADeviceID, MFADeviceID, RandomChallenge, MergedED25519PK);
                            if (AddMFADevice == true)
                            {
                                MessageBox.Show("MFA Device has been sent to server", "MFA Device", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else 
                        {
                            MessageBox.Show("Please copy and paste the exact format of merged ED25519 PK", "ED25519 PK Format Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("You have not yet added MFA device on this device", "MFA Device Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else 
                {
                    MessageBox.Show("Generated Device MFA ID and Merged ED25519 PK must not be null or empty", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public void GetMFADeviceCount(ref int MFADeviceCount)
        {
            MFADeviceCount = 0;
            Boolean ServerOnlineChecker = true;
            if (ETLSSessionIDStorage.ETLSID.CompareTo("") != 0 && ETLSSessionIDStorage.ETLSID != null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://mrchewitsoftware.com.my:5001/api/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync("OwnerAddDevice/GetMFADeviceCount?DirectoryID=" + DirectoryIDTempStorage.DirectoryID);
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
                                MessageBox.Show(StringResult, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                StringResult = StringResult.Substring(1, StringResult.Length - 2);
                                MFADeviceCount = int.Parse(StringResult);
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
                MessageBox.Show("You have not yet establish an ephemeral TLS session with the server..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                                    Re_RequestRandomChallengeBoolean = true;
                                    SodiumSecureMemory.SecureClearBytes(ServerSignedRandomChallenge);
                                    SodiumSecureMemory.SecureClearBytes(ServerRandomChallenge);
                                    SodiumSecureMemory.SecureClearBytes(ServerED25519PK);
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
                MessageBox.Show("You have not yet establish an ephemeral TLS session with the server..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Boolean UploadFirstMFADevicePK(String DirectoryID, String MFADeviceID, Byte[] RandomChallenge , Byte[] MergedED25519PK)
        {
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
            Byte[] CipheredED25519PK = new Byte[] { };
            Byte[] CombinedCipheredED25519PK = new Byte[] { };
            Byte[] ETLSSignedCombinedCipheredED25519PK = new Byte[] { };
            Boolean ServerOnlineChecker = true;
            if (ETLSSessionIDStorage.ETLSID.CompareTo("") != 0 && ETLSSessionIDStorage.ETLSID != null)
            {
                ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "ECDSASK.txt");
                SharedSecret = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "SharedSecret.txt");
                UserECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryID + "\\rootSK.txt");
                DirectoryIDByte = Encoding.UTF8.GetBytes(DirectoryID);
                Nonce = SodiumSecretBox.GenerateNonce();
                CipheredDirectoryIDByte = SodiumSecretBox.Create(DirectoryIDByte, Nonce, SharedSecret);
                CombinedCipheredDirectoryIDByte = Nonce.Concat(CipheredDirectoryIDByte).ToArray();
                ETLSSignedCombinedCipheredDirectoryIDByte = SodiumPublicKeyAuth.Sign(CombinedCipheredDirectoryIDByte, ClientECDSASK);
                Nonce = SodiumSecretBox.GenerateNonce();
                CipheredED25519PK = SodiumSecretBox.Create(MergedED25519PK, Nonce, SharedSecret,true);
                CombinedCipheredED25519PK = Nonce.Concat(CipheredED25519PK).ToArray();
                ETLSSignedCombinedCipheredED25519PK = SodiumPublicKeyAuth.Sign(CombinedCipheredED25519PK, ClientECDSASK);
                UserSignedRandomChallenge = SodiumPublicKeyAuth.Sign(RandomChallenge, UserECDSASK, true);
                ETLSSignedUserSignedRandomChallenge = SodiumPublicKeyAuth.Sign(UserSignedRandomChallenge, ClientECDSASK,true);
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://mrchewitsoftware.com.my:5001/api/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync("OwnerAddDevice/UploadFirstDevicePK?ClientPathID=" + ETLSSessionIDStorage.ETLSID + "&CipheredSignedDirectoryID=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredDirectoryIDByte)) + "&SignedSignedRandomChallenge=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserSignedRandomChallenge)) + "&CipheredSignedED25519PK=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredED25519PK)) + "&UniqueDeviceID=" + MFADeviceID);
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
                                if (StringResult.Contains("Success")==true)
                                {
                                    return true;
                                }
                                else
                                {
                                    MessageBox.Show(StringResult, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public Boolean UploadSecondMFADevicePK(String DirectoryID, String OtherMFADeviceID, String MFADeviceID ,Byte[] RandomChallenge, Byte[] MergedED25519PK)
        {
            Byte[] ClientECDSASK = new Byte[] { };
            Byte[] UserECDSASK = new Byte[] { };
            Byte[] MFADeviceECDSASK = new Byte[] { };
            Byte[] SharedSecret = new Byte[] { };
            Byte[] DirectoryIDByte = new Byte[] { };
            Byte[] CipheredDirectoryIDByte = new Byte[] { };
            Byte[] CombinedCipheredDirectoryIDByte = new Byte[] { };
            Byte[] ETLSSignedCombinedCipheredDirectoryIDByte = new Byte[] { };
            Byte[] UserSignedRandomChallenge = new Byte[] { };
            Byte[] MFADeviceSignedUserSignedRandomChallenge = new Byte[] { };
            Byte[] ETLSSignedMFADeviceSignedUserSignedRandomChallenge = new Byte[] { };
            Byte[] Nonce = new Byte[] { };
            Byte[] CipheredED25519PK = new Byte[] { };
            Byte[] CombinedCipheredED25519PK = new Byte[] { };
            Byte[] ETLSSignedCombinedCipheredED25519PK = new Byte[] { };
            Boolean ServerOnlineChecker = true;
            if (ETLSSessionIDStorage.ETLSID.CompareTo("") != 0 && ETLSSessionIDStorage.ETLSID != null)
            {
                ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "ECDSASK.txt");
                SharedSecret = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "SharedSecret.txt");
                UserECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryID + "\\rootSK.txt");
                MFADeviceECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryID + "\\MFA_Device\\" + OtherMFADeviceID + "\\SK.txt");
                DirectoryIDByte = Encoding.UTF8.GetBytes(DirectoryID);
                Nonce = SodiumSecretBox.GenerateNonce();
                CipheredDirectoryIDByte = SodiumSecretBox.Create(DirectoryIDByte, Nonce, SharedSecret);
                CombinedCipheredDirectoryIDByte = Nonce.Concat(CipheredDirectoryIDByte).ToArray();
                ETLSSignedCombinedCipheredDirectoryIDByte = SodiumPublicKeyAuth.Sign(CombinedCipheredDirectoryIDByte, ClientECDSASK);
                Nonce = SodiumSecretBox.GenerateNonce();
                CipheredED25519PK = SodiumSecretBox.Create(MergedED25519PK, Nonce, SharedSecret, true);
                CombinedCipheredED25519PK = Nonce.Concat(CipheredED25519PK).ToArray();
                ETLSSignedCombinedCipheredED25519PK = SodiumPublicKeyAuth.Sign(CombinedCipheredED25519PK, ClientECDSASK);
                UserSignedRandomChallenge = SodiumPublicKeyAuth.Sign(RandomChallenge, UserECDSASK, true);
                MFADeviceSignedUserSignedRandomChallenge = SodiumPublicKeyAuth.Sign(UserSignedRandomChallenge, MFADeviceECDSASK, true);
                ETLSSignedMFADeviceSignedUserSignedRandomChallenge = SodiumPublicKeyAuth.Sign(MFADeviceSignedUserSignedRandomChallenge, ClientECDSASK, true);
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://mrchewitsoftware.com.my:5001/api/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync("OwnerAddDevice/UploadOtherDevicePK?ClientPathID=" + ETLSSessionIDStorage.ETLSID + "&CipheredSignedDirectoryID=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredDirectoryIDByte)) + "&SignedSignedRandomChallenge=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedMFADeviceSignedUserSignedRandomChallenge)) + "&CipheredSignedED25519PK=" + System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedCombinedCipheredED25519PK)) + "&OtherUniqueDeviceID=" + OtherMFADeviceID + "&UniqueDeviceID="+MFADeviceID);
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
                                if (StringResult.Contains("Success") == true)
                                {
                                    return true;
                                }
                                else
                                {
                                    MessageBox.Show(StringResult, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
