using System.Collections.Generic;
using static System.Console;

namespace StackSample
{
    class Program
    {
        static void Main()
        {
            var alphabet = new Stack<char>();
            alphabet.Push('A');
            alphabet.Push('B');
            alphabet.Push('C');

            Write("First iteration: ");
            foreach (char item in alphabet)
            {
                Write(item);
            }
            WriteLine();

            Write("Second iteration: ");
            while (alphabet.Count > 0)
            {
                Write(alphabet.Pop());
            }
            WriteLine();


        }
    }
}
