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
    public partial class OwnerActionChooser : Form
    {
        public OwnerActionChooser()
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

        private void OwnerMakePaymentBTN_Click(object sender, EventArgs e)
        {
            OwnerMakePayment NewForm = new OwnerMakePayment();
            NewForm.Show();
        }

        private void OwnerRenewPaymentBTN_Click(object sender, EventArgs e)
        {
            OwnerRenewPayment NewForm = new OwnerRenewPayment();
            NewForm.Show();
        }

        private void OwnerStorageOperationsBTN_Click(object sender, EventArgs e)
        {
            MainFormHolder.OpenMainForm = false;
            this.Close();
            OwnerStorageActions NewForm = new OwnerStorageActions();
            NewForm.Show();
        }

        private void OwnerActionChooser_Load(object sender, EventArgs e)
        {
            MainFormHolder.OpenMainForm = true;
        }
    }
}
