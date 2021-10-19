
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
            this.label2 = new System.Windows.Forms.Label();
            this.AccessIDCB = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // RemoveAccessBTN
            // 
            this.RemoveAccessBTN.Location = new System.Drawing.Point(13, 111);
            this.RemoveAccessBTN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.RemoveAccessBTN.Name = "RemoveAccessBTN";
            this.RemoveAccessBTN.Size = new System.Drawing.Size(313, 82);
            this.RemoveAccessBTN.TabIndex = 13;
            this.RemoveAccessBTN.Text = "Remove Access From User";
            this.RemoveAccessBTN.UseVisualStyleBackColor = true;
            this.RemoveAccessBTN.Click += new System.EventHandler(this.RemoveAccessBTN_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(197, 25);
            this.label2.TabIndex = 11;
            this.label2.Text = "Allowed User Access ID";
            // 
            // AccessIDCB
            // 
            this.AccessIDCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AccessIDCB.FormattingEnabled = true;
            this.AccessIDCB.Location = new System.Drawing.Point(13, 41);
            this.AccessIDCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AccessIDCB.Name = "AccessIDCB";
            this.AccessIDCB.Size = new System.Drawing.Size(313, 33);
            this.AccessIDCB.TabIndex = 14;
            this.AccessIDCB.SelectedIndexChanged += new System.EventHandler(this.AccessIDCB_SelectedIndexChanged);
            // 
            // OwnerRemoveUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 562);
            this.Controls.Add(this.AccessIDCB);
            this.Controls.Add(this.RemoveAccessBTN);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "OwnerRemoveUser";
            this.Text = "OwnerRemoveUser";
            this.Load += new System.EventHandler(this.OwnerRemoveUser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RemoveAccessBTN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox AccessIDCB;
    }
}