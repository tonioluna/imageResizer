namespace ImageResizer
{
    partial class mainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.profileSelector = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.profileLabel = new System.Windows.Forms.Label();
            this.inputFolderTB = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.inputFolderGroupBox = new System.Windows.Forms.GroupBox();
            this.outputFolderGroupBox = new System.Windows.Forms.GroupBox();
            this.OutputFolderWarningLabel = new System.Windows.Forms.Label();
            this.outFolAutoSelCB = new System.Windows.Forms.CheckBox();
            this.outputFolderTB = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.outputParamsBox = new System.Windows.Forms.GroupBox();
            this.resizeMethodCmBx = new System.Windows.Forms.ComboBox();
            this.methodLabel = new System.Windows.Forms.Label();
            this.heightTB = new System.Windows.Forms.TextBox();
            this.widthTB = new System.Windows.Forms.TextBox();
            this.heigthLabel = new System.Windows.Forms.Label();
            this.widthLabel = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.currentImgLabel = new System.Windows.Forms.Label();
            this.backgroundProcessor = new System.ComponentModel.BackgroundWorker();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languajeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.españolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLoggerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.doBulkRunToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resizeMethodsHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.releaseNotesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patitoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fffffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rrrrToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inputFolderGroupBox.SuspendLayout();
            this.outputFolderGroupBox.SuspendLayout();
            this.outputParamsBox.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // profileSelector
            // 
            this.profileSelector.FormattingEnabled = true;
            this.profileSelector.Location = new System.Drawing.Point(54, 34);
            this.profileSelector.Name = "profileSelector";
            this.profileSelector.Size = new System.Drawing.Size(206, 21);
            this.profileSelector.TabIndex = 0;
            this.profileSelector.SelectedIndexChanged += new System.EventHandler(this.profileSelector_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(266, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(57, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // profileLabel
            // 
            this.profileLabel.AutoSize = true;
            this.profileLabel.Location = new System.Drawing.Point(12, 37);
            this.profileLabel.Name = "profileLabel";
            this.profileLabel.Size = new System.Drawing.Size(36, 13);
            this.profileLabel.TabIndex = 2;
            this.profileLabel.Text = "Profile";
            // 
            // inputFolderTB
            // 
            this.inputFolderTB.AllowDrop = true;
            this.inputFolderTB.Location = new System.Drawing.Point(6, 19);
            this.inputFolderTB.Name = "inputFolderTB";
            this.inputFolderTB.Size = new System.Drawing.Size(426, 20);
            this.inputFolderTB.TabIndex = 4;
            this.inputFolderTB.TextChanged += new System.EventHandler(this.inputFolderTB_TextChanged);
            this.inputFolderTB.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop_InputFolder);
            this.inputFolderTB.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter_InputFolder);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(438, 17);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(29, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // inputFolderGroupBox
            // 
            this.inputFolderGroupBox.Controls.Add(this.inputFolderTB);
            this.inputFolderGroupBox.Controls.Add(this.button2);
            this.inputFolderGroupBox.Location = new System.Drawing.Point(12, 61);
            this.inputFolderGroupBox.Name = "inputFolderGroupBox";
            this.inputFolderGroupBox.Size = new System.Drawing.Size(473, 53);
            this.inputFolderGroupBox.TabIndex = 5;
            this.inputFolderGroupBox.TabStop = false;
            this.inputFolderGroupBox.Text = "Input Folder";
            this.inputFolderGroupBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop_InputFolder);
            this.inputFolderGroupBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter_InputFolder);
            // 
            // outputFolderGroupBox
            // 
            this.outputFolderGroupBox.Controls.Add(this.OutputFolderWarningLabel);
            this.outputFolderGroupBox.Controls.Add(this.outFolAutoSelCB);
            this.outputFolderGroupBox.Controls.Add(this.outputFolderTB);
            this.outputFolderGroupBox.Controls.Add(this.button3);
            this.outputFolderGroupBox.Location = new System.Drawing.Point(12, 120);
            this.outputFolderGroupBox.Name = "outputFolderGroupBox";
            this.outputFolderGroupBox.Size = new System.Drawing.Size(473, 73);
            this.outputFolderGroupBox.TabIndex = 6;
            this.outputFolderGroupBox.TabStop = false;
            this.outputFolderGroupBox.Text = "Output Folder";
            // 
            // OutputFolderWarningLabel
            // 
            this.OutputFolderWarningLabel.AutoSize = true;
            this.OutputFolderWarningLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputFolderWarningLabel.ForeColor = System.Drawing.Color.Red;
            this.OutputFolderWarningLabel.Location = new System.Drawing.Point(145, 20);
            this.OutputFolderWarningLabel.Name = "OutputFolderWarningLabel";
            this.OutputFolderWarningLabel.Size = new System.Drawing.Size(117, 13);
            this.OutputFolderWarningLabel.TabIndex = 6;
            this.OutputFolderWarningLabel.Text = "Warn_Default_Text";
            // 
            // outFolAutoSelCB
            // 
            this.outFolAutoSelCB.AutoSize = true;
            this.outFolAutoSelCB.Location = new System.Drawing.Point(6, 19);
            this.outFolAutoSelCB.Name = "outFolAutoSelCB";
            this.outFolAutoSelCB.Size = new System.Drawing.Size(120, 17);
            this.outFolAutoSelCB.TabIndex = 5;
            this.outFolAutoSelCB.Text = "Automatic Selection";
            this.outFolAutoSelCB.UseVisualStyleBackColor = true;
            this.outFolAutoSelCB.CheckedChanged += new System.EventHandler(this.outFolAutoSelCB_CheckedChanged);
            // 
            // outputFolderTB
            // 
            this.outputFolderTB.Location = new System.Drawing.Point(6, 41);
            this.outputFolderTB.Name = "outputFolderTB";
            this.outputFolderTB.Size = new System.Drawing.Size(426, 20);
            this.outputFolderTB.TabIndex = 4;
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(438, 39);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(29, 23);
            this.button3.TabIndex = 1;
            this.button3.Text = "...";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // outputParamsBox
            // 
            this.outputParamsBox.Controls.Add(this.resizeMethodCmBx);
            this.outputParamsBox.Controls.Add(this.methodLabel);
            this.outputParamsBox.Controls.Add(this.heightTB);
            this.outputParamsBox.Controls.Add(this.widthTB);
            this.outputParamsBox.Controls.Add(this.heigthLabel);
            this.outputParamsBox.Controls.Add(this.widthLabel);
            this.outputParamsBox.Location = new System.Drawing.Point(12, 199);
            this.outputParamsBox.Name = "outputParamsBox";
            this.outputParamsBox.Size = new System.Drawing.Size(473, 52);
            this.outputParamsBox.TabIndex = 7;
            this.outputParamsBox.TabStop = false;
            this.outputParamsBox.Text = "Output Parameters";
            // 
            // resizeMethodCmBx
            // 
            this.resizeMethodCmBx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.resizeMethodCmBx.FormattingEnabled = true;
            this.resizeMethodCmBx.Location = new System.Drawing.Point(300, 19);
            this.resizeMethodCmBx.Name = "resizeMethodCmBx";
            this.resizeMethodCmBx.Size = new System.Drawing.Size(167, 21);
            this.resizeMethodCmBx.TabIndex = 3;
            // 
            // methodLabel
            // 
            this.methodLabel.AutoSize = true;
            this.methodLabel.Location = new System.Drawing.Point(251, 23);
            this.methodLabel.Name = "methodLabel";
            this.methodLabel.Size = new System.Drawing.Size(43, 13);
            this.methodLabel.TabIndex = 2;
            this.methodLabel.Text = "Method";
            // 
            // heightTB
            // 
            this.heightTB.Location = new System.Drawing.Point(166, 20);
            this.heightTB.Name = "heightTB";
            this.heightTB.Size = new System.Drawing.Size(64, 20);
            this.heightTB.TabIndex = 1;
            // 
            // widthTB
            // 
            this.widthTB.Location = new System.Drawing.Point(49, 20);
            this.widthTB.Name = "widthTB";
            this.widthTB.Size = new System.Drawing.Size(64, 20);
            this.widthTB.TabIndex = 1;
            // 
            // heigthLabel
            // 
            this.heigthLabel.AutoSize = true;
            this.heigthLabel.Location = new System.Drawing.Point(123, 23);
            this.heigthLabel.Name = "heigthLabel";
            this.heigthLabel.Size = new System.Drawing.Size(38, 13);
            this.heigthLabel.TabIndex = 0;
            this.heigthLabel.Text = "Height";
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Location = new System.Drawing.Point(6, 23);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(35, 13);
            this.widthLabel.TabIndex = 0;
            this.widthLabel.Text = "Width";
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(329, 32);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(57, 23);
            this.button4.TabIndex = 1;
            this.button4.Text = "Delete";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // startButton
            // 
            this.startButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startButton.Location = new System.Drawing.Point(12, 264);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(89, 28);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(118, 256);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(367, 23);
            this.progressBar.TabIndex = 8;
            // 
            // currentImgLabel
            // 
            this.currentImgLabel.Location = new System.Drawing.Point(118, 284);
            this.currentImgLabel.Name = "currentImgLabel";
            this.currentImgLabel.Size = new System.Drawing.Size(367, 15);
            this.currentImgLabel.TabIndex = 9;
            this.currentImgLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // backgroundProcessor
            // 
            this.backgroundProcessor.WorkerReportsProgress = true;
            this.backgroundProcessor.WorkerSupportsCancellation = true;
            this.backgroundProcessor.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundProcessor_DoWork);
            this.backgroundProcessor.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundProcessor_RunWorkerCompleted);
            this.backgroundProcessor.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundProcessor_ProgressChanged);
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(497, 24);
            this.mainMenuStrip.TabIndex = 10;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.languajeToolStripMenuItem,
            this.showLoggerToolStripMenuItem,
            this.doBulkRunToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // languajeToolStripMenuItem
            // 
            this.languajeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.englishToolStripMenuItem,
            this.españolToolStripMenuItem});
            this.languajeToolStripMenuItem.Name = "languajeToolStripMenuItem";
            this.languajeToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.languajeToolStripMenuItem.Text = "Languaje";
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.englishToolStripMenuItem.Tag = "English";
            this.englishToolStripMenuItem.Text = "English";
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.languageToolStripMenuItem_Click);
            // 
            // españolToolStripMenuItem
            // 
            this.españolToolStripMenuItem.Name = "españolToolStripMenuItem";
            this.españolToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.españolToolStripMenuItem.Tag = "Spanish";
            this.españolToolStripMenuItem.Text = "Español";
            this.españolToolStripMenuItem.Click += new System.EventHandler(this.languageToolStripMenuItem_Click);
            // 
            // showLoggerToolStripMenuItem
            // 
            this.showLoggerToolStripMenuItem.Name = "showLoggerToolStripMenuItem";
            this.showLoggerToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.showLoggerToolStripMenuItem.Text = "Show Logger";
            this.showLoggerToolStripMenuItem.Click += new System.EventHandler(this.showLoggerToolStripMenuItem_Click);
            // 
            // doBulkRunToolStripMenuItem
            // 
            this.doBulkRunToolStripMenuItem.Name = "doBulkRunToolStripMenuItem";
            this.doBulkRunToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.doBulkRunToolStripMenuItem.Text = "Do Bulk Run";
            this.doBulkRunToolStripMenuItem.Click += new System.EventHandler(this.doBulkRunToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resizeMethodsHelpToolStripMenuItem,
            this.releaseNotesToolStripMenuItem,
            this.toolStripSeparator1,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // resizeMethodsHelpToolStripMenuItem
            // 
            this.resizeMethodsHelpToolStripMenuItem.Name = "resizeMethodsHelpToolStripMenuItem";
            this.resizeMethodsHelpToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.resizeMethodsHelpToolStripMenuItem.Text = "Resize Methods Help";
            this.resizeMethodsHelpToolStripMenuItem.Click += new System.EventHandler(this.resizeMethodsHelpToolStripMenuItem_Click);
            // 
            // releaseNotesToolStripMenuItem
            // 
            this.releaseNotesToolStripMenuItem.Name = "releaseNotesToolStripMenuItem";
            this.releaseNotesToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.releaseNotesToolStripMenuItem.Text = "Release Notes";
            this.releaseNotesToolStripMenuItem.Click += new System.EventHandler(this.releaseNotesToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(181, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // patitoToolStripMenuItem
            // 
            this.patitoToolStripMenuItem.Name = "patitoToolStripMenuItem";
            this.patitoToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.patitoToolStripMenuItem.Text = "patito";
            // 
            // fffffToolStripMenuItem
            // 
            this.fffffToolStripMenuItem.Name = "fffffToolStripMenuItem";
            this.fffffToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.fffffToolStripMenuItem.Text = "fffff";
            // 
            // rrrrToolStripMenuItem
            // 
            this.rrrrToolStripMenuItem.Name = "rrrrToolStripMenuItem";
            this.rrrrToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.rrrrToolStripMenuItem.Text = "rrrr";
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 321);
            this.Controls.Add(this.currentImgLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.outputParamsBox);
            this.Controls.Add(this.outputFolderGroupBox);
            this.Controls.Add(this.inputFolderGroupBox);
            this.Controls.Add(this.profileLabel);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.profileSelector);
            this.Controls.Add(this.mainMenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenuStrip;
            this.MaximizeBox = false;
            this.Name = "mainForm";
            this.Text = "Image Resizer";
            this.inputFolderGroupBox.ResumeLayout(false);
            this.inputFolderGroupBox.PerformLayout();
            this.outputFolderGroupBox.ResumeLayout(false);
            this.outputFolderGroupBox.PerformLayout();
            this.outputParamsBox.ResumeLayout(false);
            this.outputParamsBox.PerformLayout();
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox profileSelector;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label profileLabel;
        private System.Windows.Forms.TextBox inputFolderTB;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox inputFolderGroupBox;
        private System.Windows.Forms.GroupBox outputFolderGroupBox;
        private System.Windows.Forms.Label OutputFolderWarningLabel;
        private System.Windows.Forms.CheckBox outFolAutoSelCB;
        private System.Windows.Forms.TextBox outputFolderTB;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox outputParamsBox;
        private System.Windows.Forms.ComboBox resizeMethodCmBx;
        private System.Windows.Forms.Label methodLabel;
        private System.Windows.Forms.TextBox heightTB;
        private System.Windows.Forms.TextBox widthTB;
        private System.Windows.Forms.Label heigthLabel;
        private System.Windows.Forms.Label widthLabel;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label currentImgLabel;
        private System.ComponentModel.BackgroundWorker backgroundProcessor;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem languajeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem españolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showLoggerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patitoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fffffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rrrrToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem doBulkRunToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resizeMethodsHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem releaseNotesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

