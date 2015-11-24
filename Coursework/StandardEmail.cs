﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    class StandardEmail : Email
    {
        private string messageTxt;
        private string sender;
        private string subject;
        private string type;
        private URLQuarantine urlQuarantine;

        public StandardEmail()
        {
            urlQuarantine = URLQuarantine.Instance;
            type = "standard_email";
        }

        public override string getMessageTxt()
        {
            return this.messageTxt;
        }

        public override string getSender()
        {
            return this.sender;
        }

        public override string getType()
        {
            return this.type;
        }

        public override void print()
        {
            
        }

        public override void setMessageTxt(string txt)
        {
            this.messageTxt = txt;
        }

        public override void setSender(string txt)
        {
            this.sender = txt;
        }
    }
}