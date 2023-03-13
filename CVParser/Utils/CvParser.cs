using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (!GetEmails())
                return false;

            return true;
        }

        private bool GetEmails()
        {
            List<string> words = new List<string>();
            foreach (string line in lines)
            {
                words = line.Split(' ').ToList();
                foreach (var word in words)
                {
                    string item = textParser.GetEmail(word);
                    if (!string.IsNullOrEmpty(item))
                        Emails.Add(item);
                }
            }

            return true;
        }

    }
}
