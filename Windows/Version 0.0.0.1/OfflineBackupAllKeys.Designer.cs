namespace PriSecFileStorageClient
{
    partial class OfflineBackupAllKeys
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
            this.KeyTypesComboBox = new System.Windows.Forms.ComboBox();
            this.BackupBTN = new System.Windows.Forms.Button();
            this.BackupDirectorySelector = new System.Windows.Forms.FolderBrowserDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.ZipNameTB = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Key types to backup";
            // 
            // KeyTypesComboBox
            // 
            this.KeyTypesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.KeyTypesComboBox.FormattingEnabled = true;
            this.KeyTypesComboBox.Items.AddRange(new object[] {
            "All Server Directory Data",
            "Server Directory Ownership Keys",
            "Server Directory File Encryption Keys"});
            this.KeyTypesComboBox.Location = new System.Drawing.Point(17, 37);
            this.KeyTypesComboBox.Name = "KeyTypesComboBox";
            this.KeyTypesComboBox.Size = new System.Drawing.Size(345, 28);
            this.KeyTypesComboBox.TabIndex = 1;
            // 
            // BackupBTN
            // 
            this.BackupBTN.Location = new System.Drawing.Point(17, 148);
            this.BackupBTN.Name = "BackupBTN";
            this.BackupBTN.Size = new System.Drawing.Size(147, 62);
            this.BackupBTN.TabIndex = 2;
            this.BackupBTN.Text = "Backup";
            this.BackupBTN.UseVisualStyleBackColor = true;
            this.BackupBTN.Click += new System.EventHandler(this.BackupBTN_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(306, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Zip Name (Required to 1st and 3rd option)";
            // 
            // ZipNameTB
            // 
            this.ZipNameTB.Location = new System.Drawing.Point(17, 101);
            this.ZipNameTB.Name = "ZipNameTB";
            this.ZipNameTB.Size = new System.Drawing.Size(345, 26);
            this.ZipNameTB.TabIndex = 4;
            // 
            // OfflineBackupAllKeys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ZipNameTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BackupBTN);
            this.Controls.Add(this.KeyTypesComboBox);
            this.Controls.Add(this.label1);
            this.Name = "OfflineBackupAllKeys";
            this.Text = "OfflineBackupAllKeys";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox KeyTypesComboBox;
        private System.Windows.Forms.Button BackupBTN;
        private System.Windows.Forms.FolderBrowserDialog BackupDirectorySelector;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ZipNameTB;
    }
}