
namespace PriSecFileStorageClient
{
    partial class OwnerMFADeleteFileContent
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
            this.FileStorageSizeTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.EncryptedRandomFileNameCB = new System.Windows.Forms.ComboBox();
            this.DeleteFileContentBTN = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SecondMFADeviceIDTB = new System.Windows.Forms.TextBox();
            this.FirstMFADeviceIDTB = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // FileStorageSizeTB
            // 
            this.FileStorageSizeTB.Location = new System.Drawing.Point(12, 33);
            this.FileStorageSizeTB.Multiline = true;
            this.FileStorageSizeTB.Name = "FileStorageSizeTB";
            this.FileStorageSizeTB.ReadOnly = true;
            this.FileStorageSizeTB.Size = new System.Drawing.Size(273, 78);
            this.FileStorageSizeTB.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(191, 20);
            this.label2.TabIndex = 26;
            this.label2.Text = "This file storage used size";
            // 
            // EncryptedRandomFileNameCB
            // 
            this.EncryptedRandomFileNameCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EncryptedRandomFileNameCB.FormattingEnabled = true;
            this.EncryptedRandomFileNameCB.Location = new System.Drawing.Point(12, 149);
            this.EncryptedRandomFileNameCB.Name = "EncryptedRandomFileNameCB";
            this.EncryptedRandomFileNameCB.Size = new System.Drawing.Size(273, 28);
            this.EncryptedRandomFileNameCB.TabIndex = 25;
            this.EncryptedRandomFileNameCB.SelectedIndexChanged += new System.EventHandler(this.EncryptedRandomFileNameCB_SelectedIndexChanged);
            // 
            // DeleteFileContentBTN
            // 
            this.DeleteFileContentBTN.Location = new System.Drawing.Point(12, 473);
            this.DeleteFileContentBTN.Name = "DeleteFileContentBTN";
            this.DeleteFileContentBTN.Size = new System.Drawing.Size(273, 53);
            this.DeleteFileContentBTN.TabIndex = 24;
            this.DeleteFileContentBTN.Text = "Delete File From Server And Local";
            this.DeleteFileContentBTN.UseVisualStyleBackColor = true;
            this.DeleteFileContentBTN.Click += new System.EventHandler(this.DeleteFileContentBTN_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 20);
            this.label1.TabIndex = 23;
            this.label1.Text = "Server Random File Name";
            // 
            // SecondMFADeviceIDTB
            // 
            this.SecondMFADeviceIDTB.Location = new System.Drawing.Point(12, 357);
            this.SecondMFADeviceIDTB.Multiline = true;
            this.SecondMFADeviceIDTB.Name = "SecondMFADeviceIDTB";
            this.SecondMFADeviceIDTB.Size = new System.Drawing.Size(273, 98);
            this.SecondMFADeviceIDTB.TabIndex = 43;
            // 
            // FirstMFADeviceIDTB
            // 
            this.FirstMFADeviceIDTB.Location = new System.Drawing.Point(12, 214);
            this.FirstMFADeviceIDTB.Multiline = true;
            this.FirstMFADeviceIDTB.Name = "FirstMFADeviceIDTB";
            this.FirstMFADeviceIDTB.Size = new System.Drawing.Size(273, 98);
            this.FirstMFADeviceIDTB.TabIndex = 42;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 324);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(175, 20);
            this.label8.TabIndex = 41;
            this.label8.Text = "Second MFA Device ID";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 191);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(151, 20);
            this.label7.TabIndex = 40;
            this.label7.Text = "First MFA Device ID";
            // 
            // OwnerMFADeleteFileContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 560);
            this.Controls.Add(this.SecondMFADeviceIDTB);
            this.Controls.Add(this.FirstMFADeviceIDTB);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.FileStorageSizeTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.EncryptedRandomFileNameCB);
            this.Controls.Add(this.DeleteFileContentBTN);
            this.Controls.Add(this.label1);
            this.Name = "OwnerMFADeleteFileContent";
            this.Text = "OwnerMFADeleteFileContent";
            this.Load += new System.EventHandler(this.OwnerMFADeleteFileContent_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox FileStorageSizeTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox EncryptedRandomFileNameCB;
        private System.Windows.Forms.Button DeleteFileContentBTN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SecondMFADeviceIDTB;
        private System.Windows.Forms.TextBox FirstMFADeviceIDTB;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
    }
}