using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    abstract class Email : Message
    {
        public abstract string getMessageTxt();
        public abstract string getSender();
        public abstract string getType();
        public abstract void print();
        public abstract void setMessageTxt(string txt);
        public abstract void setSender(string txt);
        public abstract void processUrls();

        void addUrlToQuarantine() { }

        
    }
}
