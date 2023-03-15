﻿using CVParser.Models;
using CVParser.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVParser.Extensions;

namespace CVParser.Controllers
{
    public class CvController
    {
        public bool Execute(string filePath)
        {
            CvParser cvParser = new CvParser();

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File doesn't exists: '{filePath}'");
                return false;
            }

            string newFilePath = File2TextFile(filePath);
            if (newFilePath.IsNullOrEmpty())
            {
                Console.WriteLine("File convert failed or no file content detected!");
                return false;
            }

            if (!File.Exists(newFilePath))
            {
                Console.WriteLine($"File doesn't exists: '{newFilePath}'");
                return false;
            }

            if (!cvParser.Load(newFilePath)) return false;
            if (!cvParser.Parse()) return false;

            Cv cv = new Cv();

            if (cvParser.Emails.Any())
            { 
                foreach (var item in cvParser.Emails)
                    Console.WriteLine($"email: {item}");

                cv.Email = cvParser.Emails.First();
            }

            if (cvParser.Phones.Any())
            {
                foreach (var item in cvParser.Phones)
                    Console.WriteLine($"phone: {item}");

                cv.Phone = cvParser.Phones.First();
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

            return true;
        }

        private string File2TextFile(string filePath)
        {
            string ext = Path.GetExtension(filePath);

            if (ext.IsNullOrEmpty()) return String.Empty;

            if (new[] { ".txt", ".text" }.Contains(ext.ToLower()))
            {
                Console.WriteLine("TEXT file detected");
                return filePath;
            }

            if (new[] { ".pdf" }.Contains(ext.ToLower()))
            {
                if (!PdfUtils.Pdf2Text(filePath))
                    return String.Empty;

                Console.WriteLine("PDF file detected");

                return Path.ChangeExtension(filePath, "txt");
            }

            return String.Empty;
        }
    }
}
