using static System.Console;

namespace Wrox.ProCSharp.Delegates
{
    delegate double DoubleOp(double x);

    class Program
    {
        static void Main()
        {
            DoubleOp[] operations =
            {
                MathOperations.MultiplyByTwo,
                MathOperations.Square
            };

            for (int i = 0; i < operations.Length; i++)
            {
                WriteLine($"Using operations[{i}]:");
                ProcessAndDisplayNumber(operations[i], 2.0);
                ProcessAndDisplayNumber(operations[i], 7.94);
                ProcessAndDisplayNumber(operations[i], 1.414);
                WriteLine();
            }
        }

        static void ProcessAndDisplayNumber(DoubleOp action, double value)
        {
            double result = action(value);
            WriteLine($"Value is {value}, result of operation is {result}");
        }
    }
}
