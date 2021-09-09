namespace PriSecFileStorageClient
{
    partial class AccountRecovery
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
            this.AccountIDCB = new System.Windows.Forms.ComboBox();
            this.ResetAccountBTN = new System.Windows.Forms.Button();
            this.DeleteAccountBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Choose an account ID";
            // 
            // AccountIDCB
            // 
            this.AccountIDCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AccountIDCB.FormattingEnabled = true;
            this.AccountIDCB.Location = new System.Drawing.Point(17, 50);
            this.AccountIDCB.Name = "AccountIDCB";
            this.AccountIDCB.Size = new System.Drawing.Size(492, 28);
            this.AccountIDCB.TabIndex = 1;
            // 
            // ResetAccountBTN
            // 
            this.ResetAccountBTN.Location = new System.Drawing.Point(17, 103);
            this.ResetAccountBTN.Name = "ResetAccountBTN";
            this.ResetAccountBTN.Size = new System.Drawing.Size(164, 62);
            this.ResetAccountBTN.TabIndex = 2;
            this.ResetAccountBTN.Text = "Reset Information";
            this.ResetAccountBTN.UseVisualStyleBackColor = true;
            this.ResetAccountBTN.Click += new System.EventHandler(this.ResetAccountBTN_Click);
            // 
            // DeleteAccountBTN
            // 
            this.DeleteAccountBTN.Location = new System.Drawing.Point(345, 103);
            this.DeleteAccountBTN.Name = "DeleteAccountBTN";
            this.DeleteAccountBTN.Size = new System.Drawing.Size(164, 62);
            this.DeleteAccountBTN.TabIndex = 3;
            this.DeleteAccountBTN.Text = "Delete Account";
            this.DeleteAccountBTN.UseVisualStyleBackColor = true;
            this.DeleteAccountBTN.Click += new System.EventHandler(this.DeleteAccountBTN_Click);
            // 
            // AccountRecovery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 480);
            this.Controls.Add(this.DeleteAccountBTN);
            this.Controls.Add(this.ResetAccountBTN);
            this.Controls.Add(this.AccountIDCB);
            this.Controls.Add(this.label1);
            this.Name = "AccountRecovery";
            this.Text = "AccountRecovery";
            this.Load += new System.EventHandler(this.AccountRecovery_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(CloseForm);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox AccountIDCB;
        private System.Windows.Forms.Button ResetAccountBTN;
        private System.Windows.Forms.Button DeleteAccountBTN;
    }
}