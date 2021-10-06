
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
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.RightPanel = new System.Windows.Forms.Panel();
            this.SendMFADeviceBTN = new System.Windows.Forms.Button();
            this.MergedED25519PKTB = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.DeviceMFADeviceIDTB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.LeftPanel.SuspendLayout();
            this.RightPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(293, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Check MFA Device Count On Server";
            // 
            // MFADeviceCountTB
            // 
            this.MFADeviceCountTB.Location = new System.Drawing.Point(12, 106);
            this.MFADeviceCountTB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MFADeviceCountTB.Multiline = true;
            this.MFADeviceCountTB.Name = "MFADeviceCountTB";
            this.MFADeviceCountTB.ReadOnly = true;
            this.MFADeviceCountTB.Size = new System.Drawing.Size(295, 46);
            this.MFADeviceCountTB.TabIndex = 1;
            // 
            // CheckBTN
            // 
            this.CheckBTN.Location = new System.Drawing.Point(12, 161);
            this.CheckBTN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CheckBTN.Name = "CheckBTN";
            this.CheckBTN.Size = new System.Drawing.Size(296, 66);
            this.CheckBTN.TabIndex = 2;
            this.CheckBTN.Text = "Check MFA Device Count";
            this.CheckBTN.UseVisualStyleBackColor = true;
            this.CheckBTN.Click += new System.EventHandler(this.CheckBTN_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 243);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(289, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Remarks for MFA Device (Optional)";
            // 
            // MFARemarksTB
            // 
            this.MFARemarksTB.Location = new System.Drawing.Point(12, 273);
            this.MFARemarksTB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MFARemarksTB.Multiline = true;
            this.MFARemarksTB.Name = "MFARemarksTB";
            this.MFARemarksTB.Size = new System.Drawing.Size(295, 108);
            this.MFARemarksTB.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 405);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "MFA Device ID";
            // 
            // MFADeviceIDTB
            // 
            this.MFADeviceIDTB.Location = new System.Drawing.Point(12, 433);
            this.MFADeviceIDTB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MFADeviceIDTB.Multiline = true;
            this.MFADeviceIDTB.Name = "MFADeviceIDTB";
            this.MFADeviceIDTB.ReadOnly = true;
            this.MFADeviceIDTB.Size = new System.Drawing.Size(295, 108);
            this.MFADeviceIDTB.TabIndex = 6;
            // 
            // AddDeviceBTN
            // 
            this.AddDeviceBTN.Location = new System.Drawing.Point(12, 551);
            this.AddDeviceBTN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AddDeviceBTN.Name = "AddDeviceBTN";
            this.AddDeviceBTN.Size = new System.Drawing.Size(296, 70);
            this.AddDeviceBTN.TabIndex = 7;
            this.AddDeviceBTN.Text = "Add MFA Device";
            this.AddDeviceBTN.UseVisualStyleBackColor = true;
            this.AddDeviceBTN.Click += new System.EventHandler(this.AddDeviceBTN_Click);
            // 
            // LeftPanel
            // 
            this.LeftPanel.Controls.Add(this.label4);
            this.LeftPanel.Controls.Add(this.CheckBTN);
            this.LeftPanel.Controls.Add(this.AddDeviceBTN);
            this.LeftPanel.Controls.Add(this.label1);
            this.LeftPanel.Controls.Add(this.MFADeviceIDTB);
            this.LeftPanel.Controls.Add(this.MFADeviceCountTB);
            this.LeftPanel.Controls.Add(this.label3);
            this.LeftPanel.Controls.Add(this.label2);
            this.LeftPanel.Controls.Add(this.MFARemarksTB);
            this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(435, 658);
            this.LeftPanel.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(13, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(272, 50);
            this.label4.TabIndex = 8;
            this.label4.Text = "Generate MFA Device Data on \r\ndevice and server";
            // 
            // RightPanel
            // 
            this.RightPanel.Controls.Add(this.SendMFADeviceBTN);
            this.RightPanel.Controls.Add(this.MergedED25519PKTB);
            this.RightPanel.Controls.Add(this.label7);
            this.RightPanel.Controls.Add(this.DeviceMFADeviceIDTB);
            this.RightPanel.Controls.Add(this.label6);
            this.RightPanel.Controls.Add(this.label5);
            this.RightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightPanel.Location = new System.Drawing.Point(432, 0);
            this.RightPanel.Name = "RightPanel";
            this.RightPanel.Size = new System.Drawing.Size(426, 658);
            this.RightPanel.TabIndex = 9;
            // 
            // SendMFADeviceBTN
            // 
            this.SendMFADeviceBTN.Location = new System.Drawing.Point(22, 413);
            this.SendMFADeviceBTN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SendMFADeviceBTN.Name = "SendMFADeviceBTN";
            this.SendMFADeviceBTN.Size = new System.Drawing.Size(379, 70);
            this.SendMFADeviceBTN.TabIndex = 9;
            this.SendMFADeviceBTN.Text = "Send MFA Device Data to Server";
            this.SendMFADeviceBTN.UseVisualStyleBackColor = true;
            this.SendMFADeviceBTN.Click += new System.EventHandler(this.SendMFADeviceBTN_Click);
            // 
            // MergedED25519PKTB
            // 
            this.MergedED25519PKTB.Location = new System.Drawing.Point(22, 273);
            this.MergedED25519PKTB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MergedED25519PKTB.Multiline = true;
            this.MergedED25519PKTB.Name = "MergedED25519PKTB";
            this.MergedED25519PKTB.Size = new System.Drawing.Size(379, 108);
            this.MergedED25519PKTB.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 245);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(176, 25);
            this.label7.TabIndex = 11;
            this.label7.Text = "Merged ED25519 PK";
            // 
            // DeviceMFADeviceIDTB
            // 
            this.DeviceMFADeviceIDTB.Location = new System.Drawing.Point(22, 106);
            this.DeviceMFADeviceIDTB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DeviceMFADeviceIDTB.Multiline = true;
            this.DeviceMFADeviceIDTB.Name = "DeviceMFADeviceIDTB";
            this.DeviceMFADeviceIDTB.Size = new System.Drawing.Size(379, 108);
            this.DeviceMFADeviceIDTB.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(214, 25);
            this.label6.TabIndex = 9;
            this.label6.Text = "Generated MFA Device ID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(22, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(379, 25);
            this.label5.TabIndex = 9;
            this.label5.Text = "Send Generated MFA Device Data to server";
            // 
            // OwnerAddMFADevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 658);
            this.Controls.Add(this.RightPanel);
            this.Controls.Add(this.LeftPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "OwnerAddMFADevice";
            this.Text = "OwnerAddMFADevice";
            this.Load += new System.EventHandler(this.OwnerAddMFADevice_Load);
            this.LeftPanel.ResumeLayout(false);
            this.LeftPanel.PerformLayout();
            this.RightPanel.ResumeLayout(false);
            this.RightPanel.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Panel LeftPanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel RightPanel;
        private System.Windows.Forms.Button SendMFADeviceBTN;
        private System.Windows.Forms.TextBox MergedED25519PKTB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox DeviceMFADeviceIDTB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
    }
}