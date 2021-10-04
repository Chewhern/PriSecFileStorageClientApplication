
namespace PriSecFileStorageClient
{
    partial class OwnerMFAAddUser
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
            this.AdditionalNoteTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.GiveAccessBTN = new System.Windows.Forms.Button();
            this.StorageIDTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.AccessIDTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.MergedED25519PKSTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SecondMFADeviceIDTB = new System.Windows.Forms.TextBox();
            this.FirstMFADeviceIDTB = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AdditionalNoteTB
            // 
            this.AdditionalNoteTB.Location = new System.Drawing.Point(317, 31);
            this.AdditionalNoteTB.Multiline = true;
            this.AdditionalNoteTB.Name = "AdditionalNoteTB";
            this.AdditionalNoteTB.Size = new System.Drawing.Size(283, 121);
            this.AdditionalNoteTB.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(313, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(186, 20);
            this.label4.TabIndex = 18;
            this.label4.Text = "Additional Note(Optional)";
            // 
            // GiveAccessBTN
            // 
            this.GiveAccessBTN.Location = new System.Drawing.Point(318, 447);
            this.GiveAccessBTN.Name = "GiveAccessBTN";
            this.GiveAccessBTN.Size = new System.Drawing.Size(282, 66);
            this.GiveAccessBTN.TabIndex = 17;
            this.GiveAccessBTN.Text = "Give Access To User";
            this.GiveAccessBTN.UseVisualStyleBackColor = true;
            this.GiveAccessBTN.Click += new System.EventHandler(this.GiveAccessBTN_Click);
            // 
            // StorageIDTB
            // 
            this.StorageIDTB.Location = new System.Drawing.Point(12, 292);
            this.StorageIDTB.Multiline = true;
            this.StorageIDTB.Name = "StorageIDTB";
            this.StorageIDTB.ReadOnly = true;
            this.StorageIDTB.Size = new System.Drawing.Size(283, 70);
            this.StorageIDTB.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 268);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 20);
            this.label3.TabIndex = 15;
            this.label3.Text = "Storage ID";
            // 
            // AccessIDTB
            // 
            this.AccessIDTB.Location = new System.Drawing.Point(12, 185);
            this.AccessIDTB.Multiline = true;
            this.AccessIDTB.Name = "AccessIDTB";
            this.AccessIDTB.ReadOnly = true;
            this.AccessIDTB.Size = new System.Drawing.Size(283, 70);
            this.AccessIDTB.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 161);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "Allowed User Access ID";
            // 
            // MergedED25519PKSTB
            // 
            this.MergedED25519PKSTB.Location = new System.Drawing.Point(12, 31);
            this.MergedED25519PKSTB.Multiline = true;
            this.MergedED25519PKSTB.Name = "MergedED25519PKSTB";
            this.MergedED25519PKSTB.Size = new System.Drawing.Size(283, 121);
            this.MergedED25519PKSTB.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "Merged ED25519 PKS";
            // 
            // SecondMFADeviceIDTB
            // 
            this.SecondMFADeviceIDTB.Location = new System.Drawing.Point(318, 328);
            this.SecondMFADeviceIDTB.Multiline = true;
            this.SecondMFADeviceIDTB.Name = "SecondMFADeviceIDTB";
            this.SecondMFADeviceIDTB.Size = new System.Drawing.Size(282, 98);
            this.SecondMFADeviceIDTB.TabIndex = 47;
            // 
            // FirstMFADeviceIDTB
            // 
            this.FirstMFADeviceIDTB.Location = new System.Drawing.Point(318, 185);
            this.FirstMFADeviceIDTB.Multiline = true;
            this.FirstMFADeviceIDTB.Name = "FirstMFADeviceIDTB";
            this.FirstMFADeviceIDTB.Size = new System.Drawing.Size(282, 98);
            this.FirstMFADeviceIDTB.TabIndex = 46;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(314, 295);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(175, 20);
            this.label8.TabIndex = 45;
            this.label8.Text = "Second MFA Device ID";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(314, 162);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(151, 20);
            this.label7.TabIndex = 44;
            this.label7.Text = "First MFA Device ID";
            // 
            // OwnerMFAAddUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 557);
            this.Controls.Add(this.SecondMFADeviceIDTB);
            this.Controls.Add(this.FirstMFADeviceIDTB);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.AdditionalNoteTB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.GiveAccessBTN);
            this.Controls.Add(this.StorageIDTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.AccessIDTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MergedED25519PKSTB);
            this.Controls.Add(this.label1);
            this.Name = "OwnerMFAAddUser";
            this.Text = "OwnerMFAAddUser";
            this.Load += new System.EventHandler(this.OwnerMFAAddUser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox AdditionalNoteTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button GiveAccessBTN;
        private System.Windows.Forms.TextBox StorageIDTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox AccessIDTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox MergedED25519PKSTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SecondMFADeviceIDTB;
        private System.Windows.Forms.TextBox FirstMFADeviceIDTB;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
    }
}