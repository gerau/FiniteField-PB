using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteField.GaloisField
{
    public static class Field
    {
        public const int M = 163;
        public static bool[] Generator { get; }

        public static readonly int[] GeneratorDegrees = { 0,3,6,7,163 };
        static Field()
        {
            Generator = new bool[M + 1];
            Generator[0] = true;
            Generator[3] = true;
            Generator[6] = true;
            Generator[7] = true;
            Generator[163] = true;
        }
        internal static int PolynomDegree(bool[] input)
        {
            for(int i = 0; i < input.Length; i--)
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
                return new bool[M];
            }
            while (degree >= M)
            {
                foreach(var gen in GeneratorDegrees)
                {
                    input[degree - 163 + gen] = input[degree - 163 + gen] ^ true; 
                }
                degree = PolynomDegree(input);
            }
            bool[] output = new bool[M];
            Array.Copy(input, M, output, 0,M);
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

        public static Element operator + (Element left, Element right)
        {
            Element output = new();
            for(int i = 0; i < Field.M; i++)
            {
                output[i] = left[i] ^ right[i];
            }
            return output;
        }

        public static Element operator * (Element left, Element right)
        {
            bool[] temp = new bool[2*Field.M];
            for(int i = 0; i < Field.M; i++)
            {
                for(int j = 0; j < Field.M; i++)
                {
                    temp[i + j] = left[i] ^ right[j];
                }
            }
            bool[] output = Field.Modulo(temp);
            return new Element(output);
        }


        public bool this[int i]
        {
            get { return Data[i]; }
            set { Data[i] = value; }
        }

    }
}
