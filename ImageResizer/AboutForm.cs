using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ImageResizer
{
    partial class AboutForm : Form
    {
        public AboutForm(string title, 
            string tool_name,
            string publisher,
            string version,
            string date,
            string support_url)

        {
            InitializeComponent();
            this.Text = title;
            this.ToolNameLabel.Text = tool_name;
            this.publisherLabel.Text = publisher;
            this.versionLabel.Text = version;
            this.dateLabel.Text = date;
            this.supportUrlLabel.Text = support_url;
        }

        private void supportUrlLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(this.supportUrlLabel.Text);
        }

    }
}
