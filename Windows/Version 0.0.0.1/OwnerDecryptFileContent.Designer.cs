namespace PriSecFileStorageClient
{
    partial class OwnerDecryptFileContent
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
            this.FileContentCountTB = new System.Windows.Forms.TextBox();
            this.DecryptFetchedFileContentsBTN = new System.Windows.Forms.Button();
            this.GetFileContentCountBTN = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DecryptProgressBar = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.DecryptFolderSelector = new System.Windows.Forms.FolderBrowserDialog();
            this.EncryptedRandomFileNameCB = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SymmetricEncryptionAlgorithmGB = new System.Windows.Forms.GroupBox();
            this.AES256GCMRB = new System.Windows.Forms.RadioButton();
            this.XChaCha20Poly1305RB = new System.Windows.Forms.RadioButton();
            this.DefaultRB = new System.Windows.Forms.RadioButton();
            this.SymmetricEncryptionAlgorithmGB.SuspendLayout();
            this.SuspendLayout();
            // 
            // FileContentCountTB
            // 
            this.FileContentCountTB.Location = new System.Drawing.Point(16, 104);
            this.FileContentCountTB.Name = "FileContentCountTB";
            this.FileContentCountTB.ReadOnly = true;
            this.FileContentCountTB.Size = new System.Drawing.Size(255, 26);
            this.FileContentCountTB.TabIndex = 13;
            // 
            // DecryptFetchedFileContentsBTN
            // 
            this.DecryptFetchedFileContentsBTN.Location = new System.Drawing.Point(321, 177);
            this.DecryptFetchedFileContentsBTN.Name = "DecryptFetchedFileContentsBTN";
            this.DecryptFetchedFileContentsBTN.Size = new System.Drawing.Size(330, 59);
            this.DecryptFetchedFileContentsBTN.TabIndex = 12;
            this.DecryptFetchedFileContentsBTN.Text = "Fetch And Decrypt File Contents";
            this.DecryptFetchedFileContentsBTN.UseVisualStyleBackColor = true;
            this.DecryptFetchedFileContentsBTN.Click += new System.EventHandler(this.DecryptFetchedFileContentsBTN_Click);
            // 
            // GetFileContentCountBTN
            // 
            this.GetFileContentCountBTN.Location = new System.Drawing.Point(16, 147);
            this.GetFileContentCountBTN.Name = "GetFileContentCountBTN";
            this.GetFileContentCountBTN.Size = new System.Drawing.Size(255, 53);
            this.GetFileContentCountBTN.TabIndex = 11;
            this.GetFileContentCountBTN.Text = "Get File Content Count";
            this.GetFileContentCountBTN.UseVisualStyleBackColor = true;
            this.GetFileContentCountBTN.Click += new System.EventHandler(this.GetFileContentCountBTN_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "File Content Count";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Server Random File Name";
            // 
            // DecryptProgressBar
            // 
            this.DecryptProgressBar.Location = new System.Drawing.Point(321, 274);
            this.DecryptProgressBar.Name = "DecryptProgressBar";
            this.DecryptProgressBar.Size = new System.Drawing.Size(330, 36);
            this.DecryptProgressBar.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(317, 251);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 20);
            this.label3.TabIndex = 15;
            this.label3.Text = "Decryption Progress";
            // 
            // EncryptedRandomFileNameCB
            // 
            this.EncryptedRandomFileNameCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EncryptedRandomFileNameCB.FormattingEnabled = true;
            this.EncryptedRandomFileNameCB.Location = new System.Drawing.Point(16, 33);
            this.EncryptedRandomFileNameCB.Name = "EncryptedRandomFileNameCB";
            this.EncryptedRandomFileNameCB.Size = new System.Drawing.Size(255, 28);
            this.EncryptedRandomFileNameCB.TabIndex = 16;
            this.EncryptedRandomFileNameCB.SelectedIndexChanged += new System.EventHandler(this.EncryptedRandomFileNameCB_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(321, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(307, 20);
            this.label4.TabIndex = 17;
            this.label4.Text = "Choose A Symmetric Encryption Algorithm";
            // 
            // SymmetricEncryptionAlgorithmGB
            // 
            this.SymmetricEncryptionAlgorithmGB.Controls.Add(this.AES256GCMRB);
            this.SymmetricEncryptionAlgorithmGB.Controls.Add(this.XChaCha20Poly1305RB);
            this.SymmetricEncryptionAlgorithmGB.Controls.Add(this.DefaultRB);
            this.SymmetricEncryptionAlgorithmGB.Location = new System.Drawing.Point(321, 33);
            this.SymmetricEncryptionAlgorithmGB.Name = "SymmetricEncryptionAlgorithmGB";
            this.SymmetricEncryptionAlgorithmGB.Size = new System.Drawing.Size(330, 128);
            this.SymmetricEncryptionAlgorithmGB.TabIndex = 18;
            this.SymmetricEncryptionAlgorithmGB.TabStop = false;
            // 
            // AES256GCMRB
            // 
            this.AES256GCMRB.AutoSize = true;
            this.AES256GCMRB.Location = new System.Drawing.Point(6, 87);
            this.AES256GCMRB.Name = "AES256GCMRB";
            this.AES256GCMRB.Size = new System.Drawing.Size(302, 24);
            this.AES256GCMRB.TabIndex = 2;
            this.AES256GCMRB.TabStop = true;
            this.AES256GCMRB.Text = "AES256GCM (Few device supports it)";
            this.AES256GCMRB.UseVisualStyleBackColor = true;
            // 
            // XChaCha20Poly1305RB
            // 
            this.XChaCha20Poly1305RB.AutoSize = true;
            this.XChaCha20Poly1305RB.Location = new System.Drawing.Point(7, 56);
            this.XChaCha20Poly1305RB.Name = "XChaCha20Poly1305RB";
            this.XChaCha20Poly1305RB.Size = new System.Drawing.Size(186, 24);
            this.XChaCha20Poly1305RB.TabIndex = 1;
            this.XChaCha20Poly1305RB.TabStop = true;
            this.XChaCha20Poly1305RB.Text = "XChaCha20Poly1305";
            this.XChaCha20Poly1305RB.UseVisualStyleBackColor = true;
            // 
            // DefaultRB
            // 
            this.DefaultRB.AutoSize = true;
            this.DefaultRB.Location = new System.Drawing.Point(6, 25);
            this.DefaultRB.Name = "DefaultRB";
            this.DefaultRB.Size = new System.Drawing.Size(233, 24);
            this.DefaultRB.TabIndex = 0;
            this.DefaultRB.TabStop = true;
            this.DefaultRB.Text = "Default - XSalsa20Poly1305";
            this.DefaultRB.UseVisualStyleBackColor = true;
            // 
            // OwnerDecryptFileContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.SymmetricEncryptionAlgorithmGB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.EncryptedRandomFileNameCB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DecryptProgressBar);
            this.Controls.Add(this.FileContentCountTB);
            this.Controls.Add(this.DecryptFetchedFileContentsBTN);
            this.Controls.Add(this.GetFileContentCountBTN);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "OwnerDecryptFileContent";
            this.Text = "OwnerDecryptFileContent";
            this.Load += new System.EventHandler(this.OwnerDecryptFileContent_Load);
            this.SymmetricEncryptionAlgorithmGB.ResumeLayout(false);
            this.SymmetricEncryptionAlgorithmGB.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox FileContentCountTB;
        private System.Windows.Forms.Button DecryptFetchedFileContentsBTN;
        private System.Windows.Forms.Button GetFileContentCountBTN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar DecryptProgressBar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FolderBrowserDialog DecryptFolderSelector;
        private System.Windows.Forms.ComboBox EncryptedRandomFileNameCB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox SymmetricEncryptionAlgorithmGB;
        private System.Windows.Forms.RadioButton AES256GCMRB;
        private System.Windows.Forms.RadioButton XChaCha20Poly1305RB;
        private System.Windows.Forms.RadioButton DefaultRB;
    }
}