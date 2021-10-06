﻿
namespace PriSecFileStorageClient
{
    partial class OwnerViewMFADevice
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
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.ServerMFADeviceRefreshBTN = new System.Windows.Forms.Button();
            this.ServerMFADeviceIDTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ServerMFADeviceCB = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LeftPanelMainStatusLabel = new System.Windows.Forms.Label();
            this.RightPanel = new System.Windows.Forms.Panel();
            this.RemarksTB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.DeviceMFADeviceRefreshBTN = new System.Windows.Forms.Button();
            this.RightPanelMainStatusLabel = new System.Windows.Forms.Label();
            this.DeviceMFADeviceIDTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DeviceMFADeviceCB = new System.Windows.Forms.ComboBox();
            this.LeftPanel.SuspendLayout();
            this.RightPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LeftPanel
            // 
            this.LeftPanel.Controls.Add(this.ServerMFADeviceRefreshBTN);
            this.LeftPanel.Controls.Add(this.ServerMFADeviceIDTB);
            this.LeftPanel.Controls.Add(this.label2);
            this.LeftPanel.Controls.Add(this.ServerMFADeviceCB);
            this.LeftPanel.Controls.Add(this.label1);
            this.LeftPanel.Controls.Add(this.LeftPanelMainStatusLabel);
            this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(450, 686);
            this.LeftPanel.TabIndex = 0;
            // 
            // ServerMFADeviceRefreshBTN
            // 
            this.ServerMFADeviceRefreshBTN.Location = new System.Drawing.Point(14, 332);
            this.ServerMFADeviceRefreshBTN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ServerMFADeviceRefreshBTN.Name = "ServerMFADeviceRefreshBTN";
            this.ServerMFADeviceRefreshBTN.Size = new System.Drawing.Size(418, 69);
            this.ServerMFADeviceRefreshBTN.TabIndex = 5;
            this.ServerMFADeviceRefreshBTN.Text = "Refresh Server MFA Device List";
            this.ServerMFADeviceRefreshBTN.UseVisualStyleBackColor = true;
            this.ServerMFADeviceRefreshBTN.Click += new System.EventHandler(this.ServerMFADeviceRefreshBTN_Click);
            // 
            // ServerMFADeviceIDTB
            // 
            this.ServerMFADeviceIDTB.Location = new System.Drawing.Point(14, 214);
            this.ServerMFADeviceIDTB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ServerMFADeviceIDTB.Multiline = true;
            this.ServerMFADeviceIDTB.Name = "ServerMFADeviceIDTB";
            this.ServerMFADeviceIDTB.ReadOnly = true;
            this.ServerMFADeviceIDTB.Size = new System.Drawing.Size(417, 98);
            this.ServerMFADeviceIDTB.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 184);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "MFA device ID";
            // 
            // ServerMFADeviceCB
            // 
            this.ServerMFADeviceCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ServerMFADeviceCB.FormattingEnabled = true;
            this.ServerMFADeviceCB.Location = new System.Drawing.Point(14, 128);
            this.ServerMFADeviceCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ServerMFADeviceCB.Name = "ServerMFADeviceCB";
            this.ServerMFADeviceCB.Size = new System.Drawing.Size(417, 33);
            this.ServerMFADeviceCB.TabIndex = 2;
            this.ServerMFADeviceCB.SelectedIndexChanged += new System.EventHandler(this.ServerMFADeviceCB_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "List of MFA devices";
            // 
            // LeftPanelMainStatusLabel
            // 
            this.LeftPanelMainStatusLabel.AutoSize = true;
            this.LeftPanelMainStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LeftPanelMainStatusLabel.Location = new System.Drawing.Point(88, 11);
            this.LeftPanelMainStatusLabel.Name = "LeftPanelMainStatusLabel";
            this.LeftPanelMainStatusLabel.Size = new System.Drawing.Size(235, 50);
            this.LeftPanelMainStatusLabel.TabIndex = 0;
            this.LeftPanelMainStatusLabel.Text = "Lists of registered MFA\r\ndevices on server";
            // 
            // RightPanel
            // 
            this.RightPanel.Controls.Add(this.RemarksTB);
            this.RightPanel.Controls.Add(this.label5);
            this.RightPanel.Controls.Add(this.DeviceMFADeviceRefreshBTN);
            this.RightPanel.Controls.Add(this.RightPanelMainStatusLabel);
            this.RightPanel.Controls.Add(this.DeviceMFADeviceIDTB);
            this.RightPanel.Controls.Add(this.label3);
            this.RightPanel.Controls.Add(this.label4);
            this.RightPanel.Controls.Add(this.DeviceMFADeviceCB);
            this.RightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightPanel.Location = new System.Drawing.Point(448, 0);
            this.RightPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.RightPanel.Name = "RightPanel";
            this.RightPanel.Size = new System.Drawing.Size(441, 686);
            this.RightPanel.TabIndex = 1;
            // 
            // RemarksTB
            // 
            this.RemarksTB.Location = new System.Drawing.Point(10, 361);
            this.RemarksTB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.RemarksTB.Multiline = true;
            this.RemarksTB.Name = "RemarksTB";
            this.RemarksTB.ReadOnly = true;
            this.RemarksTB.Size = new System.Drawing.Size(417, 98);
            this.RemarksTB.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 331);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 25);
            this.label5.TabIndex = 10;
            this.label5.Text = "Remarks";
            // 
            // DeviceMFADeviceRefreshBTN
            // 
            this.DeviceMFADeviceRefreshBTN.Location = new System.Drawing.Point(10, 490);
            this.DeviceMFADeviceRefreshBTN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DeviceMFADeviceRefreshBTN.Name = "DeviceMFADeviceRefreshBTN";
            this.DeviceMFADeviceRefreshBTN.Size = new System.Drawing.Size(418, 69);
            this.DeviceMFADeviceRefreshBTN.TabIndex = 6;
            this.DeviceMFADeviceRefreshBTN.Text = "Refresh Device MFA Device List";
            this.DeviceMFADeviceRefreshBTN.UseVisualStyleBackColor = true;
            this.DeviceMFADeviceRefreshBTN.Click += new System.EventHandler(this.DeviceMFADeviceRefreshBTN_Click);
            // 
            // RightPanelMainStatusLabel
            // 
            this.RightPanelMainStatusLabel.AutoSize = true;
            this.RightPanelMainStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.RightPanelMainStatusLabel.Location = new System.Drawing.Point(96, 11);
            this.RightPanelMainStatusLabel.Name = "RightPanelMainStatusLabel";
            this.RightPanelMainStatusLabel.Size = new System.Drawing.Size(235, 50);
            this.RightPanelMainStatusLabel.TabIndex = 1;
            this.RightPanelMainStatusLabel.Text = "Lists of registered MFA\r\ndevices on your device";
            // 
            // DeviceMFADeviceIDTB
            // 
            this.DeviceMFADeviceIDTB.Location = new System.Drawing.Point(10, 214);
            this.DeviceMFADeviceIDTB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DeviceMFADeviceIDTB.Multiline = true;
            this.DeviceMFADeviceIDTB.Name = "DeviceMFADeviceIDTB";
            this.DeviceMFADeviceIDTB.ReadOnly = true;
            this.DeviceMFADeviceIDTB.Size = new System.Drawing.Size(417, 98);
            this.DeviceMFADeviceIDTB.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 184);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 25);
            this.label3.TabIndex = 8;
            this.label3.Text = "MFA device ID";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(164, 25);
            this.label4.TabIndex = 6;
            this.label4.Text = "List of MFA devices";
            // 
            // DeviceMFADeviceCB
            // 
            this.DeviceMFADeviceCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DeviceMFADeviceCB.FormattingEnabled = true;
            this.DeviceMFADeviceCB.Location = new System.Drawing.Point(10, 128);
            this.DeviceMFADeviceCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DeviceMFADeviceCB.Name = "DeviceMFADeviceCB";
            this.DeviceMFADeviceCB.Size = new System.Drawing.Size(417, 33);
            this.DeviceMFADeviceCB.TabIndex = 7;
            this.DeviceMFADeviceCB.SelectedIndexChanged += new System.EventHandler(this.DeviceMFADeviceCB_SelectedIndexChanged);
            // 
            // OwnerViewMFADevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 686);
            this.Controls.Add(this.RightPanel);
            this.Controls.Add(this.LeftPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "OwnerViewMFADevice";
            this.Text = "OwnerViewMFADevice";
            this.Load += new System.EventHandler(this.OwnerViewMFADevice_Load);
            this.LeftPanel.ResumeLayout(false);
            this.LeftPanel.PerformLayout();
            this.RightPanel.ResumeLayout(false);
            this.RightPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel LeftPanel;
        private System.Windows.Forms.Label LeftPanelMainStatusLabel;
        private System.Windows.Forms.Panel RightPanel;
        private System.Windows.Forms.Label RightPanelMainStatusLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ServerMFADeviceRefreshBTN;
        private System.Windows.Forms.TextBox ServerMFADeviceIDTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ServerMFADeviceCB;
        private System.Windows.Forms.Button DeviceMFADeviceRefreshBTN;
        private System.Windows.Forms.TextBox DeviceMFADeviceIDTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox DeviceMFADeviceCB;
        private System.Windows.Forms.TextBox RemarksTB;
        private System.Windows.Forms.Label label5;
    }
}