
namespace PriSecFileStorageClient
{
    partial class UserActionChooser
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
            this.UserStorageOperationsBTN = new System.Windows.Forms.Button();
            this.GenerateKeyBTN = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // UserStorageOperationsBTN
            // 
            this.UserStorageOperationsBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.UserStorageOperationsBTN.Location = new System.Drawing.Point(208, 150);
            this.UserStorageOperationsBTN.Name = "UserStorageOperationsBTN";
            this.UserStorageOperationsBTN.Size = new System.Drawing.Size(359, 58);
            this.UserStorageOperationsBTN.TabIndex = 9;
            this.UserStorageOperationsBTN.Text = "User Storage Operations";
            this.UserStorageOperationsBTN.UseVisualStyleBackColor = true;
            this.UserStorageOperationsBTN.Click += new System.EventHandler(this.UserStorageOperationsBTN_Click);
            // 
            // GenerateKeyBTN
            // 
            this.GenerateKeyBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.GenerateKeyBTN.Location = new System.Drawing.Point(208, 56);
            this.GenerateKeyBTN.Name = "GenerateKeyBTN";
            this.GenerateKeyBTN.Size = new System.Drawing.Size(359, 58);
            this.GenerateKeyBTN.TabIndex = 8;
            this.GenerateKeyBTN.Text = "User Generate Access Key";
            this.GenerateKeyBTN.UseVisualStyleBackColor = true;
            this.GenerateKeyBTN.Click += new System.EventHandler(this.GenerateKeyBTN_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(151, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(489, 29);
            this.label1.TabIndex = 7;
            this.label1.Text = "Choose a destination by clicking a button";
            // 
            // UserActionChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.UserStorageOperationsBTN);
            this.Controls.Add(this.GenerateKeyBTN);
            this.Controls.Add(this.label1);
            this.Name = "UserActionChooser";
            this.Text = "UserActionChooser";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
            this.Load += new System.EventHandler(this.UserActionChooser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button UserStorageOperationsBTN;
        private System.Windows.Forms.Button GenerateKeyBTN;
        private System.Windows.Forms.Label label1;
    }
}