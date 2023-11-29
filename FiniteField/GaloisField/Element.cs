using FiniteField.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteField.GaloisField
{
    public static class Field
    {
        public const int M = 491;
        public static readonly int[] GeneratorDegrees = { 0,2,6,17,491 };

        internal static int PolynomDegree(bool[] input)
        {
            for(int i = input.Length - 1; i >= 0; i--)
            {
                if (input[i])
                {
                    return i;
                }
            }
            return -1;
        }

        internal static bool[] Modulo(bool[] input)
        {
            int degree = PolynomDegree(input);
            if (degree < M)
            {
                return input;
            }
            while (degree >= M)
            {
                foreach(var gen in GeneratorDegrees)
                {
                    input[degree - M + gen] = input[degree - M + gen] ^ true; 
                }
                degree = PolynomDegree(input);
            }
            bool[] output = new bool[M];
            Array.Copy(input, 0, output, 0,M);
            return output;
        }
    }


    public class Element
    {
        internal bool[] Data { get; }

        public Element()
        {
            Data = new bool[Field.M];
        }
        public Element(bool[] data)
        {
            Data = data;
        }
        public Element(Element element)
        {
            Data = element.Data;
        }
        

        public static Element operator + (Element left, Element right)
        {
            Element output = new();
            for(int i = 0; i < Field.M; i++)
            {
                output[i] = left[i] ^ right[i];
            }
            return output;
        }

        public static Element operator << (Element left, int shift)
        {
            Element output = new();
            for(int i = 0; i < Field.M - shift; i++)
            {
                output[i + shift] = left[i];
            }
            return output;
        }

        public static Element operator * (Element left, Element right)
        {
            bool[] temp = new bool[2 * Field.M - 1];
            for (int i = 0; i < Field.M; i++)
            {
                for (int j = 0; j < Field.M; j++)
                {
                    temp[i + j] ^= left[i] & right[j];
                }
            }
            bool[] output = Field.Modulo(temp);
            return new Element(output);
        }

        public Element ToSquare()
        {
            bool[] temp = new bool[2 * Field.M - 1];
            for (int i = 0; i < Field.M; i++)
            {
                temp[i * 2] = this[i];
            }
            bool[] output = Field.Modulo(temp);
            return new Element(output);
        }
        public bool Trace()
        {
            Element temp = new(this);
            Element output = new();
            for (int i = 0; i < Field.M; i++)
            {
                temp = temp.ToSquare();
                output += temp;
            }
            return output[0];
        }
        public override string ToString()
        {
            return Convertor.ToBinary(this);
        }
        public string ToHexString()
        {
            return Convertor.ToHex(this);
        }
        public bool this[int i]
        {
            get { return Data[i]; }
            set { Data[i] = value; }
        }

    }
}
