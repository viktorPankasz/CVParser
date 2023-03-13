using CVParser.Models;
using CVParser.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVParser.Controllers
{
    public class CvController
    {
        public void Execute(string filePath)
        {
            CvParser cvParser = new CvParser(filePath);
            if (cvParser.Load())
                if (cvParser.Parse())
                {
                    Cv cv = new Cv();

                    if (cvParser.Emails != null)
                    { 
                        foreach (var item in cvParser.Emails)
                        {
                            Console.WriteLine($"email: {item}");
                        }

                        cv.Email = cvParser.Emails.First();
                    }

                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        StringEscapeHandling = StringEscapeHandling.EscapeNonAscii, // ez rendezi az unicode és escape-lt dolgokat (pl: "name":"Egy\u00e9b:")
                        PreserveReferencesHandling = PreserveReferencesHandling.None,
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore, // üres array-ek esetén null, vagy skip, ne essen el // pl.: "characteristics":[]
                        Formatting = Formatting.Indented
                    };

                    string jsn = JsonConvert.SerializeObject(cv, settings);
                    if (!string.IsNullOrEmpty(jsn))
                    { 
                        Console.WriteLine(Environment.NewLine);
                        Console.WriteLine($"cv json:{Environment.NewLine}{jsn}");
                    }
                }

        }
    }
}
