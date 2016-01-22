using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recognition_of_Handwritten_Digits.Model;

namespace Recognition_of_Handwritten_Digits.BL
{
    class CsvFileReader
    {
        public List<DigitModel> ParseCsvAsStream(string csvPath, int pictureSize)
        {
            List<DigitModel> digitModels = new List<DigitModel>();

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

            return digitModels;
        }

        public DigitModel ParseCsvSingleRow(string csvPath, int pictureSize)
        {
            var reader = new StreamReader(File.OpenRead(csvPath));
            string firstLine = reader.ReadLine();
            byte acumulator = 0;
            int pixelNr = 0;
            byte digitLabel = (byte)(firstLine[0] - '0');
            DigitModel currentModel = new DigitModel(digitLabel, pictureSize);
            for (int i = 1; i < firstLine.Length; i++)
            {
                char currentChar = firstLine[i];
                if (currentChar >= '0' && currentChar <= '9')
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

            return currentModel;
        }

        public int OptimizedIntConversion(string s)
        {
            int total = 0;
            for (int i = 0; i < s.Length; i++)
            {
                total = total * 10 + (s[i] - '0');
            }

            return total;
        }
    }
}
