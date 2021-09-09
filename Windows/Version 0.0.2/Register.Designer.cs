namespace PriSecFileStorageClient
{
    partial class Register
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
            this.MyGroupBox = new System.Windows.Forms.GroupBox();
            this.ProceedBTN = new System.Windows.Forms.Button();
            this.SignedCipheredRandomDataTB = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.LoginPKTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.UserIDTB = new System.Windows.Forms.TextBox();
            this.RegisterBTN = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.MyGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // MyGroupBox
            // 
            this.MyGroupBox.Controls.Add(this.ProceedBTN);
            this.MyGroupBox.Controls.Add(this.SignedCipheredRandomDataTB);
            this.MyGroupBox.Controls.Add(this.label8);
            this.MyGroupBox.Controls.Add(this.LoginPKTB);
            this.MyGroupBox.Controls.Add(this.label2);
            this.MyGroupBox.Controls.Add(this.UserIDTB);
            this.MyGroupBox.Controls.Add(this.RegisterBTN);
            this.MyGroupBox.Controls.Add(this.label1);
            this.MyGroupBox.Location = new System.Drawing.Point(13, 13);
            this.MyGroupBox.Name = "MyGroupBox";
            this.MyGroupBox.Size = new System.Drawing.Size(679, 300);
            this.MyGroupBox.TabIndex = 0;
            this.MyGroupBox.TabStop = false;
            // 
            // ProceedBTN
            // 
            this.ProceedBTN.BackColor = System.Drawing.Color.Green;
            this.ProceedBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ProceedBTN.ForeColor = System.Drawing.Color.White;
            this.ProceedBTN.Location = new System.Drawing.Point(276, 193);
            this.ProceedBTN.Name = "ProceedBTN";
            this.ProceedBTN.Size = new System.Drawing.Size(205, 63);
            this.ProceedBTN.TabIndex = 19;
            this.ProceedBTN.Text = "Proceed";
            this.ProceedBTN.UseVisualStyleBackColor = false;
            this.ProceedBTN.Click += new System.EventHandler(this.ProceedBTN_Click);
            // 
            // SignedCipheredRandomDataTB
            // 
            this.SignedCipheredRandomDataTB.Location = new System.Drawing.Point(276, 124);
            this.SignedCipheredRandomDataTB.Name = "SignedCipheredRandomDataTB";
            this.SignedCipheredRandomDataTB.ReadOnly = true;
            this.SignedCipheredRandomDataTB.Size = new System.Drawing.Size(321, 26);
            this.SignedCipheredRandomDataTB.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 127);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(231, 20);
            this.label8.TabIndex = 17;
            this.label8.Text = "Signed Ciphered Random Data";
            // 
            // LoginPKTB
            // 
            this.LoginPKTB.Location = new System.Drawing.Point(276, 70);
            this.LoginPKTB.Name = "LoginPKTB";
            this.LoginPKTB.ReadOnly = true;
            this.LoginPKTB.Size = new System.Drawing.Size(321, 26);
            this.LoginPKTB.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Login ED25519 PK";
            // 
            // UserIDTB
            // 
            this.UserIDTB.Location = new System.Drawing.Point(276, 25);
            this.UserIDTB.Name = "UserIDTB";
            this.UserIDTB.ReadOnly = true;
            this.UserIDTB.Size = new System.Drawing.Size(321, 26);
            this.UserIDTB.TabIndex = 2;
            // 
            // RegisterBTN
            // 
            this.RegisterBTN.BackColor = System.Drawing.Color.Green;
            this.RegisterBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.RegisterBTN.ForeColor = System.Drawing.Color.White;
            this.RegisterBTN.Location = new System.Drawing.Point(10, 193);
            this.RegisterBTN.Name = "RegisterBTN";
            this.RegisterBTN.Size = new System.Drawing.Size(187, 63);
            this.RegisterBTN.TabIndex = 1;
            this.RegisterBTN.Text = "Register";
            this.RegisterBTN.UseVisualStyleBackColor = false;
            this.RegisterBTN.Click += new System.EventHandler(this.RegisterBTN_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Your User ID";
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 485);
            this.Controls.Add(this.MyGroupBox);
            this.Name = "Register";
            this.Text = "Register";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ShowHomeForm);
            this.Load += new System.EventHandler(this.Register_Load);
            this.Resize += new System.EventHandler(this.OnReSize);
            this.MyGroupBox.ResumeLayout(false);
            this.MyGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox MyGroupBox;
        private System.Windows.Forms.Button RegisterBTN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox LoginPKTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox UserIDTB;
        private System.Windows.Forms.TextBox SignedCipheredRandomDataTB;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button ProceedBTN;
    }
}