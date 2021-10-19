namespace PriSecFileStorageClient
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
            this.label1.Location = new System.Drawing.Point(14, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server Random File Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "File Content Count";
            // 
            // GetFileContentCountBTN
            // 
            this.GetFileContentCountBTN.Location = new System.Drawing.Point(19, 185);
            this.GetFileContentCountBTN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GetFileContentCountBTN.Name = "GetFileContentCountBTN";
            this.GetFileContentCountBTN.Size = new System.Drawing.Size(298, 66);
            this.GetFileContentCountBTN.TabIndex = 4;
            this.GetFileContentCountBTN.Text = "Get File Content Count";
            this.GetFileContentCountBTN.UseVisualStyleBackColor = true;
            this.GetFileContentCountBTN.Click += new System.EventHandler(this.GetFileContentCountBTN_Click);
            // 
            // CheckFileContentsBTN
            // 
            this.CheckFileContentsBTN.Location = new System.Drawing.Point(19, 260);
            this.CheckFileContentsBTN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CheckFileContentsBTN.Name = "CheckFileContentsBTN";
            this.CheckFileContentsBTN.Size = new System.Drawing.Size(298, 74);
            this.CheckFileContentsBTN.TabIndex = 5;
            this.CheckFileContentsBTN.Text = "Check File Contents";
            this.CheckFileContentsBTN.UseVisualStyleBackColor = true;
            this.CheckFileContentsBTN.Click += new System.EventHandler(this.CheckFileContentsBTN_Click);
            // 
            // CheckFileContentStatusLB
            // 
            this.CheckFileContentStatusLB.FormattingEnabled = true;
            this.CheckFileContentStatusLB.ItemHeight = 25;
            this.CheckFileContentStatusLB.Location = new System.Drawing.Point(364, 16);
            this.CheckFileContentStatusLB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CheckFileContentStatusLB.Name = "CheckFileContentStatusLB";
            this.CheckFileContentStatusLB.Size = new System.Drawing.Size(440, 379);
            this.CheckFileContentStatusLB.TabIndex = 6;
            // 
            // FileContentCountTB
            // 
            this.FileContentCountTB.Location = new System.Drawing.Point(19, 131);
            this.FileContentCountTB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.FileContentCountTB.Name = "FileContentCountTB";
            this.FileContentCountTB.ReadOnly = true;
            this.FileContentCountTB.Size = new System.Drawing.Size(297, 31);
            this.FileContentCountTB.TabIndex = 7;
            // 
            // EncryptedRandomFileNameCB
            // 
            this.EncryptedRandomFileNameCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EncryptedRandomFileNameCB.FormattingEnabled = true;
            this.EncryptedRandomFileNameCB.Location = new System.Drawing.Point(13, 45);
            this.EncryptedRandomFileNameCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.EncryptedRandomFileNameCB.Name = "EncryptedRandomFileNameCB";
            this.EncryptedRandomFileNameCB.Size = new System.Drawing.Size(303, 33);
            this.EncryptedRandomFileNameCB.TabIndex = 9;
            this.EncryptedRandomFileNameCB.SelectedIndexChanged += new System.EventHandler(this.EncryptedRandomFileNameCB_SelectedIndexChanged);
            // 
            // OwnerCheckFileContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 562);
            this.Controls.Add(this.EncryptedRandomFileNameCB);
            this.Controls.Add(this.FileContentCountTB);
            this.Controls.Add(this.CheckFileContentStatusLB);
            this.Controls.Add(this.CheckFileContentsBTN);
            this.Controls.Add(this.GetFileContentCountBTN);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
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