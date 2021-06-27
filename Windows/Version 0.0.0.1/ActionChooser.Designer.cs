namespace PriSecFileStorageClient
{
    partial class ActionChooser
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
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.VerifyPaymentBTN = new System.Windows.Forms.Button();
            this.DirectoryIDTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CreatePaymentBTN = new System.Windows.Forms.Button();
            this.CheckOutPageURLTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.OrderIDTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.RightPanel = new System.Windows.Forms.Panel();
            this.DirectoryIDComboBox = new System.Windows.Forms.ComboBox();
            this.GoBTN = new System.Windows.Forms.Button();
            this.ActionCB = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.IsOwnerCB = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.MiddlePanel = new System.Windows.Forms.Panel();
            this.RenewServerDirectoryIDCB = new System.Windows.Forms.ComboBox();
            this.RenewPaymentBTN = new System.Windows.Forms.Button();
            this.CreateRenewPaymentBTN = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.RenewCheckOutPageURLTB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.RenewOrderIDTB = new System.Windows.Forms.TextBox();
            this.LeftPanel.SuspendLayout();
            this.RightPanel.SuspendLayout();
            this.MiddlePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LeftPanel
            // 
            this.LeftPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LeftPanel.Controls.Add(this.VerifyPaymentBTN);
            this.LeftPanel.Controls.Add(this.DirectoryIDTB);
            this.LeftPanel.Controls.Add(this.label4);
            this.LeftPanel.Controls.Add(this.CreatePaymentBTN);
            this.LeftPanel.Controls.Add(this.CheckOutPageURLTB);
            this.LeftPanel.Controls.Add(this.label2);
            this.LeftPanel.Controls.Add(this.OrderIDTB);
            this.LeftPanel.Controls.Add(this.label1);
            this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(291, 654);
            this.LeftPanel.TabIndex = 0;
            // 
            // VerifyPaymentBTN
            // 
            this.VerifyPaymentBTN.Location = new System.Drawing.Point(17, 406);
            this.VerifyPaymentBTN.Name = "VerifyPaymentBTN";
            this.VerifyPaymentBTN.Size = new System.Drawing.Size(250, 61);
            this.VerifyPaymentBTN.TabIndex = 9;
            this.VerifyPaymentBTN.Text = "Verify Payment";
            this.VerifyPaymentBTN.UseVisualStyleBackColor = true;
            this.VerifyPaymentBTN.Click += new System.EventHandler(this.VerifyPaymentBTN_Click);
            // 
            // DirectoryIDTB
            // 
            this.DirectoryIDTB.Location = new System.Drawing.Point(17, 333);
            this.DirectoryIDTB.Multiline = true;
            this.DirectoryIDTB.Name = "DirectoryIDTB";
            this.DirectoryIDTB.ReadOnly = true;
            this.DirectoryIDTB.Size = new System.Drawing.Size(250, 58);
            this.DirectoryIDTB.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 310);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Server Directory ID";
            // 
            // CreatePaymentBTN
            // 
            this.CreatePaymentBTN.Location = new System.Drawing.Point(17, 240);
            this.CreatePaymentBTN.Name = "CreatePaymentBTN";
            this.CreatePaymentBTN.Size = new System.Drawing.Size(250, 52);
            this.CreatePaymentBTN.TabIndex = 4;
            this.CreatePaymentBTN.Text = "Create Payment";
            this.CreatePaymentBTN.UseVisualStyleBackColor = true;
            this.CreatePaymentBTN.Click += new System.EventHandler(this.CreatePaymentBTN_Click);
            // 
            // CheckOutPageURLTB
            // 
            this.CheckOutPageURLTB.Location = new System.Drawing.Point(17, 143);
            this.CheckOutPageURLTB.Multiline = true;
            this.CheckOutPageURLTB.Name = "CheckOutPageURLTB";
            this.CheckOutPageURLTB.ReadOnly = true;
            this.CheckOutPageURLTB.Size = new System.Drawing.Size(250, 82);
            this.CheckOutPageURLTB.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "CheckOut Page URL";
            // 
            // OrderIDTB
            // 
            this.OrderIDTB.Location = new System.Drawing.Point(17, 37);
            this.OrderIDTB.Multiline = true;
            this.OrderIDTB.Name = "OrderIDTB";
            this.OrderIDTB.ReadOnly = true;
            this.OrderIDTB.Size = new System.Drawing.Size(250, 58);
            this.OrderIDTB.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Order ID";
            // 
            // RightPanel
            // 
            this.RightPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RightPanel.Controls.Add(this.DirectoryIDComboBox);
            this.RightPanel.Controls.Add(this.GoBTN);
            this.RightPanel.Controls.Add(this.ActionCB);
            this.RightPanel.Controls.Add(this.label11);
            this.RightPanel.Controls.Add(this.IsOwnerCB);
            this.RightPanel.Controls.Add(this.label10);
            this.RightPanel.Controls.Add(this.label9);
            this.RightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightPanel.Location = new System.Drawing.Point(654, 0);
            this.RightPanel.Name = "RightPanel";
            this.RightPanel.Size = new System.Drawing.Size(363, 654);
            this.RightPanel.TabIndex = 1;
            // 
            // DirectoryIDComboBox
            // 
            this.DirectoryIDComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DirectoryIDComboBox.FormattingEnabled = true;
            this.DirectoryIDComboBox.Location = new System.Drawing.Point(24, 37);
            this.DirectoryIDComboBox.Name = "DirectoryIDComboBox";
            this.DirectoryIDComboBox.Size = new System.Drawing.Size(320, 28);
            this.DirectoryIDComboBox.TabIndex = 16;
            // 
            // GoBTN
            // 
            this.GoBTN.Location = new System.Drawing.Point(24, 250);
            this.GoBTN.Name = "GoBTN";
            this.GoBTN.Size = new System.Drawing.Size(320, 61);
            this.GoBTN.TabIndex = 15;
            this.GoBTN.Text = "Go";
            this.GoBTN.UseVisualStyleBackColor = true;
            this.GoBTN.Click += new System.EventHandler(this.GoBTN_Click);
            // 
            // ActionCB
            // 
            this.ActionCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ActionCB.FormattingEnabled = true;
            this.ActionCB.Items.AddRange(new object[] {
            "Upload Endpoint Encrypted Files",
            "Compare Endpoint Encrypted Files From Server Locally",
            "Decrypt Fetched Endpoint Encrypted Files From Server",
            "Delete Endpoint Encrypted Files From Server",
            "Backup information locally",
            "Create offline E2EE session(No Receiving Chain)[Pending update]",
            "Upload Digital Signature Public Key of Allowed User[Pending update]",
            "Upload E2EE encrypted information[Pending update]",
            "Fetch E2EE encrypted information[Pending update]"});
            this.ActionCB.Location = new System.Drawing.Point(24, 197);
            this.ActionCB.Name = "ActionCB";
            this.ActionCB.Size = new System.Drawing.Size(320, 28);
            this.ActionCB.TabIndex = 5;
            this.ActionCB.SelectedIndexChanged += new System.EventHandler(this.ActionCB_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(20, 165);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(185, 20);
            this.label11.TabIndex = 4;
            this.label11.Text = "What do you want to do?";
            // 
            // IsOwnerCB
            // 
            this.IsOwnerCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.IsOwnerCB.FormattingEnabled = true;
            this.IsOwnerCB.Items.AddRange(new object[] {
            "Yes"});
            this.IsOwnerCB.Location = new System.Drawing.Point(24, 120);
            this.IsOwnerCB.Name = "IsOwnerCB";
            this.IsOwnerCB.Size = new System.Drawing.Size(320, 28);
            this.IsOwnerCB.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(20, 88);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(141, 20);
            this.label10.TabIndex = 2;
            this.label10.Text = "Are you an owner?";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(143, 20);
            this.label9.TabIndex = 0;
            this.label9.Text = "Server Directory ID";
            // 
            // MiddlePanel
            // 
            this.MiddlePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MiddlePanel.Controls.Add(this.RenewServerDirectoryIDCB);
            this.MiddlePanel.Controls.Add(this.RenewPaymentBTN);
            this.MiddlePanel.Controls.Add(this.CreateRenewPaymentBTN);
            this.MiddlePanel.Controls.Add(this.label7);
            this.MiddlePanel.Controls.Add(this.RenewCheckOutPageURLTB);
            this.MiddlePanel.Controls.Add(this.label6);
            this.MiddlePanel.Controls.Add(this.label5);
            this.MiddlePanel.Controls.Add(this.RenewOrderIDTB);
            this.MiddlePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MiddlePanel.Location = new System.Drawing.Point(291, 0);
            this.MiddlePanel.Name = "MiddlePanel";
            this.MiddlePanel.Size = new System.Drawing.Size(363, 654);
            this.MiddlePanel.TabIndex = 2;
            // 
            // RenewServerDirectoryIDCB
            // 
            this.RenewServerDirectoryIDCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RenewServerDirectoryIDCB.FormattingEnabled = true;
            this.RenewServerDirectoryIDCB.Location = new System.Drawing.Point(27, 333);
            this.RenewServerDirectoryIDCB.Name = "RenewServerDirectoryIDCB";
            this.RenewServerDirectoryIDCB.Size = new System.Drawing.Size(250, 28);
            this.RenewServerDirectoryIDCB.TabIndex = 15;
            // 
            // RenewPaymentBTN
            // 
            this.RenewPaymentBTN.Location = new System.Drawing.Point(27, 406);
            this.RenewPaymentBTN.Name = "RenewPaymentBTN";
            this.RenewPaymentBTN.Size = new System.Drawing.Size(250, 61);
            this.RenewPaymentBTN.TabIndex = 10;
            this.RenewPaymentBTN.Text = "Renew Payment";
            this.RenewPaymentBTN.UseVisualStyleBackColor = true;
            this.RenewPaymentBTN.Click += new System.EventHandler(this.RenewPaymentBTN_Click);
            // 
            // CreateRenewPaymentBTN
            // 
            this.CreateRenewPaymentBTN.Location = new System.Drawing.Point(27, 240);
            this.CreateRenewPaymentBTN.Name = "CreateRenewPaymentBTN";
            this.CreateRenewPaymentBTN.Size = new System.Drawing.Size(250, 52);
            this.CreateRenewPaymentBTN.TabIndex = 14;
            this.CreateRenewPaymentBTN.Text = "Create Payment For Renewal";
            this.CreateRenewPaymentBTN.UseVisualStyleBackColor = true;
            this.CreateRenewPaymentBTN.Click += new System.EventHandler(this.CreateRenewPaymentBTN_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 310);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(197, 20);
            this.label7.TabIndex = 12;
            this.label7.Text = "Renew Server Directory ID";
            // 
            // RenewCheckOutPageURLTB
            // 
            this.RenewCheckOutPageURLTB.Location = new System.Drawing.Point(27, 143);
            this.RenewCheckOutPageURLTB.Multiline = true;
            this.RenewCheckOutPageURLTB.Name = "RenewCheckOutPageURLTB";
            this.RenewCheckOutPageURLTB.ReadOnly = true;
            this.RenewCheckOutPageURLTB.Size = new System.Drawing.Size(250, 82);
            this.RenewCheckOutPageURLTB.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(124, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "Renew Order ID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(212, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "Renew CheckOut Page URL";
            // 
            // RenewOrderIDTB
            // 
            this.RenewOrderIDTB.Location = new System.Drawing.Point(27, 37);
            this.RenewOrderIDTB.Multiline = true;
            this.RenewOrderIDTB.Name = "RenewOrderIDTB";
            this.RenewOrderIDTB.ReadOnly = true;
            this.RenewOrderIDTB.Size = new System.Drawing.Size(250, 58);
            this.RenewOrderIDTB.TabIndex = 11;
            // 
            // ActionChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 654);
            this.Controls.Add(this.MiddlePanel);
            this.Controls.Add(this.RightPanel);
            this.Controls.Add(this.LeftPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ActionChooser";
            this.Text = "Action Chooser";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
            this.Load += new System.EventHandler(this.ActionChooser_Load);
            this.LeftPanel.ResumeLayout(false);
            this.LeftPanel.PerformLayout();
            this.RightPanel.ResumeLayout(false);
            this.RightPanel.PerformLayout();
            this.MiddlePanel.ResumeLayout(false);
            this.MiddlePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel LeftPanel;
        private System.Windows.Forms.Button VerifyPaymentBTN;
        private System.Windows.Forms.TextBox DirectoryIDTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button CreatePaymentBTN;
        private System.Windows.Forms.TextBox CheckOutPageURLTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox OrderIDTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel RightPanel;
        private System.Windows.Forms.ComboBox ActionCB;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox IsOwnerCB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel MiddlePanel;
        private System.Windows.Forms.Button RenewPaymentBTN;
        private System.Windows.Forms.Button CreateRenewPaymentBTN;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox RenewCheckOutPageURLTB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox RenewOrderIDTB;
        private System.Windows.Forms.Button GoBTN;
        private System.Windows.Forms.ComboBox DirectoryIDComboBox;
        private System.Windows.Forms.ComboBox RenewServerDirectoryIDCB;
    }
}