namespace PriSecFileStorageClient
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.UserIDsCB = new System.Windows.Forms.ComboBox();
            this.RequestBTN = new System.Windows.Forms.Button();
            this.SignBTN = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.CurrentUserIDTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ChallengeBase64TB = new System.Windows.Forms.TextBox();
            this.MyGroupBox = new System.Windows.Forms.GroupBox();
            this.MyGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Choose a registered user ID";
            // 
            // UserIDsCB
            // 
            this.UserIDsCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UserIDsCB.FormattingEnabled = true;
            this.UserIDsCB.Location = new System.Drawing.Point(221, 25);
            this.UserIDsCB.Name = "UserIDsCB";
            this.UserIDsCB.Size = new System.Drawing.Size(380, 28);
            this.UserIDsCB.TabIndex = 1;
            this.UserIDsCB.SelectedIndexChanged += new System.EventHandler(this.UserIDsCB_SelectedIndexChanged);
            // 
            // RequestBTN
            // 
            this.RequestBTN.BackColor = System.Drawing.Color.DarkCyan;
            this.RequestBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.RequestBTN.ForeColor = System.Drawing.Color.White;
            this.RequestBTN.Location = new System.Drawing.Point(10, 254);
            this.RequestBTN.Name = "RequestBTN";
            this.RequestBTN.Size = new System.Drawing.Size(237, 69);
            this.RequestBTN.TabIndex = 2;
            this.RequestBTN.Text = "Request Challenge";
            this.RequestBTN.UseVisualStyleBackColor = false;
            this.RequestBTN.Click += new System.EventHandler(this.RequestBTN_Click);
            // 
            // SignBTN
            // 
            this.SignBTN.BackColor = System.Drawing.Color.LimeGreen;
            this.SignBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.SignBTN.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.SignBTN.Location = new System.Drawing.Point(303, 254);
            this.SignBTN.Name = "SignBTN";
            this.SignBTN.Size = new System.Drawing.Size(298, 69);
            this.SignBTN.TabIndex = 3;
            this.SignBTN.Text = "Sign and send challenge";
            this.SignBTN.UseVisualStyleBackColor = false;
            this.SignBTN.Click += new System.EventHandler(this.SignBTN_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Current User ID";
            // 
            // CurrentUserIDTB
            // 
            this.CurrentUserIDTB.Location = new System.Drawing.Point(221, 69);
            this.CurrentUserIDTB.Multiline = true;
            this.CurrentUserIDTB.Name = "CurrentUserIDTB";
            this.CurrentUserIDTB.ReadOnly = true;
            this.CurrentUserIDTB.Size = new System.Drawing.Size(380, 71);
            this.CurrentUserIDTB.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Challenge Base 64";
            // 
            // ChallengeBase64TB
            // 
            this.ChallengeBase64TB.Location = new System.Drawing.Point(221, 163);
            this.ChallengeBase64TB.Multiline = true;
            this.ChallengeBase64TB.Name = "ChallengeBase64TB";
            this.ChallengeBase64TB.ReadOnly = true;
            this.ChallengeBase64TB.Size = new System.Drawing.Size(380, 71);
            this.ChallengeBase64TB.TabIndex = 7;
            // 
            // MyGroupBox
            // 
            this.MyGroupBox.Controls.Add(this.UserIDsCB);
            this.MyGroupBox.Controls.Add(this.SignBTN);
            this.MyGroupBox.Controls.Add(this.ChallengeBase64TB);
            this.MyGroupBox.Controls.Add(this.RequestBTN);
            this.MyGroupBox.Controls.Add(this.label1);
            this.MyGroupBox.Controls.Add(this.label3);
            this.MyGroupBox.Controls.Add(this.CurrentUserIDTB);
            this.MyGroupBox.Controls.Add(this.label2);
            this.MyGroupBox.Location = new System.Drawing.Point(16, 12);
            this.MyGroupBox.Name = "MyGroupBox";
            this.MyGroupBox.Size = new System.Drawing.Size(617, 340);
            this.MyGroupBox.TabIndex = 8;
            this.MyGroupBox.TabStop = false;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.MyGroupBox);
            this.Name = "Login";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.Resize += new System.EventHandler(this.OnReSize);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ShowHomeForm);
            this.MyGroupBox.ResumeLayout(false);
            this.MyGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox UserIDsCB;
        private System.Windows.Forms.Button RequestBTN;
        private System.Windows.Forms.Button SignBTN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox CurrentUserIDTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ChallengeBase64TB;
        private System.Windows.Forms.GroupBox MyGroupBox;
    }
}