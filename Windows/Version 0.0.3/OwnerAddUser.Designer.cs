
namespace PriSecFileStorageClient
{
    partial class OwnerAddUser
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
            this.MergedED25519PKSTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AccessIDTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.StorageIDTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.GiveAccessBTN = new System.Windows.Forms.Button();
            this.AdditionalNoteTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // MergedED25519PKSTB
            // 
            this.MergedED25519PKSTB.Location = new System.Drawing.Point(12, 34);
            this.MergedED25519PKSTB.Multiline = true;
            this.MergedED25519PKSTB.Name = "MergedED25519PKSTB";
            this.MergedED25519PKSTB.Size = new System.Drawing.Size(283, 121);
            this.MergedED25519PKSTB.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Merged ED25519 PKS";
            // 
            // AccessIDTB
            // 
            this.AccessIDTB.Location = new System.Drawing.Point(12, 188);
            this.AccessIDTB.Multiline = true;
            this.AccessIDTB.Name = "AccessIDTB";
            this.AccessIDTB.ReadOnly = true;
            this.AccessIDTB.Size = new System.Drawing.Size(283, 70);
            this.AccessIDTB.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Allowed User Access ID";
            // 
            // StorageIDTB
            // 
            this.StorageIDTB.Location = new System.Drawing.Point(12, 295);
            this.StorageIDTB.Multiline = true;
            this.StorageIDTB.Name = "StorageIDTB";
            this.StorageIDTB.ReadOnly = true;
            this.StorageIDTB.Size = new System.Drawing.Size(283, 70);
            this.StorageIDTB.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 271);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Storage ID";
            // 
            // GiveAccessBTN
            // 
            this.GiveAccessBTN.Location = new System.Drawing.Point(317, 164);
            this.GiveAccessBTN.Name = "GiveAccessBTN";
            this.GiveAccessBTN.Size = new System.Drawing.Size(282, 66);
            this.GiveAccessBTN.TabIndex = 8;
            this.GiveAccessBTN.Text = "Give Access To User";
            this.GiveAccessBTN.UseVisualStyleBackColor = true;
            this.GiveAccessBTN.Click += new System.EventHandler(this.GiveAccessBTN_Click);
            // 
            // AdditionalNoteTB
            // 
            this.AdditionalNoteTB.Location = new System.Drawing.Point(317, 34);
            this.AdditionalNoteTB.Multiline = true;
            this.AdditionalNoteTB.Name = "AdditionalNoteTB";
            this.AdditionalNoteTB.Size = new System.Drawing.Size(283, 121);
            this.AdditionalNoteTB.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(313, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(186, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Additional Note(Optional)";
            // 
            // OwnerAddUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.AdditionalNoteTB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.GiveAccessBTN);
            this.Controls.Add(this.StorageIDTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.AccessIDTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MergedED25519PKSTB);
            this.Controls.Add(this.label1);
            this.Name = "OwnerAddUser";
            this.Text = "OwnerAddUser";
            this.Load += new System.EventHandler(this.OwnerAddUser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox MergedED25519PKSTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox AccessIDTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox StorageIDTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button GiveAccessBTN;
        private System.Windows.Forms.TextBox AdditionalNoteTB;
        private System.Windows.Forms.Label label4;
    }
}