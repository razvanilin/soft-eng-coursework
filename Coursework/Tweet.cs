using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Coursework
{
    [DataContract]
    class Tweet : Message
    {
        [DataMember]
        private Hashtag hashtag;
        [DataMember]
        private Mention mention;
        [DataMember]
        private string id;
        [DataMember]
        private string messageTxt;
        [DataMember]
        private string sender;
        [DataMember]
        private string type;
        
        private TxtAbbreviation txtAbbreviation;

        public Tweet()
        {
            hashtag = Hashtag.Instance;
            mention = Mention.Instance;
            txtAbbreviation = TxtAbbreviation.Instance;
            type = "tweet";
        }

        public Tweet(string id, string messageTxt, string sender)
        {
            hashtag = Hashtag.Instance;
            mention = Mention.Instance;
            type = "tweet";
            this.messageTxt = messageTxt;
            this.sender = sender;
            this.id = id;
            txtAbbreviation = TxtAbbreviation.Instance;

            this.processAbbreviations();
            this.processHashtags();
            this.processMentions();
        }

        public void processAll()
        {
            this.processAbbreviations();
            this.processHashtags();
            this.processMentions();
        }

        public string getMessageTxt()
        {
            return this.messageTxt;
        }

        public string getSender()
        {
            return this.sender;
        }

        public string getType()
        {
            return this.type;
        }

        public string print()
        {
            return this.type + ": " + sender + " " + messageTxt;
        }

        public void setMessageTxt(string txt)
        {
            this.messageTxt = txt;
        }

        public void setSender(string txt)
        {
            this.sender = txt;
        }

        public void processAbbreviations()
        {
            messageTxt = TxtAbbreviation.Instance.porcessAbbreviations(messageTxt);
        }

        public void processHashtags()
        {
            string[] words = messageTxt.Split(' ');
            string hashtagRegexString = @"(?<=\s|^)#(\w*[A-Za-z_]+\w*)";
            Regex hashtagRegex = new Regex(hashtagRegexString);

            for (int i = 0; i < words.Length; i++)
            {
                if (hashtagRegex.IsMatch(words[i]))
                {
                    hashtag.addHashtag(words[i]);
                }
                else if (hashtagRegex.IsMatch(words[i].Substring(0, words[i].Length - 1)))
                {
                    hashtag.addHashtag(words[i].Substring(0, words[i].Length - 1));
                }
            }
        }

        public void processMentions()
        {
            string[] words = messageTxt.Split(' ');
            string mentionRegexString = @"^@(\w){1,15}$";
            Regex mentionRegex = new Regex(mentionRegexString);

            for (int i = 0; i < words.Length; i++)
            {
                if (mentionRegex.IsMatch(words[i]))
                {
                    mention.addMention(words[i]);
                }
                else if (mentionRegex.IsMatch(words[i].Substring(0, words[i].Length - 1)))
                {
                    mention.addMention(words[i].Substring(0, words[i].Length - 1));
                }
            }
        }

        public string getId()
        {
            return this.id;
        }

        public MemoryStream serialize()
        {
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Tweet));

            js.WriteObject(ms, this);

            return ms;
        }
    }
}
