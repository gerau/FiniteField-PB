using FiniteField.GaloisField;
using FiniteField.Helpers;
using System.Diagnostics;
using System.Text;

namespace FiniteField
{
    internal class Program
    {
        static void MeasureTime(int numOfIterations)
        {
            long[] ticks = new long[8];
            Stopwatch st = new();
            for(int i = 0 ; i < numOfIterations; i++)
            {
                var num1 = Generator.Generate();
                var num2 = Generator.Generate();
                st.Start();
                _ = num1 + num2;
                st.Stop();
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine($"Addition ticks = {st.ElapsedTicks}");
                ticks[0] += st.ElapsedTicks;
                st.Restart();

                num1 = Generator.Generate();
                num2 = Generator.Generate();
                st.Start();
                _ = num1 * num2;
                st.Stop();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Multiplication ticks = {st.ElapsedTicks}");
                ticks[1] += st.ElapsedTicks;
                st.Restart();

                num1 = Generator.Generate();
                st.Start();
                _ = num1.ToSquare();
                st.Stop();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"To square ticks = {st.ElapsedTicks}");
                ticks[2] += st.ElapsedTicks;
                st.Restart();

                num1 = Generator.Generate();
                st.Start();
                _ = num1.Trace();
                st.Stop();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Trace ticks = {st.ElapsedTicks}");
                ticks[3] += st.ElapsedTicks;
                st.Restart();

                num1 = Generator.Generate();
                num2 = Generator.Generate();
                st.Start();
                _ = num1.Pow(num2);
                st.Stop();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Power ticks = {st.ElapsedTicks}");
                ticks[4] += st.ElapsedTicks;
                st.Restart();

                num1 = Generator.Generate();
                st.Start();
                _ = num1.InverseElement();
                st.Stop();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Inverse Element ticks = {st.ElapsedTicks}");
                ticks[5] += st.ElapsedTicks;
                st.Restart();

                num1 = Generator.Generate();
                num2 = Generator.Generate();
                st.Start();
                try
                {
                    _ = Field.SolveQuadradicEquation(num1, num2);
                }
                catch
                {
                    Console.WriteLine("Roots don't exists");
                }
                finally { st.Stop(); }
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"SolveQuadratic ticks = {st.ElapsedTicks}");
                ticks[6] += st.ElapsedTicks;
                st.Restart();

                num1 = Generator.Generate();
                st.Start();
                _ = num1.SquareRoot();
                st.Stop();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Square root ticks = {st.ElapsedTicks}");
                ticks[7] += st.ElapsedTicks;
                st.Restart();
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Total amount of ticks(in average):");
            Console.WriteLine($"Addition: {ticks[0]/numOfIterations}");
            Console.WriteLine($"Multiplication: {ticks[1] / numOfIterations}");
            Console.WriteLine($"Square: {ticks[2] / numOfIterations}");
            Console.WriteLine($"Trace: {ticks[3] / numOfIterations}");
            Console.WriteLine($"Power: {ticks[4] / numOfIterations}");
            Console.WriteLine($"Inverse: {ticks[5] / numOfIterations}");
            Console.WriteLine($"Quadratic equation: {ticks[6] / numOfIterations}");
            Console.WriteLine($"Square root: {ticks[7] / numOfIterations}");
        } 

        static void Main()
        {
            Element A = new Element("01011000001011001000101010100110001111011111110111111100011001100101101100110010010111010001110111101110110100010000111000101110010001110100100000101111011011110111110111110011110000010101001000010001100101011000000010011010110110110111110011100010101000011110011011100100010010101111000100001001110101100100011100111000001010011010111100000101111000111001000010100001111011100100011100011000100011001110001110011110111000011111101101011001010101101001110000001110011101001100101111100011100");
            Element B = new Element("11110110111001101011101110100100010100101010111001100100100110110011111000101011010110101000110110001000000100000010110001011010000111010011011100000010111011000010000111011000100111010010111001000011001010000001011011000111001001101011000101000101010001010010110001100000100111010011111000100001001100101001110000100111001100110111000101101001010010111011100011101010111000010111011111100111000101110100011011000101001011111001100100000100110001011110010100110010001000011111000010100101011");

            Element N = new Element("11010110101101000101010101100000100100110100110110111111110111011000101000101110111000000100000010010110011101100011101111000101001000000000011111100100010111111011011000000001100111101001110010111110011111100111001100001000011010011011001011100000101111111100001011000111001110001010110101111111011010110000101100000100011010100111011110001100100110010111010100110110110010110000100100111110110010111101000100001111100101001011111100000110111001010011101010011000010001101110000001010010001");

            var add = A + B;
            var mult = A * B;
            var square = A.ToSquare();
            var inverse = A.InverseElement();
            var power = A.Pow(N);

            var str = new StringBuilder();

            str.AppendLine($"A = {A}");
            str.AppendLine($"B = {B}");
            str.AppendLine($"A + B = {add}");
            str.AppendLine($"A * B = {mult}");
            str.AppendLine($"A^2 = {square}");
            str.AppendLine($"A^(-1) = {inverse}");
            str.AppendLine($"A^N = {power}");
            Console.WriteLine(str);

        }
    }
}