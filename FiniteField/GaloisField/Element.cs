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
            while (degree >= M)
            {
                foreach(var gen in GeneratorDegrees)
                {
                    input[degree - M + gen] = input[degree - M + gen] ^ true; 
                }
                degree = PolynomDegree(input);
            }
            bool[] output = new bool[M];
            for(int i = 0; i < Field.M; i++)
            {
                output[i] = input[i];
            }
            return output;
        }
        public static Element One()
        {
            Element output = new();
            output[0] = true;
            return output;
        }
        public static Element Zero()
        {
            return new Element();
        }
        public static Element MaxValue()
        {
            Element output = new();
            for(int i = 0; i < M; i++)
            {
                output[i] = true;
            }
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
                    temp[i + j] ^= left.Data[i] & right.Data[j];
                }
            }
            return new Element(Field.Modulo(temp));
        }
        public static bool operator == (Element left, Element right)
        {
            for(int i = 0; i < Field.M; i++)
            {
                if (left[i]^right[i])
                {
                    return false;
                }
            }
            return true;
        }
        public static bool operator != (Element left, Element right)
        {
            return !(left == right);
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
        public Element Trace()
        {
            Element temp = new(this);
            Element output = new();
            for (int i = 0; i < Field.M; i++)
            {
                temp = temp.ToSquare();
                output += temp;
            }
            return output;
        }
        public Element Pow(Element power)
        {
            Element output = Field.One();
            for(int i = Field.M - 1; i > 0; i--)
            {
                if (power[i])
                {
                    output *= this;
                }
                output = output.ToSquare();
            }
            if (power[0])
            {
                output *= this;
            }
            return output;
        }
        public Element InverseElement()
        {
            Element temp = new(this);
            Element output = Field.One();
            for(int i = 0; i < Field.M - 1; i++)
            {
                temp = temp.ToSquare();
                output *= temp;
            }
            return output; 

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
