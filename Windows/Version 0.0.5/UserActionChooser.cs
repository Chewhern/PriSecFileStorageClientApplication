using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PriSecFileStorageClient.Helper;

namespace PriSecFileStorageClient
{
    public partial class UserActionChooser : Form
    {
        public UserActionChooser()
        {
            InitializeComponent();
        }

        private void OnClosing(object sender, EventArgs e)
        {
            if (MainFormHolder.OpenMainForm == true)
            {
                MainFormHolder.myForm.Show();
                MainFormHolder.OpenMainForm = false;
            }
        }

        private void GenerateKeyBTN_Click(object sender, EventArgs e)
        {
            UserGenerateKey NewForm = new UserGenerateKey();
            NewForm.Show();
        }

        private void UserStorageOperationsBTN_Click(object sender, EventArgs e)
        {
            MainFormHolder.OpenMainForm = false;
            this.Close();
            UserStorageActions NewForm = new UserStorageActions();
            NewForm.Show();
        }

        private void UserActionChooser_Load(object sender, EventArgs e)
        {
            MainFormHolder.OpenMainForm = true;
        }
    }
}
