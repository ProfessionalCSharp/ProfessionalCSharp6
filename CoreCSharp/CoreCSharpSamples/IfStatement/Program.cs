using static System.Console;

namespace IfStatement
{
    class Program
    {
        static void Main()
        {
            WriteLine("Type in a string");
            string input;
            input = ReadLine();
            if (input == "")
            {
                WriteLine("You typed in an empty string.");
            }
            else if (input.Length < 5)
            {
                WriteLine("The string had less than 5 characters.");
            }
            else if (input.Length < 10)
            {
                WriteLine("The string had at least 5 but less than 10 Characters.");
            }
            WriteLine("The string was " + input);

        }
    }
}
