﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    [DataContract]
    class Incident
    {
        [DataMember]
        private string incidentNature;
        [DataMember]
        private string sortCode;

        public Incident() { }
        public Incident(string incidentNature, string sortCode)
        {
            this.incidentNature = incidentNature;
            this.sortCode = sortCode;
        } 

        public string getNature() { return incidentNature; }

        public string getSortCode() { return sortCode; }

        public void setNature(string incidentNature) { this.incidentNature = incidentNature; }

        public void setSortCode(string sortCode) { this.sortCode = sortCode; }
    }
}
