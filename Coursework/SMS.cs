using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Coursework
{
    [DataContract]
    class SMS : Message
    {
        [DataMember]
        private string id;
        [DataMember]
        private string messageTxt;
        [DataMember]
        private string sender;
        [DataMember]
        private string type;

        TxtAbbreviation txtAbbreviation;

        public SMS()
        {
            txtAbbreviation = TxtAbbreviation.Instance;
            this.type = "sms";
        }

        public SMS(string id, string messageTxt, string sender)
        {
            this.messageTxt = messageTxt;
            this.sender = sender;
            txtAbbreviation = TxtAbbreviation.Instance;
            this.type = "sms";
            this.id = id;

            // process the message body and check it for any abbreviations
            this.processAbbreviations();
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
            return this.type + ": " + sender + " --- " + messageTxt;
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

        public string getId()
        {
            return this.id;
        }

        public MemoryStream serialize()
        {
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(SMS));

            js.WriteObject(ms, this);

            return ms;
        }

        public void processAll()
        {
            this.processAbbreviations();
        }
    }
}
