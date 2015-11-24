using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    interface Message
    {
        String getMessageTxt();

        String getSender();

        String getType();

        void print();

        void setMessageTxt(String txt);

        void setSender(String txt);
    }
}
