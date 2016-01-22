using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recognition_of_Handwritten_Digits.Model
{
    class DigitModel
    {
        public byte Digit;
        public byte[] DigitRepresentation;

        public DigitModel(byte digit, byte[] digitRepresentation)
        {
            Digit = digit;
            DigitRepresentation = digitRepresentation;
        }

        public DigitModel(byte digit, int pixelsCount)
        {
            Digit = digit;
            DigitRepresentation = new byte[pixelsCount];
        }
    }
}
