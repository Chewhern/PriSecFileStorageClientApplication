
namespace PriSecFileStorageClient
{
    partial class UserOfflineBackupAllKeys
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
            this.ZipNameTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BackupBTN = new System.Windows.Forms.Button();
            this.KeyTypesComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BackupDirectorySelector = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // ZipNameTB
            // 
            this.ZipNameTB.Location = new System.Drawing.Point(12, 101);
            this.ZipNameTB.Name = "ZipNameTB";
            this.ZipNameTB.Size = new System.Drawing.Size(609, 26);
            this.ZipNameTB.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(272, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "Zip Name (Applies to 2nd option only)";
            // 
            // BackupBTN
            // 
            this.BackupBTN.Location = new System.Drawing.Point(12, 148);
            this.BackupBTN.Name = "BackupBTN";
            this.BackupBTN.Size = new System.Drawing.Size(147, 62);
            this.BackupBTN.TabIndex = 9;
            this.BackupBTN.Text = "Backup";
            this.BackupBTN.UseVisualStyleBackColor = true;
            this.BackupBTN.Click += new System.EventHandler(this.BackupBTN_Click);
            // 
            // KeyTypesComboBox
            // 
            this.KeyTypesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.KeyTypesComboBox.FormattingEnabled = true;
            this.KeyTypesComboBox.Items.AddRange(new object[] {
            "All Server Directory Data",
            "Server Directory File Encryption Keys",
            "Server Direcotry File Name And Extension"});
            this.KeyTypesComboBox.Location = new System.Drawing.Point(12, 37);
            this.KeyTypesComboBox.Name = "KeyTypesComboBox";
            this.KeyTypesComboBox.Size = new System.Drawing.Size(609, 28);
            this.KeyTypesComboBox.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Key types to backup";
            // 
            // UserOfflineBackupAllKeys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ZipNameTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BackupBTN);
            this.Controls.Add(this.KeyTypesComboBox);
            this.Controls.Add(this.label1);
            this.Name = "UserOfflineBackupAllKeys";
            this.Text = "UserOfflineBackupAllKeys";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox ZipNameTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BackupBTN;
        private System.Windows.Forms.ComboBox KeyTypesComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog BackupDirectorySelector;
    }
}