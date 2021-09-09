
namespace PriSecFileStorageClient
{
    partial class OwnerMakePayment
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
            this.VerifyPaymentBTN = new System.Windows.Forms.Button();
            this.DirectoryIDTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CreatePaymentBTN = new System.Windows.Forms.Button();
            this.CheckOutPageURLTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.OrderIDTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // VerifyPaymentBTN
            // 
            this.VerifyPaymentBTN.Location = new System.Drawing.Point(286, 407);
            this.VerifyPaymentBTN.Name = "VerifyPaymentBTN";
            this.VerifyPaymentBTN.Size = new System.Drawing.Size(250, 61);
            this.VerifyPaymentBTN.TabIndex = 17;
            this.VerifyPaymentBTN.Text = "Verify Payment";
            this.VerifyPaymentBTN.UseVisualStyleBackColor = true;
            this.VerifyPaymentBTN.Click += new System.EventHandler(this.VerifyPaymentBTN_Click);
            // 
            // DirectoryIDTB
            // 
            this.DirectoryIDTB.Location = new System.Drawing.Point(286, 334);
            this.DirectoryIDTB.Multiline = true;
            this.DirectoryIDTB.Name = "DirectoryIDTB";
            this.DirectoryIDTB.ReadOnly = true;
            this.DirectoryIDTB.Size = new System.Drawing.Size(250, 58);
            this.DirectoryIDTB.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(286, 311);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 20);
            this.label4.TabIndex = 15;
            this.label4.Text = "Server Directory ID";
            // 
            // CreatePaymentBTN
            // 
            this.CreatePaymentBTN.Location = new System.Drawing.Point(286, 241);
            this.CreatePaymentBTN.Name = "CreatePaymentBTN";
            this.CreatePaymentBTN.Size = new System.Drawing.Size(250, 52);
            this.CreatePaymentBTN.TabIndex = 14;
            this.CreatePaymentBTN.Text = "Create Payment";
            this.CreatePaymentBTN.UseVisualStyleBackColor = true;
            this.CreatePaymentBTN.Click += new System.EventHandler(this.CreatePaymentBTN_Click);
            // 
            // CheckOutPageURLTB
            // 
            this.CheckOutPageURLTB.Location = new System.Drawing.Point(286, 144);
            this.CheckOutPageURLTB.Multiline = true;
            this.CheckOutPageURLTB.Name = "CheckOutPageURLTB";
            this.CheckOutPageURLTB.ReadOnly = true;
            this.CheckOutPageURLTB.Size = new System.Drawing.Size(250, 82);
            this.CheckOutPageURLTB.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(282, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(247, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "Make Payment Through This URL";
            // 
            // OrderIDTB
            // 
            this.OrderIDTB.Location = new System.Drawing.Point(286, 38);
            this.OrderIDTB.Multiline = true;
            this.OrderIDTB.Name = "OrderIDTB";
            this.OrderIDTB.ReadOnly = true;
            this.OrderIDTB.Size = new System.Drawing.Size(250, 58);
            this.OrderIDTB.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(282, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Order ID";
            // 
            // OwnerMakePayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 481);
            this.Controls.Add(this.VerifyPaymentBTN);
            this.Controls.Add(this.DirectoryIDTB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CreatePaymentBTN);
            this.Controls.Add(this.CheckOutPageURLTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.OrderIDTB);
            this.Controls.Add(this.label1);
            this.Name = "OwnerMakePayment";
            this.Text = "OwnerMakePayment";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button VerifyPaymentBTN;
        private System.Windows.Forms.TextBox DirectoryIDTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button CreatePaymentBTN;
        private System.Windows.Forms.TextBox CheckOutPageURLTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox OrderIDTB;
        private System.Windows.Forms.Label label1;
    }
}