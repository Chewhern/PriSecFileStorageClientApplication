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
            this.label2 = new System.Windows.Forms.Label();
            this.FileNameWithPathTB = new System.Windows.Forms.TextBox();
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
            this.MyBackGroundWorker = new System.ComponentModel.BackgroundWorker();
            this.UploadBackGroundWorker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // ChooseFileBTN
            // 
            this.ChooseFileBTN.Location = new System.Drawing.Point(17, 13);
            this.ChooseFileBTN.Name = "ChooseFileBTN";
            this.ChooseFileBTN.Size = new System.Drawing.Size(210, 47);
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "File name with path";
            // 
            // FileNameWithPathTB
            // 
            this.FileNameWithPathTB.Location = new System.Drawing.Point(17, 95);
            this.FileNameWithPathTB.Multiline = true;
            this.FileNameWithPathTB.Name = "FileNameWithPathTB";
            this.FileNameWithPathTB.ReadOnly = true;
            this.FileNameWithPathTB.Size = new System.Drawing.Size(206, 74);
            this.FileNameWithPathTB.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 189);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "File Size";
            // 
            // FileSizeTB
            // 
            this.FileSizeTB.Location = new System.Drawing.Point(17, 212);
            this.FileSizeTB.Multiline = true;
            this.FileSizeTB.Name = "FileSizeTB";
            this.FileSizeTB.ReadOnly = true;
            this.FileSizeTB.Size = new System.Drawing.Size(202, 78);
            this.FileSizeTB.TabIndex = 6;
            // 
            // EncryptBTN
            // 
            this.EncryptBTN.Location = new System.Drawing.Point(17, 307);
            this.EncryptBTN.Name = "EncryptBTN";
            this.EncryptBTN.Size = new System.Drawing.Size(202, 54);
            this.EncryptBTN.TabIndex = 7;
            this.EncryptBTN.Text = "Encrypt";
            this.EncryptBTN.UseVisualStyleBackColor = true;
            this.EncryptBTN.Click += new System.EventHandler(this.EncryptBTN_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(274, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(151, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Encryption Progress";
            // 
            // EncryptionProgressBar
            // 
            this.EncryptionProgressBar.Location = new System.Drawing.Point(278, 37);
            this.EncryptionProgressBar.Name = "EncryptionProgressBar";
            this.EncryptionProgressBar.Size = new System.Drawing.Size(178, 35);
            this.EncryptionProgressBar.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(278, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(145, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "Random File Name";
            // 
            // RandomFileNameTB
            // 
            this.RandomFileNameTB.Location = new System.Drawing.Point(278, 107);
            this.RandomFileNameTB.Multiline = true;
            this.RandomFileNameTB.Name = "RandomFileNameTB";
            this.RandomFileNameTB.ReadOnly = true;
            this.RandomFileNameTB.Size = new System.Drawing.Size(178, 98);
            this.RandomFileNameTB.TabIndex = 13;
            // 
            // UploadServerBTN
            // 
            this.UploadServerBTN.Location = new System.Drawing.Point(278, 212);
            this.UploadServerBTN.Name = "UploadServerBTN";
            this.UploadServerBTN.Size = new System.Drawing.Size(178, 57);
            this.UploadServerBTN.TabIndex = 14;
            this.UploadServerBTN.Text = "Upload File to Server";
            this.UploadServerBTN.UseVisualStyleBackColor = true;
            this.UploadServerBTN.Click += new System.EventHandler(this.UploadServerBTN_Click);
            // 
            // UploadProgressBar
            // 
            this.UploadProgressBar.Location = new System.Drawing.Point(274, 313);
            this.UploadProgressBar.Name = "UploadProgressBar";
            this.UploadProgressBar.Size = new System.Drawing.Size(178, 35);
            this.UploadProgressBar.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(274, 287);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(156, 20);
            this.label5.TabIndex = 16;
            this.label5.Text = "Upload File Progress";
            // 
            // OwnerUploadFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 501);
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
            this.Controls.Add(this.FileNameWithPathTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ChooseFileBTN);
            this.Name = "OwnerUploadFile";
            this.Text = "OwnerUploadFile";
            this.Load += new System.EventHandler(this.OwnerUploadFile_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button ChooseFileBTN;
        private System.Windows.Forms.OpenFileDialog PlainTextFileChooserDialog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox FileNameWithPathTB;
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
        private System.ComponentModel.BackgroundWorker MyBackGroundWorker;
        private System.ComponentModel.BackgroundWorker UploadBackGroundWorker;
    }
}