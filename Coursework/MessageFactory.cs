using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    class MessageFactory
    {
        private static MessageFactory instance;
        private List<Message> messageList;
        private Serializer serializer;
        private IncidentManager incidentManager;

        private MessageFactory()
        {
            incidentManager = IncidentManager.Instance;
            serializer = new Serializer();
            messageList = deserialize();
        }

        public static MessageFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MessageFactory();
                }
                return instance;
            }
        }

        public void addMessage(Message message)
        {
            messageList.Add(message);
        }

        public List<Message> getAllMessages()
        {
            return this.messageList;
        }

        public void print(Message message)
        {
            message.print();
        }

        public void printAll()
        {
            foreach (var message in messageList)
            {
                message.print();
            }
        }

        public void recordIncident(SIREmail email)
        {
            incidentManager.recordIncident(email.getIncident());
        }

        public void removeMessage(Message message)
        {
            messageList.Remove(message);
        }

        public void serializeAll()
        {
            serializer.serializeMessages(messageList);
        }

        private List<Message> deserialize()
        {
            return serializer.deserializeMessages();
        } 
    }
}
