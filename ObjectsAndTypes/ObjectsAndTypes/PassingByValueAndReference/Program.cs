using static System.Console;

namespace PassingByValueAndReference
{
    public class A
    {
        public int X { get; set; }
    }

    public class Program
    {
        public static void ChangeA(A a)
        {
            a.X = 2;
        }

        public static void Main()
        {

            A a1 = new A { X = 1 };
            ChangeA(a1);
            WriteLine($"a1.X: {a1.X}");
            ReadLine();
        }
    }
}
