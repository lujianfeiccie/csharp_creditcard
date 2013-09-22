using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace creditcard
{
    public partial class FormProgressDialog : Form
    {
        public FormProgressDialog()
        {
            InitializeComponent();
        }

        private void FormProgressDialog_Load(object sender, EventArgs e)
        {
        }
        public string Title
        {
            get { return this.Text; }
            set { this.Text = value; }
        }
        public string Message
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }
    }
}