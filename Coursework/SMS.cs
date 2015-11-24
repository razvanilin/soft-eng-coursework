using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    class SMS : Message
    {
        private string messageTxt;
        private string sender;
        private string type;
        TxtAbbreviation txtAbbreviation;

        public SMS()
        {
            txtAbbreviation = TxtAbbreviation.Instance;
            this.type = "sms";
        }

        public SMS(string messageTxt, string sender, string type)
        {
            this.messageTxt = messageTxt;
            this.sender = sender;
            this.type = type;
            txtAbbreviation = TxtAbbreviation.Instance;
            this.type = "sms";
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

        public bool processAbbreviations()
        {
            return false;
        }
    }
}
