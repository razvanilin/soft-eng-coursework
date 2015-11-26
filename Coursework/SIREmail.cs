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
    class SIREmail : Email
    {
        [DataMember]
        private string id;
        [DataMember]
        private Incident incident;
        [DataMember]
        private String messageTxt;
        [DataMember]
        private String sender;
        [DataMember]
        private String subject;
        [DataMember]
        private string type;

        private URLQuarantine urlQuarantine;

        public SIREmail()
        {
            urlQuarantine = URLQuarantine.Instance;
            type = "sir_email";
        }

        public SIREmail(string id, string messageTxt, string sender, string subject, string sortCode, string incidentNature)
        {
            urlQuarantine = URLQuarantine.Instance;
            type = "sir_email";
            this.messageTxt = messageTxt;
            this.sender = sender;
            this.subject = subject;
            this.incident = new Incident(incidentNature, sortCode);
            this.id = id;

            processUrls();
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
            return this.type + ": " + sender + " --- " + subject + " --- " + incident.getSortCode() + " --- " + incident.getNature();
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
            string[] words = messageTxt.Split(' ');
            string urlRegexString = @"^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$";
            Regex urlRegex = new Regex(urlRegexString);

            for (int i = 0; i < words.Length; i++)
            {
                if (urlRegex.IsMatch(words[i]))
                {
                    URLQuarantine.Instance.addUrl(words[i]);
                    words[i] = "<URL Quarantined>";
                }
                else if (urlRegex.IsMatch(words[i].Substring(0, words[i].Length - 1)))
                {
                    URLQuarantine.Instance.addUrl(words[i].Substring(0, words[i].Length - 1));
                    words[i] = "<URL Quarantined>" + words[i].Substring(words[i].Length - 1);
                }
            }

            // empty the string to prepare it for reinitialisation
            this.messageTxt = "";
            // put the message text back together
            for (int i = 0; i < words.Length; i++)
            {
                // add a space between words if it's not the first word
                if (i != 0)
                    this.messageTxt += " ";

                this.messageTxt += words[i];
            }
        }

        public override string getId()
        {
            return this.id;
        }

        public override MemoryStream serialize()
        {
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(SIREmail));

            js.WriteObject(ms, (SIREmail)this);

            return ms;
        }

        public override void processAll()
        {
            this.processUrls();
        }
    }
}
