using System;
using static System.Console;

namespace MulticastDelegates
{
  class Program
  {
    static void Main()
    {
      Action<double> operations = MathOperations.MultiplyByTwo;
      operations += MathOperations.Square;

      ProcessAndDisplayNumber(operations, 2.0);
      ProcessAndDisplayNumber(operations, 7.94);
      ProcessAndDisplayNumber(operations, 1.414);
      WriteLine();

    }

    static void ProcessAndDisplayNumber(Action<double> action, double value)
    {
      WriteLine();
      WriteLine($"ProcessAndDisplayNumber called with value = {value}");
      action(value);
    }

  }
}
