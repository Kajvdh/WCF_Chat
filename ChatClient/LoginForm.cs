using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        public string UserName { get; set; }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Regex r = new Regex("^[a-zA-Z0-9]*$");
            if (!r.IsMatch(txtUserName.Text))
            {
                MessageBox.Show("Deze nicknaam bevat speciale karakters", "Error validation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if (txtUserName.Text.Equals("global",StringComparison.InvariantCultureIgnoreCase)) 
            {
                MessageBox.Show("Deze nicknaam mag niet gebruikt worden", "Error validation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if (String.IsNullOrEmpty(txtUserName.Text))
            {
                 MessageBox.Show("Gelieve gebruikersnaam in te vullen", "Error validation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                this.UserName = txtUserName.Text;
                this.Close();
            }
                
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.UserName = String.Empty;
            this.Close();
        }

        private void txtUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnOK_Click(sender, e);
            }


        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
