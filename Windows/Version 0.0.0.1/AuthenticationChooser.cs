using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PriSecFileStorageClient
{
    public partial class AuthenticationChooser : Form
    {
        private bool mouseDown;
        private Point lastLocation;

        public AuthenticationChooser()
        {
            InitializeComponent();
        }

        private void RegisterBTN_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Close();
        }

        private void LoginBTN_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
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
            MainFormHolder.myForm.Show();
            MainFormHolder.OpenMainForm = false;
            this.Close();
        }

        private void AccountRecoveryBTN_Click(object sender, EventArgs e)
        {
            AccountRecovery accountRecovery = new AccountRecovery();
            accountRecovery.Show();
            this.Close();
        }
    }
}
