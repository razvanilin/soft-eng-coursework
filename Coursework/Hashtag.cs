using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    class Hashtag
    {
        private static Hashtag instance;
        private List<string> hashtags;
        private Serializer serializer;

        private  Hashtag() {
            serializer = new Serializer();
            hashtags = deserialize();
        }

        public static Hashtag Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Hashtag();
                }
                return instance;
            }
        }

        public void serialize()
        {
            serializer.serializeHashtags(this.hashtags);
        }

        private List<string> deserialize()
        {
            return serializer.deserializeHashtags();
        }
    }
}
