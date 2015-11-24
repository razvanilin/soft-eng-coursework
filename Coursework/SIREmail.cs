using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    class SIREmail : Email
    {
        private Incident incident;
        private String messageTxt;
        private String sender;
        private String subject;
        private string type;
        private URLQuarantine urlQuarantine;

        public SIREmail()
        {
            urlQuarantine = URLQuarantine.Instance;
            type = "sir_email";
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

        public Incident getIncident() { return incident; }
    }
}
