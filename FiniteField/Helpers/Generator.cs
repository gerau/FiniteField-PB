using FiniteField.GaloisField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteField.Helpers
{
    public class Generator
    {
        public static Element Generate()
        {
            Random rand = new Random();
            string s = "";
            for(int i = 0; i < Field.M; i++)
            {
                s += rand.NextDouble() >= 0.5 ? '0' : '1';
            }
            return Convertor.FromBinary(s);
        }
    }
}
