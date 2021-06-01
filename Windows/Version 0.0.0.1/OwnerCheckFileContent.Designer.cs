﻿namespace PriSecFileStorageClient
{
    partial class OwnerCheckFileContent
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.GetFileContentCountBTN = new System.Windows.Forms.Button();
            this.CheckFileContentsBTN = new System.Windows.Forms.Button();
            this.CheckFileContentStatusLB = new System.Windows.Forms.ListBox();
            this.FileContentCountTB = new System.Windows.Forms.TextBox();
            this.EncryptedRandomFileNameCB = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server Random File Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "File Content Count";
            // 
            // GetFileContentCountBTN
            // 
            this.GetFileContentCountBTN.Location = new System.Drawing.Point(17, 148);
            this.GetFileContentCountBTN.Name = "GetFileContentCountBTN";
            this.GetFileContentCountBTN.Size = new System.Drawing.Size(268, 53);
            this.GetFileContentCountBTN.TabIndex = 4;
            this.GetFileContentCountBTN.Text = "Get File Content Count";
            this.GetFileContentCountBTN.UseVisualStyleBackColor = true;
            this.GetFileContentCountBTN.Click += new System.EventHandler(this.GetFileContentCountBTN_Click);
            // 
            // CheckFileContentsBTN
            // 
            this.CheckFileContentsBTN.Location = new System.Drawing.Point(17, 208);
            this.CheckFileContentsBTN.Name = "CheckFileContentsBTN";
            this.CheckFileContentsBTN.Size = new System.Drawing.Size(268, 59);
            this.CheckFileContentsBTN.TabIndex = 5;
            this.CheckFileContentsBTN.Text = "Check File Contents";
            this.CheckFileContentsBTN.UseVisualStyleBackColor = true;
            this.CheckFileContentsBTN.Click += new System.EventHandler(this.CheckFileContentsBTN_Click);
            // 
            // CheckFileContentStatusLB
            // 
            this.CheckFileContentStatusLB.FormattingEnabled = true;
            this.CheckFileContentStatusLB.ItemHeight = 20;
            this.CheckFileContentStatusLB.Location = new System.Drawing.Point(328, 13);
            this.CheckFileContentStatusLB.Name = "CheckFileContentStatusLB";
            this.CheckFileContentStatusLB.Size = new System.Drawing.Size(396, 304);
            this.CheckFileContentStatusLB.TabIndex = 6;
            // 
            // FileContentCountTB
            // 
            this.FileContentCountTB.Location = new System.Drawing.Point(17, 105);
            this.FileContentCountTB.Name = "FileContentCountTB";
            this.FileContentCountTB.ReadOnly = true;
            this.FileContentCountTB.Size = new System.Drawing.Size(268, 26);
            this.FileContentCountTB.TabIndex = 7;
            // 
            // EncryptedRandomFileNameCB
            // 
            this.EncryptedRandomFileNameCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EncryptedRandomFileNameCB.FormattingEnabled = true;
            this.EncryptedRandomFileNameCB.Location = new System.Drawing.Point(12, 36);
            this.EncryptedRandomFileNameCB.Name = "EncryptedRandomFileNameCB";
            this.EncryptedRandomFileNameCB.Size = new System.Drawing.Size(273, 28);
            this.EncryptedRandomFileNameCB.TabIndex = 9;
            this.EncryptedRandomFileNameCB.SelectedIndexChanged += new System.EventHandler(this.EncryptedRandomFileNameCB_SelectedIndexChanged);
            // 
            // OwnerCheckFileContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.EncryptedRandomFileNameCB);
            this.Controls.Add(this.FileContentCountTB);
            this.Controls.Add(this.CheckFileContentStatusLB);
            this.Controls.Add(this.CheckFileContentsBTN);
            this.Controls.Add(this.GetFileContentCountBTN);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "OwnerCheckFileContent";
            this.Text = "OwnerCheckFileContent";
            this.Load += new System.EventHandler(this.OwnerCheckFileContent_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button GetFileContentCountBTN;
        private System.Windows.Forms.Button CheckFileContentsBTN;
        private System.Windows.Forms.ListBox CheckFileContentStatusLB;
        private System.Windows.Forms.TextBox FileContentCountTB;
        private System.Windows.Forms.ComboBox EncryptedRandomFileNameCB;
    }
}