
namespace PriSecFileStorageClient
{
    partial class OwnerMFAUpload
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
            this.UploadProgressBar = new System.Windows.Forms.ProgressBar();
            this.UploadServerBTN = new System.Windows.Forms.Button();
            this.RandomFileNameTB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.EncryptionProgressBar = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.AES256GCMRB = new System.Windows.Forms.RadioButton();
            this.XChaCha20Poly1305RB = new System.Windows.Forms.RadioButton();
            this.DefaultRB = new System.Windows.Forms.RadioButton();
            this.FileStorageSizeTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SymmetricEncryptionAlgorithmGB = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.EncryptBTN = new System.Windows.Forms.Button();
            this.FileSizeTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ChooseFileBTN = new System.Windows.Forms.Button();
            this.PlainTextFileChooserDialog = new System.Windows.Forms.OpenFileDialog();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.FirstMFADeviceIDTB = new System.Windows.Forms.TextBox();
            this.SecondMFADeviceIDTB = new System.Windows.Forms.TextBox();
            this.SymmetricEncryptionAlgorithmGB.SuspendLayout();
            this.SuspendLayout();
            // 
            // UploadProgressBar
            // 
            this.UploadProgressBar.Location = new System.Drawing.Point(686, 120);
            this.UploadProgressBar.Name = "UploadProgressBar";
            this.UploadProgressBar.Size = new System.Drawing.Size(178, 35);
            this.UploadProgressBar.TabIndex = 30;
            // 
            // UploadServerBTN
            // 
            this.UploadServerBTN.Location = new System.Drawing.Point(686, 19);
            this.UploadServerBTN.Name = "UploadServerBTN";
            this.UploadServerBTN.Size = new System.Drawing.Size(178, 57);
            this.UploadServerBTN.TabIndex = 29;
            this.UploadServerBTN.Text = "Upload File to Server";
            this.UploadServerBTN.UseVisualStyleBackColor = true;
            this.UploadServerBTN.Click += new System.EventHandler(this.UploadServerBTN_Click);
            // 
            // RandomFileNameTB
            // 
            this.RandomFileNameTB.Location = new System.Drawing.Point(439, 111);
            this.RandomFileNameTB.Multiline = true;
            this.RandomFileNameTB.Name = "RandomFileNameTB";
            this.RandomFileNameTB.ReadOnly = true;
            this.RandomFileNameTB.Size = new System.Drawing.Size(178, 98);
            this.RandomFileNameTB.TabIndex = 28;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(439, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(145, 20);
            this.label6.TabIndex = 27;
            this.label6.Text = "Random File Name";
            // 
            // EncryptionProgressBar
            // 
            this.EncryptionProgressBar.Location = new System.Drawing.Point(439, 41);
            this.EncryptionProgressBar.Name = "EncryptionProgressBar";
            this.EncryptionProgressBar.Size = new System.Drawing.Size(178, 35);
            this.EncryptionProgressBar.TabIndex = 26;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(435, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(151, 20);
            this.label4.TabIndex = 25;
            this.label4.Text = "Encryption Progress";
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
            // FileStorageSizeTB
            // 
            this.FileStorageSizeTB.Location = new System.Drawing.Point(12, 195);
            this.FileStorageSizeTB.Multiline = true;
            this.FileStorageSizeTB.Name = "FileStorageSizeTB";
            this.FileStorageSizeTB.ReadOnly = true;
            this.FileStorageSizeTB.Size = new System.Drawing.Size(330, 78);
            this.FileStorageSizeTB.TabIndex = 35;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(191, 20);
            this.label2.TabIndex = 34;
            this.label2.Text = "This file storage used size";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 291);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(307, 20);
            this.label1.TabIndex = 33;
            this.label1.Text = "Choose A Symmetric Encryption Algorithm";
            // 
            // SymmetricEncryptionAlgorithmGB
            // 
            this.SymmetricEncryptionAlgorithmGB.Controls.Add(this.AES256GCMRB);
            this.SymmetricEncryptionAlgorithmGB.Controls.Add(this.XChaCha20Poly1305RB);
            this.SymmetricEncryptionAlgorithmGB.Controls.Add(this.DefaultRB);
            this.SymmetricEncryptionAlgorithmGB.Location = new System.Drawing.Point(12, 319);
            this.SymmetricEncryptionAlgorithmGB.Name = "SymmetricEncryptionAlgorithmGB";
            this.SymmetricEncryptionAlgorithmGB.Size = new System.Drawing.Size(330, 128);
            this.SymmetricEncryptionAlgorithmGB.TabIndex = 32;
            this.SymmetricEncryptionAlgorithmGB.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(682, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(156, 20);
            this.label5.TabIndex = 31;
            this.label5.Text = "Upload File Progress";
            // 
            // EncryptBTN
            // 
            this.EncryptBTN.Location = new System.Drawing.Point(12, 466);
            this.EncryptBTN.Name = "EncryptBTN";
            this.EncryptBTN.Size = new System.Drawing.Size(330, 54);
            this.EncryptBTN.TabIndex = 24;
            this.EncryptBTN.Text = "Encrypt";
            this.EncryptBTN.UseVisualStyleBackColor = true;
            this.EncryptBTN.Click += new System.EventHandler(this.EncryptBTN_Click);
            // 
            // FileSizeTB
            // 
            this.FileSizeTB.Location = new System.Drawing.Point(12, 88);
            this.FileSizeTB.Multiline = true;
            this.FileSizeTB.Name = "FileSizeTB";
            this.FileSizeTB.ReadOnly = true;
            this.FileSizeTB.Size = new System.Drawing.Size(330, 78);
            this.FileSizeTB.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 20);
            this.label3.TabIndex = 22;
            this.label3.Text = "File Size (bytes)";
            // 
            // ChooseFileBTN
            // 
            this.ChooseFileBTN.Location = new System.Drawing.Point(12, 13);
            this.ChooseFileBTN.Name = "ChooseFileBTN";
            this.ChooseFileBTN.Size = new System.Drawing.Size(330, 47);
            this.ChooseFileBTN.TabIndex = 21;
            this.ChooseFileBTN.Text = "Choose File";
            this.ChooseFileBTN.UseVisualStyleBackColor = true;
            this.ChooseFileBTN.Click += new System.EventHandler(this.ChooseFileBTN_Click);
            // 
            // PlainTextFileChooserDialog
            // 
            this.PlainTextFileChooserDialog.FileName = "PlainTextFileChooserDialog";
            this.PlainTextFileChooserDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.PlainTextFileChooserDialog_FileOk);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(435, 230);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(151, 20);
            this.label7.TabIndex = 36;
            this.label7.Text = "First MFA Device ID";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(435, 363);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(175, 20);
            this.label8.TabIndex = 37;
            this.label8.Text = "Second MFA Device ID";
            // 
            // FirstMFADeviceIDTB
            // 
            this.FirstMFADeviceIDTB.Location = new System.Drawing.Point(439, 253);
            this.FirstMFADeviceIDTB.Multiline = true;
            this.FirstMFADeviceIDTB.Name = "FirstMFADeviceIDTB";
            this.FirstMFADeviceIDTB.Size = new System.Drawing.Size(178, 98);
            this.FirstMFADeviceIDTB.TabIndex = 38;
            // 
            // SecondMFADeviceIDTB
            // 
            this.SecondMFADeviceIDTB.Location = new System.Drawing.Point(439, 396);
            this.SecondMFADeviceIDTB.Multiline = true;
            this.SecondMFADeviceIDTB.Name = "SecondMFADeviceIDTB";
            this.SecondMFADeviceIDTB.Size = new System.Drawing.Size(178, 98);
            this.SecondMFADeviceIDTB.TabIndex = 39;
            // 
            // OwnerMFAUpload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 573);
            this.Controls.Add(this.SecondMFADeviceIDTB);
            this.Controls.Add(this.FirstMFADeviceIDTB);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.UploadProgressBar);
            this.Controls.Add(this.UploadServerBTN);
            this.Controls.Add(this.RandomFileNameTB);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.EncryptionProgressBar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.FileStorageSizeTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SymmetricEncryptionAlgorithmGB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.EncryptBTN);
            this.Controls.Add(this.FileSizeTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ChooseFileBTN);
            this.Name = "OwnerMFAUpload";
            this.Text = "OwnerMFAUpload";
            this.Load += new System.EventHandler(this.OwnerMFAUpload_Load);
            this.SymmetricEncryptionAlgorithmGB.ResumeLayout(false);
            this.SymmetricEncryptionAlgorithmGB.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar UploadProgressBar;
        private System.Windows.Forms.Button UploadServerBTN;
        private System.Windows.Forms.TextBox RandomFileNameTB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ProgressBar EncryptionProgressBar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton AES256GCMRB;
        private System.Windows.Forms.RadioButton XChaCha20Poly1305RB;
        private System.Windows.Forms.RadioButton DefaultRB;
        private System.Windows.Forms.TextBox FileStorageSizeTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox SymmetricEncryptionAlgorithmGB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button EncryptBTN;
        private System.Windows.Forms.TextBox FileSizeTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ChooseFileBTN;
        private System.Windows.Forms.OpenFileDialog PlainTextFileChooserDialog;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox FirstMFADeviceIDTB;
        private System.Windows.Forms.TextBox SecondMFADeviceIDTB;
    }
}