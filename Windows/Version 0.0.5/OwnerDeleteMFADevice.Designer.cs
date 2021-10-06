
namespace PriSecFileStorageClient
{
    partial class OwnerDeleteMFADevice
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
            this.ServerMFADeviceIDTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DeleteMFADeviceServerBTN = new System.Windows.Forms.Button();
            this.ChoiceCB = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(327, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "MFA Device ID that exists in your device";
            // 
            // MFADeviceIDTB
            // 
            this.MFADeviceIDTB.Location = new System.Drawing.Point(13, 46);
            this.MFADeviceIDTB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MFADeviceIDTB.Multiline = true;
            this.MFADeviceIDTB.Name = "MFADeviceIDTB";
            this.MFADeviceIDTB.Size = new System.Drawing.Size(388, 108);
            this.MFADeviceIDTB.TabIndex = 1;
            // 
            // ServerMFADeviceIDTB
            // 
            this.ServerMFADeviceIDTB.Location = new System.Drawing.Point(13, 201);
            this.ServerMFADeviceIDTB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ServerMFADeviceIDTB.Multiline = true;
            this.ServerMFADeviceIDTB.Name = "ServerMFADeviceIDTB";
            this.ServerMFADeviceIDTB.Size = new System.Drawing.Size(388, 108);
            this.ServerMFADeviceIDTB.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(283, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "MFA Device ID that exists in server";
            // 
            // DeleteMFADeviceServerBTN
            // 
            this.DeleteMFADeviceServerBTN.Location = new System.Drawing.Point(13, 408);
            this.DeleteMFADeviceServerBTN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DeleteMFADeviceServerBTN.Name = "DeleteMFADeviceServerBTN";
            this.DeleteMFADeviceServerBTN.Size = new System.Drawing.Size(324, 84);
            this.DeleteMFADeviceServerBTN.TabIndex = 4;
            this.DeleteMFADeviceServerBTN.Text = "Delete MFA Device From Server";
            this.DeleteMFADeviceServerBTN.UseVisualStyleBackColor = true;
            this.DeleteMFADeviceServerBTN.Click += new System.EventHandler(this.DeleteMFADeviceServerBTN_Click);
            // 
            // ChoiceCB
            // 
            this.ChoiceCB.AutoSize = true;
            this.ChoiceCB.Location = new System.Drawing.Point(13, 368);
            this.ChoiceCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ChoiceCB.Name = "ChoiceCB";
            this.ChoiceCB.Size = new System.Drawing.Size(63, 29);
            this.ChoiceCB.TabIndex = 5;
            this.ChoiceCB.Text = "Yes";
            this.ChoiceCB.UseVisualStyleBackColor = true;
            this.ChoiceCB.CheckedChanged += new System.EventHandler(this.ChoiceCB_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 334);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(206, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "Is it the last MFA device?";
            // 
            // OwnerDeleteMFADevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 562);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ChoiceCB);
            this.Controls.Add(this.DeleteMFADeviceServerBTN);
            this.Controls.Add(this.ServerMFADeviceIDTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MFADeviceIDTB);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "OwnerDeleteMFADevice";
            this.Text = "OwnerDeleteMFADevice";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox MFADeviceIDTB;
        private System.Windows.Forms.TextBox ServerMFADeviceIDTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button DeleteMFADeviceServerBTN;
        private System.Windows.Forms.CheckBox ChoiceCB;
        private System.Windows.Forms.Label label3;
    }
}