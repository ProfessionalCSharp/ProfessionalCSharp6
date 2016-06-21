using System.Runtime.CompilerServices;
using static System.Console;

namespace PassingByValueAndReference
{
    public class A
    {
        public int X { get; set; }
    }

    class Program
    {
        public static void ChangeA(A a)
        {
            a.X = 2;
        }

        static void Main()
        {

            A a1 = new A { X = 1 };
            ChangeA(a1);
            WriteLine($"a1.X: {a1.X}");

            abc(1);
            abc(1, a3: 10);
            abc(1, a2: 99);

            ReadLine();
        }

        public static void abc(int a1,int a2=3, int a3=5)
        {
            WriteLine(a1 + a2 + a3);
        }
    }
}
