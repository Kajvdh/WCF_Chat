using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfChat
{
    /**
     * ServiceContract:
     *  Deze interface wordt ter beschikking gesteld van de Client
     * */
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface IChatService
    {
        //Method om in te loggen
        [OperationContract]
        ChatUser ClientConnect(string userName);

        //Method om berichten op te halen
        [OperationContract]
        List<ChatMessage> GetNewMessages(ChatUser user);

        //Method om bericht te versturen
        [OperationContract]
        void SendNewMessage(ChatMessage newMessage);

        //Method om een lijst van alle gebruikers op de chat op te vragen
        [OperationContract]
        List<ChatUser> GetAllUsers();

        //Method om uit te loggen
        [OperationContract]
        void RemoveUser(ChatUser user);        
    }

    /**
    * ChatMessage klasse
    * */
    [DataContract]
    public class ChatMessage
    {
        //Gebruiker die het bericht verstuurd
        private ChatUser user;
        [DataMember]
        public ChatUser User
        {
            get { return user; }
            set { user = value; }
        }

        //Het bericht
        private string message;
        [DataMember]
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        //Het tijdstip waarop het bericht is verstuurd
        private DateTime date;
        [DataMember]
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
    }

    //ChatUser klasse    
    [DataContract]
    public class ChatUser
    {
        private string userName, ipAddress, hostName;
        //Gebruikersnaam
        [DataMember]
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        //IP-Adres
        [DataMember]
        public string IpAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
        }
        //Hostnaam
        [DataMember]
        public string HostName
        {
            get { return hostName; }
            set { hostName = value; }
        }

        //Zorgt ervoor dat we de ChatUser klasse als een String kunnen gebruiken
        public override string ToString()
        {
            return this.UserName;
        }
    }
}
