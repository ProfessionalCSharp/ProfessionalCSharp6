using static System.Console;

namespace ArgumentsSample
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                WriteLine(args[i]);
            }

        }
    }
}
