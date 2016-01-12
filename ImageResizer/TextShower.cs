using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImageResizer
{
    public partial class TextShower : Form
    {
        public TextShower()
        {
            InitializeComponent();
        }

        private void TextShower_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        internal void set_text(string p)
        {
            this.mainTextBox.Text = p;
            this.mainTextBox.SelectionStart = 0;
            this.mainTextBox.SelectionLength = 0;
        }
    }
}
