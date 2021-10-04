
namespace PriSecFileStorageClient
{
    partial class OwnerRenewPayment
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
            this.RenewServerDirectoryIDCB = new System.Windows.Forms.ComboBox();
            this.RenewPaymentBTN = new System.Windows.Forms.Button();
            this.CreateRenewPaymentBTN = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.RenewCheckOutPageURLTB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.RenewOrderIDTB = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // RenewServerDirectoryIDCB
            // 
            this.RenewServerDirectoryIDCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RenewServerDirectoryIDCB.FormattingEnabled = true;
            this.RenewServerDirectoryIDCB.Location = new System.Drawing.Point(285, 327);
            this.RenewServerDirectoryIDCB.Name = "RenewServerDirectoryIDCB";
            this.RenewServerDirectoryIDCB.Size = new System.Drawing.Size(250, 28);
            this.RenewServerDirectoryIDCB.TabIndex = 23;
            // 
            // RenewPaymentBTN
            // 
            this.RenewPaymentBTN.Location = new System.Drawing.Point(285, 377);
            this.RenewPaymentBTN.Name = "RenewPaymentBTN";
            this.RenewPaymentBTN.Size = new System.Drawing.Size(250, 61);
            this.RenewPaymentBTN.TabIndex = 16;
            this.RenewPaymentBTN.Text = "Renew Payment";
            this.RenewPaymentBTN.UseVisualStyleBackColor = true;
            this.RenewPaymentBTN.Click += new System.EventHandler(this.RenewPaymentBTN_Click);
            // 
            // CreateRenewPaymentBTN
            // 
            this.CreateRenewPaymentBTN.Location = new System.Drawing.Point(285, 234);
            this.CreateRenewPaymentBTN.Name = "CreateRenewPaymentBTN";
            this.CreateRenewPaymentBTN.Size = new System.Drawing.Size(250, 52);
            this.CreateRenewPaymentBTN.TabIndex = 22;
            this.CreateRenewPaymentBTN.Text = "Create Payment For Renewal";
            this.CreateRenewPaymentBTN.UseVisualStyleBackColor = true;
            this.CreateRenewPaymentBTN.Click += new System.EventHandler(this.CreateRenewPaymentBTN_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(285, 304);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(197, 20);
            this.label7.TabIndex = 19;
            this.label7.Text = "Renew Server Directory ID";
            // 
            // RenewCheckOutPageURLTB
            // 
            this.RenewCheckOutPageURLTB.Location = new System.Drawing.Point(285, 137);
            this.RenewCheckOutPageURLTB.Multiline = true;
            this.RenewCheckOutPageURLTB.Name = "RenewCheckOutPageURLTB";
            this.RenewCheckOutPageURLTB.ReadOnly = true;
            this.RenewCheckOutPageURLTB.Size = new System.Drawing.Size(250, 82);
            this.RenewCheckOutPageURLTB.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(281, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(124, 20);
            this.label6.TabIndex = 17;
            this.label6.Text = "Renew Order ID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(281, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(212, 20);
            this.label5.TabIndex = 20;
            this.label5.Text = "Renew CheckOut Page URL";
            // 
            // RenewOrderIDTB
            // 
            this.RenewOrderIDTB.Location = new System.Drawing.Point(285, 31);
            this.RenewOrderIDTB.Multiline = true;
            this.RenewOrderIDTB.Name = "RenewOrderIDTB";
            this.RenewOrderIDTB.ReadOnly = true;
            this.RenewOrderIDTB.Size = new System.Drawing.Size(250, 58);
            this.RenewOrderIDTB.TabIndex = 18;
            // 
            // OwnerRenewPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.RenewServerDirectoryIDCB);
            this.Controls.Add(this.RenewPaymentBTN);
            this.Controls.Add(this.CreateRenewPaymentBTN);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.RenewCheckOutPageURLTB);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.RenewOrderIDTB);
            this.Name = "OwnerRenewPayment";
            this.Text = "OwnerRenewPayment";
            this.Load += new System.EventHandler(this.OwnerRenewPayment_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox RenewServerDirectoryIDCB;
        private System.Windows.Forms.Button RenewPaymentBTN;
        private System.Windows.Forms.Button CreateRenewPaymentBTN;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox RenewCheckOutPageURLTB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox RenewOrderIDTB;
    }
}