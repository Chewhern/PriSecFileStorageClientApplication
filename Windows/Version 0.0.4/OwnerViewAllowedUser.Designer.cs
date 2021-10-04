
namespace PriSecFileStorageClient
{
    partial class OwnerViewAllowedUser
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
            this.AllowedUserIDCB = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CommentTB = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Allowed User IDs";
            // 
            // AllowedUserIDCB
            // 
            this.AllowedUserIDCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AllowedUserIDCB.FormattingEnabled = true;
            this.AllowedUserIDCB.Location = new System.Drawing.Point(13, 37);
            this.AllowedUserIDCB.Name = "AllowedUserIDCB";
            this.AllowedUserIDCB.Size = new System.Drawing.Size(279, 28);
            this.AllowedUserIDCB.TabIndex = 1;
            this.AllowedUserIDCB.SelectedIndexChanged += new System.EventHandler(this.AllowedUserIDCB_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(169, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Comment (If Available)";
            // 
            // CommentTB
            // 
            this.CommentTB.Location = new System.Drawing.Point(13, 101);
            this.CommentTB.Multiline = true;
            this.CommentTB.Name = "CommentTB";
            this.CommentTB.ReadOnly = true;
            this.CommentTB.Size = new System.Drawing.Size(279, 90);
            this.CommentTB.TabIndex = 3;
            // 
            // OwnerViewAllowedUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.CommentTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.AllowedUserIDCB);
            this.Controls.Add(this.label1);
            this.Name = "OwnerViewAllowedUser";
            this.Text = "OwnerViewAllowedUser";
            this.Load += new System.EventHandler(this.OwnerViewAllowedUser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox AllowedUserIDCB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox CommentTB;
    }
}