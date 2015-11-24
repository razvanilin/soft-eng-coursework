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

        public SIREmail(string messageTxt, string sender, string subject, string sortCode, string incidentNature)
        {
            urlQuarantine = URLQuarantine.Instance;
            type = "sir_email";
            this.messageTxt = messageTxt;
            this.sender = sender;
            this.subject = subject;
            this.incident = new Incident(incidentNature, sortCode);
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

        public override string print()
        {
            return "SIREmail: " + sender + " --- " + subject + " --- " + incident.getSortCode() + " --- " + incident.getNature();
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

        public override void processUrls()
        {
            throw new NotImplementedException();
        }
    }
}
