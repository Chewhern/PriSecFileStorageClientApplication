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
using PriSecFileStorageClient.Helper;

namespace PriSecFileStorageClient
{
    public partial class UserGenerateKey : Form
    {
        public UserGenerateKey()
        {
            InitializeComponent();
        }

        private void GenerateSignatureKPBTN_Click(object sender, EventArgs e)
        {
            RevampedKeyPair MyKeyPair = SodiumPublicKeyAuth.GenerateRevampedKeyPair();
            Byte[] SignedED25519PK = SodiumPublicKeyAuth.Sign(MyKeyPair.PublicKey, MyKeyPair.PrivateKey);
            Byte[] ED25519PK = MyKeyPair.PublicKey;
            Byte[] MergedED25519PKS = ED25519PK.Concat(SignedED25519PK).ToArray();
            ED25519SKTB.Text = Convert.ToBase64String(MyKeyPair.PrivateKey);
            MergedED25519PKSTB.Text = Convert.ToBase64String(MergedED25519PKS);
        }

        private void ConfirmAccessBTN_Click(object sender, EventArgs e)
        {
            if (MergedED25519PKSTB.Text!=null && MergedED25519PKSTB.Text.CompareTo("")!=0
                && ED25519SKTB.Text!=null && ED25519SKTB.Text.CompareTo("")!=0
                && AccessIDTB.Text!=null && AccessIDTB.Text.CompareTo("")!=0
                && OwnerStorageIDTB.Text!=null && OwnerStorageIDTB.Text.CompareTo("")!=0) 
            {
                Byte[] MergedED25519PKS = Convert.FromBase64String(MergedED25519PKSTB.Text);
                Byte[] ED25519SK = Convert.FromBase64String(ED25519SKTB.Text);
                Byte[] ED25519PK = new Byte[32];
                Byte[] SignedED25519PK = new Byte[96];
                String UserID = UserIDTempStorage.UserID;
                String AccessID = AccessIDTB.Text;
                String OwnerStorageID = OwnerStorageIDTB.Text;
                Array.Copy(MergedED25519PKS, 0, ED25519PK, 0, 32);
                Array.Copy(MergedED25519PKS, 32, SignedED25519PK, 0, 96);
                if (Directory.Exists(Application.StartupPath + "\\Application_Data\\User\\" + UserID + "\\Other_Directory_Data") ==false) 
                {
                    Directory.CreateDirectory(Application.StartupPath + "\\Application_Data\\User\\" + UserID + "\\Other_Directory_Data");
                }
                if(Directory.Exists(Application.StartupPath + "\\Application_Data\\User\\" + UserID + "\\Other_Directory_Data\\" + OwnerStorageID) == false) 
                {
                    Directory.CreateDirectory(Application.StartupPath + "\\Application_Data\\User\\" + UserID + "\\Other_Directory_Data\\" + OwnerStorageID);
                }
                File.WriteAllText(Application.StartupPath + "\\Application_Data\\User\\" + UserID + "\\Other_Directory_Data\\" + OwnerStorageID+"\\AccessID.txt",AccessID);
                File.WriteAllBytes(Application.StartupPath + "\\Application_Data\\User\\" + UserID + "\\Other_Directory_Data\\" + OwnerStorageID + "\\SK.txt",ED25519SK);
                File.WriteAllBytes(Application.StartupPath + "\\Application_Data\\User\\" + UserID + "\\Other_Directory_Data\\" + OwnerStorageID + "\\PK.txt", ED25519PK);
                File.WriteAllBytes(Application.StartupPath + "\\Application_Data\\User\\" + UserID + "\\Other_Directory_Data\\" + OwnerStorageID + "\\SPK.txt", SignedED25519PK);
                SodiumSecureMemory.SecureClearString(MergedED25519PKSTB.Text);
                SodiumSecureMemory.SecureClearString(ED25519SKTB.Text);
                SodiumSecureMemory.SecureClearBytes(ED25519SK);
                SodiumSecureMemory.SecureClearBytes(ED25519PK);
                SodiumSecureMemory.SecureClearBytes(SignedED25519PK);
                SodiumSecureMemory.SecureClearBytes(MergedED25519PKS);
                MessageBox.Show("Access has been granted by user's confirmation", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else 
            {
                MessageBox.Show("Please click 'Generate Signature KeyPair' button","Information",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void UserGenerateKey_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Please take note, this is an online usage","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
