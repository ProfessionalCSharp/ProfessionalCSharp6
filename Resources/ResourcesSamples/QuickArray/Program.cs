using static System.Console;

namespace QuickArray
{
    public class Program
    {
        unsafe public static void Main()
        {
            Write("How big an array do you want? \n> ");
            string userInput = ReadLine();
            int size = int.Parse(userInput);

            long* pArray = stackalloc long[size];
            for (int i = 0; i < size; i++)
            {
                pArray[i] = i * i;
            }

            for (int i = 0; i < size; i++)
            {
                WriteLine($"Element {i} = {*(pArray + i)}");
            }

            ReadLine();
        }
    }
}
