using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    class TxtAbbreviation
    {
        private static TxtAbbreviation instance;
        private List<string> abbreviations;
        private Serializer serializer;

        private TxtAbbreviation()
        {
            serializer = new Serializer();
            abbreviations = this.deserialize();
        }

        public static TxtAbbreviation Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TxtAbbreviation();
                }
                return instance;
            }
        }

        private List<string> deserialize()
        {
            return serializer.deserializeAbbreviations();
        }
    }
}
