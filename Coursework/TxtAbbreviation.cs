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
        private Dictionary<string, string> abbreviations;
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

        public Dictionary<string, string> getAbbreviations()
        {
            return this.abbreviations;
        }

        public string porcessAbbreviations(string messageTxt)
        {
            string[] words = messageTxt.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                foreach (var abbreviation in abbreviations.Keys)
                {
                    // if the word matches an abbreviation, then replace that word with the extended version
                    if (words[i].ToLower() == abbreviation.ToLower())
                    {
                        string abbValue;
                        abbreviations.TryGetValue(abbreviation, out abbValue);
                        words[i] = "<" + abbValue + ">";
                    }
                    // also check if the abbreviations are preceded by punctuation signs and symbols
                    else if (words[i].ToLower().Substring(1) == abbreviation.ToLower())
                    {
                        string abbValue;
                        abbreviations.TryGetValue(abbreviation, out abbValue);
                        words[i] = words[i].Substring(0, 1) + "<" + abbValue + ">";
                    }
                    else if (words[i].ToLower().Substring(0, words[i].Length - 1) == abbreviation.ToLower())
                    {
                        string abbValue;
                        abbreviations.TryGetValue(abbreviation, out abbValue);
                        words[i] = "<" + abbValue + ">" + words[i].Substring(words[i].Length - 1);
                    }
                }
            }

            // empty the string to prepare it for reinitialisation
            messageTxt = "";
            // put the message text back together
            for (int i = 0; i < words.Length; i++)
            {
                // add a space between words if it's not the first word
                if (i != 0)
                    messageTxt += " ";

                messageTxt += words[i];
            }

            return messageTxt;
        }

        private Dictionary<string, string> deserialize()
        {
            return serializer.deserializeAbbreviations();
        }
    }
}
