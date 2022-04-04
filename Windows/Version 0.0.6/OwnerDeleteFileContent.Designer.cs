namespace PriSecFileStorageClient
{
    partial class OwnerDeleteFileContent
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
            this.DeleteFileContentBTN = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.EncryptedRandomFileNameCB = new System.Windows.Forms.ComboBox();
            this.FileStorageSizeTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DeleteFileContentBTN
            // 
            this.DeleteFileContentBTN.Location = new System.Drawing.Point(13, 251);
            this.DeleteFileContentBTN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DeleteFileContentBTN.Name = "DeleteFileContentBTN";
            this.DeleteFileContentBTN.Size = new System.Drawing.Size(303, 66);
            this.DeleteFileContentBTN.TabIndex = 7;
            this.DeleteFileContentBTN.Text = "Delete File From Server And Local";
            this.DeleteFileContentBTN.UseVisualStyleBackColor = true;
            this.DeleteFileContentBTN.Click += new System.EventHandler(this.DeleteFileContentBTN_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 156);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Server Random File Name";
            // 
            // EncryptedRandomFileNameCB
            // 
            this.EncryptedRandomFileNameCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EncryptedRandomFileNameCB.FormattingEnabled = true;
            this.EncryptedRandomFileNameCB.Location = new System.Drawing.Point(13, 186);
            this.EncryptedRandomFileNameCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.EncryptedRandomFileNameCB.Name = "EncryptedRandomFileNameCB";
            this.EncryptedRandomFileNameCB.Size = new System.Drawing.Size(303, 33);
            this.EncryptedRandomFileNameCB.TabIndex = 8;
            this.EncryptedRandomFileNameCB.SelectedIndexChanged += new System.EventHandler(this.EncryptedRandomFileNameCB_SelectedIndexChanged);
            // 
            // FileStorageSizeTB
            // 
            this.FileStorageSizeTB.Location = new System.Drawing.Point(13, 41);
            this.FileStorageSizeTB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.FileStorageSizeTB.Multiline = true;
            this.FileStorageSizeTB.Name = "FileStorageSizeTB";
            this.FileStorageSizeTB.ReadOnly = true;
            this.FileStorageSizeTB.Size = new System.Drawing.Size(303, 96);
            this.FileStorageSizeTB.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(213, 25);
            this.label2.TabIndex = 21;
            this.label2.Text = "This file storage used size";
            // 
            // OwnerDeleteFileContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 562);
            this.Controls.Add(this.FileStorageSizeTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.EncryptedRandomFileNameCB);
            this.Controls.Add(this.DeleteFileContentBTN);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "OwnerDeleteFileContent";
            this.Text = "OwnerDeleteFileContent";
            this.Load += new System.EventHandler(this.OwnerDeleteFileContent_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button DeleteFileContentBTN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox EncryptedRandomFileNameCB;
        private System.Windows.Forms.TextBox FileStorageSizeTB;
        private System.Windows.Forms.Label label2;
    }
}