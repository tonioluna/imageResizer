namespace ImageResizer
{
    partial class AboutForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ToolNameLabel = new System.Windows.Forms.Label();
            this.publisherLabel = new System.Windows.Forms.Label();
            this.versionLabel = new System.Windows.Forms.Label();
            this.dateLabel = new System.Windows.Forms.Label();
            this.supportUrlLabel = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ImageResizer.Properties.Resources.Icon_PNG_150px;
            this.pictureBox1.InitialImage = global::ImageResizer.Properties.Resources.Icon_PNG_150px;
            this.pictureBox1.Location = new System.Drawing.Point(88, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 150);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // ToolNameLabel
            // 
            this.ToolNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToolNameLabel.Location = new System.Drawing.Point(12, 169);
            this.ToolNameLabel.Name = "ToolNameLabel";
            this.ToolNameLabel.Size = new System.Drawing.Size(306, 15);
            this.ToolNameLabel.TabIndex = 1;
            this.ToolNameLabel.Text = "label1";
            this.ToolNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // publisherLabel
            // 
            this.publisherLabel.Location = new System.Drawing.Point(12, 184);
            this.publisherLabel.Name = "publisherLabel";
            this.publisherLabel.Size = new System.Drawing.Size(306, 15);
            this.publisherLabel.TabIndex = 2;
            this.publisherLabel.Text = "label1";
            this.publisherLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // versionLabel
            // 
            this.versionLabel.Location = new System.Drawing.Point(12, 212);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(306, 15);
            this.versionLabel.TabIndex = 2;
            this.versionLabel.Text = "label1";
            this.versionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dateLabel
            // 
            this.dateLabel.Location = new System.Drawing.Point(12, 227);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(306, 15);
            this.dateLabel.TabIndex = 2;
            this.dateLabel.Text = "label1";
            this.dateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // supportUrlLabel
            // 
            this.supportUrlLabel.Location = new System.Drawing.Point(15, 260);
            this.supportUrlLabel.Name = "supportUrlLabel";
            this.supportUrlLabel.Size = new System.Drawing.Size(303, 23);
            this.supportUrlLabel.TabIndex = 3;
            this.supportUrlLabel.TabStop = true;
            this.supportUrlLabel.Text = "linkLabel1";
            this.supportUrlLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.supportUrlLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.supportUrlLabel_LinkClicked);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 292);
            this.Controls.Add(this.supportUrlLabel);
            this.Controls.Add(this.dateLabel);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.publisherLabel);
            this.Controls.Add(this.ToolNameLabel);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AboutBox1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Label ToolNameLabel;
        public System.Windows.Forms.Label publisherLabel;
        public System.Windows.Forms.Label versionLabel;
        public System.Windows.Forms.Label dateLabel;
        private System.Windows.Forms.LinkLabel supportUrlLabel;

    }
}
