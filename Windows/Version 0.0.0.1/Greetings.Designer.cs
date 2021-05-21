namespace PriSecFileStorageClient
{
    partial class Greetings
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
            this.ETLSSessionStatusTB = new System.Windows.Forms.TextBox();
            this.ReadMeBTN = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ProceedBTN = new System.Windows.Forms.Button();
            this.ETLSSessionErrorMessageTB = new System.Windows.Forms.TextBox();
            this.RetryBTN = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SharedSecretCheckStatusTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.MyGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // MyGroupBox
            // 
            this.MyGroupBox.Controls.Add(this.ETLSSessionStatusTB);
            this.MyGroupBox.Controls.Add(this.ReadMeBTN);
            this.MyGroupBox.Controls.Add(this.label1);
            this.MyGroupBox.Controls.Add(this.ProceedBTN);
            this.MyGroupBox.Controls.Add(this.ETLSSessionErrorMessageTB);
            this.MyGroupBox.Controls.Add(this.RetryBTN);
            this.MyGroupBox.Controls.Add(this.label3);
            this.MyGroupBox.Controls.Add(this.SharedSecretCheckStatusTB);
            this.MyGroupBox.Controls.Add(this.label2);
            this.MyGroupBox.Location = new System.Drawing.Point(14, 53);
            this.MyGroupBox.Name = "MyGroupBox";
            this.MyGroupBox.Size = new System.Drawing.Size(772, 345);
            this.MyGroupBox.TabIndex = 10;
            this.MyGroupBox.TabStop = false;
            // 
            // ETLSSessionStatusTB
            // 
            this.ETLSSessionStatusTB.Location = new System.Drawing.Point(283, 25);
            this.ETLSSessionStatusTB.Multiline = true;
            this.ETLSSessionStatusTB.Name = "ETLSSessionStatusTB";
            this.ETLSSessionStatusTB.ReadOnly = true;
            this.ETLSSessionStatusTB.Size = new System.Drawing.Size(263, 58);
            this.ETLSSessionStatusTB.TabIndex = 1;
            // 
            // ReadMeBTN
            // 
            this.ReadMeBTN.BackColor = System.Drawing.Color.Gold;
            this.ReadMeBTN.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.ReadMeBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ReadMeBTN.Location = new System.Drawing.Point(599, 208);
            this.ReadMeBTN.Name = "ReadMeBTN";
            this.ReadMeBTN.Size = new System.Drawing.Size(149, 63);
            this.ReadMeBTN.TabIndex = 8;
            this.ReadMeBTN.Text = "Read Me";
            this.ReadMeBTN.UseVisualStyleBackColor = false;
            this.ReadMeBTN.Click += new System.EventHandler(this.ReadMeBTN_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ephemeral TLS Session Status";
            // 
            // ProceedBTN
            // 
            this.ProceedBTN.BackColor = System.Drawing.Color.Green;
            this.ProceedBTN.Enabled = false;
            this.ProceedBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ProceedBTN.ForeColor = System.Drawing.Color.White;
            this.ProceedBTN.Location = new System.Drawing.Point(599, 118);
            this.ProceedBTN.Name = "ProceedBTN";
            this.ProceedBTN.Size = new System.Drawing.Size(149, 63);
            this.ProceedBTN.TabIndex = 7;
            this.ProceedBTN.Text = "Proceed";
            this.ProceedBTN.UseVisualStyleBackColor = false;
            this.ProceedBTN.Click += new System.EventHandler(this.ProceedBTN_Click);
            // 
            // ETLSSessionErrorMessageTB
            // 
            this.ETLSSessionErrorMessageTB.Location = new System.Drawing.Point(283, 120);
            this.ETLSSessionErrorMessageTB.Multiline = true;
            this.ETLSSessionErrorMessageTB.Name = "ETLSSessionErrorMessageTB";
            this.ETLSSessionErrorMessageTB.ReadOnly = true;
            this.ETLSSessionErrorMessageTB.Size = new System.Drawing.Size(263, 61);
            this.ETLSSessionErrorMessageTB.TabIndex = 5;
            // 
            // RetryBTN
            // 
            this.RetryBTN.BackColor = System.Drawing.Color.MediumTurquoise;
            this.RetryBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.RetryBTN.ForeColor = System.Drawing.Color.White;
            this.RetryBTN.Location = new System.Drawing.Point(599, 25);
            this.RetryBTN.Name = "RetryBTN";
            this.RetryBTN.Size = new System.Drawing.Size(149, 63);
            this.RetryBTN.TabIndex = 6;
            this.RetryBTN.Text = "Retry";
            this.RetryBTN.UseVisualStyleBackColor = false;
            this.RetryBTN.Click += new System.EventHandler(this.RetryBTN_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(218, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "ETLS Session Error Message";
            // 
            // SharedSecretCheckStatusTB
            // 
            this.SharedSecretCheckStatusTB.Location = new System.Drawing.Point(283, 208);
            this.SharedSecretCheckStatusTB.Multiline = true;
            this.SharedSecretCheckStatusTB.Name = "SharedSecretCheckStatusTB";
            this.SharedSecretCheckStatusTB.ReadOnly = true;
            this.SharedSecretCheckStatusTB.Size = new System.Drawing.Size(263, 63);
            this.SharedSecretCheckStatusTB.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 208);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(229, 40);
            this.label2.TabIndex = 2;
            this.label2.Text = "Is shared secret between client\r\nand server same";
            // 
            // Greetings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.MyGroupBox);
            this.Name = "Greetings";
            this.Text = "Greetings";
            this.Load += new System.EventHandler(this.Greetings_Load);
            this.Resize += new System.EventHandler(this.OnResizeFunction);
            this.MyGroupBox.ResumeLayout(false);
            this.MyGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox MyGroupBox;
        private System.Windows.Forms.TextBox ETLSSessionStatusTB;
        private System.Windows.Forms.Button ReadMeBTN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ProceedBTN;
        private System.Windows.Forms.TextBox ETLSSessionErrorMessageTB;
        private System.Windows.Forms.Button RetryBTN;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox SharedSecretCheckStatusTB;
        private System.Windows.Forms.Label label2;
    }
}

