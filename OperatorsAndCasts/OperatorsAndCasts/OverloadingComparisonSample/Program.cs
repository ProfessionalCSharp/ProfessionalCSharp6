using System;
using static System.Console;

namespace OverloadingComparisonSample
{
    class Program
    {
        static void Main()
        {
            var vect1 = new Vector(3.0, 3.0, -10.0);
            var vect2 = new Vector(3.0, 3.0, -10.0);
            var vect3 = new Vector(2.0, 3.0, 6.0);

            WriteLine($"vect1 == vect2 returns {(vect1 == vect2)}");
            WriteLine($"vect1 == vect3 returns {(vect1 == vect3)}");
            WriteLine($"vect2 == vect3 returns {(vect2 == vect3)}");

            WriteLine();

            WriteLine($"vect1 != vect2 returns {(vect1 != vect2)}");
            WriteLine($"vect1 != vect3 returns {(vect1 != vect3)}");
            WriteLine($"vect2 != vect3 returns {(vect2 != vect3)}");

            var vect4 = new Vector(5.0, 2.0, 0);
            var vect5 = new Vector(2.0, 5.0, 0);
            WriteLine(vect4.GetHashCode());
            WriteLine(vect5.GetHashCode());
        }
    }
}
