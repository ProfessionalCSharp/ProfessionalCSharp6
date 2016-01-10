using static System.Console;

namespace StringSample
{
    class Program
    {
        static void Main()
        {
            string s1 = "a string";
            string s2 = s1;
            WriteLine("s1 is " + s1);
            WriteLine("s2 is " + s2);
            s1 = "another string";
            WriteLine("s1 is now " + s1);
            WriteLine("s2 is now " + s2);
        }
    }
}
