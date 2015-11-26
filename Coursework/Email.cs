using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    [DataContract]
    abstract class Email : Message
    {
        public abstract string getId();
        public abstract string getMessageTxt();
        public abstract string getSender();
        public abstract string getType();
        public abstract string print();
        public abstract void setMessageTxt(string txt);
        public abstract void setSender(string txt);
        public abstract void processUrls();

        void addUrlToQuarantine() { }

        public abstract MemoryStream serialize();

        public abstract void processAll();
    }
}
