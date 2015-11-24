using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    class Mention
    {
        private static Mention instance;
        private List<string> mentions;
        private Serializer serializer;

        private Mention()
        {
            mentions = new List<string>();   
        }

        public static Mention Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Mention();
                }
                return instance;
            }
        }

        private List<string> deserialize() { return null; }

        public void serialize() { }
    }
}
