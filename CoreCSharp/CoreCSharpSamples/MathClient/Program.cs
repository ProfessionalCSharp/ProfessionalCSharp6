using MathLib;
using static System.Console;

namespace MathClient
{
    class Program
    {
        static void Main()
        {
            double d1 = 3;
            double d2 = 4;
            double result = Calculator.Add(d1, d2);
            WriteLine(result);
        }
    }
}
