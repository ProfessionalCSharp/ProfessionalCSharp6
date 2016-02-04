using System;
using static System.Console;

namespace TuplesSample
{
    class Program
    {
        static void Main()
        {
            Tuple<string, string> name = new Tuple<string, string>("Jochen", "Rindt");
            WriteLine(name.ToString());

            var result = Divide(5, 2);
            WriteLine($"result of division: {result.Item1}, remainder: {result.Item2}");

            AnyElementNumber();
        }

        static void AnyElementNumber()
        {
            var tuple = Tuple.Create<string, string, string, int, int, int, double, Tuple<int, int>>(
                "Stephanie", "Alina", "Nagel", 2009, 6, 2, 1.37, Tuple.Create<int, int>(52, 3490));
            WriteLine(tuple.Item1);
        }

        public static Tuple<int, int> Divide(int dividend, int divisor)
        {
            int result = dividend / divisor;
            int remainder = dividend % divisor;

            return Tuple.Create(result, remainder);
        }
    }
}
