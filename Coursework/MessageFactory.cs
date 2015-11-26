using System.Collections.Generic;

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

            if (messageList == null) messageList = new List<Message>();
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

        public void recordIncident(Email email)
        {
            if (email.GetType() == typeof(SIREmail))
            {
                SIREmail sirEmail = (SIREmail)email;
                incidentManager.recordIncident(sirEmail.getIncident());
            }
        }

        public void removeMessage(Message message)
        {
            messageList.Remove(message);
        }

        public void serializeAll()
        {
            serializer.serializeMessages();
        }

        private List<Message> deserialize()
        {
            return serializer.deserializeMessages();
        }
        
        public bool hasId(string id)
        {
            foreach(Message message in messageList)
            {
                if (message.getId() == id)
                {
                    return true;
                }
            }

            return false;
        }
        
        public List<SMS> getSmsList()
        {
            List<SMS> smsList = null;
            foreach(Message m in messageList)
            {
                if (m.GetType() == typeof(SMS))
                {
                    if (smsList == null) smsList = new List<SMS>();

                    smsList.Add((SMS)m);
                } 
            }

            return smsList;
        }
        
        public List<StandardEmail> getStandardEmailList()
        {
            List<StandardEmail> standardEmailList = null;

            foreach(Message m in messageList)
            {
                if (m.GetType() == typeof(StandardEmail))
                {
                    if (standardEmailList == null) standardEmailList = new List<StandardEmail>();

                    standardEmailList.Add((StandardEmail)m);
                }
            }

            return standardEmailList;
        }
        
        public List<SIREmail> getSirEmailList()
        {
            List<SIREmail> sirEmailList = null;

            foreach(Message m in messageList)
            {
                if (m.GetType() == typeof(SIREmail))
                {
                    if (sirEmailList == null) sirEmailList = new List<SIREmail>();
                    sirEmailList.Add((SIREmail)m);
                }
            }

            return sirEmailList;
        } 

        public List<Tweet> getTweetList()
        {
            List<Tweet> tweetList = null;

            foreach(Message m in messageList)
            {
                if (m.GetType() == typeof(Tweet))
                {
                    if (tweetList == null) tweetList = new List<Tweet>();
                    tweetList.Add((Tweet)m);
                }
            }

            return tweetList;
        }

        public void processAll()
        {
            foreach(Message m in messageList)
            {
                m.processAll();
            }
        }
    }
}
