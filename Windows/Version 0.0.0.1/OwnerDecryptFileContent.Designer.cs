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
            this.ServerRandomFileNameTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DecryptProgressBar = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // FileContentCountTB
            // 
            this.FileContentCountTB.Location = new System.Drawing.Point(16, 148);
            this.FileContentCountTB.Name = "FileContentCountTB";
            this.FileContentCountTB.ReadOnly = true;
            this.FileContentCountTB.Size = new System.Drawing.Size(255, 26);
            this.FileContentCountTB.TabIndex = 13;
            // 
            // DecryptFetchedFileContentsBTN
            // 
            this.DecryptFetchedFileContentsBTN.Location = new System.Drawing.Point(16, 251);
            this.DecryptFetchedFileContentsBTN.Name = "DecryptFetchedFileContentsBTN";
            this.DecryptFetchedFileContentsBTN.Size = new System.Drawing.Size(255, 59);
            this.DecryptFetchedFileContentsBTN.TabIndex = 12;
            this.DecryptFetchedFileContentsBTN.Text = "Fetch And Decrypt File Contents";
            this.DecryptFetchedFileContentsBTN.UseVisualStyleBackColor = true;
            this.DecryptFetchedFileContentsBTN.Click += new System.EventHandler(this.DecryptFetchedFileContentsBTN_Click);
            // 
            // GetFileContentCountBTN
            // 
            this.GetFileContentCountBTN.Location = new System.Drawing.Point(16, 191);
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
            this.label2.Location = new System.Drawing.Point(12, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "File Content Count";
            // 
            // ServerRandomFileNameTB
            // 
            this.ServerRandomFileNameTB.Location = new System.Drawing.Point(16, 33);
            this.ServerRandomFileNameTB.Multiline = true;
            this.ServerRandomFileNameTB.Name = "ServerRandomFileNameTB";
            this.ServerRandomFileNameTB.Size = new System.Drawing.Size(255, 74);
            this.ServerRandomFileNameTB.TabIndex = 9;
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
            this.DecryptProgressBar.Location = new System.Drawing.Point(16, 350);
            this.DecryptProgressBar.Name = "DecryptProgressBar";
            this.DecryptProgressBar.Size = new System.Drawing.Size(255, 36);
            this.DecryptProgressBar.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 327);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 20);
            this.label3.TabIndex = 15;
            this.label3.Text = "Decryption Progress";
            // 
            // OwnerDecryptFileContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DecryptProgressBar);
            this.Controls.Add(this.FileContentCountTB);
            this.Controls.Add(this.DecryptFetchedFileContentsBTN);
            this.Controls.Add(this.GetFileContentCountBTN);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ServerRandomFileNameTB);
            this.Controls.Add(this.label1);
            this.Name = "OwnerDecryptFileContent";
            this.Text = "OwnerDecryptFileContent";
            this.Load += new System.EventHandler(this.OwnerDecryptFileContent_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox FileContentCountTB;
        private System.Windows.Forms.Button DecryptFetchedFileContentsBTN;
        private System.Windows.Forms.Button GetFileContentCountBTN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ServerRandomFileNameTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar DecryptProgressBar;
        private System.Windows.Forms.Label label3;
    }
}