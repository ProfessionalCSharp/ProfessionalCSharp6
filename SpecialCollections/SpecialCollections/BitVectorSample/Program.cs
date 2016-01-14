using System.Collections.Specialized;
using System.Text;
using static System.Console;

namespace BitVectorSample
{
    class Program
    {
        static void Main()
        {
            // create a mask using the CreateMask method
            var bits1 = new BitVector32();
            int bit1 = BitVector32.CreateMask();
            int bit2 = BitVector32.CreateMask(bit1);
            int bit3 = BitVector32.CreateMask(bit2);
            int bit4 = BitVector32.CreateMask(bit3);
            int bit5 = BitVector32.CreateMask(bit4);

            bits1[bit1] = true;
            bits1[bit2] = false;
            bits1[bit3] = true;
            bits1[bit4] = true;
            bits1[bit5] = true;
            WriteLine(bits1);

            // create a mask using an indexer
            bits1[0xabcdef] = true;
            WriteLine(bits1);

            int received = 0x79abcdef;

            BitVector32 bits2 = new BitVector32(received);
            WriteLine(bits2);

            // sections: FF EEE DDD CCCC BBBBBBBB
            // AAAAAAAAAAAA
            BitVector32.Section sectionA = BitVector32.CreateSection(0xfff);
            BitVector32.Section sectionB = BitVector32.CreateSection(0xff, sectionA);
            BitVector32.Section sectionC = BitVector32.CreateSection(0xf, sectionB);
            BitVector32.Section sectionD = BitVector32.CreateSection(0x7, sectionC);
            BitVector32.Section sectionE = BitVector32.CreateSection(0x7, sectionD);
            BitVector32.Section sectionF = BitVector32.CreateSection(0x3, sectionE);

            WriteLine($"Section A: {IntToBinaryString(bits2[sectionA], true)}");
            WriteLine($"Section B: {IntToBinaryString(bits2[sectionB], true)}");
            WriteLine($"Section C: {IntToBinaryString(bits2[sectionC], true)}");
            WriteLine($"Section D: {IntToBinaryString(bits2[sectionD], true)}");
            WriteLine($"Section E: {IntToBinaryString(bits2[sectionE], true)}");
            WriteLine($"Section F: {IntToBinaryString(bits2[sectionF], true)}");


            ReadLine();
        }

        static string IntToBinaryString(int bits, bool removeTrailingZero)
        {
            var sb = new StringBuilder(32);

            for (int i = 0; i < 32; i++)
            {
                if ((bits & 0x80000000) != 0)
                {
                    sb.Append("1");
                }
                else
                {
                    sb.Append("0");
                }
                bits = bits << 1;
            }

            string s = sb.ToString();
            if (removeTrailingZero)
            {
                return s.TrimStart('0');
            }
            else
            {
                return s;
            }
        }

    }
}
