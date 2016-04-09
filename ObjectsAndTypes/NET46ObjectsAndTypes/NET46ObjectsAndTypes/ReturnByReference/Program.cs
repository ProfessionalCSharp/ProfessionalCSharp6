using System;
using static System.Console;

namespace ReturnByReference
{
    class Program
    {
        static void Main()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            int n1 = Find(numbers, x => x >= 3);
            WriteLine($"{n1} == {numbers[2]} {n1 == numbers[2]}");
            n1 = 7;
            WriteLine($"changed {nameof(n1)} to 7, this is the value of {nameof(numbers)}[2]: {numbers[2]}");
            ref int n2 = ref FindRef(numbers, x => x >= 4);
            WriteLine($"{n2} == {numbers[3]} {n2 == numbers[3]}");
            n2 = 7;
            WriteLine($"changed {nameof(n2)} to 7, this is the value of {nameof(numbers)}[3]: {numbers[3]}");
        }

        static int Find(int[] numbers, Func<int, bool> pred)
        {
            int i;
            for (i = 0; !pred(numbers[i]); i++) ;
            return numbers[i];  
        }

        static ref int FindRef(int[] numbers, Func<int, bool> pred)
        {
            int i;
            for (i = 0; !pred(numbers[i]); i++) ;
            return ref numbers[i];
        }
    }
}
