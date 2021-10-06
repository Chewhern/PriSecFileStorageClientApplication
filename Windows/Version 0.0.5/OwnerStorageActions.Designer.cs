
namespace PriSecFileStorageClient
{
    partial class OwnerStorageActions
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
            this.DirectoryIDComboBox = new System.Windows.Forms.ComboBox();
            this.GoBTN = new System.Windows.Forms.Button();
            this.ActionCB = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ChoosenActionTB = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // DirectoryIDComboBox
            // 
            this.DirectoryIDComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DirectoryIDComboBox.FormattingEnabled = true;
            this.DirectoryIDComboBox.Location = new System.Drawing.Point(271, 61);
            this.DirectoryIDComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DirectoryIDComboBox.Name = "DirectoryIDComboBox";
            this.DirectoryIDComboBox.Size = new System.Drawing.Size(355, 33);
            this.DirectoryIDComboBox.TabIndex = 23;
            // 
            // GoBTN
            // 
            this.GoBTN.Location = new System.Drawing.Point(271, 391);
            this.GoBTN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GoBTN.Name = "GoBTN";
            this.GoBTN.Size = new System.Drawing.Size(356, 76);
            this.GoBTN.TabIndex = 22;
            this.GoBTN.Text = "Go";
            this.GoBTN.UseVisualStyleBackColor = true;
            this.GoBTN.Click += new System.EventHandler(this.GoBTN_Click);
            // 
            // ActionCB
            // 
            this.ActionCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ActionCB.FormattingEnabled = true;
            this.ActionCB.Items.AddRange(new object[] {
            "Upload Endpoint Encrypted Files",
            "Compare Endpoint Encrypted Files From Server Locally",
            "Decrypt Fetched Endpoint Encrypted Files From Server",
            "Delete Endpoint Encrypted Files From Server",
            "Backup information locally",
            "Import information locally",
            "Upload Digital Signature Public Key of Allowed User",
            "Delete Digital Signature Public Key of Allowed User",
            "View list of granted allowed user"});
            this.ActionCB.Location = new System.Drawing.Point(271, 156);
            this.ActionCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ActionCB.Name = "ActionCB";
            this.ActionCB.Size = new System.Drawing.Size(355, 33);
            this.ActionCB.TabIndex = 21;
            this.ActionCB.SelectedIndexChanged += new System.EventHandler(this.ActionCB_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(267, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(216, 25);
            this.label2.TabIndex = 20;
            this.label2.Text = "What do you want to do?";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(267, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 25);
            this.label1.TabIndex = 17;
            this.label1.Text = "Server Directory ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(267, 211);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 25);
            this.label3.TabIndex = 24;
            this.label3.Text = "Choosen Action";
            // 
            // ChoosenActionTB
            // 
            this.ChoosenActionTB.Location = new System.Drawing.Point(271, 241);
            this.ChoosenActionTB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ChoosenActionTB.Multiline = true;
            this.ChoosenActionTB.Name = "ChoosenActionTB";
            this.ChoosenActionTB.ReadOnly = true;
            this.ChoosenActionTB.Size = new System.Drawing.Size(355, 125);
            this.ChoosenActionTB.TabIndex = 25;
            // 
            // OwnerStorageActions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 562);
            this.Controls.Add(this.ChoosenActionTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DirectoryIDComboBox);
            this.Controls.Add(this.GoBTN);
            this.Controls.Add(this.ActionCB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "OwnerStorageActions";
            this.Text = "OwnerStorageActions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
            this.Load += new System.EventHandler(this.OwnerStorageActions_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox DirectoryIDComboBox;
        private System.Windows.Forms.Button GoBTN;
        private System.Windows.Forms.ComboBox ActionCB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ChoosenActionTB;
    }
}