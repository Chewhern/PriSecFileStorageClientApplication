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
using PriSecFileStorageClient.Helper;

namespace PriSecFileStorageClient
{
    public partial class OwnerRemoveMFADeviceData : Form
    {
        public OwnerRemoveMFADeviceData()
        {
            InitializeComponent();
        }

        private void OwnerRemoveMFADeviceData_Load(object sender, EventArgs e)
        {
            ReloadCurrentMFADeviceIDArray();
        }

        private void DeviceMFADeviceCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DeviceMFADeviceCB.SelectedIndex != -1)
            {
                String Remark = File.ReadAllText(Application.StartupPath + "\\Application_Data\\Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\MFA_Device\\" + DeviceMFADeviceCB.Text + "\\Note.txt");
                RemarksTB.Text = Remark;
                DeviceMFADeviceIDTB.Text = DeviceMFADeviceCB.Text;
            }
        }

        private void DeleteMFADeviceDataBTN_Click(object sender, EventArgs e)
        {
            if (DeviceMFADeviceCB.SelectedIndex != -1)
            {
                if (Directory.Exists(Application.StartupPath + "\\Application_Data\\Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\MFA_Device\\" + DeviceMFADeviceCB.Text) == true)
                {
                    Directory.Delete(Application.StartupPath + "\\Application_Data\\Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\MFA_Device\\" + DeviceMFADeviceCB.Text,true);
                    MessageBox.Show("The MFA device data has now been deleted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public void ReloadCurrentMFADeviceIDArray()
        {
            DeviceMFADeviceCB.Items.Clear();
            String[] MFADeviceIDFullPathArray = Directory.GetDirectories(Application.StartupPath + "\\Application_Data\\Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\MFA_Device");
            String[] MFADeviceIDArray = new string[MFADeviceIDFullPathArray.Length];
            int Count = 0;
            int RootDirectoryCount = 0;
            RootDirectoryCount = (Application.StartupPath + "\\Application_Data\\Server_Directory_Data\\" + DirectoryIDTempStorage.DirectoryID + "\\MFA_Device\\").Length;
            while (Count < MFADeviceIDFullPathArray.Length)
            {
                MFADeviceIDArray[Count] = MFADeviceIDFullPathArray[Count].Remove(0, RootDirectoryCount);
                Count += 1;
            }
            DeviceMFADeviceCB.Items.AddRange(MFADeviceIDArray);
        }
    }
}
