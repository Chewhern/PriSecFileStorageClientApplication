
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
            this.VerifyPaymentBTN.Location = new System.Drawing.Point(318, 509);
            this.VerifyPaymentBTN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.VerifyPaymentBTN.Name = "VerifyPaymentBTN";
            this.VerifyPaymentBTN.Size = new System.Drawing.Size(278, 76);
            this.VerifyPaymentBTN.TabIndex = 17;
            this.VerifyPaymentBTN.Text = "Verify Payment";
            this.VerifyPaymentBTN.UseVisualStyleBackColor = true;
            this.VerifyPaymentBTN.Click += new System.EventHandler(this.VerifyPaymentBTN_Click);
            // 
            // DirectoryIDTB
            // 
            this.DirectoryIDTB.Location = new System.Drawing.Point(318, 418);
            this.DirectoryIDTB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DirectoryIDTB.Multiline = true;
            this.DirectoryIDTB.Name = "DirectoryIDTB";
            this.DirectoryIDTB.ReadOnly = true;
            this.DirectoryIDTB.Size = new System.Drawing.Size(277, 72);
            this.DirectoryIDTB.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(318, 389);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 25);
            this.label4.TabIndex = 15;
            this.label4.Text = "Server Directory ID";
            // 
            // CreatePaymentBTN
            // 
            this.CreatePaymentBTN.Location = new System.Drawing.Point(318, 301);
            this.CreatePaymentBTN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CreatePaymentBTN.Name = "CreatePaymentBTN";
            this.CreatePaymentBTN.Size = new System.Drawing.Size(278, 65);
            this.CreatePaymentBTN.TabIndex = 14;
            this.CreatePaymentBTN.Text = "Create Payment";
            this.CreatePaymentBTN.UseVisualStyleBackColor = true;
            this.CreatePaymentBTN.Click += new System.EventHandler(this.CreatePaymentBTN_Click);
            // 
            // CheckOutPageURLTB
            // 
            this.CheckOutPageURLTB.Location = new System.Drawing.Point(318, 180);
            this.CheckOutPageURLTB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CheckOutPageURLTB.Multiline = true;
            this.CheckOutPageURLTB.Name = "CheckOutPageURLTB";
            this.CheckOutPageURLTB.ReadOnly = true;
            this.CheckOutPageURLTB.Size = new System.Drawing.Size(277, 102);
            this.CheckOutPageURLTB.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(313, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(272, 25);
            this.label2.TabIndex = 12;
            this.label2.Text = "Make Payment Through This URL";
            // 
            // OrderIDTB
            // 
            this.OrderIDTB.Location = new System.Drawing.Point(318, 48);
            this.OrderIDTB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.OrderIDTB.Multiline = true;
            this.OrderIDTB.Name = "OrderIDTB";
            this.OrderIDTB.ReadOnly = true;
            this.OrderIDTB.Size = new System.Drawing.Size(277, 72);
            this.OrderIDTB.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(313, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 25);
            this.label1.TabIndex = 10;
            this.label1.Text = "Order ID";
            // 
            // OwnerMakePayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 601);
            this.Controls.Add(this.VerifyPaymentBTN);
            this.Controls.Add(this.DirectoryIDTB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CreatePaymentBTN);
            this.Controls.Add(this.CheckOutPageURLTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.OrderIDTB);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
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