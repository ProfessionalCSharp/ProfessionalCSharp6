using static System.Console;

namespace OutKeywordSample
{
    class Program
    {
        static void Main()
        {
            int? x = 3;
            int x1 = x.HasValue ? x.Value : -1;
            int x2 = x ?? -1;

            
            // version 1
            string input1 = ReadLine();
            int n = int.Parse(input1);
            WriteLine($"n: {n}");

            // version 2
            string input2 = ReadLine();
            int result;
            if (int.TryParse(input2, out result))
            {
                WriteLine($"n: {n}");
            }
            else
            {
                WriteLine("not a number");
            }
            ReadLine();
        }
    }
}
