using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVParser.Resources
{
    public class ZipItem
    {
        public string Zip { get; set; }
        public string City { get; set; }
        public string Regio { get; set; }

        public ZipItem(string zip, string city, string regio)
        {
            this.Zip = zip;
            this.City = city;
            this.Regio = regio;
        }
    }
}
