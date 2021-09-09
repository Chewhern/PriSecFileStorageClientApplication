
namespace PriSecFileStorageClient
{
    partial class OwnerActionChooser
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
            this.OwnerRenewPaymentBTN = new System.Windows.Forms.Button();
            this.OwnerMakePaymentBTN = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.OwnerStorageOperationsBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OwnerRenewPaymentBTN
            // 
            this.OwnerRenewPaymentBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OwnerRenewPaymentBTN.Location = new System.Drawing.Point(217, 150);
            this.OwnerRenewPaymentBTN.Name = "OwnerRenewPaymentBTN";
            this.OwnerRenewPaymentBTN.Size = new System.Drawing.Size(359, 58);
            this.OwnerRenewPaymentBTN.TabIndex = 5;
            this.OwnerRenewPaymentBTN.Text = "Owner Renew Payment";
            this.OwnerRenewPaymentBTN.UseVisualStyleBackColor = true;
            this.OwnerRenewPaymentBTN.Click += new System.EventHandler(this.OwnerRenewPaymentBTN_Click);
            // 
            // OwnerMakePaymentBTN
            // 
            this.OwnerMakePaymentBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OwnerMakePaymentBTN.Location = new System.Drawing.Point(217, 56);
            this.OwnerMakePaymentBTN.Name = "OwnerMakePaymentBTN";
            this.OwnerMakePaymentBTN.Size = new System.Drawing.Size(359, 58);
            this.OwnerMakePaymentBTN.TabIndex = 4;
            this.OwnerMakePaymentBTN.Text = "Owner Make Payment";
            this.OwnerMakePaymentBTN.UseVisualStyleBackColor = true;
            this.OwnerMakePaymentBTN.Click += new System.EventHandler(this.OwnerMakePaymentBTN_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(160, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(489, 29);
            this.label1.TabIndex = 3;
            this.label1.Text = "Choose a destination by clicking a button";
            // 
            // OwnerStorageOperationsBTN
            // 
            this.OwnerStorageOperationsBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OwnerStorageOperationsBTN.Location = new System.Drawing.Point(217, 246);
            this.OwnerStorageOperationsBTN.Name = "OwnerStorageOperationsBTN";
            this.OwnerStorageOperationsBTN.Size = new System.Drawing.Size(359, 58);
            this.OwnerStorageOperationsBTN.TabIndex = 6;
            this.OwnerStorageOperationsBTN.Text = "Owner Storage Operations";
            this.OwnerStorageOperationsBTN.UseVisualStyleBackColor = true;
            this.OwnerStorageOperationsBTN.Click += new System.EventHandler(this.OwnerStorageOperationsBTN_Click);
            // 
            // OwnerActionChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.OwnerStorageOperationsBTN);
            this.Controls.Add(this.OwnerRenewPaymentBTN);
            this.Controls.Add(this.OwnerMakePaymentBTN);
            this.Controls.Add(this.label1);
            this.Name = "OwnerActionChooser";
            this.Text = "OwnerActionChooser";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
            this.Load += new System.EventHandler(this.OwnerActionChooser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OwnerRenewPaymentBTN;
        private System.Windows.Forms.Button OwnerMakePaymentBTN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button OwnerStorageOperationsBTN;
    }
}