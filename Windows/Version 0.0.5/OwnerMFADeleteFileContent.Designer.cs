
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
            this.FileStorageSizeTB.Location = new System.Drawing.Point(13, 41);
            this.FileStorageSizeTB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.FileStorageSizeTB.Multiline = true;
            this.FileStorageSizeTB.Name = "FileStorageSizeTB";
            this.FileStorageSizeTB.ReadOnly = true;
            this.FileStorageSizeTB.Size = new System.Drawing.Size(303, 96);
            this.FileStorageSizeTB.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(213, 25);
            this.label2.TabIndex = 26;
            this.label2.Text = "This file storage used size";
            // 
            // EncryptedRandomFileNameCB
            // 
            this.EncryptedRandomFileNameCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EncryptedRandomFileNameCB.FormattingEnabled = true;
            this.EncryptedRandomFileNameCB.Location = new System.Drawing.Point(13, 186);
            this.EncryptedRandomFileNameCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.EncryptedRandomFileNameCB.Name = "EncryptedRandomFileNameCB";
            this.EncryptedRandomFileNameCB.Size = new System.Drawing.Size(303, 33);
            this.EncryptedRandomFileNameCB.TabIndex = 25;
            this.EncryptedRandomFileNameCB.SelectedIndexChanged += new System.EventHandler(this.EncryptedRandomFileNameCB_SelectedIndexChanged);
            // 
            // DeleteFileContentBTN
            // 
            this.DeleteFileContentBTN.Location = new System.Drawing.Point(13, 591);
            this.DeleteFileContentBTN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DeleteFileContentBTN.Name = "DeleteFileContentBTN";
            this.DeleteFileContentBTN.Size = new System.Drawing.Size(303, 66);
            this.DeleteFileContentBTN.TabIndex = 24;
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
            this.label1.TabIndex = 23;
            this.label1.Text = "Server Random File Name";
            // 
            // SecondMFADeviceIDTB
            // 
            this.SecondMFADeviceIDTB.Location = new System.Drawing.Point(13, 446);
            this.SecondMFADeviceIDTB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SecondMFADeviceIDTB.Multiline = true;
            this.SecondMFADeviceIDTB.Name = "SecondMFADeviceIDTB";
            this.SecondMFADeviceIDTB.Size = new System.Drawing.Size(303, 122);
            this.SecondMFADeviceIDTB.TabIndex = 43;
            // 
            // FirstMFADeviceIDTB
            // 
            this.FirstMFADeviceIDTB.Location = new System.Drawing.Point(13, 268);
            this.FirstMFADeviceIDTB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.FirstMFADeviceIDTB.Multiline = true;
            this.FirstMFADeviceIDTB.Name = "FirstMFADeviceIDTB";
            this.FirstMFADeviceIDTB.Size = new System.Drawing.Size(303, 122);
            this.FirstMFADeviceIDTB.TabIndex = 42;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 405);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(192, 25);
            this.label8.TabIndex = 41;
            this.label8.Text = "Second MFA Device ID";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 239);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(166, 25);
            this.label7.TabIndex = 40;
            this.label7.Text = "First MFA Device ID";
            // 
            // OwnerMFADeleteFileContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 700);
            this.Controls.Add(this.SecondMFADeviceIDTB);
            this.Controls.Add(this.FirstMFADeviceIDTB);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.FileStorageSizeTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.EncryptedRandomFileNameCB);
            this.Controls.Add(this.DeleteFileContentBTN);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
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