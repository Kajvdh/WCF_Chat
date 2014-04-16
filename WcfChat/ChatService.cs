using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfChat
{
    //InstanceContextMode.Single --> Er is maar 1 object dat met alle clients zal communiceren
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ChatService :IChatService
    {
        private ChatEngine mainEngine=new ChatEngine();

        //Client met de server laten verbinden
        public ChatUser ClientConnect(string userName)
        {
            return mainEngine.AddNewChatUser(new ChatUser() { UserName = userName });        
        }

        //Berichten ophalen van de server
        public List<ChatMessage> GetNewMessages(ChatUser user)
        {
            return mainEngine.GetNewMessages(user);
        }

        //Bericht naar de server sturen
        public void SendNewMessage(ChatMessage newMessage)
        {
            mainEngine.AddNewMessage(newMessage);
        }

        //Lijst van de gebruikers verbonden op de server opvragen
        public List<ChatUser> GetAllUsers()
        {
            return mainEngine.ConnectedUsers;
        }

        //Gebruiker van de server verwijderen
        public void RemoveUser(ChatUser user)
        {
            mainEngine.RemoveUser(user);
        }
    }
}
