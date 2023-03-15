using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CVParser.Utils
{
    public class TextParser
    {
        public string GetEmail(string value)
        {
            Regex regex = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
            Match match = regex.Match(value);
            if (match.Success)
                return value;
            return null;
        }

        public string GetPhone(string value)
        {
            // +36 30 9000/367
            Regex regex = new Regex(@"\(?\d{2}\)?-? *\d{2}-? *-?\d{4,9}[ /]*-?\d{3,5}");
            Match match = regex.Match(value);
            if (match.Success)
                return match.Value; // match.Value;

            // +36-30-1239000
            regex = new Regex(@"\(?\d{2}\)?-? *\d{2}-? *-?\d{4,9}");
            match = regex.Match(value);
            if (match.Success)
                return value; // match.Value;
            return null;
        }

        public bool isPartOfPhone(string value)
        {
            Regex regex = new Regex(@"\(?\d{2,11}\)?-?");
            Match match = regex.Match(value);
            return match.Success;
        }
    }
}
