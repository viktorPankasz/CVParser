using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVParser.Models
{
    public class Cv
    {
        public string Person { get; set; }
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BirthDateStr { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        [JsonIgnore]
        public DateTime BirthDate
        {
            get 
            {
                return DateTime.Parse(BirthDateStr);
            }
        }
   
    }
}
