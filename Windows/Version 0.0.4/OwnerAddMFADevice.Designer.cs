
namespace PriSecFileStorageClient
{
    partial class OwnerAddMFADevice
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
            this.MFADeviceCountTB = new System.Windows.Forms.TextBox();
            this.CheckBTN = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.MFARemarksTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.MFADeviceIDTB = new System.Windows.Forms.TextBox();
            this.AddDeviceBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(266, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Check MFA Device Count On Server";
            // 
            // MFADeviceCountTB
            // 
            this.MFADeviceCountTB.Location = new System.Drawing.Point(12, 36);
            this.MFADeviceCountTB.Multiline = true;
            this.MFADeviceCountTB.Name = "MFADeviceCountTB";
            this.MFADeviceCountTB.ReadOnly = true;
            this.MFADeviceCountTB.Size = new System.Drawing.Size(266, 38);
            this.MFADeviceCountTB.TabIndex = 1;
            // 
            // CheckBTN
            // 
            this.CheckBTN.Location = new System.Drawing.Point(12, 80);
            this.CheckBTN.Name = "CheckBTN";
            this.CheckBTN.Size = new System.Drawing.Size(266, 53);
            this.CheckBTN.TabIndex = 2;
            this.CheckBTN.Text = "Check MFA Device Count";
            this.CheckBTN.UseVisualStyleBackColor = true;
            this.CheckBTN.Click += new System.EventHandler(this.CheckBTN_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(259, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Remarks for MFA Device (Optional)";
            // 
            // MFARemarksTB
            // 
            this.MFARemarksTB.Location = new System.Drawing.Point(12, 170);
            this.MFARemarksTB.Multiline = true;
            this.MFARemarksTB.Name = "MFARemarksTB";
            this.MFARemarksTB.Size = new System.Drawing.Size(266, 87);
            this.MFARemarksTB.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 275);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "MFA Device ID";
            // 
            // MFADeviceIDTB
            // 
            this.MFADeviceIDTB.Location = new System.Drawing.Point(12, 298);
            this.MFADeviceIDTB.Multiline = true;
            this.MFADeviceIDTB.Name = "MFADeviceIDTB";
            this.MFADeviceIDTB.ReadOnly = true;
            this.MFADeviceIDTB.Size = new System.Drawing.Size(266, 87);
            this.MFADeviceIDTB.TabIndex = 6;
            // 
            // AddDeviceBTN
            // 
            this.AddDeviceBTN.Location = new System.Drawing.Point(12, 392);
            this.AddDeviceBTN.Name = "AddDeviceBTN";
            this.AddDeviceBTN.Size = new System.Drawing.Size(266, 56);
            this.AddDeviceBTN.TabIndex = 7;
            this.AddDeviceBTN.Text = "Add MFA Device";
            this.AddDeviceBTN.UseVisualStyleBackColor = true;
            this.AddDeviceBTN.Click += new System.EventHandler(this.AddDeviceBTN_Click);
            // 
            // OwnerAddMFADevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 481);
            this.Controls.Add(this.AddDeviceBTN);
            this.Controls.Add(this.MFADeviceIDTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.MFARemarksTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CheckBTN);
            this.Controls.Add(this.MFADeviceCountTB);
            this.Controls.Add(this.label1);
            this.Name = "OwnerAddMFADevice";
            this.Text = "OwnerAddMFADevice";
            this.Load += new System.EventHandler(this.OwnerAddMFADevice_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox MFADeviceCountTB;
        private System.Windows.Forms.Button CheckBTN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox MFARemarksTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox MFADeviceIDTB;
        private System.Windows.Forms.Button AddDeviceBTN;
    }
}