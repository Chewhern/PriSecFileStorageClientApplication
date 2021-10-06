namespace PriSecFileStorageClient
{
    partial class OwnerUploadFile
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
            this.ChooseFileBTN = new System.Windows.Forms.Button();
            this.PlainTextFileChooserDialog = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.FileSizeTB = new System.Windows.Forms.TextBox();
            this.EncryptBTN = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.EncryptionProgressBar = new System.Windows.Forms.ProgressBar();
            this.label6 = new System.Windows.Forms.Label();
            this.RandomFileNameTB = new System.Windows.Forms.TextBox();
            this.UploadServerBTN = new System.Windows.Forms.Button();
            this.UploadProgressBar = new System.Windows.Forms.ProgressBar();
            this.label5 = new System.Windows.Forms.Label();
            this.SymmetricEncryptionAlgorithmGB = new System.Windows.Forms.GroupBox();
            this.AES256GCMRB = new System.Windows.Forms.RadioButton();
            this.XChaCha20Poly1305RB = new System.Windows.Forms.RadioButton();
            this.DefaultRB = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.FileStorageSizeTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SymmetricEncryptionAlgorithmGB.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChooseFileBTN
            // 
            this.ChooseFileBTN.Location = new System.Drawing.Point(19, 16);
            this.ChooseFileBTN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ChooseFileBTN.Name = "ChooseFileBTN";
            this.ChooseFileBTN.Size = new System.Drawing.Size(367, 59);
            this.ChooseFileBTN.TabIndex = 2;
            this.ChooseFileBTN.Text = "Choose File";
            this.ChooseFileBTN.UseVisualStyleBackColor = true;
            this.ChooseFileBTN.Click += new System.EventHandler(this.ChooseFileBTN_Click);
            // 
            // PlainTextFileChooserDialog
            // 
            this.PlainTextFileChooserDialog.FileName = "PlainTextFileChooserDialog";
            this.PlainTextFileChooserDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.PlainTextFileChooserDialog_FileOk);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "File Size (bytes)";
            // 
            // FileSizeTB
            // 
            this.FileSizeTB.Location = new System.Drawing.Point(19, 110);
            this.FileSizeTB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.FileSizeTB.Multiline = true;
            this.FileSizeTB.Name = "FileSizeTB";
            this.FileSizeTB.ReadOnly = true;
            this.FileSizeTB.Size = new System.Drawing.Size(366, 96);
            this.FileSizeTB.TabIndex = 6;
            // 
            // EncryptBTN
            // 
            this.EncryptBTN.Location = new System.Drawing.Point(19, 615);
            this.EncryptBTN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.EncryptBTN.Name = "EncryptBTN";
            this.EncryptBTN.Size = new System.Drawing.Size(367, 68);
            this.EncryptBTN.TabIndex = 7;
            this.EncryptBTN.Text = "Encrypt";
            this.EncryptBTN.UseVisualStyleBackColor = true;
            this.EncryptBTN.Click += new System.EventHandler(this.EncryptBTN_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(489, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(170, 25);
            this.label4.TabIndex = 8;
            this.label4.Text = "Encryption Progress";
            // 
            // EncryptionProgressBar
            // 
            this.EncryptionProgressBar.Location = new System.Drawing.Point(493, 51);
            this.EncryptionProgressBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.EncryptionProgressBar.Name = "EncryptionProgressBar";
            this.EncryptionProgressBar.Size = new System.Drawing.Size(198, 44);
            this.EncryptionProgressBar.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(493, 110);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(163, 25);
            this.label6.TabIndex = 12;
            this.label6.Text = "Random File Name";
            // 
            // RandomFileNameTB
            // 
            this.RandomFileNameTB.Location = new System.Drawing.Point(493, 139);
            this.RandomFileNameTB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.RandomFileNameTB.Multiline = true;
            this.RandomFileNameTB.Name = "RandomFileNameTB";
            this.RandomFileNameTB.ReadOnly = true;
            this.RandomFileNameTB.Size = new System.Drawing.Size(197, 122);
            this.RandomFileNameTB.TabIndex = 13;
            // 
            // UploadServerBTN
            // 
            this.UploadServerBTN.Location = new System.Drawing.Point(493, 270);
            this.UploadServerBTN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.UploadServerBTN.Name = "UploadServerBTN";
            this.UploadServerBTN.Size = new System.Drawing.Size(198, 71);
            this.UploadServerBTN.TabIndex = 14;
            this.UploadServerBTN.Text = "Upload File to Server";
            this.UploadServerBTN.UseVisualStyleBackColor = true;
            this.UploadServerBTN.Click += new System.EventHandler(this.UploadServerBTN_Click);
            // 
            // UploadProgressBar
            // 
            this.UploadProgressBar.Location = new System.Drawing.Point(493, 396);
            this.UploadProgressBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.UploadProgressBar.Name = "UploadProgressBar";
            this.UploadProgressBar.Size = new System.Drawing.Size(198, 44);
            this.UploadProgressBar.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(489, 364);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(175, 25);
            this.label5.TabIndex = 16;
            this.label5.Text = "Upload File Progress";
            // 
            // SymmetricEncryptionAlgorithmGB
            // 
            this.SymmetricEncryptionAlgorithmGB.Controls.Add(this.AES256GCMRB);
            this.SymmetricEncryptionAlgorithmGB.Controls.Add(this.XChaCha20Poly1305RB);
            this.SymmetricEncryptionAlgorithmGB.Controls.Add(this.DefaultRB);
            this.SymmetricEncryptionAlgorithmGB.Location = new System.Drawing.Point(19, 431);
            this.SymmetricEncryptionAlgorithmGB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SymmetricEncryptionAlgorithmGB.Name = "SymmetricEncryptionAlgorithmGB";
            this.SymmetricEncryptionAlgorithmGB.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SymmetricEncryptionAlgorithmGB.Size = new System.Drawing.Size(367, 160);
            this.SymmetricEncryptionAlgorithmGB.TabIndex = 17;
            this.SymmetricEncryptionAlgorithmGB.TabStop = false;
            // 
            // AES256GCMRB
            // 
            this.AES256GCMRB.AutoSize = true;
            this.AES256GCMRB.Location = new System.Drawing.Point(7, 109);
            this.AES256GCMRB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AES256GCMRB.Name = "AES256GCMRB";
            this.AES256GCMRB.Size = new System.Drawing.Size(329, 29);
            this.AES256GCMRB.TabIndex = 2;
            this.AES256GCMRB.TabStop = true;
            this.AES256GCMRB.Text = "AES256GCM (Few device supports it)";
            this.AES256GCMRB.UseVisualStyleBackColor = true;
            // 
            // XChaCha20Poly1305RB
            // 
            this.XChaCha20Poly1305RB.AutoSize = true;
            this.XChaCha20Poly1305RB.Location = new System.Drawing.Point(8, 70);
            this.XChaCha20Poly1305RB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.XChaCha20Poly1305RB.Name = "XChaCha20Poly1305RB";
            this.XChaCha20Poly1305RB.Size = new System.Drawing.Size(201, 29);
            this.XChaCha20Poly1305RB.TabIndex = 1;
            this.XChaCha20Poly1305RB.TabStop = true;
            this.XChaCha20Poly1305RB.Text = "XChaCha20Poly1305";
            this.XChaCha20Poly1305RB.UseVisualStyleBackColor = true;
            // 
            // DefaultRB
            // 
            this.DefaultRB.AutoSize = true;
            this.DefaultRB.Location = new System.Drawing.Point(7, 31);
            this.DefaultRB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DefaultRB.Name = "DefaultRB";
            this.DefaultRB.Size = new System.Drawing.Size(255, 29);
            this.DefaultRB.TabIndex = 0;
            this.DefaultRB.TabStop = true;
            this.DefaultRB.Text = "Default - XSalsa20Poly1305";
            this.DefaultRB.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 396);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(351, 25);
            this.label1.TabIndex = 18;
            this.label1.Text = "Choose A Symmetric Encryption Algorithm";
            // 
            // FileStorageSizeTB
            // 
            this.FileStorageSizeTB.Location = new System.Drawing.Point(19, 244);
            this.FileStorageSizeTB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.FileStorageSizeTB.Multiline = true;
            this.FileStorageSizeTB.Name = "FileStorageSizeTB";
            this.FileStorageSizeTB.ReadOnly = true;
            this.FileStorageSizeTB.Size = new System.Drawing.Size(366, 96);
            this.FileStorageSizeTB.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 215);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(213, 25);
            this.label2.TabIndex = 19;
            this.label2.Text = "This file storage used size";
            // 
            // OwnerUploadFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 729);
            this.Controls.Add(this.FileStorageSizeTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SymmetricEncryptionAlgorithmGB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.UploadProgressBar);
            this.Controls.Add(this.UploadServerBTN);
            this.Controls.Add(this.RandomFileNameTB);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.EncryptionProgressBar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.EncryptBTN);
            this.Controls.Add(this.FileSizeTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ChooseFileBTN);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "OwnerUploadFile";
            this.Text = "OwnerUploadFile";
            this.SymmetricEncryptionAlgorithmGB.ResumeLayout(false);
            this.SymmetricEncryptionAlgorithmGB.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button ChooseFileBTN;
        private System.Windows.Forms.OpenFileDialog PlainTextFileChooserDialog;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox FileSizeTB;
        private System.Windows.Forms.Button EncryptBTN;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar EncryptionProgressBar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox RandomFileNameTB;
        private System.Windows.Forms.Button UploadServerBTN;
        private System.Windows.Forms.ProgressBar UploadProgressBar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox SymmetricEncryptionAlgorithmGB;
        private System.Windows.Forms.RadioButton AES256GCMRB;
        private System.Windows.Forms.RadioButton XChaCha20Poly1305RB;
        private System.Windows.Forms.RadioButton DefaultRB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox FileStorageSizeTB;
        private System.Windows.Forms.Label label2;
    }
}