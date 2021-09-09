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
using System.Web;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Net.Http;
using System.IO;
using System.Security.Cryptography;
using PriSecFileStorageClient.Helper;
using PriSecFileStorageClient.Model;

namespace PriSecFileStorageClient
{
    public partial class AuthenticationChooser : Form
    {
        private bool mouseDown;
        private Point lastLocation;
        private SecureIDGenerator MySecureIDGenerator = new SecureIDGenerator();

        public AuthenticationChooser()
        {
            InitializeComponent();
        }

        private void AccountRecoveryBTN_Click(object sender, EventArgs e)
        {
            MainFormHolder.myForm = this;
            MainFormHolder.OpenMainForm = true;
            AccountRecovery accountRecovery = new AccountRecovery();
            accountRecovery.Show();
            this.Hide();
        }

        private void RegisterBTN_Click(object sender, EventArgs e)
        {
            MainFormHolder.myForm = this;
            MainFormHolder.OpenMainForm = true;
            Register register = new Register();
            register.Show();
            this.Hide();
        }

        private void LoginBTN_Click(object sender, EventArgs e)
        {
            MainFormHolder.myForm = this;
            MainFormHolder.OpenMainForm = true;
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void MiddlePanel_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            Rectangle rect = panel.ClientRectangle;
            rect.Width--;
            rect.Height--;
            e.Graphics.DrawRectangle(Pens.White, rect);
        }

