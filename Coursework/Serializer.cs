using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    class Serializer
    {
        public void serialize(Message msg) { }

        public List<string> deserializeMentions()
        {
            return null;
        }
        public void serializeMentions(List<string> mentions)
        {

        }

        public List<string> deserializeHashtags()
        {
            return null;
        }
        public void serializeHashtags(List<string> hashtags)
        {

        }

        public List<string> deserializeUrls()
        {
            return null;
        }
        public void serializeUrls(List<string> urls)
        {

        }

        public Dictionary<string, string> deserializeAbbreviations()
        {
            var dict = File.ReadLines("textwords.csv").Select(line => line.Split(',')).ToDictionary(line => line[0], line => line[1]);
            return dict;
        }

        public List<Incident> deserializeIncidents()
        {
            return null;
        }
        public void serializeIncident(List<Incident> incidents)
        {

        }

        public List<Message> deserializeMessages()
        {
            return null;
        }
        public void serializeMessages(List<Message> messageList)
        {

        }

        public List<string> deserializeIncidentsType()
        {
            var dict = File.ReadLines("incidents.txt").Select(line => line.Split(','));
            foreach (var incidents in dict)
            {
                List<string> incidentList = incidents.ToList<string>();
                return incidentList;
            }

            return null;
        }
    }
}
