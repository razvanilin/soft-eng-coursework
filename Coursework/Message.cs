using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    interface Message
    {
        String getId();

        String getMessageTxt();

        String getSender();

        String getType();

        string print();

        void setMessageTxt(String txt);

        void setSender(String txt);

        MemoryStream serialize();

        void processAll();
    }
}
