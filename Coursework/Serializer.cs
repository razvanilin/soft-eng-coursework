using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    class Serializer
    {
        public void serialize(Message msg) { }

        public Dictionary<string, int> deserializeMentions()
        {
            var dict = new Dictionary<string, int>();
            try {
                dict = File.ReadLines("mentions.csv").Select(line => line.Split(',')).ToDictionary(line => line[0], line => Int32.Parse(line[1]));
            } catch (Exception e)
            {
                dict = null;
            }

            if (dict != null)
                return dict;

            return new Dictionary<string, int>();
        }
        public void serializeMentions(Dictionary<string, int> mentions)
        {
            StreamWriter writer = new StreamWriter("mentions.csv");

            foreach (string key in mentions.Keys)
            {
                writer.WriteLine(key + "," + mentions[key]);
            }

            writer.Close();
        }

        public Dictionary<string, int> deserializeHashtags()
        {
            var dict = File.ReadLines("hashtags.csv").Select(line => line.Split(',')).ToDictionary(line => line[0], line => Int32.Parse(line[1]));

            if (dict != null || dict.Count > 0)
                return dict;

            return new Dictionary<string, int>();
        }
        public void serializeHashtags(Dictionary<string, int> hashtags)
        {
            StreamWriter writer = new StreamWriter("hashtags.csv");

            foreach(string key in hashtags.Keys)
            {
                writer.WriteLine(key+","+hashtags[key]);
            }

            writer.Close();
        }

        public List<string> deserializeUrls()
        {
            List<string> urls = new List<string>();
            StreamReader reader = new StreamReader("urls.txt");
            string line;
            while ((line = reader.ReadLine()) !=null)
            {
                urls.Add(line);
            }

            reader.Close();

            return urls;
        }
        public void serializeUrls(List<string> urlList)
        {
            if (urlList == null || urlList.Count < 1) return;

            string[] urls = urlList.ToArray();

            StreamWriter writer = new StreamWriter("urls.txt");

            foreach(string s in urls)
            {
                writer.WriteLine(s);
            }

            writer.Close();
        }

        public Dictionary<string, string> deserializeAbbreviations()
        {
            var dict = File.ReadLines("textwords.csv").Select(line => line.Split(',')).ToDictionary(line => line[0], line => line[1]);
            return dict;
        }

        public List<Incident> deserializeIncidents()
        {
            var dict = new Dictionary<string, string>();
            try {
                dict = File.ReadLines("incidents.csv").Select(line => line.Split(',')).ToDictionary(line => line[0], line => line[1]);
            } catch (Exception e)
            {
                dict = null;
            }

            if (dict == null) return new List<Incident>();

            List<Incident> incidentList = new List<Incident>();
            foreach(string key in dict.Keys)
            {
                incidentList.Add(new Incident(dict[key], key));
            }

            return incidentList;
        }
        public void serializeIncident(List<Incident> incidents)
        {
            if (incidents == null) return;

            //string[] urls = urlList.ToArray();

            StreamWriter writer = new StreamWriter("incidents.csv");

            foreach (Incident s in incidents)
            {
                writer.WriteLine(s.getSortCode() + "," + s.getNature());
            }

            writer.Close();
        }

        public List<Message> deserializeMessages()
        {
            List<Message> messages = new List<Message>();
            StreamReader reader = new StreamReader("messageList.json");
            string line = reader.ReadLine();

            // if the file is empty, return a new list
            if (line == null || line.Length < 1)
            {
                return new List<Message>();
            }

            // get rid of the square brackets
            line = line.Substring(1, line.Length-1);
            // split the objects
            string delimiter = ", ";
            string[] sMessages = line.Split(new[] { delimiter }, StringSplitOptions.None);

            foreach(string s in sMessages)
            {
                // isolate the type from the string
                string type = s.Substring(s.IndexOf("type") + 7);
                type = type.Substring(0, type.IndexOf("\""));
                // check the type of the object and deserialize accordingly
                switch (type)
                {
                    case "sms":
                        DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(SMS));
                        MemoryStream ms = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(s));
                        SMS sms = (SMS)js.ReadObject(ms);
                        messages.Add(sms);
                        break;
                    case "standard_email":
                        DataContractJsonSerializer jsSEmail = new DataContractJsonSerializer(typeof(StandardEmail));
                        MemoryStream msSEmail = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(s));
                        StandardEmail sEmail = (StandardEmail)jsSEmail.ReadObject(msSEmail);
                        messages.Add(sEmail);
                        break;
                    case "sir_email":
                        DataContractJsonSerializer jsSirEmail = new DataContractJsonSerializer(typeof(SIREmail));
                        MemoryStream msSirEmail = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(s));
                        SIREmail sirEmail = (SIREmail)jsSirEmail.ReadObject(msSirEmail);
                        messages.Add(sirEmail);
                        break;
                    case "tweet":
                        DataContractJsonSerializer jsTweet = new DataContractJsonSerializer(typeof(Tweet));
                        MemoryStream msTweet = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(s));
                        Tweet tweet = (Tweet)jsTweet.ReadObject(msTweet);
                        messages.Add(tweet);
                        break;
                    default:
                        break;
                }
            }

            return messages;
        }
        public void serializeMessages()
        {
            
            FileStream file = new FileStream("messageList.json", FileMode.Create, System.IO.FileAccess.Write);
            StreamWriter writer = new StreamWriter(file);

            writer.Write("[");
            writer.Flush();

            bool first = true;
            foreach(Message m in MessageFactory.Instance.getAllMessages())
            {
                // add a comma if it's not the first object in the list
                if (!first)
                {
                    writer.Write(", ");
                    writer.Flush();
                }
                m.serialize().WriteTo(file);

                first = false;
            }

            writer.Write("]");
            writer.Close();
            file.Close();
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
