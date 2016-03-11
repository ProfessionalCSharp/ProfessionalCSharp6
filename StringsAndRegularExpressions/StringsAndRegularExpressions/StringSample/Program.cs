using System.Text;
using static System.Console;

namespace StringSample
{
    class Program
    {
        static void Main()
        {
            SimpleStrings();
            UsingStringBuilder();
            ReadLine();
        }

        public static void UsingStringBuilder()
        {
            var greetingBuilder =
                new StringBuilder("Hello from all the guys at Wrox Press. ", 150);
            greetingBuilder.AppendFormat("We do hope you enjoy this book as much as we " +
                "enjoyed writing it");

            WriteLine("Not Encoded:\n" + greetingBuilder);

            for (int i = 'z'; i >= 'a'; i--)
            {
                char old1 = (char)i;
                char new1 = (char)(i + 1);
                greetingBuilder = greetingBuilder.Replace(old1, new1);
            }

            for (int i = 'Z'; i >= 'A'; i--)
            {
                char old1 = (char)i;
                char new1 = (char)(i + 1);
                greetingBuilder = greetingBuilder.Replace(old1, new1);
            }

            WriteLine("Encoded:\n" + greetingBuilder);

        }

        public static void SimpleStrings()
        {
            string greetingText = "Hello from all the guys at Wrox Press. ";
            greetingText += "We do hope you enjoy this book as much as we enjoyed writing it.";

            WriteLine("Not Encoded:\n" + greetingText);

            for (int i = 'z'; i >= 'a'; i--)
            {
                char old1 = (char)i;
                char new1 = (char)(i + 1);
                greetingText = greetingText.Replace(old1, new1);
            }

            for (int i = 'Z'; i >= 'A'; i--)
            {
                char old1 = (char)i;
                char new1 = (char)(i + 1);
                greetingText = greetingText.Replace(old1, new1);
            }

            WriteLine($"Encoded:\n {greetingText}");

        }
    }
}
