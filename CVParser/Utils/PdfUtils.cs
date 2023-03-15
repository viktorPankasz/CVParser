﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVParser.Utils
{
    public static class PdfUtils
    {
        public static bool Pdf2Text(string fileName)
        {
            // https://www.xpdfreader.com/pdftotext-man.html

            if (!File.Exists("pdftotext.exe"))
            {
                Console.WriteLine("pdftotext.exe doesn't exists!");
                return false;
            }

            // pdftotext -layout "Teszt Aladár.pdf"
            try
            {
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Normal;// ProcessWindowStyle.Hidden;
                startInfo.FileName = "pdftotext.exe";
                startInfo.Arguments = $"-layout \"{fileName}\"";
                process.StartInfo = startInfo;
                process.Start();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}