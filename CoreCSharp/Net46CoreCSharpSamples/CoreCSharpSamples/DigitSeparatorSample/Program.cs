using static System.Console;

namespace DigitSeparatorSample
{
    class Program
    {
        static void Main()
        {
            // digit separator
            int n1 = 123_456_789;
            WriteLine(n1);
            long n2 = 0x123_4567_89ab_cdef;
            WriteLine($"{n2:x}");

            // binary literals
            int b1 = 0b0000_1111_0000;
            WriteLine(b1);
            int b2 = 0b1111_1111_1111_1111;
            WriteLine(b2);
        }
    }
}
