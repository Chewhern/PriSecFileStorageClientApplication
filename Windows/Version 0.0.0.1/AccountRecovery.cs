using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Web;
using ASodium;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace PriSecFileStorageClient
{
    public partial class AccountRecovery : Form
    {
        public AccountRecovery()
        {
            InitializeComponent();
        }

        private void ResetAccountBTN_Click(object sender, EventArgs e)
        {
            String UserID = "";
            Byte[] StreamCipherKey = new Byte[] { };
            Byte[] ED25519PK = new Byte[] { };
            String CipheredRecoveryDataString = "";
            Byte[] UnverifiedCipheredRecoveryDataByte = new Byte[] { };
            Boolean VerifyStatus = true;
            Byte[] VerifiedCipheredRecoveryDataByte = new Byte[] { };
            Byte[] NonceByte = new Byte[] { };
            Byte[] CipheredRecoveryDataByte = new Byte[] { };
            Boolean DecryptStatus = true;
            Byte[] RecoveryDataByte = new Byte[] { };
            String ETLSSessionID = "";
            GCHandle MyGeneralGCHandle = new GCHandle();
            Byte[] ClientECDSASK = new Byte[] { };
            ETLSSessionID = File.ReadAllText(Application.StartupPath + "\\Temp_Session\\" + "SessionID.txt");
            if (ETLSSessionID != null && ETLSSessionID.CompareTo("") != 0)
            {
                ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionID + "\\" + "ECDSASK.txt");
                if (AccountIDCB.Text != null && AccountIDCB.Text.CompareTo("") != 0)
                {
                    UserID = AccountIDCB.Text;
                    CipheredRecoveryDataString = GetRecoveryData(UserID);
                    if (CipheredRecoveryDataString.Contains("Error") || CipheredRecoveryDataString.CompareTo("") == 0)
                    {
                        MessageBox.Show("Something went wrong.. when fetching values from server");
                    }
                    else
                    {
                        CipheredRecoveryDataString = CipheredRecoveryDataString.Substring(1, CipheredRecoveryDataString.Length - 2);
                        UnverifiedCipheredRecoveryDataByte = Convert.FromBase64String(CipheredRecoveryDataString);
                        ED25519PK = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\User\\" + UserID + "\\Recovery_Data\\RecoveryPK.txt");
                        try
                        {
                            VerifiedCipheredRecoveryDataByte = SodiumPublicKeyAuth.Verify(UnverifiedCipheredRecoveryDataByte, ED25519PK);
                        }
                        catch
                        {
                            VerifyStatus = false;
                        }
                        if (VerifyStatus == true)
                        {
                            NonceByte = new Byte[24];
                            Array.Copy(VerifiedCipheredRecoveryDataByte, NonceByte, 24);
                            CipheredRecoveryDataByte = new Byte[VerifiedCipheredRecoveryDataByte.Length - 24];
                            Array.Copy(VerifiedCipheredRecoveryDataByte, 24, CipheredRecoveryDataByte, 0, VerifiedCipheredRecoveryDataByte.Length - 24);
                            StreamCipherKey = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\User\\" + UserID + "\\Recovery_Data\\StreamCipherKey.txt");
                            try
                            {
                                RecoveryDataByte = SodiumSecretBox.Open(CipheredRecoveryDataByte, NonceByte, StreamCipherKey);
                            }
                            catch (Exception exception)
                            {
                                MessageBox.Show(exception.Message);
                                DecryptStatus = false;
                            }
                            if (DecryptStatus == true)
                            {
                                if (ResetAccount(UserID, ED25519PK) == true)
                                {
                                    MessageBox.Show("Successfully reset account information");
                                }
                                else
                                {
                                    MessageBox.Show("Failed to reset account information");
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You haven't choose a user ID");
                }
            }
            else
            {
                MessageBox.Show("ETLS Session ID not found or empty...");
            }
            MyGeneralGCHandle = GCHandle.Alloc(ED25519PK, GCHandleType.Pinned);
            SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), ED25519PK.Length);
            MyGeneralGCHandle.Free();
            MyGeneralGCHandle = GCHandle.Alloc(StreamCipherKey, GCHandleType.Pinned);
            SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), StreamCipherKey.Length);
            MyGeneralGCHandle.Free();
            MyGeneralGCHandle = GCHandle.Alloc(ClientECDSASK, GCHandleType.Pinned);
            SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), ClientECDSASK.Length);
            MyGeneralGCHandle.Free();
        }

        private void DeleteAccountBTN_Click(object sender, EventArgs e)
        {
            String UserID = "";
            Byte[] StreamCipherKey = new Byte[] { };
            Byte[] ED25519PK = new Byte[] { };
            String CipheredRecoveryDataString = "";
            Byte[] UnverifiedCipheredRecoveryDataByte = new Byte[] { };
            Boolean VerifyStatus = true;
            Byte[] VerifiedCipheredRecoveryDataByte = new Byte[] { };
            Byte[] NonceByte = new Byte[] { };
            Byte[] CipheredRecoveryDataByte = new Byte[] { };
            Boolean DecryptStatus = true;
            Byte[] RecoveryDataByte = new Byte[] { };
            Boolean DeleteAccountStatus = true;
            String ETLSSessionID = "";
            GCHandle MyGeneralGCHandle = new GCHandle();
            Byte[] ClientECDSASK = new Byte[] { };
            ETLSSessionID = File.ReadAllText(Application.StartupPath + "\\Temp_Session\\" + "SessionID.txt");
            if (ETLSSessionID != null && ETLSSessionID.CompareTo("") != 0)
            {
                ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionID + "\\" + "ECDSASK.txt");
                if (AccountIDCB.Text != null && AccountIDCB.Text.CompareTo("") != 0)
                {
                    UserID = AccountIDCB.Text;
                    CipheredRecoveryDataString = GetRecoveryData(UserID);
                    if (CipheredRecoveryDataString.Contains("Error") || CipheredRecoveryDataString.CompareTo("") == 0)
                    {
                        MessageBox.Show("Something went wrong.. when fetching values from server");
                    }
                    else
                    {
                        CipheredRecoveryDataString = CipheredRecoveryDataString.Substring(1, CipheredRecoveryDataString.Length - 2);
                        UnverifiedCipheredRecoveryDataByte = Convert.FromBase64String(CipheredRecoveryDataString);
                        ED25519PK = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\User\\" + UserID + "\\Recovery_Data\\RecoveryPK.txt");
                        try
                        {
                            VerifiedCipheredRecoveryDataByte = SodiumPublicKeyAuth.Verify(UnverifiedCipheredRecoveryDataByte, ED25519PK);
                        }
                        catch
                        {
                            VerifyStatus = false;
                        }
                        if (VerifyStatus == true)
                        {
                            NonceByte = new Byte[24];
                            Array.Copy(VerifiedCipheredRecoveryDataByte, NonceByte, 24);
                            CipheredRecoveryDataByte = new Byte[VerifiedCipheredRecoveryDataByte.Length - 24];
                            Array.Copy(VerifiedCipheredRecoveryDataByte, 24, CipheredRecoveryDataByte, 0, VerifiedCipheredRecoveryDataByte.Length - 24);
                            StreamCipherKey = File.ReadAllBytes(Application.StartupPath + "\\Application_Data\\User\\" + UserID + "\\Recovery_Data\\StreamCipherKey.txt");
                            try
                            {
                                RecoveryDataByte = SodiumSecretBox.Open(CipheredRecoveryDataByte, NonceByte, StreamCipherKey);
                            }
                            catch
                            {
                                DecryptStatus = false;
                            }
                            if (DecryptStatus == true)
                            {
                                DeleteAccountStatus = DeleteAccount(UserID, ED25519PK);

                                if (DeleteAccountStatus == true)
                                {
                                    MessageBox.Show("Successfully delete account");
                                    MessageBox.Show("It's up to you to decide whether you want to preserve some of your files located in Application Data");
                                }
                                else
                                {
                                    MessageBox.Show("Failed to delete account");
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You haven't choose a user ID");
                }
            }
            else
            {
                MessageBox.Show("ETLS Session ID not found or empty...");
            }
            MyGeneralGCHandle = GCHandle.Alloc(ED25519PK, GCHandleType.Pinned);
            SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), ED25519PK.Length);
            MyGeneralGCHandle.Free();
            MyGeneralGCHandle = GCHandle.Alloc(StreamCipherKey, GCHandleType.Pinned);
            SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), StreamCipherKey.Length);
            MyGeneralGCHandle.Free();
            MyGeneralGCHandle = GCHandle.Alloc(ClientECDSASK, GCHandleType.Pinned);
            SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), ClientECDSASK.Length);
            MyGeneralGCHandle.Free();
        }

        private void AccountRecovery_Load(object sender, EventArgs e)
        {
            SetEnvironmentVariableHelper.SetEnvironmentVariable();
            String[] UserIDDirectoryArray = Directory.GetDirectories(Application.StartupPath + "\\Application_Data\\User\\");
            DirectoryInfo directoryInfo;
            String[] UserIDArray = new String[UserIDDirectoryArray.Length];
            int Count = 0;
            while (Count < UserIDDirectoryArray.Length)
            {
                directoryInfo = new DirectoryInfo(UserIDDirectoryArray[Count]);
                UserIDArray[Count] = directoryInfo.Name;
                Count += 1;
            }
            AccountIDCB.Items.AddRange(UserIDArray);
        }

        public String GetRecoveryData(String UserID)
        {
            Byte[] UserIDByte = new Byte[] { };
            Byte[] ETLSSignedUserIDByte = new Byte[] { };
            Boolean ServerOnlineChecker = true;
            String ETLSSessionID = "";
            Byte[] ClientECDSASK = new Byte[] { };
            ETLSSessionID = File.ReadAllText(Application.StartupPath + "\\Temp_Session\\" + "SessionID.txt");
            if (ETLSSessionID != null && ETLSSessionID.CompareTo("") != 0)
            {
                ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionID + "\\" + "ECDSASK.txt");
                if (UserID != null && UserID.CompareTo("") != 0)
                {
                    UserIDByte = Encoding.UTF8.GetBytes(UserID);
                    ETLSSignedUserIDByte = SodiumPublicKeyAuth.Sign(UserIDByte, ClientECDSASK);
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://{link to the API}");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.GetAsync("AccountRecovery/byUserID?ClientPathID=" + ETLSSessionID + "&SignedUserID=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserIDByte)));
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

                                return readTask.Result;
                            }
                            else
                            {
                                return "Error: Failed to fetch value from server";
                            }
                        }
                        else
                        {
                            return "Error: Server is not online";
                        }
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

        public Boolean DeleteAccount(String UserID, Byte[] ED25519PK)
        {
            Byte[] UserIDByte = new Byte[] { };
            Byte[] ETLSSignedUserIDByte = new Byte[] { };
            Byte[] ETLSSignedED25519PK = new Byte[] { };
            Boolean ServerOnlineChecker = true;
            String ETLSSessionID = "";
            String Status = "";
            Byte[] ClientECDSASK = new Byte[] { };
            ETLSSessionID = File.ReadAllText(Application.StartupPath + "\\Temp_Session\\" + "SessionID.txt");
            if (ETLSSessionID != null && ETLSSessionID.CompareTo("") != 0)
            {
                ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionID + "\\" + "ECDSASK.txt");
                if (UserID != null && UserID.CompareTo("") != 0)
                {
                    UserIDByte = Encoding.UTF8.GetBytes(UserID);
                    ETLSSignedUserIDByte = SodiumPublicKeyAuth.Sign(UserIDByte, ClientECDSASK);
                    ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionID + "\\" + "ECDSASK.txt");
                    ETLSSignedED25519PK = SodiumPublicKeyAuth.Sign(ED25519PK, ClientECDSASK);
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://{link to the API}");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.GetAsync("AccountRecovery/DeletebyUserID?ClientPathID=" + ETLSSessionID + "&SignedUserID=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserIDByte)) + "&SignedECDSAPK=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedED25519PK)));
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

                                Status = readTask.Result;

                                if (Status.Contains("Error"))
                                {
                                    return false;
                                }
                                else
                                {
                                    return true;
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

        public Boolean ResetAccount(String UserID, Byte[] ED25519PK)
        {
            Boolean ResetStatus = false;
            String ETLSSessionID = "";
            RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
            Byte[] UserIDByte = new Byte[] { };
            Byte[] MyETLSSignedSecureUserIDByte = new Byte[] { };
            Byte[] UserLoginED25519PK = new Byte[] { };
            Byte[] UserLoginSignedED25519PK = new Byte[] { };
            Byte[] ETLSSignedUserLoginED25519PK = new Byte[] { };
            Byte[] ETLSSignedUserLoginSignedED25519PK = new Byte[] { };
            Byte[] RandomData = new Byte[128];
            Byte[] CipheredRandomData = new Byte[] { };
            Byte[] NonceByte = new Byte[] { };
            Byte[] CombinedCipheredRandomData = new Byte[] { };
            Byte[] SignedCombinedCipheredRandomData = new Byte[] { };
            Byte[] ETLSSignedSignedCombinedCipheredRandomData = new Byte[] { };
            Byte[] StreamCipherKeyByte = new Byte[] { };
            Byte[] ClientECDSASK = new Byte[] { };
            Byte[] ETLSSignedED25519PK = new Byte[] { };
            Boolean CheckServerBoolean = true;
            RevampedKeyPair MyLoginKeyPair = SodiumPublicKeyAuth.GenerateRevampedKeyPair();
            RevampedKeyPair MyRecoveryKeyPair = SodiumPublicKeyAuth.GenerateRevampedKeyPair();
            GCHandle MyGeneralGCHandle;
            String UserIDStoragePath = Application.StartupPath + "\\Application_Data\\User\\";
            ETLSSessionID = File.ReadAllText(Application.StartupPath + "\\Temp_Session\\" + "SessionID.txt");
            if (ETLSSessionID != null && ETLSSessionID.CompareTo("") != 0)
            {
                ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionID + "\\" + "ECDSASK.txt");
                if (UserID == null || UserID.CompareTo("") == 0)
                {
                    MessageBox.Show("User ID can't be null");
                }
                else
                {
                    UserIDByte = Encoding.UTF8.GetBytes(UserID);
                    UserLoginED25519PK = MyLoginKeyPair.PublicKey;
                    rngCsp.GetBytes(RandomData);
                    NonceByte = SodiumSecretBox.GenerateNonce();
                    StreamCipherKeyByte = SodiumSecretBox.GenerateKey();
                    CipheredRandomData = SodiumSecretBox.Create(RandomData, NonceByte, StreamCipherKeyByte);
                    CombinedCipheredRandomData = NonceByte.Concat(CipheredRandomData).ToArray();
                    SignedCombinedCipheredRandomData = SodiumPublicKeyAuth.Sign(CombinedCipheredRandomData, MyRecoveryKeyPair.PrivateKey);
                    MyETLSSignedSecureUserIDByte = SodiumPublicKeyAuth.Sign(UserIDByte, ClientECDSASK);
                    UserLoginSignedED25519PK = SodiumPublicKeyAuth.Sign(UserLoginED25519PK, MyLoginKeyPair.PrivateKey);
                    ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionID + "\\" + "ECDSASK.txt");
                    ETLSSignedUserLoginSignedED25519PK = SodiumPublicKeyAuth.Sign(UserLoginSignedED25519PK, ClientECDSASK);
                    ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionID + "\\" + "ECDSASK.txt");
                    ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionID + "\\" + "ECDSASK.txt");
                    ETLSSignedUserLoginED25519PK = SodiumPublicKeyAuth.Sign(UserLoginED25519PK, ClientECDSASK);
                    ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionID + "\\" + "ECDSASK.txt");
                    ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionID + "\\" + "ECDSASK.txt");
                    ETLSSignedSignedCombinedCipheredRandomData = SodiumPublicKeyAuth.Sign(SignedCombinedCipheredRandomData, ClientECDSASK);
                    ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionID + "\\" + "ECDSASK.txt");
                    ETLSSignedED25519PK = SodiumPublicKeyAuth.Sign(ED25519PK, ClientECDSASK);
                    MyGeneralGCHandle = GCHandle.Alloc(ClientECDSASK, GCHandleType.Pinned);
                    SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), ClientECDSASK.Length);
                    MyGeneralGCHandle.Free();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://{link to the API}");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.GetAsync("AccountRecovery/UpdatebyUserID?ClientPathID=" + ETLSSessionID + "&SignedUserID=" + HttpUtility.UrlEncode(Convert.ToBase64String(MyETLSSignedSecureUserIDByte)) + "&SignedECDSAPK=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedED25519PK)) + "&SignedLoginSignedPK=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserLoginSignedED25519PK)) + "&SignedLoginPK=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserLoginED25519PK)) + "&SignedCiphered_Recovery_Data=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedSignedCombinedCipheredRandomData)));
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
                                if (Result.Contains("Error"))
                                {
                                    MessageBox.Show("Something went wrong when registering...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    MessageBox.Show("Error: " + Result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    UserIDStoragePath += UserID;
                                    if (Directory.Exists(UserIDStoragePath))
                                    {
                                        File.WriteAllBytes(UserIDStoragePath + "\\" + "Authentication_Data" + "\\" + "Login" + "\\" + "LoginPK.txt", UserLoginED25519PK);
                                        File.WriteAllBytes(UserIDStoragePath + "\\" + "Authentication_Data" + "\\" + "Login" + "\\" + "LoginSK.txt", MyLoginKeyPair.PrivateKey);
                                        File.WriteAllBytes(UserIDStoragePath + "\\" + "Recovery_Data" + "\\" + "RecoveryPK.txt", MyRecoveryKeyPair.PublicKey);
                                        File.WriteAllBytes(UserIDStoragePath + "\\" + "Recovery_Data" + "\\" + "RecoverySK.txt", MyRecoveryKeyPair.PrivateKey);
                                        File.WriteAllBytes(UserIDStoragePath + "\\" + "Recovery_Data" + "\\" + "StreamCipherKey.txt", StreamCipherKeyByte);
                                        File.WriteAllBytes(UserIDStoragePath + "\\" + "Recovery_Data" + "\\" + "SignedCombinedCipheredRandomData.txt", SignedCombinedCipheredRandomData);
                                    }
                                    else
                                    {
                                        Directory.CreateDirectory(UserIDStoragePath);
                                        Directory.CreateDirectory(UserIDStoragePath + "\\" + "Authentication_Data");
                                        Directory.CreateDirectory(UserIDStoragePath + "\\" + "Recovery_Data");
                                        Directory.CreateDirectory(UserIDStoragePath + "\\" + "Authentication_Data" + "\\" + "Login");
                                        File.WriteAllBytes(UserIDStoragePath + "\\" + "Authentication_Data" + "\\" + "Login" + "\\" + "LoginPK.txt", UserLoginED25519PK);
                                        File.WriteAllBytes(UserIDStoragePath + "\\" + "Authentication_Data" + "\\" + "Login" + "\\" + "LoginSK.txt", MyLoginKeyPair.PrivateKey);
                                        File.WriteAllBytes(UserIDStoragePath + "\\" + "Recovery_Data" + "\\" + "RecoveryPK.txt", MyRecoveryKeyPair.PublicKey);
                                        File.WriteAllBytes(UserIDStoragePath + "\\" + "Recovery_Data" + "\\" + "RecoverySK.txt", MyRecoveryKeyPair.PrivateKey);
                                        File.WriteAllBytes(UserIDStoragePath + "\\" + "Recovery_Data" + "\\" + "StreamCipherKey.txt", StreamCipherKeyByte);
                                        File.WriteAllBytes(UserIDStoragePath + "\\" + "Recovery_Data" + "\\" + "SignedCombinedCipheredRandomData.txt", SignedCombinedCipheredRandomData);
                                    }
                                    MyRecoveryKeyPair.Clear();
                                    MyLoginKeyPair.Clear();
                                    ResetStatus = true;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Something went wrong with fetching values from server ...", "Reset Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Server is having some problems or offline...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("ETLS Session ID not found or empty...");
                MyGeneralGCHandle = GCHandle.Alloc(StreamCipherKeyByte, GCHandleType.Pinned);
                SodiumSecureMemory.MemZero(MyGeneralGCHandle.AddrOfPinnedObject(), StreamCipherKeyByte.Length);
                MyGeneralGCHandle.Free();
                MyRecoveryKeyPair.Clear();
                MyLoginKeyPair.Clear();
            }
            return ResetStatus;
        }

        private void CloseForm(object sender, EventArgs e)
        {
            MainFormHolder.myForm.Show();
            MainFormHolder.OpenMainForm = false;
        }
    }
}
