using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recognition_of_Handwritten_Digits.Model
{
    class DigitModel
    {
        public int Digit;
        public int[] DigitRepresentation;

        public DigitModel(int digit, int[] digitRepresentation)
        {
            Digit = digit;
            DigitRepresentation = digitRepresentation;
        }
    }
}
