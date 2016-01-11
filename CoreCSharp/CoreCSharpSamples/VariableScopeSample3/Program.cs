using static System.Console;

namespace VariableScopeSample3
{
    class Program
    {
        static int j = 20;
        static int Main(string[] args)
        {
            int j = 30;
            WriteLine(j);
            return 0;
        }
    }
}