        private void AuthenticationChooser_Load(object sender, EventArgs e)
        {
            Image LockImage = Image.FromFile(Application.StartupPath + "\\UI_Pictures\\Lock.png");
            LeftLockPB1.Image = LockImage;
            LeftLockPB1.SizeMode = PictureBoxSizeMode.StretchImage;
            Image MaleActorImage = Image.FromFile(Application.StartupPath + "\\UI_Pictures\\Actor.png");
            LeftMaleActorPB.Image = MaleActorImage;
            LeftMaleActorPB.SizeMode = PictureBoxSizeMode.StretchImage;
            Image FemaleActorImage = Image.FromFile(Application.StartupPath + "\\UI_Pictures\\FemaleActor.png");
            RightFemaleActorPB.Image = FemaleActorImage;
            RightFemaleActorPB.SizeMode = PictureBoxSizeMode.StretchImage;
            Image LockOpenImage = Image.FromFile(Application.StartupPath + "\\UI_Pictures\\LockOpen.png");
            RightLockOpenPB.Image = LockOpenImage;
            RightLockOpenPB.SizeMode = PictureBoxSizeMode.StretchImage;
            if (Directory.GetFileSystemEntries(Application.StartupPath + "\\Temp_Session\\").Length == 0 || Directory.GetFileSystemEntries(Application.StartupPath + "\\Temp_Session\\").Length == 1)
            {
                CreateNewSession();
            }
            else
            {
                Boolean ServerOnlineChecker = true;
                StreamReader MyStreamReader = new StreamReader(Application.StartupPath + "\\Temp_Session\\" + "SessionID.txt");
                String Temp_Session_ID = MyStreamReader.ReadLine();
                MyStreamReader.Close();
                Byte[] ClientECDSASKByte = new Byte[] { };
                Byte[] RandomData = new Byte[240];
                RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
                rngCsp.GetBytes(RandomData);
                Byte[] SignedRandomData = new Byte[] { };
                String Status = "";
                if (Temp_Session_ID != null && Temp_Session_ID.CompareTo("") != 0)
                {
                    using (var client = new HttpClient())
                    {
                        ClientECDSASKByte = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + Temp_Session_ID + "\\ECDSASK.txt");
                        SignedRandomData = SodiumPublicKeyAuth.Sign(RandomData, ClientECDSASKByte,true);
                        client.BaseAddress = new Uri("https://mrchewitsoftware.com.my:5001/api/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.GetAsync("ECDH_ECDSA_TempSession/DeleteByClientCryptographicID?ClientPathID=" + Temp_Session_ID + "&ValidationData=" + HttpUtility.UrlEncode(Convert.ToBase64String(SignedRandomData)));
                        SodiumSecureMemory.SecureClearBytes(RandomData);
                        SodiumSecureMemory.SecureClearBytes(SignedRandomData);
                        try
                        {
                            response.Wait();
                        }
                        catch
                        {
                            ServerOnlineChecker = false;
                            MessageBox.Show("The server is offline now please wait for awhile ..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                                    File.WriteAllText(Application.StartupPath + "\\Error_Data\\Greetings\\DeleteHandShakeSessionIDStatus.txt", Status);
                                }
                                else
                                {
                                    Directory.Delete(Application.StartupPath + "\\Temp_Session\\" + Temp_Session_ID, true);
                                    File.WriteAllText(Application.StartupPath + "\\Temp_Session\\" + "SessionID.txt", "");
                                    CreateNewSession();
                                }
                            }
                            else
                            {
                                File.WriteAllText(Application.StartupPath + "\\Error_Data\\Greetings\\GeneralStatus.txt", "Something went wrong on server side... refer to any status text to find out..");
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Something's wrong .. did you delete the values in the text file but not the folder?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void CreateNewSession()
        {
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
                                        File.WriteAllBytes(Application.StartupPath + "\\Temp_Session\\" + MySession_ID + "\\ServerECDHPK.txt", ServerECDHPKByte);
                                        RegisterBTN.Enabled = true;
                                        LoginBTN.Enabled = true;
                                        AccountRecoveryBTN.Enabled = true;
                                        StatusLabel.Text = "Please click one of the button below";
                                    }
                                    else 
                                    {
                                        StatusLabel.Text = "Encounter error, shared secret created "+
                                            Environment.NewLine+"is not the same, please restart application";
                                    }
                                }
                                else 
                                {
                                    StatusLabel.Text = "Encounter error, unable to create "+
                                        Environment.NewLine+"shared secret, please restart application";
                                }
                            }
                            else
                            {
                                File.WriteAllText(Application.StartupPath + "\\Error_Data\\Greetings\\FetchHandShakeSessionParameterStatus.txt", "Server's ECDH public key can't be verified with given ECDSA public key");
                                StatusLabel.Text = "Encounter error, unable to verify "+
                                    Environment.NewLine+"public key, please restart application";
                            }
                            SodiumSecureMemory.SecureClearBytes(ServerECDSAPKByte);
                            SodiumSecureMemory.SecureClearBytes(ServerECDHSPKByte);
                            SodiumSecureMemory.SecureClearBytes(ServerECDHPKByte);
                            SodiumSecureMemory.SecureClearString(MyECDH_ECDSA_Models.ECDSA_PK_Base64String);
                            SodiumSecureMemory.SecureClearString(MyECDH_ECDSA_Models.ECDH_SPK_Base64String);
                        }
                        else
                        {
                            MessageBox.Show(MyECDH_ECDSA_Models.ID_Checker_Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        File.WriteAllText(Application.StartupPath + "\\Error_Data\\Greetings\\FetchHandShakeSessionParameterStatus.txt", "Failed to get handshake parameters from server");
                    }
                }
                else
                {
                    MessageBox.Show("The server is offline now please wait for awhile ..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            SessionECDHKeyPair.Clear();
            SessionECDSAKeyPair.Clear();
        }

        public void CreateSharedSecret(ref Boolean CheckBoolean, String MySession_ID, Byte[] SignedClientSessionECDHPKByte, Byte[] ClientSessionECDSAPKByte)
        {
            CheckBoolean = false;
            String SessionStatus = "";
            var CreateSharedSecretHttpClient = new HttpClient();
            CreateSharedSecretHttpClient.BaseAddress = new Uri("https://mrchewitsoftware.com.my:5001/api/");
            CreateSharedSecretHttpClient.DefaultRequestHeaders.Accept.Clear();
            CreateSharedSecretHttpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var newresponse = CreateSharedSecretHttpClient.GetAsync("ECDH_ECDSA_TempSession/ByHandshake?ClientPathID=" + MySession_ID + "&SECDHPK=" + HttpUtility.UrlEncode(Convert.ToBase64String(SignedClientSessionECDHPKByte)) + "&ECDSAPK=" + HttpUtility.UrlEncode(Convert.ToBase64String(ClientSessionECDSAPKByte)));
            try
            {
                newresponse.Wait();
                var newresult = newresponse.Result;

                if (newresult.IsSuccessStatusCode)
                {
                    var newreadTask = newresult.Content.ReadAsStringAsync();
                    newreadTask.Wait();

                    SessionStatus += newreadTask.Result;

                    if (SessionStatus.Contains("Error")==true)
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
                    MessageBox.Show("Failed to fetch values from server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("The server is offline now please wait for awhile ..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CheckSharedSecret(ref Boolean CheckBoolean, String MySession_ID, Byte[] ServerECDHPKByte, Byte[] SessionECDHPrivateKey, Byte[] SessionECDSAPrivateKey)
        {
            CheckBoolean = false;
            Boolean CheckServerOnline = true;
            String CheckSharedSecretStatus = "";
            Byte[] TestData = new Byte[] { 255, 255, 255 };
            Byte[] SharedSecretByte = SodiumScalarMult.Mult(SessionECDHPrivateKey, ServerECDHPKByte,true);
            Byte[] NonceByte = SodiumSecretBox.GenerateNonce();
            Byte[] TestEncryptedData = SodiumSecretBox.Create(TestData, NonceByte, SharedSecretByte);
            var CheckSharedSecretHttpClient = new HttpClient();
            CheckSharedSecretHttpClient.BaseAddress = new Uri("https://mrchewitsoftware.com.my:5001/api/");
            CheckSharedSecretHttpClient.DefaultRequestHeaders.Accept.Clear();
            CheckSharedSecretHttpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var CheckSharedSecretHttpClientResponse = CheckSharedSecretHttpClient.GetAsync("ECDH_ECDSA_TempSession/BySharedSecret?ClientPathID=" + MySession_ID + "&CipheredData=" + HttpUtility.UrlEncode(Convert.ToBase64String(TestEncryptedData)) + "&Nonce=" + HttpUtility.UrlEncode(Convert.ToBase64String(NonceByte)));
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
                        if (Directory.Exists(Application.StartupPath + "\\Temp_Session\\" + MySession_ID))
                        {
                            File.WriteAllBytes(Application.StartupPath + "\\Temp_Session\\" + MySession_ID + "\\" + "SharedSecret.txt", SharedSecretByte);
                            File.WriteAllBytes(Application.StartupPath + "\\Temp_Session\\" + MySession_ID + "\\" + "ECDSASK.txt", SessionECDSAPrivateKey);
                        }
                        else
                        {
                            Directory.CreateDirectory(Application.StartupPath + "\\Temp_Session\\" + MySession_ID);
                            File.WriteAllBytes(Application.StartupPath + "\\Temp_Session\\" + MySession_ID + "\\" + "SharedSecret.txt", SharedSecretByte);
                            File.WriteAllBytes(Application.StartupPath + "\\Temp_Session\\" + MySession_ID + "\\" + "ECDSASK.txt", SessionECDSAPrivateKey);
                            File.WriteAllText(Application.StartupPath + "\\Temp_Session\\" + "SessionID.txt", MySession_ID);
                        }
                        ETLSSessionIDStorage.ETLSID = MySession_ID;
                        CheckBoolean = true;
                    }
                }
                else
                {
                    MessageBox.Show("Failed to fetch values from server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                SodiumSecureMemory.SecureClearBytes(SharedSecretByte);
            }
            else
            {
                MessageBox.Show("The server is offline now please wait for awhile ..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AuthenticationChooser_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void AuthenticationChooser_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void AuthenticationChooser_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void CloseBTN_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
