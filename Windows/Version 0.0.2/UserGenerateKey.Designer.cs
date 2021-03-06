
namespace PriSecFileStorageClient
{
    partial class UserGenerateKey
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
            this.MergedED25519PKSTB = new System.Windows.Forms.TextBox();
            this.ED25519SKTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.GenerateSignatureKPBTN = new System.Windows.Forms.Button();
            this.AccessIDTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.OwnerStorageIDTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ConfirmAccessBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Merged ED25519 PKS (B64)";
            // 
            // MergedED25519PKSTB
            // 
            this.MergedED25519PKSTB.Location = new System.Drawing.Point(17, 37);
            this.MergedED25519PKSTB.Multiline = true;
            this.MergedED25519PKSTB.Name = "MergedED25519PKSTB";
            this.MergedED25519PKSTB.ReadOnly = true;
            this.MergedED25519PKSTB.Size = new System.Drawing.Size(283, 121);
            this.MergedED25519PKSTB.TabIndex = 1;
            // 
            // ED25519SKTB
            // 
            this.ED25519SKTB.Location = new System.Drawing.Point(17, 188);
            this.ED25519SKTB.Multiline = true;
            this.ED25519SKTB.Name = "ED25519SKTB";
            this.ED25519SKTB.ReadOnly = true;
            this.ED25519SKTB.Size = new System.Drawing.Size(283, 121);
            this.ED25519SKTB.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "ED25519 SK (B64)";
            // 
            // GenerateSignatureKPBTN
            // 
            this.GenerateSignatureKPBTN.Location = new System.Drawing.Point(17, 315);
            this.GenerateSignatureKPBTN.Name = "GenerateSignatureKPBTN";
            this.GenerateSignatureKPBTN.Size = new System.Drawing.Size(283, 64);
            this.GenerateSignatureKPBTN.TabIndex = 4;
            this.GenerateSignatureKPBTN.Text = "Generate Signature KeyPair";
            this.GenerateSignatureKPBTN.UseVisualStyleBackColor = true;
            this.GenerateSignatureKPBTN.Click += new System.EventHandler(this.GenerateSignatureKPBTN_Click);
            // 
            // AccessIDTB
            // 
            this.AccessIDTB.Location = new System.Drawing.Point(325, 56);
            this.AccessIDTB.Multiline = true;
            this.AccessIDTB.Name = "AccessIDTB";
            this.AccessIDTB.Size = new System.Drawing.Size(232, 97);
            this.AccessIDTB.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(321, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(198, 40);
            this.label3.TabIndex = 5;
            this.label3.Text = "ID To Allow Access\r\n(Get From Storage Owner)";
            // 
            // OwnerStorageIDTB
            // 
            this.OwnerStorageIDTB.Location = new System.Drawing.Point(325, 199);
            this.OwnerStorageIDTB.Multiline = true;
            this.OwnerStorageIDTB.Name = "OwnerStorageIDTB";
            this.OwnerStorageIDTB.Size = new System.Drawing.Size(232, 110);
            this.OwnerStorageIDTB.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(321, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(207, 40);
            this.label4.TabIndex = 7;
            this.label4.Text = "File Storage ID From Owner\r\n(must be the exactly same)";
            // 
            // ConfirmAccessBTN
            // 
            this.ConfirmAccessBTN.Location = new System.Drawing.Point(325, 315);
            this.ConfirmAccessBTN.Name = "ConfirmAccessBTN";
            this.ConfirmAccessBTN.Size = new System.Drawing.Size(232, 64);
            this.ConfirmAccessBTN.TabIndex = 9;
            this.ConfirmAccessBTN.Text = "Confirm Granted Access";
            this.ConfirmAccessBTN.UseVisualStyleBackColor = true;
            this.ConfirmAccessBTN.Click += new System.EventHandler(this.ConfirmAccessBTN_Click);
            // 
            // UserGenerateKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ConfirmAccessBTN);
            this.Controls.Add(this.OwnerStorageIDTB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.AccessIDTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.GenerateSignatureKPBTN);
            this.Controls.Add(this.ED25519SKTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MergedED25519PKSTB);
            this.Controls.Add(this.label1);
            this.Name = "UserGenerateKey";
            this.Text = "UserGenerateKey";
            this.Load += new System.EventHandler(this.UserGenerateKey_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox MergedED25519PKSTB;
        private System.Windows.Forms.TextBox ED25519SKTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button GenerateSignatureKPBTN;
        private System.Windows.Forms.TextBox AccessIDTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox OwnerStorageIDTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button ConfirmAccessBTN;
    }
}