
namespace PriSecFileStorageClient
{
    partial class UserImportInformationFromZip
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
            this.ImportBTN = new System.Windows.Forms.Button();
            this.ZipFileChooser = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // ImportBTN
            // 
            this.ImportBTN.Location = new System.Drawing.Point(13, 15);
            this.ImportBTN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ImportBTN.Name = "ImportBTN";
            this.ImportBTN.Size = new System.Drawing.Size(300, 78);
            this.ImportBTN.TabIndex = 8;
            this.ImportBTN.Text = "Import Information/Keys";
            this.ImportBTN.UseVisualStyleBackColor = true;
            this.ImportBTN.Click += new System.EventHandler(this.ImportBTN_Click);
            // 
            // UserImportInformationFromZip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 562);
            this.Controls.Add(this.ImportBTN);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "UserImportInformationFromZip";
            this.Text = "UserImportInformationFromZip";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ImportBTN;
        private System.Windows.Forms.OpenFileDialog ZipFileChooser;
    }
}