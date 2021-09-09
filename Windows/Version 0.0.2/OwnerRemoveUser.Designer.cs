
namespace PriSecFileStorageClient
{
    partial class OwnerRemoveUser
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
            this.RemoveAccessBTN = new System.Windows.Forms.Button();
            this.AccessIDTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // RemoveAccessBTN
            // 
            this.RemoveAccessBTN.Location = new System.Drawing.Point(12, 119);
            this.RemoveAccessBTN.Name = "RemoveAccessBTN";
            this.RemoveAccessBTN.Size = new System.Drawing.Size(282, 66);
            this.RemoveAccessBTN.TabIndex = 13;
            this.RemoveAccessBTN.Text = "Remove Access From User";
            this.RemoveAccessBTN.UseVisualStyleBackColor = true;
            this.RemoveAccessBTN.Click += new System.EventHandler(this.RemoveAccessBTN_Click);
            // 
            // AccessIDTB
            // 
            this.AccessIDTB.Location = new System.Drawing.Point(12, 33);
            this.AccessIDTB.Multiline = true;
            this.AccessIDTB.Name = "AccessIDTB";
            this.AccessIDTB.Size = new System.Drawing.Size(283, 70);
            this.AccessIDTB.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "Allowed User Access ID";
            // 
            // OwnerRemoveUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.RemoveAccessBTN);
            this.Controls.Add(this.AccessIDTB);
            this.Controls.Add(this.label2);
            this.Name = "OwnerRemoveUser";
            this.Text = "OwnerRemoveUser";
            this.Load += new System.EventHandler(this.OwnerRemoveUser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RemoveAccessBTN;
        private System.Windows.Forms.TextBox AccessIDTB;
        private System.Windows.Forms.Label label2;
    }
}