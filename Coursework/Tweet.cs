using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    class Tweet : Message
    {
        private Hashtag hashtag;
        private Mention mention;
        private string messageTxt;
        private string sender;
        private string type;

        public Tweet()
        {
            hashtag = Hashtag.Instance;
            mention = Mention.Instance;
            type = "tweet";
        }

        public Tweet(string messageTxt, string sender)
        {
            hashtag = Hashtag.Instance;
            mention = Mention.Instance;
            type = "tweet";
            this.messageTxt = messageTxt;
            this.sender = sender;
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

        public void print()
        {
        }

        public void setMessageTxt(string txt)
        {
            this.messageTxt = txt;
        }

        public void setSender(string txt)
        {
            this.sender = txt;
        }
    }
}
