using System;
using System.Collections.Generic;
using static System.Console;

namespace SetSample
{
    class Program
    {
        static void Main()
        {
            var companyTeams = new HashSet<string>() { "Ferrari", "McLaren", "Mercedes" };
            var traditionalTeams = new HashSet<string>() { "Ferrari", "McLaren" };
            var privateTeams = new HashSet<string>() { "Red Bull", "Lotus", "Toro Rosso", "Force India", "Sauber" };

            if (privateTeams.Add("Williams"))
                WriteLine("Williams added");
            if (!companyTeams.Add("McLaren"))
                WriteLine("McLaren was already in this set");

            if (traditionalTeams.IsSubsetOf(companyTeams))
            {
                WriteLine("traditionalTeams is subset of companyTeams");
            }

            if (companyTeams.IsSupersetOf(traditionalTeams))
            {
                WriteLine("companyTeams is a superset of traditionalTeams");
            }


            traditionalTeams.Add("Williams");
            if (privateTeams.Overlaps(traditionalTeams))
            {
                WriteLine("At least one team is the same with traditional and private teams");
            }

            var allTeams = new SortedSet<string>(companyTeams);
            allTeams.UnionWith(privateTeams);
            allTeams.UnionWith(traditionalTeams);

            WriteLine();
            WriteLine("all teams");
            foreach (var team in allTeams)
            {
                WriteLine(team);
            }

            allTeams.ExceptWith(privateTeams);
            WriteLine();
            WriteLine("no private team left");
            foreach (var team in allTeams)
            {
                WriteLine(team);
            }

        }
    }
}
