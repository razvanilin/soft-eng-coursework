using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    class Incident
    {
        private String incidentNature;
        private String sortCode;

        public Incident() { }

        public String getNature() { return incidentNature; }

        public String getSortCode() { return sortCode; }

        public void setNature(String incidentNature) { this.incidentNature = incidentNature; }

        public void setSortCode(String sortCode) { this.sortCode = sortCode; }
    }
}
