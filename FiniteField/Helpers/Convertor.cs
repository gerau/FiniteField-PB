using FiniteField.GaloisField;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteField.Helpers
{
    internal class Convertor
    {
        public static uint HexSymbolIntoDigit(char c)
        {
            if ("0123456789".Contains(c))
            {
                return (uint)c - 48;
            }
            else if ("ABCDEF".Contains(c))
            {
                return (uint)c - 55;
            }
            else if ("abcdef".Contains(c))
            {
                return (uint)c - 87;
            }
            throw new ArgumentException("Incorrect symbol");
        }

        public static char DigitIntoHexSymbol(uint i, bool isSmall = false)
        {
            if ((i >= 0) && (i < 10))
            {
                return (char)(48 + i);
            }
            else if ((i >= 10) && (i < 16))
            {
                if (isSmall)
                {
                    return (char)(87 + i);
                }
                else
                {
                    return (char)(55 + i);
                }
            }
            throw new ArgumentException("Incorrect number");
        }

        public static string ToBinary(Element input)
        {
            string output = ""; 
            for(int i = Field.M - 1; i >= 0; i--)
            {
                if (input[i]) 
                {
                    output += "1";
                }
                else
                {
                    output += "0";
                }
                
            }
            return output;
        }
        public static Element FromBinary(string input)
        {
            Element output = new();
            var temp = "";
            temp = input.PadLeft(Field.M, '0');
            temp = new(temp.Reverse().ToArray());
            for(int i = temp.Length - Field.M; i < Field.M; i++)
            {
                if(temp.ElementAt(i) == '0') 
                {
                    continue;
                }
                else if(temp.ElementAt(i) == '1')
                {
                    output[i] = true;
                }
                else
                {
                    throw new ArgumentException("Error: incorrect symbol in binary;");
                }
            }
            return output;
        }
        public static string ToHex(Element input, bool isSmall = false)
        {
            string output = "";
            string binary = ToBinary(input);
            binary = binary.PadLeft(Field.M + 4 - Field.M % 4,'0');

            for(int i = 0; i < binary.Length/4; i++)
            {
                int sum = 0;
                for(int j = 3; j >= 0; j--) 
                {
                    if (binary[i*4 + 3 - j] == '1')  sum += 1 << j; 
                }
                output += DigitIntoHexSymbol((uint)sum, isSmall);
            }
            return output;
        }
        public static Element FromHex(string input)
        {
            string output = "";
            foreach(char c in input)
            {
                var bin = Convert.ToString(HexSymbolIntoDigit(c), 2);
                bin = bin.PadLeft(4,'0');
                output += bin;
            }

            while (output.Length > Field.M)
            {
                output = output.Remove(0, 1);
            }
            return FromBinary(output);
        }
    }
}
