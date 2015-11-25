using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    class IncidentManager
    {
        private static IncidentManager instance;
        private List<Incident> incidentList;
        private List<string> incidentsType;
        private Serializer serializer;

        private IncidentManager()
        {
            serializer = new Serializer();
            incidentList = deserialize();
            incidentsType = serializer.deserializeIncidentsType();
        }

        private List<Incident> deserialize()
        {
            return serializer.deserializeIncidents();
        }

        public static IncidentManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new IncidentManager();
                }
                return instance;
            }
        }

        public void recordIncident(Incident incident)
        {
            incidentList.Add(incident);
        }

        public List<Incident> getIncidents()
        {
            return incidentList;
        }

        public List<string> getTypes()
        {
            return incidentsType;
        }
    }
}
