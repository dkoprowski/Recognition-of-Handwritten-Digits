using Recognition_of_Handwritten_Digits.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recognition_of_Handwritten_Digits.BL
{
    class DigitPrinter
    {
        public void PrintDigit(DigitModel digit)
        {
            Console.WriteLine("\n\n" + digit.Digit);
            int rowLength = (int)Math.Sqrt(digit.DigitRepresentation.Length);
            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < rowLength; j++)
                {
                    int pixel = digit.DigitRepresentation[(i * rowLength) + j];
                    if (pixel != 0)
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
        }
    }
}
