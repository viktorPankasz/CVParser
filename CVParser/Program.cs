using System;
using CVParser.Controllers;

namespace CVParser
{
    class Program
    {
        static void Main(string[] args)
        {
            //string[] args = Environment.GetCommandLineArgs();

            if (args.Length < 1)
            {
                Console.WriteLine("nincs cv filename");
                return;
            }

            CvController cvController = new CvController();
            cvController.Execute(args[0]);
        }
    }
}
