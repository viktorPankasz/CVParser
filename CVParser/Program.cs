using System;
using CVParser.Controllers;

namespace CVParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CvParser");
            Console.WriteLine("(c) 2023 viktorPankasz");

            //string[] args = Environment.GetCommandLineArgs();

            if (args.Length < 1)
            {
                Console.WriteLine("Cv filename parameter missed!");
                return;
            }

            CvController cvController = new CvController();
            cvController.Execute(args[0]);

            Console.WriteLine($"{Environment.NewLine}Bye!");
        }
    }
}
