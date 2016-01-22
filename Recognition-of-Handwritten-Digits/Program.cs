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
            //  ParseCsvToModel("train.csv");
            ParseCsvAsStream("train.csv", 28*28);
            var finish = DateTime.Now;

            Console.WriteLine("[TIME] Whole time for _"+digitModels.Count+"_ records: " + (finish - start).TotalMilliseconds);
            PrintDigit(digitModels[0]);
            Console.ReadKey();
        }
        /*
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
        */
        static void ParseCsvAsStream(string csvPath,int pictureSize)
        {
            var file = File.ReadAllText(csvPath);
            byte acumulator = 0;
            int pixelNr = 0;
            byte digitLabel = (byte)(file[0] - '0');
            DigitModel currentModel = new DigitModel(digitLabel, pictureSize);
            for (int i = 1; i < file.Length; i++)
            {
                char currentChar = file[i];
                if ((i + 1) == file.Length)
                {
                    digitModels.Add(currentModel);
                }
                else if (currentChar == '\n')
                {
                    digitModels.Add(currentModel);
                    digitLabel = (byte)(file[i + 1] - '0');
                    currentModel = new DigitModel(digitLabel, pictureSize);
                    pixelNr = 0;
                    i += 1;
                }
                else if (currentChar >= '0' && currentChar <= '9')
                {
                    acumulator = (byte)(acumulator * 10 + (currentChar - '0'));
                }
                else if (pixelNr < pictureSize)
                {
                    currentModel.DigitRepresentation[pixelNr] = acumulator;
                    acumulator = 0;
                    pixelNr++;
                }
            }
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
            int rowLength = (int)Math.Sqrt(digit.DigitRepresentation.Length);
            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < rowLength; j++)
                {
                    int pixel = digit.DigitRepresentation[(i * rowLength) + j];
                    if(pixel != 0)
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write((char)176);
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
