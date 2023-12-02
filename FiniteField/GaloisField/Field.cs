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
        public static readonly int[] GeneratorDegrees = { 0, 2, 6, 17, 491 };

        internal static int PolynomDegree(bool[] input)
        {
            for (int i = input.Length - 1; i >= 0; i--)
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
                foreach (var gen in GeneratorDegrees)
                {
                    input[degree - M + gen] = input[degree - M + gen] ^ true;
                }
                degree = PolynomDegree(input);
            }
            bool[] output = new bool[M];
            for (int i = 0; i < Field.M; i++)
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
            for (int i = 0; i < M; i++)
            {
                output[i] = true;
            }
            return output;
        }
        public static (Element, Element) SolveQuadradicEquation(Element a, Element b)
        {
            Element output;
            if (a == Field.Zero())
            {
                output = b.SquareRoot();
                return (output, output);
            }
            if (b == Field.Zero())
            {
                return (a, Field.Zero());
            }
            Element tempA = a.ToSquare();
            tempA = tempA.InverseElement();
            Element C = b * tempA;
            if (C.Trace() == Field.One())
            {
                throw new Exception("Solution for quadratic equation doesn't exist.");
            }
            else
            {
                output = C.HalfTrace() * a;
                var output2 = output + a;
                return (output, output2);
            }
        }
    }


}
