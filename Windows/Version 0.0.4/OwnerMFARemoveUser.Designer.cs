
namespace PriSecFileStorageClient
{
    partial class OwnerMFARemoveUser
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
            this.AccessIDCB = new System.Windows.Forms.ComboBox();
            this.RemoveAccessBTN = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SecondMFADeviceIDTB = new System.Windows.Forms.TextBox();
            this.FirstMFADeviceIDTB = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AccessIDCB
            // 
            this.AccessIDCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AccessIDCB.FormattingEnabled = true;
            this.AccessIDCB.Location = new System.Drawing.Point(12, 32);
            this.AccessIDCB.Name = "AccessIDCB";
            this.AccessIDCB.Size = new System.Drawing.Size(273, 28);
            this.AccessIDCB.TabIndex = 17;
            this.AccessIDCB.SelectedIndexChanged += new System.EventHandler(this.AccessIDCB_SelectedIndexChanged);
            // 
            // RemoveAccessBTN
            // 
            this.RemoveAccessBTN.Location = new System.Drawing.Point(12, 352);
            this.RemoveAccessBTN.Name = "RemoveAccessBTN";
            this.RemoveAccessBTN.Size = new System.Drawing.Size(282, 66);
            this.RemoveAccessBTN.TabIndex = 16;
            this.RemoveAccessBTN.Text = "Remove Access From User";
            this.RemoveAccessBTN.UseVisualStyleBackColor = true;
            this.RemoveAccessBTN.Click += new System.EventHandler(this.RemoveAccessBTN_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "Allowed User Access ID";
            // 
            // SecondMFADeviceIDTB
            // 
            this.SecondMFADeviceIDTB.Location = new System.Drawing.Point(12, 238);
            this.SecondMFADeviceIDTB.Multiline = true;
            this.SecondMFADeviceIDTB.Name = "SecondMFADeviceIDTB";
            this.SecondMFADeviceIDTB.Size = new System.Drawing.Size(273, 98);
            this.SecondMFADeviceIDTB.TabIndex = 47;
            // 
            // FirstMFADeviceIDTB
            // 
            this.FirstMFADeviceIDTB.Location = new System.Drawing.Point(12, 95);
            this.FirstMFADeviceIDTB.Multiline = true;
            this.FirstMFADeviceIDTB.Name = "FirstMFADeviceIDTB";
            this.FirstMFADeviceIDTB.Size = new System.Drawing.Size(273, 98);
            this.FirstMFADeviceIDTB.TabIndex = 46;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 205);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(175, 20);
            this.label8.TabIndex = 45;
            this.label8.Text = "Second MFA Device ID";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(151, 20);
            this.label7.TabIndex = 44;
            this.label7.Text = "First MFA Device ID";
            // 
            // OwnerMFARemoveUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.SecondMFADeviceIDTB);
            this.Controls.Add(this.FirstMFADeviceIDTB);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.AccessIDCB);
            this.Controls.Add(this.RemoveAccessBTN);
            this.Controls.Add(this.label2);
            this.Name = "OwnerMFARemoveUser";
            this.Text = "OwnerMFARemoveUser";
            this.Load += new System.EventHandler(this.OwnerMFARemoveUser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox AccessIDCB;
        private System.Windows.Forms.Button RemoveAccessBTN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox SecondMFADeviceIDTB;
        private System.Windows.Forms.TextBox FirstMFADeviceIDTB;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
    }
}