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
    public partial class OwnerMFAStorageActions : Form
    {
        public OwnerMFAStorageActions()
        {
            InitializeComponent();
        }

        private void OnClosing(object sender, EventArgs e)
        {
            MainFormHolder.myForm.Show();
            MainFormHolder.OpenMainForm = false;
        }

        private void OwnerMFAStorageActions_Load(object sender, EventArgs e)
        {
            if (Directory.Exists(Application.StartupPath + "\\Application_Data\\Server_Directory_Data\\") == true)
            {
                ReloadDirectoryIDArray();
            }
        }

        private void ActionCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActionCB.SelectedIndex != -1)
            {
                ChoosenActionTB.Text = ActionCB.Text;
            }
        }

        private void GoBTN_Click(object sender, EventArgs e)
        {
            if (DirectoryIDComboBox.Text != null && DirectoryIDComboBox.Text.CompareTo("") != 0)
            {
                if (ActionCB.SelectedIndex != -1)
                {
                    DirectoryIDTempStorage.DirectoryID = DirectoryIDComboBox.Text;
                    if (ActionCB.SelectedIndex == 0) 
                    {
                        var NewForm = new OwnerGenerateMFADevice();
                        NewForm.Show();
                    }
                    else if (ActionCB.SelectedIndex == 1)
                    {
                        var NewForm = new OwnerAddMFADevice();
                        NewForm.Show();
                    }
                    else if (ActionCB.SelectedIndex == 2)
                    {
                        var NewForm = new OwnerViewMFADevice();
                        NewForm.Show();
                    }
                    else if (ActionCB.SelectedIndex == 3)
                    {
                        var NewForm = new OwnerDeleteMFADevice();
                        NewForm.Show();
                    }
                    else if (ActionCB.SelectedIndex == 4)
                    {
                        var NewForm = new OwnerRemoveMFADeviceData();
                        NewForm.Show();
                    }
                    else if (ActionCB.SelectedIndex == 5)
                    {
                        var NewForm = new OwnerMFALogin();
                        NewForm.Show();
                    }
                    else if (ActionCB.SelectedIndex == 6)
                    {
                        var NewForm = new OwnerMFAUpload();
                        NewForm.Show();
                    }
                    else if (ActionCB.SelectedIndex == 7)
                    {
                        var NewForm = new OwnerMFADeleteFileContent();
                        NewForm.Show();
                    }
                    else if (ActionCB.SelectedIndex == 8)
                    {
                        var NewForm = new OwnerMFAAddUser();
                        NewForm.Show();
                    }
                    else if (ActionCB.SelectedIndex == 9)
                    {
                        var NewForm = new OwnerMFARemoveUser();
                        NewForm.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Please choose an action", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please choose a directory ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ReloadDirectoryIDArray()
        {
            DirectoryIDComboBox.Items.Clear();
            String[] DirectoryIDFullPathArray = Directory.GetDirectories(Application.StartupPath + "\\Application_Data\\Server_Directory_Data\\");
            String[] DirectoryIDArray = new string[DirectoryIDFullPathArray.Length];
            int Count = 0;
            int RootDirectoryCount = 0;
            RootDirectoryCount = (Application.StartupPath + "\\Application_Data\\Server_Directory_Data\\").Length;
            while (Count < DirectoryIDFullPathArray.Length)
            {
                DirectoryIDArray[Count] = DirectoryIDFullPathArray[Count].Remove(0, RootDirectoryCount);
                Count += 1;
            }
            DirectoryIDComboBox.Items.AddRange(DirectoryIDArray);
        }
    }
}
