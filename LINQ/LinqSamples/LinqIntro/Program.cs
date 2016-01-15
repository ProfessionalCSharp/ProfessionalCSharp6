using DataLib;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace LinqIntro
{
    class Program
    {
        static void Main()
        {
            LINQQuery();
            ExtensionMethods();
            DeferredQuery();
        }

        static void DeferredQuery()
        {
            var names = new List<string> { "Nino", "Alberto", "Juan", "Mike", "Phil" };

            var namesWithJ = from n in names
                             where n.StartsWith("J")
                             orderby n
                             select n;

            WriteLine("First iteration");
            foreach (string name in namesWithJ)
            {
                WriteLine(name);
            }
            WriteLine();

            names.Add("John");
            names.Add("Jim");
            names.Add("Jack");
            names.Add("Denny");

            WriteLine("Second iteration");
            foreach (string name in namesWithJ)
            {
                WriteLine(name);
            }
            WriteLine();

        }

        static void ExtensionMethods()
        {
            var champions = new List<Racer>(Formula1.GetChampions());
            IEnumerable<Racer> brazilChampions =
                champions.Where(r => r.Country == "Brazil").
                        OrderByDescending(r => r.Wins).
                        Select(r => r);

            foreach (Racer r in brazilChampions)
            {
                WriteLine($"{r:A}");
            }
            WriteLine();
        }


        static void LINQQuery()
        {
            var query = from r in Formula1.GetChampions()
                        where r.Country == "Brazil"
                        orderby r.Wins descending
                        select r;

            foreach (var r in query)
            {
                WriteLine($"{r:A}");
            }
            WriteLine();
        }
    }
}
