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
using ASodium;
using System.Security.Cryptography;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using Newtonsoft.Json;
using PriSecFileStorageClient.Helper;
using PriSecFileStorageClient.Model;

namespace PriSecFileStorageClient
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        public void OnReSize(Object sender, EventArgs e)
        {
            MyGroupBox.Top = ((ClientSize.Height) - (MyGroupBox.Height)) / 2;
            MyGroupBox.Left = ((ClientSize.Width) - (MyGroupBox.Width)) / 2;
        }

        private void RegisterBTN_Click(object sender, EventArgs e)
        {
            RegisterModel MyRegisterModel = new RegisterModel();
            SecureIDGenerator secureIDGenerator = new SecureIDGenerator();
            String ETLSSessionID = "";
            String MySecureUserID = secureIDGenerator.GenerateUniqueString();
            RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
            Byte[] MySecureUserIDByte = new Byte[] { };
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
            Boolean CheckServerBoolean = true;
            RevampedKeyPair MyLoginKeyPair = SodiumPublicKeyAuth.GenerateRevampedKeyPair();
            RevampedKeyPair MyRecoveryKeyPair = SodiumPublicKeyAuth.GenerateRevampedKeyPair();
            String UserIDStoragePath = Application.StartupPath + "\\Application_Data\\User\\";
            ETLSSessionID = File.ReadAllText(Application.StartupPath + "\\Temp_Session\\" + "SessionID.txt");
            if (ETLSSessionID != null && ETLSSessionID.CompareTo("") != 0)
            {
                ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionID + "\\" + "ECDSASK.txt");
                if (MySecureUserID.Length > 45)
                {
                    MySecureUserID = MySecureUserID.Substring(0, 30);
                }
                MySecureUserIDByte = Encoding.UTF8.GetBytes(MySecureUserID);
                UserLoginED25519PK = MyLoginKeyPair.PublicKey;
                rngCsp.GetBytes(RandomData);
                NonceByte = SodiumSecretBox.GenerateNonce();
                StreamCipherKeyByte = SodiumSecretBox.GenerateKey();
                CipheredRandomData = SodiumSecretBox.Create(RandomData, NonceByte, StreamCipherKeyByte);
                CombinedCipheredRandomData = NonceByte.Concat(CipheredRandomData).ToArray();
                SignedCombinedCipheredRandomData = SodiumPublicKeyAuth.Sign(CombinedCipheredRandomData, MyRecoveryKeyPair.PrivateKey);
                MyETLSSignedSecureUserIDByte = SodiumPublicKeyAuth.Sign(MySecureUserIDByte, ClientECDSASK);
                UserLoginSignedED25519PK = SodiumPublicKeyAuth.Sign(UserLoginED25519PK, MyLoginKeyPair.PrivateKey);
                ETLSSignedUserLoginSignedED25519PK = SodiumPublicKeyAuth.Sign(UserLoginSignedED25519PK, ClientECDSASK);
                ETLSSignedUserLoginED25519PK = SodiumPublicKeyAuth.Sign(UserLoginED25519PK, ClientECDSASK);
                ETLSSignedSignedCombinedCipheredRandomData = SodiumPublicKeyAuth.Sign(SignedCombinedCipheredRandomData, ClientECDSASK,true);
                if (Directory.GetFileSystemEntries(UserIDStoragePath).Length < 1)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://mrchewitsoftware.com.my:5001/api/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.GetAsync("Register/byValues?ClientPathID=" + ETLSSessionID + "&SignedUserID=" + HttpUtility.UrlEncode(Convert.ToBase64String(MyETLSSignedSecureUserIDByte)) + "&SignedLoginSignedPK=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserLoginSignedED25519PK)) + "&SignedLoginPK=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserLoginED25519PK)) +  "&SignedCiphered_Recovery_Data=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedSignedCombinedCipheredRandomData)));
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

                                var RegisterModelResult = readTask.Result;
                                MyRegisterModel = JsonConvert.DeserializeObject<RegisterModel>(RegisterModelResult);
                                if (MyRegisterModel.Error.Contains("Error"))
                                {
                                    MessageBox.Show("Something went wrong when registering...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    MessageBox.Show("Error: " + MyRegisterModel.Error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    MessageBox.Show("User ID Checker: " + MyRegisterModel.UserIDChecker, "User ID Checker", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    UserIDTB.Text = MySecureUserID;
                                    LoginPKTB.Text = Convert.ToBase64String(UserLoginED25519PK);
                                    SignedCipheredRandomDataTB.Text = Convert.ToBase64String(SignedCombinedCipheredRandomData);
                                    UserIDStoragePath += MySecureUserID;
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
                                        UserIDTempStorage.UserID = MySecureUserID;
                                    }
                                    SodiumSecureMemory.SecureClearBytes(StreamCipherKeyByte);
                                    MyRecoveryKeyPair.Clear();
                                    MyLoginKeyPair.Clear();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Something went wrong with fetching values from server ...", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Server is having some problems or offline...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You are not allowed to have more than 1 account on a single device", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("ETLS Session ID not found or empty...");
                MyRecoveryKeyPair.Clear();
                MyLoginKeyPair.Clear();
            }
        }

        private void Register_Load(object sender, EventArgs e)
        {
            MyGroupBox.Top = ((ClientSize.Height) - (MyGroupBox.Height)) / 2;
            MyGroupBox.Left = ((ClientSize.Width) - (MyGroupBox.Width)) / 2;
        }

        private void ProceedBTN_Click(object sender, EventArgs e)
        {
            if (UserIDTB.Text != null && UserIDTB.Text.CompareTo("") != 0)
            {
                MainFormHolder.OpenMainForm = false;
                this.Close();
                var newForm = new ActionChooser();
                newForm.Show();
            }
            else
            {
                MessageBox.Show("You have not yet register", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowHomeForm(object sender, EventArgs e)
        {
            if (MainFormHolder.OpenMainForm == true)
            {
                MainFormHolder.myForm.Show();
                MainFormHolder.OpenMainForm = false;
            }
        }
    }
}
