using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfChat
{
    public class ChatEngine
    {
        private List<ChatUser> connectedUsers = new List<ChatUser>();
        private Dictionary<string,List<ChatMessage>> incomingMessages= new Dictionary<string,List<ChatMessage>>();      
        public List<ChatUser> ConnectedUsers
        {
            get { return connectedUsers; }
        }

        //ChatUser object aanmaken voor nieuwe gebruiker
        public ChatUser AddNewChatUser(ChatUser user)
        {           
            var exists =
                from ChatUser e in this.ConnectedUsers
                where e.UserName == user.UserName
                select e;

            if (exists.Count() == 0)
            {
                this.ConnectedUsers.Add(user);
                incomingMessages.Add(user.UserName, new List<ChatMessage>() { 
                    new ChatMessage(){User=new ChatUser{UserName="Global"},Message=user+" is het kanaal binnengekomen",Date=DateTime.Now}
                });

                Console.WriteLine("New user connected: " + user);
                return user;
            }
            else
                return null;           
        }

        public void AddNewMessage(ChatMessage newMessage)
        {
            Console.WriteLine(newMessage.User.UserName+" says :"+newMessage.Message+" at "+newMessage.Date);
            

            foreach (var user in this.ConnectedUsers)
            {
                if (!newMessage.User.UserName.Equals(user.UserName))
                {
                    incomingMessages[user.UserName].Add(newMessage);                    
                }
            }
        }

        public List<ChatMessage> GetNewMessages(ChatUser user)
        {
            List<ChatMessage> myNewMessages = incomingMessages[user.UserName];  
            incomingMessages[user.UserName]=new List<ChatMessage>();

            if (myNewMessages.Count > 0)
                return myNewMessages;
            else
                return null;
        }

        public void RemoveUser(ChatUser user)
        {
            this.ConnectedUsers.RemoveAll(u=>u.UserName==user.UserName);
        }
    }
}
