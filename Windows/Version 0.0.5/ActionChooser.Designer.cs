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
            this.StatusLabel = new System.Windows.Forms.Label();
            this.YesBTN = new System.Windows.Forms.Button();
            this.NoBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.StatusLabel.Location = new System.Drawing.Point(162, 9);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(352, 29);
            this.StatusLabel.TabIndex = 0;
            this.StatusLabel.Text = "Are you a file storage owner?";
            // 
            // YesBTN
            // 
            this.YesBTN.Enabled = false;
            this.YesBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.YesBTN.Location = new System.Drawing.Point(232, 140);
            this.YesBTN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.YesBTN.Name = "YesBTN";
            this.YesBTN.Size = new System.Drawing.Size(338, 72);
            this.YesBTN.TabIndex = 1;
            this.YesBTN.Text = "Yes";
            this.YesBTN.UseVisualStyleBackColor = true;
            this.YesBTN.Click += new System.EventHandler(this.YesBTN_Click);
            // 
            // NoBTN
            // 
            this.NoBTN.Enabled = false;
            this.NoBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.NoBTN.Location = new System.Drawing.Point(232, 258);
            this.NoBTN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NoBTN.Name = "NoBTN";
            this.NoBTN.Size = new System.Drawing.Size(338, 72);
            this.NoBTN.TabIndex = 2;
            this.NoBTN.Text = "No";
            this.NoBTN.UseVisualStyleBackColor = true;
            this.NoBTN.Click += new System.EventHandler(this.NoBTN_Click);
            // 
            // ActionChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 565);
            this.Controls.Add(this.NoBTN);
            this.Controls.Add(this.YesBTN);
            this.Controls.Add(this.StatusLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "ActionChooser";
            this.Text = "Action Chooser";
            this.Load += new System.EventHandler(this.ActionChooser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Button YesBTN;
        private System.Windows.Forms.Button NoBTN;
    }
}