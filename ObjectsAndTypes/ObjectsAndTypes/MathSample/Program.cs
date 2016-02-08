using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Console;

namespace MathSample
{
    class Program
    {
        static void Main()
        {
            // Try calling some static functions.
            WriteLine($"Pi is {Math.GetPi()}");
            int x = Math.GetSquareOf(5);
            WriteLine($"Square of 5 is {x}");

            // Instantiate a Math object
            var math = new Math();   // instantiate a reference type

            // Call instance members
            math.Value = 30;
            WriteLine($"Value field of math variable contains {math.Value}");
            WriteLine($"Square of 30 is {math.GetSquare()}");

        }
    }
}
