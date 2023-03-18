using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVParser.Extensions;

namespace CVParser.Utils
{
    public enum ParseLineReason { None, Phone, Address, Person }

    public class CvParser
    {
        public string filePath { get; set; }
        public string Content_PlainText { get; set; }

        private List<string> lines;

        private List<string> persons;
        public List<string> Persons
        {
            get
            {
                if (persons == null)
                    persons = new List<string>();
                return persons;
            }
        }

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

        private List<string> addresses;
        public List<string> Addresses
        {
            get
            {
                if (addresses == null)
                    addresses = new List<string>();
                return addresses;
            }
        }

        private TextParser textParser;

        public CvParser()
        {
            textParser = new TextParser();
        }

        public bool Load(string fileName)
        {
            filePath = fileName;
            // TODO el lehet szórakozni a path-al

            if (string.IsNullOrEmpty(filePath))
                return false;

            if (!File.Exists(filePath))
                return false;

            using (StreamReader fs = new StreamReader(filePath, Encoding.UTF8)) // TODO custom GetEncoding(?)))
                lines = File.ReadAllLines(filePath, Encoding.UTF8).ToList();

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
            List<string> words;
            string item;

            foreach (string line in lines)
            {
                if (line.IsNullOrEmpty()) continue;

                words = line.Split(' ').ToList();
                foreach (var word in words)
                {
                    if (word.IsNullOrEmpty()) continue;

                    if (textParser.IsPerson(word))
                        if (ParseLine(line, ParseLineReason.Person))
                            break;

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

                    // find zip
                    if (textParser.IsZipCode(word))
                        if (ParseLine(line, ParseLineReason.Address))
                            break;

                    // Phone, split szóköz miatt nézzük meg a teljes sort
                    if (textParser.isPartOfPhone(word))
                        if (ParseLine(line, ParseLineReason.Phone))
                            break;
                }
            }

            return true;
        }

        private bool ParseLine(string line, ParseLineReason parseLine)
        {
            string item;

            switch (parseLine)
            {
                case ParseLineReason.None:
                    break;
                case ParseLineReason.Phone:
                    item = textParser.GetPhone(line);
                    if (!string.IsNullOrEmpty(item))
                    {
                        Phones.Add(item);
                        return true;
                    }
                    break;
                case ParseLineReason.Address:
                    // TODO bejárni a sort
                    item = line; // TODO cheat
                    if (!string.IsNullOrEmpty(item))
                    {
                        Addresses.Add(item);
                        return true;
                    }
                    break;
                case ParseLineReason.Person:
                    // TODO bejárni a sort
                    // nagy kezdő betűket keresni, ami több mint 1 karakter
                    item = line; // TODO cheat
                    if (!string.IsNullOrEmpty(item))
                    {
                        Persons.Add(item);
                        return true;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return false;
        }
    }
}
