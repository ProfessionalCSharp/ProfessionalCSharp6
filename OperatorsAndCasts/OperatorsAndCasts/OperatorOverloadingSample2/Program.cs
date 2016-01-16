using static System.Console;

namespace OperatorOverloadingSample
{
  class Program
  {
    static void Main()
    {
      // stuff to demonstrate arithmetic operations
      Vector vect1, vect2, vect3;
      vect1 = new Vector(1.0, 1.5, 2.0);
      vect2 = new Vector(0.0, 0.0, -10.0);

      vect3 = vect1 + vect2;

      WriteLine($"vect1 = {vect1}");
      WriteLine($"vect2 = {vect2}");
      WriteLine($"vect3 = vect1 + vect2 = {vect3}");
      WriteLine($"2 * vect3 = {2 * vect3}");
      WriteLine($"vect3 += vect2 gives {vect3 += vect2}");
      WriteLine($"vect3 = vect1 * 2 gives {vect3 = vect1 * 2}");
      WriteLine($"vect1 * vect3 = {vect1 * vect3}");
    }
  }
}
