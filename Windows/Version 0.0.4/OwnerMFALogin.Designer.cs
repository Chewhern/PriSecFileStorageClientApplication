
namespace PriSecFileStorageClient
{
    partial class OwnerMFALogin
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
            this.DeviceMFADeviceCB = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.RemarksTB = new System.Windows.Forms.TextBox();
            this.LoginBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Choose a MFA device";
            // 
            // DeviceMFADeviceCB
            // 
            this.DeviceMFADeviceCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DeviceMFADeviceCB.FormattingEnabled = true;
            this.DeviceMFADeviceCB.Location = new System.Drawing.Point(12, 36);
            this.DeviceMFADeviceCB.Name = "DeviceMFADeviceCB";
            this.DeviceMFADeviceCB.Size = new System.Drawing.Size(296, 28);
            this.DeviceMFADeviceCB.TabIndex = 1;
            this.DeviceMFADeviceCB.SelectedIndexChanged += new System.EventHandler(this.DeviceMFADeviceCB_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "MFA Device Remark";
            // 
            // RemarksTB
            // 
            this.RemarksTB.Location = new System.Drawing.Point(12, 101);
            this.RemarksTB.Multiline = true;
            this.RemarksTB.Name = "RemarksTB";
            this.RemarksTB.ReadOnly = true;
            this.RemarksTB.Size = new System.Drawing.Size(296, 73);
            this.RemarksTB.TabIndex = 3;
            // 
            // LoginBTN
            // 
            this.LoginBTN.Location = new System.Drawing.Point(13, 191);
            this.LoginBTN.Name = "LoginBTN";
            this.LoginBTN.Size = new System.Drawing.Size(295, 60);
            this.LoginBTN.TabIndex = 4;
            this.LoginBTN.Text = "MFA Device Login";
            this.LoginBTN.UseVisualStyleBackColor = true;
            this.LoginBTN.Click += new System.EventHandler(this.LoginBTN_Click);
            // 
            // OwnerMFALogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.LoginBTN);
            this.Controls.Add(this.RemarksTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DeviceMFADeviceCB);
            this.Controls.Add(this.label1);
            this.Name = "OwnerMFALogin";
            this.Text = "OwnerMFALogin";
            this.Load += new System.EventHandler(this.OwnerMFALogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox DeviceMFADeviceCB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox RemarksTB;
        private System.Windows.Forms.Button LoginBTN;
    }
}