﻿using System;
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
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Web;
using PriSecFileStorageClient.Helper;
using PriSecFileStorageClient.Model;

namespace PriSecFileStorageClient
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            String[] UserIDArray = new String[] { };
            int Count = 0;
            int RootDirectoryCount = 0;
            UserIDArray = Directory.GetDirectories(Application.StartupPath + "\\Application_Data\\User\\");
            RootDirectoryCount = (Application.StartupPath + "\\Application_Data\\User\\").Length;
            while (Count < UserIDArray.Length)
            {
                UserIDArray[Count] = UserIDArray[Count].Remove(0, RootDirectoryCount);
                Count += 1;
            }
            UserIDsCB.Items.AddRange(UserIDArray);
            MyGroupBox.Top = ((ClientSize.Height) - (MyGroupBox.Height)) / 2;
            MyGroupBox.Left = ((ClientSize.Width) - (MyGroupBox.Width)) / 2;
        }

        private void OnReSize(Object sender, EventArgs e)
        {
            MyGroupBox.Top = ((ClientSize.Height) - (MyGroupBox.Height)) / 2;
            MyGroupBox.Left = ((ClientSize.Width) - (MyGroupBox.Width)) / 2;
        }

        private void UserIDsCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentUserIDTB.Text = UserIDsCB.Text;
        }

        private void RequestBTN_Click(object sender, EventArgs e)
        {
            Byte[] ServerRandomChallenge = new Byte[] { };
            Byte[] ServerSignedRandomChallenge = new Byte[] { };
            Byte[] ServerED25519PK = new Byte[] { };
            Boolean ServerOnlineChecker = true;
            Boolean CheckServerED25519PK = true;
            Boolean Re_RequestRandomChallengeBoolean = true;
            LoginModels MyLoginModels = new LoginModels();
            if (ETLSSessionIDStorage.ETLSID.CompareTo("") != 0 && ETLSSessionIDStorage.ETLSID != null)
            {
                if (CurrentUserIDTB.Text.CompareTo("") != 0 && CurrentUserIDTB.Text != null)
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
                                        ChallengeBase64TB.Text = Convert.ToBase64String(ServerRandomChallenge);
                                        SodiumSecureMemory.SecureClearBytes(ServerSignedRandomChallenge);
                                        SodiumSecureMemory.SecureClearBytes(ServerRandomChallenge);
                                        SodiumSecureMemory.SecureClearBytes(ServerED25519PK);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Man in the middle spotted ..., proceeding with re-requesting random challenge from server...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        Re_RequestRandomChallenge(ref Re_RequestRandomChallengeBoolean);
                                        while (Re_RequestRandomChallengeBoolean == false)
                                        {
                                            Re_RequestRandomChallenge(ref Re_RequestRandomChallengeBoolean);
                                        }
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
                    MessageBox.Show("You have not yet choose a user ID...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("You have not yet establish an ephemeral TLS session with the server..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Re_RequestRandomChallenge(ref Boolean RefBoolean)
        {
            Byte[] ServerRandomChallenge = new Byte[] { };
            Byte[] ServerSignedRandomChallenge = new Byte[] { };
            Byte[] ServerED25519PK = new Byte[] { };
            Boolean ServerOnlineChecker = true;
            Boolean CheckServerED25519PK = true;
            LoginModels MyLoginModels = new LoginModels();
            if (ETLSSessionIDStorage.ETLSID.CompareTo("") != 0 && ETLSSessionIDStorage.ETLSID != null)
            {
                if (CurrentUserIDTB.Text.CompareTo("") != 0 && CurrentUserIDTB.Text != null)
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
                                        ChallengeBase64TB.Text = Convert.ToBase64String(ServerRandomChallenge);
                                        SodiumSecureMemory.SecureClearBytes(ServerSignedRandomChallenge);
                                        SodiumSecureMemory.SecureClearBytes(ServerRandomChallenge);
                                        SodiumSecureMemory.SecureClearBytes(ServerED25519PK);
                                        RefBoolean = true;
                                    }
                                    else
                                    {
                                        RefBoolean = false;
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
                    MessageBox.Show("You have not yet choose a user ID...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("You have not yet establish an ephemeral TLS session with the server..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SignBTN_Click(object sender, EventArgs e)
        {
            String UserID = "";
            Byte[] ServerRandomChallenge = new Byte[] { };
            Byte[] UserSignedRandomChallenge = new Byte[] { };
            Byte[] ETLSSignedUserSignedRandomChallenge = new Byte[] { };
            Byte[] UserIDByte = new Byte[] { };
            Byte[] ETLSSignedUserIDByte = new Byte[] { };
            Byte[] ClientECDSASK = new Byte[] { };
            Byte[] LoginECDSASK = new Byte[] { };
            String LoginPath = Application.StartupPath + "\\Application_Data\\User\\";
            String VerifyStatus = "";
            Boolean ServerOnlineChecker = true;
            if (ETLSSessionIDStorage.ETLSID.CompareTo("") != 0 && ETLSSessionIDStorage.ETLSID != null)
            {
                if (CurrentUserIDTB.Text.CompareTo("") != 0 && CurrentUserIDTB.Text != null && ChallengeBase64TB.Text.CompareTo("") != 0 && ChallengeBase64TB.Text != null)
                {
                    UserID = CurrentUserIDTB.Text;
                    UserIDByte = Encoding.UTF8.GetBytes(UserID);
                    ServerRandomChallenge = Convert.FromBase64String(ChallengeBase64TB.Text);
                    LoginPath += UserID;
                    LoginPath += "\\" + "Authentication_Data" + "\\" + "Login" + "\\" + "LoginSK.txt";
                    LoginECDSASK = File.ReadAllBytes(LoginPath);
                    UserSignedRandomChallenge = SodiumPublicKeyAuth.Sign(ServerRandomChallenge, LoginECDSASK,true);
                    ClientECDSASK = File.ReadAllBytes(Application.StartupPath + "\\Temp_Session\\" + ETLSSessionIDStorage.ETLSID + "\\" + "ECDSASK.txt");
                    ETLSSignedUserIDByte = SodiumPublicKeyAuth.Sign(UserIDByte, ClientECDSASK);
                    ETLSSignedUserSignedRandomChallenge = SodiumPublicKeyAuth.Sign(UserSignedRandomChallenge, ClientECDSASK,true);
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://mrchewitsoftware.com.my:5001/api/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.GetAsync("Login/VerifySignatureBy?ClientPathID=" + ETLSSessionIDStorage.ETLSID + "&SignedUserID=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserIDByte)) + "&SignedSignedRandomChallenge=" + HttpUtility.UrlEncode(Convert.ToBase64String(ETLSSignedUserSignedRandomChallenge)));
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

                                VerifyStatus = readTask.Result;
                                if (VerifyStatus.Contains("Error"))
                                {
                                    MessageBox.Show("Something went wrong on server side when verifying signed challenge...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    MessageBox.Show(VerifyStatus, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    SodiumSecureMemory.SecureClearBytes(UserSignedRandomChallenge);
                                    SodiumSecureMemory.SecureClearBytes(ETLSSignedUserSignedRandomChallenge);
                                    SodiumSecureMemory.SecureClearBytes(ServerRandomChallenge);
                                    SodiumSecureMemory.SecureClearBytes(ETLSSignedUserIDByte);
                                    UserIDTempStorage.UserID = UserID;
                                    MainFormHolder.OpenMainForm = false;
                                    this.Close();
                                    var ActionChooserForm = new ActionChooser();
                                    ActionChooserForm.Show();
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
                    MessageBox.Show("You have not yet choose a user ID... and there's absence of random challenge", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("You have not yet establish an ephemeral TLS session with the server..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
