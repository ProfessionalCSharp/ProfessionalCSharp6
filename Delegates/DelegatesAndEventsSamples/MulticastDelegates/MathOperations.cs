using static System.Console;

namespace MulticastDelegates
{
  class MathOperations
  {
    public static void MultiplyByTwo(double value)
    {
      double result = value * 2;
      WriteLine($"Multiplying by 2: {value} gives {result}");
    }

    public static void Square(double value)
    {
      double result = value * value;
      WriteLine($"Squaring: {value} gives {result}");
    }
  }

}