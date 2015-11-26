using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    [DataContract]
    class Hashtag
    {
        private static Hashtag instance;
        [DataMember]
        private Dictionary<string, int> hashtags;
        private Serializer serializer;

        private  Hashtag() {
            serializer = new Serializer();
            hashtags = deserialize();
            if (hashtags == null) hashtags = new Dictionary<string, int>();
        }

        public static Hashtag Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Hashtag();
                }
                return instance;
            }
        }

        public void serialize()
        {
            serializer.serializeHashtags(this.hashtags);
        }

        private Dictionary<string, int> deserialize()
        {
            return serializer.deserializeHashtags();
        }

        public void addHashtag(string hashtag)
        {
            // check if the value already exists in the dictionary and increment the counter value
            bool exists = false;
            foreach(string hash in hashtags.Keys)
            {
                if (hash == hashtag)
                {
                    hashtags[hash] += 1;
                    exists = true;
                    break; 
                }
            }

            // if it doesn't exists, then add a new entry in the dictionary with the value 1
            if (!exists)
            {
                hashtags.Add(hashtag, 1);
            }
        }

        public Dictionary<string, int> getHashtags()
        {
            return hashtags;
        }
    }
}
