using System;
using System.Collections;
using static System.Console;

namespace BitArraySample
{
    class Program
    {
        static void Main(string[] args)
        {
            var bits1 = new BitArray(8);
            bits1.SetAll(true);
            bits1.Set(1, false);
            bits1[5] = false;
            bits1[7] = false;
            Write("initialized: ");
            DisplayBits(bits1);
            WriteLine();

            Write("not ");
            DisplayBits(bits1);
            bits1.Not();
            Write(" = ");
            DisplayBits(bits1);
            WriteLine();

            var bits2 = new BitArray(bits1);
            bits2[0] = true;
            bits2[1] = false;
            bits2[4] = true;
            DisplayBits(bits1);
            Write(" or ");
            DisplayBits(bits2);
            Write(" = ");
            bits1.Or(bits2);
            DisplayBits(bits1);
            WriteLine();

            DisplayBits(bits2);
            Write(" and ");
            DisplayBits(bits1);
            Write(" = ");
            bits2.And(bits1);
            DisplayBits(bits2);
            WriteLine();

            DisplayBits(bits1);
            Write(" xor ");
            DisplayBits(bits2);
            bits1.Xor(bits2);
            Write(" = ");
            DisplayBits(bits1);
            WriteLine();

            ReadLine();
        }

        static void DisplayBits(BitArray bits)
        {
            foreach (bool bit in bits)
            {
                Write(bit ? 1 : 0);
            }
        }

    }
}
