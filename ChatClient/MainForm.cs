using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using WcfChat;
using System.ServiceModel;

namespace ChatClient
{
    public partial class MainForm : Form
    {
        private ChannelFactory<IChatService> remoteFactory;
        private IChatService remoteProxy;
        private ChatUser clientUser;
        private bool isConnected = false;

        public MainForm()
        {
            InitializeComponent();
        }


        /**
         * Inloggen
         */
        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                lblStatus.Text = "Verbinden...";

                /**
                 * Loginform openen
                 */
                LoginForm loginDialog = new LoginForm();
                loginDialog.ShowDialog(this);
                if (!String.IsNullOrEmpty(loginDialog.UserName))
                {
                    //Verbinding maken
                    remoteFactory = new ChannelFactory<IChatService>("ChatConfig");
                    remoteProxy = remoteFactory.CreateChannel();
                    clientUser = remoteProxy.ClientConnect(loginDialog.UserName);

                    if (clientUser != null)
                    {
                        usersTimer.Enabled = true;
                        messagesTimer.Enabled = true;
                        loginToolStripMenuItem.Enabled = false;
                        btnSend.Enabled = true;
                        txtMessage.Enabled = true;
                        isConnected = true;
                        lblStatus.Text = "Verbonden als: " + clientUser.UserName;
                    }
                }
                else
                    lblStatus.Text = "Verbinding verbroken";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Er is een fout opgetreden\nClient kan geen verbinding maken\n:"+ex.Message,
                    "FATAL ERROR",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void usersTimer_Tick(object sender, EventArgs e)
        {
            List<ChatUser> listUsers = remoteProxy.GetAllUsers();
            lstUsers.DataSource = listUsers;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtMessage.Text))
            {
                ChatMessage newMessage = new ChatMessage()
                {
                    Date = DateTime.Now,
                    Message = txtMessage.Text,
                    User = clientUser
                };
                InsertMessage(newMessage);
                remoteProxy.SendNewMessage(newMessage);
                txtMessage.Text = String.Empty;
            }
                
            
        }

        private void messagesTimer_Tick(object sender, EventArgs e)
        {
            List<ChatMessage> messages = remoteProxy.GetNewMessages(clientUser);

            if (messages != null)
                foreach (var message in messages)
                    InsertMessage(message);
             
        }

        private void InsertMessage(ChatMessage message)
        {
            txtChat.AppendText("["+ message.Date + "] " + "<" + message.User.UserName + "> " + message.Message + Environment.NewLine);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isConnected)
            {
                remoteProxy.SendNewMessage(new ChatMessage()
                {
                    Date = DateTime.Now,
                    Message = "i'm logged out",
                    User = clientUser
                });
                remoteProxy.RemoveUser(clientUser);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==(char)13)            
            {
                btnSend_Click(sender, e);
                txtMessage.Text = String.Empty;
            }

        }

        private void txtMessage_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
