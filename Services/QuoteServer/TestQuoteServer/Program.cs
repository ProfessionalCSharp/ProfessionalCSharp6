using Wrox.ProCSharp.WinServices;
using static System.Console;

namespace TestQuoteServer
{
    public class Program
    {
        public static void Main()
        {
            var qs = new QuoteServer("quotes.txt", 4567);
            qs.Start();
            WriteLine("Hit return to exit");
            ReadLine();
            qs.Stop();

        }
    }
}
