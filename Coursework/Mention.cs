using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    [DataContract]
    class Mention
    {
        private static Mention instance;
        [DataMember]
        private Dictionary<string, int> mentions;
        private Serializer serializer;

        private Mention()
        {
            serializer = new Serializer();
            mentions = deserialize();

            if (mentions == null) mentions = new Dictionary<string, int>();  
        }

        public static Mention Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Mention();
                }
                return instance;
            }
        }

        private Dictionary<string, int> deserialize() { return serializer.deserializeMentions(); }

        public void serialize()
        {
            serializer.serializeMentions(mentions);
        }

        public Dictionary<string, int> getMentions()
        {
            return this.mentions;
        }

        public void addMention(string mention)
        {
            // check if the mention exists and increment its value if it does
            bool exists = false;
            foreach(string m in mentions.Keys)
            {
                if (m == mention)
                {
                    mentions[m] += 1;
                    exists = true;
                    break;
                }
            }

            // if it doesn't exist, add it to the list
            if (!exists)
            {
                mentions.Add(mention, 1);
            }
        }
    }
}
