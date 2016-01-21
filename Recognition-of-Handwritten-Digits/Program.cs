using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recognition_of_Handwritten_Digits.Model;
namespace Recognition_of_Handwritten_Digits
{
    class Program
    {
        static List<DigitModel> digitModels = new List<DigitModel>();

        static void Main(string[] args)
        {
            if (args.Length > 0 && !string.IsNullOrEmpty(args[0]))
            {
                //   Console.WriteLine(args[0]);
                //    ParseCsvToModel(args[0]);
            }
            else
            {
                Console.WriteLine("[ERROR] gib me csv");
            }

            var start = DateTime.Now;
            ParseCsvToModel("train.csv");
            var finish = DateTime.Now;

            Console.WriteLine("[TIME] Whole time for _"+digitModels.Count+"_ records: " + (finish - start).TotalMilliseconds);
        //    PrintDigit(digitModels[0]);
            Console.ReadKey();
        }

        static void ParseCsvToModel(string csvPath)
        {
            var reader = new StreamReader(File.OpenRead(csvPath));
                
            var whileStart = DateTime.Now;
            double splitTime = 0f;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                var splitStart = DateTime.Now;
                var values = line.Split(',');
                var splitFinish = DateTime.Now;
                splitTime += ((splitFinish - splitStart).TotalMilliseconds);

                int[] pixels = new int[values.Length - 1];
                for (int i = 0; i < pixels.Length; i++)
                {
                    pixels[i] = OptimizedIntConversion(values[i + 1]);
                    //pixels[i] = int.Parse(values[i + 1]);
                }

                var digit = new DigitModel(int.Parse(values[0]), pixels);
                digitModels.Add(digit);
            }
            var whileFinish = DateTime.Now;

            Console.WriteLine("[TIME] Whole split: " + splitTime);
            Console.WriteLine("[TIME] Whole parse time: " + (whileFinish - whileStart).TotalMilliseconds);
        }

        static int OptimizedIntConversion(string s)
        {
            int total = 0;
            for (int i = 0; i < s.Length; i++)
            {
                total = total * 10 + (s[i] - '0');
            }

            return total;
        }



    static void PrintDigit(DigitModel digit)
        {
            Console.WriteLine("\n\n"+digit.Digit);
            for (int i = 0; i < Math.Sqrt(digit.DigitRepresentation.Length); i++)
            {
                for (int j = 0; j < Math.Sqrt(digit.DigitRepresentation.Length); j++)
                {
                    int pixel = digit.DigitRepresentation[(i * (int)Math.Sqrt(digit.DigitRepresentation.Length)) + j];
                    if(pixel != 0)
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write(pixel);
                    }
                }
                Console.WriteLine();
            }

            string option = Console.ReadLine();
            int otherDigit;
            if(int.TryParse(option, out otherDigit))
            {
                PrintDigit(digitModels[otherDigit]);
            }
            else
            {
                return;
            }
        }
    }
}
