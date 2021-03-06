
namespace PriSecFileStorageClient
{
    partial class OwnerRemoveMFADeviceData
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
            this.RemarksTB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.RightPanelMainStatusLabel = new System.Windows.Forms.Label();
            this.DeviceMFADeviceIDTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DeviceMFADeviceCB = new System.Windows.Forms.ComboBox();
            this.DeleteMFADeviceDataBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RemarksTB
            // 
            this.RemarksTB.Location = new System.Drawing.Point(17, 254);
            this.RemarksTB.Multiline = true;
            this.RemarksTB.Name = "RemarksTB";
            this.RemarksTB.ReadOnly = true;
            this.RemarksTB.Size = new System.Drawing.Size(457, 79);
            this.RemarksTB.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 230);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 20);
            this.label5.TabIndex = 17;
            this.label5.Text = "Remarks";
            // 
            // RightPanelMainStatusLabel
            // 
            this.RightPanelMainStatusLabel.AutoSize = true;
            this.RightPanelMainStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.RightPanelMainStatusLabel.Location = new System.Drawing.Point(12, 9);
            this.RightPanelMainStatusLabel.Name = "RightPanelMainStatusLabel";
            this.RightPanelMainStatusLabel.Size = new System.Drawing.Size(462, 25);
            this.RightPanelMainStatusLabel.TabIndex = 12;
            this.RightPanelMainStatusLabel.Text = "Lists of registered MFA devices on your device";
            // 
            // DeviceMFADeviceIDTB
            // 
            this.DeviceMFADeviceIDTB.Location = new System.Drawing.Point(17, 136);
            this.DeviceMFADeviceIDTB.Multiline = true;
            this.DeviceMFADeviceIDTB.Name = "DeviceMFADeviceIDTB";
            this.DeviceMFADeviceIDTB.ReadOnly = true;
            this.DeviceMFADeviceIDTB.Size = new System.Drawing.Size(457, 79);
            this.DeviceMFADeviceIDTB.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 20);
            this.label3.TabIndex = 15;
            this.label3.Text = "MFA device ID";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(147, 20);
            this.label4.TabIndex = 13;
            this.label4.Text = "List of MFA devices";
            // 
            // DeviceMFADeviceCB
            // 
            this.DeviceMFADeviceCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DeviceMFADeviceCB.FormattingEnabled = true;
            this.DeviceMFADeviceCB.Location = new System.Drawing.Point(17, 67);
            this.DeviceMFADeviceCB.Name = "DeviceMFADeviceCB";
            this.DeviceMFADeviceCB.Size = new System.Drawing.Size(457, 28);
            this.DeviceMFADeviceCB.TabIndex = 14;
            this.DeviceMFADeviceCB.SelectedIndexChanged += new System.EventHandler(this.DeviceMFADeviceCB_SelectedIndexChanged);
            // 
            // DeleteMFADeviceDataBTN
            // 
            this.DeleteMFADeviceDataBTN.Location = new System.Drawing.Point(17, 350);
            this.DeleteMFADeviceDataBTN.Name = "DeleteMFADeviceDataBTN";
            this.DeleteMFADeviceDataBTN.Size = new System.Drawing.Size(457, 70);
            this.DeleteMFADeviceDataBTN.TabIndex = 19;
            this.DeleteMFADeviceDataBTN.Text = "Delete MFA Device Data";
            this.DeleteMFADeviceDataBTN.UseVisualStyleBackColor = true;
            this.DeleteMFADeviceDataBTN.Click += new System.EventHandler(this.DeleteMFADeviceDataBTN_Click);
            // 
            // OwnerRemoveMFADeviceData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.DeleteMFADeviceDataBTN);
            this.Controls.Add(this.RemarksTB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.RightPanelMainStatusLabel);
            this.Controls.Add(this.DeviceMFADeviceIDTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DeviceMFADeviceCB);
            this.Name = "OwnerRemoveMFADeviceData";
            this.Text = "OwnerRemoveMFADeviceData";
            this.Load += new System.EventHandler(this.OwnerRemoveMFADeviceData_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox RemarksTB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label RightPanelMainStatusLabel;
        private System.Windows.Forms.TextBox DeviceMFADeviceIDTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox DeviceMFADeviceCB;
        private System.Windows.Forms.Button DeleteMFADeviceDataBTN;
    }
}