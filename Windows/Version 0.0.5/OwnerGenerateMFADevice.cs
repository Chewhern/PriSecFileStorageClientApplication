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
using PriSecFileStorageClient.Helper;

namespace PriSecFileStorageClient
{
    public partial class OwnerGenerateMFADevice : Form
    {
        public OwnerGenerateMFADevice()
        {
            InitializeComponent();
        }

        private void GenerateDataBTN_Click(object sender, EventArgs e)
        {
            SecureIDGenerator MySecureIDGenerator = new SecureIDGenerator();
            String MFADeviceID = MySecureIDGenerator.GenerateUniqueString();
            RevampedKeyPair MyKeyPair = SodiumPublicKeyAuth.GenerateRevampedKeyPair();
            Byte[] ED25519PK = new Byte[] { };
            Byte[] SignedED25519PK = new Byte[] { };
            Byte[] MergedED25519PK = new Byte[] { };
            if (MFADeviceID.Length > 24) 
            {
                MFADeviceID = MFADeviceID.Substring(0, 24);
            }
            ED25519PK = MyKeyPair.PublicKey;
            SignedED25519PK = SodiumPublicKeyAuth.Sign(ED25519PK, MyKeyPair.PrivateKey);
            MergedED25519PK = ED25519PK.Concat(SignedED25519PK).ToArray();
            MFADeviceIDTB.Text = MFADeviceID;
            MergedED25519PKTB.Text = Convert.ToBase64String(MergedED25519PK);
            ED25519SKTB.Text = Convert.ToBase64String(MyKeyPair.PrivateKey);
            MyKeyPair.Clear();
        }

        private void AddMFADeviceBTN_Click(object sender, EventArgs e)
        {
            if(MFADeviceIDTB.Text.CompareTo("")!=0 && ED25519SKTB.Text.CompareTo("") != 0) 
            {
                String MFADeviceID = MFADeviceIDTB.Text;
                String ED25519SKB64 = ED25519SKTB.Text;
                Byte[] ED25519SK = new Byte[] { };
                Byte[] ED25519PK = new Byte[] { };
                ED25519SK = Convert.FromBase64String(ED25519SKB64);
                ED25519PK = SodiumPublicKeyAuth.GeneratePublicKey(ED25519SK);
                if (Directory.Exists(Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\MFA_Device") == false)
                {
                    Directory.CreateDirectory(Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\MFA_Device");
                }
                if (Directory.Exists(Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\MFA_Device\\" + MFADeviceID) == false)
                {
                    Directory.CreateDirectory(Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\MFA_Device\\" + MFADeviceID);
                }
                File.WriteAllBytes(Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\MFA_Device\\" + MFADeviceID + "\\SK.txt", ED25519SK);
                File.WriteAllBytes(Application.StartupPath + "\\Application_Data\\" + "Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\MFA_Device\\" + MFADeviceID + "\\PK.txt", ED25519PK);
                MessageBox.Show("The MFA device data has now been stored on this device", "MFA Device Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else 
            {
                MessageBox.Show("You need to generate MFA device data before you can use this button", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
