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
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using Newtonsoft.Json;
using PriSecFileStorageClient.Helper;
using PriSecFileStorageClient.Model;

namespace PriSecFileStorageClient
{
    public partial class ActionChooser : Form
    {
        public ActionChooser()
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

        private void YesBTN_Click(object sender, EventArgs e)
        {
            MainFormHolder.OpenMainForm = false;
            this.Close();
            OwnerActionChooser NewForm = new OwnerActionChooser();
            NewForm.Show();
        }

        private void NoBTN_Click(object sender, EventArgs e)
        {
            MainFormHolder.OpenMainForm = false;
            this.Close();
            UserActionChooser NewForm = new UserActionChooser();
            NewForm.Show();
        }

        private void ActionChooser_Load(object sender, EventArgs e)
        {
            MainFormHolder.OpenMainForm = true;
        }
    }
}
