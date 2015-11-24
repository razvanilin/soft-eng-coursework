﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    class URLQuarantine
    {
        private static URLQuarantine instance;
        private List<string> urlList;
        private Serializer serializer;

        private URLQuarantine() {
            serializer = new Serializer();
            urlList = new List<string>();
        }

        public static URLQuarantine Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new URLQuarantine();
                }
                return instance;
            }
        }

        public void addUrl(String url) { }

        public List<string> getList() { return urlList; } 

        public List<string> deserialize()
        {
            return serializer.deserializeUrls();
        }
    }
}