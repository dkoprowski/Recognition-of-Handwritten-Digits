using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recognition_of_Handwritten_Digits.Model;
using Recognition_of_Handwritten_Digits.BL;

namespace Recognition_of_Handwritten_Digits
{
    class Program
    {
        static private List<DigitModel> digitModels = new List<DigitModel>();
        static private CsvFileReader csvReader = new CsvFileReader();

        static void Main(string[] args)
        {
            if (args.Length > 0 && !string.IsNullOrEmpty(args[0]))
            {
                var start = DateTime.Now;
                digitModels = csvReader.ParseCsvAsStream(args[0], 28 * 28);
                var finish = DateTime.Now;
                Console.WriteLine("[TIME] Whole time for _" + digitModels.Count + "_ records: " + (finish - start).TotalMilliseconds);

            }
            else
            {
                Console.WriteLine("[ERROR] gib me csv");
            }
            Communication();
        }
        
        static void Communication()
        {

            string option = "q";
            do
            {
                Console.WriteLine("\nSelect one option:");
                Console.WriteLine("\t[1] Print one digit");
                Console.WriteLine("\t[2] Load from CSV one digit");
                Console.WriteLine("\t[q] Quit");
                option = Console.ReadLine();
                Answer(option);

            } while (option != "q");

        }

        static void Answer(string option)
        {
            DigitPrinter digitPrinter = new DigitPrinter();

            switch (option)
            {
                case "1":
                    Console.WriteLine("Write digit index:");
                    var index = int.Parse( Console.ReadLine());
                    digitPrinter.PrintDigit(digitModels[index]);
                    break;
                case "2":
                    Console.WriteLine("Write CSV name:");
                    var csvName = Console.ReadLine();
                    digitPrinter.PrintDigit( csvReader.ParseCsvSingleRow(csvName, 28 * 28) );
                    break;
            }

        }
    }
}
