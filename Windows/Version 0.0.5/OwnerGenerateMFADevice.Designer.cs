
namespace PriSecFileStorageClient
{
    partial class OwnerGenerateMFADevice
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
            this.MFADeviceIDTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.MergedED25519PKTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ED25519SKTB = new System.Windows.Forms.TextBox();
            this.GenerateDataBTN = new System.Windows.Forms.Button();
            this.AddMFADeviceBTN = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "MFA Device ID";
            // 
            // MFADeviceIDTB
            // 
            this.MFADeviceIDTB.Location = new System.Drawing.Point(13, 42);
            this.MFADeviceIDTB.Multiline = true;
            this.MFADeviceIDTB.Name = "MFADeviceIDTB";
            this.MFADeviceIDTB.ReadOnly = true;
            this.MFADeviceIDTB.Size = new System.Drawing.Size(274, 108);
            this.MFADeviceIDTB.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(176, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Merged ED25519 PK";
            // 
            // MergedED25519PKTB
            // 
            this.MergedED25519PKTB.Location = new System.Drawing.Point(13, 203);
            this.MergedED25519PKTB.Multiline = true;
            this.MergedED25519PKTB.Name = "MergedED25519PKTB";
            this.MergedED25519PKTB.ReadOnly = true;
            this.MergedED25519PKTB.Size = new System.Drawing.Size(274, 108);
            this.MergedED25519PKTB.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 329);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "ED25519 SK";
            // 
            // ED25519SKTB
            // 
            this.ED25519SKTB.Location = new System.Drawing.Point(13, 368);
            this.ED25519SKTB.Multiline = true;
            this.ED25519SKTB.Name = "ED25519SKTB";
            this.ED25519SKTB.ReadOnly = true;
            this.ED25519SKTB.Size = new System.Drawing.Size(274, 108);
            this.ED25519SKTB.TabIndex = 5;
            // 
            // GenerateDataBTN
            // 
            this.GenerateDataBTN.Location = new System.Drawing.Point(13, 493);
            this.GenerateDataBTN.Name = "GenerateDataBTN";
            this.GenerateDataBTN.Size = new System.Drawing.Size(274, 67);
            this.GenerateDataBTN.TabIndex = 6;
            this.GenerateDataBTN.Text = "1. Generate MFA Device Data";
            this.GenerateDataBTN.UseVisualStyleBackColor = true;
            this.GenerateDataBTN.Click += new System.EventHandler(this.GenerateDataBTN_Click);
            // 
            // AddMFADeviceBTN
            // 
            this.AddMFADeviceBTN.Location = new System.Drawing.Point(339, 110);
            this.AddMFADeviceBTN.Name = "AddMFADeviceBTN";
            this.AddMFADeviceBTN.Size = new System.Drawing.Size(246, 60);
            this.AddMFADeviceBTN.TabIndex = 7;
            this.AddMFADeviceBTN.Text = "2. Add MFA Device";
            this.AddMFADeviceBTN.UseVisualStyleBackColor = true;
            this.AddMFADeviceBTN.Click += new System.EventHandler(this.AddMFADeviceBTN_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(339, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(311, 75);
            this.label4.TabIndex = 8;
            this.label4.Text = "After you have added the device in\r\n\"OwnerAddMFADevice\", click this\r\nbutton below" +
    " to proceed.";
            // 
            // OwnerGenerateMFADevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 581);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.AddMFADeviceBTN);
            this.Controls.Add(this.GenerateDataBTN);
            this.Controls.Add(this.ED25519SKTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.MergedED25519PKTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MFADeviceIDTB);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "OwnerGenerateMFADevice";
            this.Text = "OwnerGenerateMFADevice";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox MFADeviceIDTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox MergedED25519PKTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ED25519SKTB;
        private System.Windows.Forms.Button GenerateDataBTN;
        private System.Windows.Forms.Button AddMFADeviceBTN;
        private System.Windows.Forms.Label label4;
    }
}