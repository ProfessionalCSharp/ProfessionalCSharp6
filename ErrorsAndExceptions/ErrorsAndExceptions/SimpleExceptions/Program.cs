using System;
using static System.Console;

namespace SimpleExceptions
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                try
                {
                    string userInput;

                    Write("Input a number between 0 and 5 or just hit return to exit)> ");
                    userInput = ReadLine();

                    if (string.IsNullOrEmpty(userInput))
                    {
                        break;
                    }

                    int index = Convert.ToInt32(userInput);

                    if (index < 0 || index > 5)
                    {
                        throw new IndexOutOfRangeException($"You typed in {userInput}");
                    }

                    WriteLine($"Your number was {index}");
                }
                catch (IndexOutOfRangeException ex)
                {
                    WriteLine($"Exception: Number should be between 0 and 5. {ex.Message}");
                }
                catch (Exception ex)
                {
                    WriteLine($"An exception was thrown. Message was: {ex.Message}");
                }
                finally
                {
                    WriteLine("Thank you\n");
                }
            }
        }
    }
}
