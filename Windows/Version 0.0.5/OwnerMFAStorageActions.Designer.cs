
namespace PriSecFileStorageClient
{
    partial class OwnerMFAStorageActions
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
            this.ChoosenActionTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.DirectoryIDComboBox = new System.Windows.Forms.ComboBox();
            this.GoBTN = new System.Windows.Forms.Button();
            this.ActionCB = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ChoosenActionTB
            // 
            this.ChoosenActionTB.Location = new System.Drawing.Point(257, 232);
            this.ChoosenActionTB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ChoosenActionTB.Multiline = true;
            this.ChoosenActionTB.Name = "ChoosenActionTB";
            this.ChoosenActionTB.ReadOnly = true;
            this.ChoosenActionTB.Size = new System.Drawing.Size(355, 125);
            this.ChoosenActionTB.TabIndex = 32;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(252, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 25);
            this.label3.TabIndex = 31;
            this.label3.Text = "Choosen Action";
            // 
            // DirectoryIDComboBox
            // 
            this.DirectoryIDComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DirectoryIDComboBox.FormattingEnabled = true;
            this.DirectoryIDComboBox.Location = new System.Drawing.Point(257, 52);
            this.DirectoryIDComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DirectoryIDComboBox.Name = "DirectoryIDComboBox";
            this.DirectoryIDComboBox.Size = new System.Drawing.Size(355, 33);
            this.DirectoryIDComboBox.TabIndex = 30;
            // 
            // GoBTN
            // 
            this.GoBTN.Location = new System.Drawing.Point(257, 382);
            this.GoBTN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GoBTN.Name = "GoBTN";
            this.GoBTN.Size = new System.Drawing.Size(356, 76);
            this.GoBTN.TabIndex = 29;
            this.GoBTN.Text = "Go";
            this.GoBTN.UseVisualStyleBackColor = true;
            this.GoBTN.Click += new System.EventHandler(this.GoBTN_Click);
            // 
            // ActionCB
            // 
            this.ActionCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ActionCB.FormattingEnabled = true;
            this.ActionCB.Items.AddRange(new object[] {
            "Generate MFA Device Data",
            "Add MFA Device",
            "View list of MFA Devices",
            "Delete MFA Device",
            "Delete MFA Data",
            "Login Through MFA Device",
            "Upload MFA Embedded Endpoint Encrypted Files",
            "Delete MFA Embedded Endpoint Encrypted Files From Server",
            "Upload MFA Embedded Digital Signature Public Key of Allowed User",
            "Delete MFA Embedded Digital Signature Public Key of Allowed User"});
            this.ActionCB.Location = new System.Drawing.Point(257, 148);
            this.ActionCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ActionCB.Name = "ActionCB";
            this.ActionCB.Size = new System.Drawing.Size(355, 33);
            this.ActionCB.TabIndex = 28;
            this.ActionCB.SelectedIndexChanged += new System.EventHandler(this.ActionCB_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(252, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(216, 25);
            this.label2.TabIndex = 27;
            this.label2.Text = "What do you want to do?";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(252, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 25);
            this.label1.TabIndex = 26;
            this.label1.Text = "Server Directory ID";
            // 
            // OwnerMFAStorageActions
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
            this.Name = "OwnerMFAStorageActions";
            this.Text = "OwnerMFAStorageActions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
            this.Load += new System.EventHandler(this.OwnerMFAStorageActions_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ChoosenActionTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox DirectoryIDComboBox;
        private System.Windows.Forms.Button GoBTN;
        private System.Windows.Forms.ComboBox ActionCB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}