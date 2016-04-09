using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace LocalFunctionSample
{
    class Program
    {
        static void Main(string[] args)
        {
            // local function, requires compilation symbol or 
            int Foo(int x)
            {
                return x + 42;
            }
            WriteLine($"{Foo(3)}");

            int a = 1;
            int Bar(int x)  // accessing variables extern to the function
            {
                return x + a;
            }
            WriteLine($"{Bar(2)}");

            void FooBar(ref int a1)
            {
                WriteLine($"{nameof(FooBar)}, received a: {a1}");
                a1++;
            }

            int a2 = 1;
            FooBar(ref a2);
            WriteLine($"received a1 {a2}");


        }
    }
}
