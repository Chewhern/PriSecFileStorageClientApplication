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
            this.IsOwnerCB = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Key types to backup";
            // 
            // KeyTypesComboBox
            // 
            this.KeyTypesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.KeyTypesComboBox.FormattingEnabled = true;
            this.KeyTypesComboBox.Items.AddRange(new object[] {
            "All Server Directory Data",
            "Server Directory File Encryption Keys",
            "Server Directory File Name And Extension"});
            this.KeyTypesComboBox.Location = new System.Drawing.Point(19, 46);
            this.KeyTypesComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.KeyTypesComboBox.Name = "KeyTypesComboBox";
            this.KeyTypesComboBox.Size = new System.Drawing.Size(676, 33);
            this.KeyTypesComboBox.TabIndex = 1;
            // 
            // BackupBTN
            // 
            this.BackupBTN.Location = new System.Drawing.Point(19, 308);
            this.BackupBTN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BackupBTN.Name = "BackupBTN";
            this.BackupBTN.Size = new System.Drawing.Size(163, 78);
            this.BackupBTN.TabIndex = 2;
            this.BackupBTN.Text = "Backup";
            this.BackupBTN.UseVisualStyleBackColor = true;
            this.BackupBTN.Click += new System.EventHandler(this.BackupBTN_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Zip Name";
            // 
            // ZipNameTB
            // 
            this.ZipNameTB.Location = new System.Drawing.Point(19, 126);
            this.ZipNameTB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ZipNameTB.Name = "ZipNameTB";
            this.ZipNameTB.Size = new System.Drawing.Size(676, 31);
            this.ZipNameTB.TabIndex = 4;
            // 
            // IsOwnerCB
            // 
            this.IsOwnerCB.AutoSize = true;
            this.IsOwnerCB.Location = new System.Drawing.Point(19, 258);
            this.IsOwnerCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.IsOwnerCB.Name = "IsOwnerCB";
            this.IsOwnerCB.Size = new System.Drawing.Size(63, 29);
            this.IsOwnerCB.TabIndex = 5;
            this.IsOwnerCB.Text = "Yes";
            this.IsOwnerCB.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 189);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(387, 50);
            this.label3.TabIndex = 6;
            this.label3.Text = "Do you need to let others have signature secret\r\nkeys? (Apply only to second choi" +
    "ce)";
            // 
            // OfflineBackupAllKeys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 562);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.IsOwnerCB);
            this.Controls.Add(this.ZipNameTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BackupBTN);
            this.Controls.Add(this.KeyTypesComboBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
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
        private System.Windows.Forms.CheckBox IsOwnerCB;
        private System.Windows.Forms.Label label3;
    }
}