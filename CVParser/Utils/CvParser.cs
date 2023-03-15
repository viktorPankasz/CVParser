using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVParser.Extensions;

namespace CVParser.Utils
{
    public class CvParser
    {
        public string FilePath { get; set; }
        public string Content_PlainText { get; set; }

        private List<string> lines;

        private List<string> emails;
        public List<string> Emails
        {
            get
            {
                if (emails == null)
                    emails = new List<string>();
                return emails;
            }
        }

        private List<string> phones;
        public List<string> Phones
        {
            get
            {
                if (phones == null)
                    phones = new List<string>();
                return phones;
            }
        }

        private TextParser textParser;

        public CvParser(string fileName)
        {
            FilePath = fileName;
            textParser = new TextParser();
        }

        public bool Load()
        {
            // TODO el lehet szórakozni a path-al

            if (string.IsNullOrEmpty(FilePath))
                return false;

            if (!File.Exists(FilePath))
                return false;

            using (StreamReader fs = new StreamReader(FilePath, Encoding.UTF8)) // TODO custom GetEncoding(?)))
            {
                //if (lines == null)
                //    lines = new List<string>;

                lines = File.ReadAllLines(FilePath).ToList();
            }

            return true;
        }

        public bool Parse()
        {
            if (!DoParse())
                return false;

            return true;
        }

        private bool DoParse()
        {
            List<string> words = new List<string>();
            string item;

            bool parseLine = false;
            foreach (string line in lines)
            {
                if (line.IsNullOrEmpty()) continue;

                words = line.Split(' ').ToList();
                foreach (var word in words)
                {
                    if (word.IsNullOrEmpty()) continue;

                    item = textParser.GetEmail(word);
                    if (!string.IsNullOrEmpty(item))
                    {
                        Emails.Add(item);
                        continue;
                    }

                    item = textParser.GetPhone(word);
                    if (!string.IsNullOrEmpty(item))
                    {
                        Phones.Add(item);
                        continue;
                    }

                    // szóköz miatt nézzük meg a teljes sort
                    if (textParser.isPartOfPhone(word))
                    {
                        parseLine = true;
                        break;
                    }
                }

                if (parseLine)
                {
                    parseLine = false;
                    item = textParser.GetPhone(line);
                    if (!string.IsNullOrEmpty(item))
                        Phones.Add(item);
                }
            }

            return true;
        }

    }
}
