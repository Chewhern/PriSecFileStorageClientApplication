namespace PriSecFileStorageClient
{
    partial class OwnerDeleteFileContent
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
            this.DeleteFileContentBTN = new System.Windows.Forms.Button();
            this.ServerRandomFileNameTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DeleteFileContentBTN
            // 
            this.DeleteFileContentBTN.Location = new System.Drawing.Point(16, 122);
            this.DeleteFileContentBTN.Name = "DeleteFileContentBTN";
            this.DeleteFileContentBTN.Size = new System.Drawing.Size(273, 53);
            this.DeleteFileContentBTN.TabIndex = 7;
            this.DeleteFileContentBTN.Text = "Delete File From Server And Local";
            this.DeleteFileContentBTN.UseVisualStyleBackColor = true;
            this.DeleteFileContentBTN.Click += new System.EventHandler(this.DeleteFileContentBTN_Click);
            // 
            // ServerRandomFileNameTB
            // 
            this.ServerRandomFileNameTB.Location = new System.Drawing.Point(16, 33);
            this.ServerRandomFileNameTB.Multiline = true;
            this.ServerRandomFileNameTB.Name = "ServerRandomFileNameTB";
            this.ServerRandomFileNameTB.Size = new System.Drawing.Size(273, 74);
            this.ServerRandomFileNameTB.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Server Random File Name";
            // 
            // OwnerDeleteFileContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.DeleteFileContentBTN);
            this.Controls.Add(this.ServerRandomFileNameTB);
            this.Controls.Add(this.label1);
            this.Name = "OwnerDeleteFileContent";
            this.Text = "OwnerDeleteFileContent";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button DeleteFileContentBTN;
        private System.Windows.Forms.TextBox ServerRandomFileNameTB;
        private System.Windows.Forms.Label label1;
    }
}