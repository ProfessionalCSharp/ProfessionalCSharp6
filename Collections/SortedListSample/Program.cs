using System.Collections.Generic;
using static System.Console;

namespace Wrox.ProCSharp.Collections
{
    class Program
    {
        static void Main()
        {
            var books = new SortedList<string, string>();
            books.Add("Professional WPF Programming", "978–0–470–04180–2");
            books.Add("Professional ASP.NET MVC 5", "978–1–118-79475–3");

            books["Beginning Visual C# 2012 Programming"] = "978–1–118-31441-8";
            books["Professional C# 5.0 and .NET 4.5.1"] = "978–1–118–83303–2";

            foreach (KeyValuePair<string, string> book in books)
            {
                WriteLine($"{book.Key}, {book.Value}");
            }

            foreach (string isbn in books.Values)
            {
                WriteLine(isbn);
            }

            foreach (string title in books.Keys)
            {
                WriteLine(title);
            }

            {
                string isbn;
                string title = "Professional C# 7.0";
                if (!books.TryGetValue(title, out isbn))
                {
                    WriteLine($"{title} not found");
                }
            }



        }
    }
}
