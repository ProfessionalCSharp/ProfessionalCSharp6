using DataLib;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;
using System.Collections;

namespace EnumerableSample
{
    public static class StringExtension
    {
        public static string FirstName(this string name)
        {
            int ix = name.LastIndexOf(' ');
            return name.Substring(0, ix);
        }
        public static string LastName(this string name)
        {
            int ix = name.LastIndexOf(' ');
            return name.Substring(ix + 1);
        }
    }
    class Program
    {
        static void Main()
        {
            // uncomment the corresponding method call as reading through the LINQ chapter
            Filtering();
            // IndexFiltering();
            // TypeFiltering();
            // CompoundFrom();
            // Sorting();
            // Grouping4();
            // GroupingWithNestedObjects();
            // InnerJoin();
            // LeftOuterJoin();
            // GroupJoin2();
            // SetOperations();
            // Except();
            // ZipOperation();
            // Partitioning();
            // Aggregate();
            // Aggregate2();
            // Lookup();
            // Untyped();
            // CombineRacers();
            // SelectMany2();
        }

        private static void SelectMany2()
        {
            // flatten the year list to return a list of all racers and positions in the championship 
            var racers = Formula1.GetChampionships()
              .SelectMany(cs => new List<dynamic>()
             {
                new
                {
                    Year = cs.Year,
                    Position = 1,
                    Name = cs.First
                },
                new
                {
                    Year = cs.Year,
                    Position = 2,
                    Name = cs.Second
                },
                new
                {
                    Year = cs.Year,
                    Position = 3,
                    Name = cs.Third
                }
             });


            foreach (var s in racers)
            {
                WriteLine(s);
            }
        }

        private static void CombineRacers()
        {
            var q = from r in Formula1.GetChampions()
                    join r2 in Formula1.GetChampionships().GetRacerInfo() on
                    new
                    {
                        FirstName = r.FirstName,
                        LastName = r.LastName
                    }
                      equals
                    new
                    {
                        FirstName = r2.FirstName,
                        LastName = r2.LastName
                    }
                    into yearResults
                    select new
                    {
                        FirstName = r.FirstName,
                        LastName = r.LastName,
                        Wins = r.Wins,
                        Starts = r.Starts,
                        Results = yearResults
                    };

            foreach (var item in q)
            {
                WriteLine($"{item.FirstName} {item.LastName}");
                foreach (var item2 in item.Results)
                {
                    WriteLine($"{item2.Year} {item2.Position}");
                }
            }
        }

        private static void Except()
        {
            var racers = Formula1.GetChampionships().SelectMany(cs => new List<RacerInfo>()
               {
                 new RacerInfo {
                   Year = cs.Year,
                   Position = 1,
                   FirstName = cs.First.FirstName(),
                   LastName = cs.First.LastName()
                 },
                 new RacerInfo {
                   Year = cs.Year,
                   Position = 2,
                   FirstName = cs.Second.FirstName(),
                   LastName = cs.Second.LastName()
                 },
                 new RacerInfo {
                   Year = cs.Year,
                   Position = 3,
                   FirstName = cs.Third.FirstName(),
                   LastName = cs.Third.LastName()
                 }
               });


            var nonChampions = racers.Select(r =>
              new
              {
                  FirstName = r.FirstName,
                  LastName = r.LastName
              }).Except(Formula1.GetChampions().Select(r =>
                new
                {
                    FirstName = r.FirstName,
                    LastName = r.LastName
                }));

            foreach (var r in nonChampions)
            {
                WriteLine($"{r.FirstName} {r.LastName}");
            }

        }

        private static void Sorting()
        {
            var racers = (from r in Formula1.GetChampions()
                          orderby r.Country, r.LastName, r.FirstName
                          select r).Take(10);

            foreach (var racer in racers)
            {
                WriteLine($"{racer.Country}: {racer.LastName}, {racer.FirstName}");
            }

        }

        static void Untyped()
        {
            var list = new System.Collections.ArrayList(Formula1.GetChampions() as System.Collections.ICollection);

            var query = from r in list.Cast<Racer>()
                        where r.Country == "USA"
                        orderby r.Wins descending
                        select r;
            foreach (var racer in query)
            {
                WriteLine($"{racer:A}");
            }

        }

        static void ZipOperation()
        {
            var racerNames = from r in Formula1.GetChampions()
                             where r.Country == "Italy"
                             orderby r.Wins descending
                             select new
                             {
                                 Name = r.FirstName + " " + r.LastName
                             };

            var racerNamesAndStarts = from r in Formula1.GetChampions()
                                      where r.Country == "Italy"
                                      orderby r.Wins descending
                                      select new
                                      {
                                          LastName = r.LastName,
                                          Starts = r.Starts
                                      };


            var racers = racerNames.Zip(racerNamesAndStarts, (first, second) => first.Name + ", starts: " + second.Starts);
            foreach (var r in racers)
            {
                WriteLine(r);
            }

        }

        static void Lookup()
        {
            var racers = (from r in Formula1.GetChampions()
                          from c in r.Cars
                          select new
                          {
                              Car = c,
                              Racer = r
                          }).ToLookup(cr => cr.Car, cr => cr.Racer);

            if (racers.Contains("Williams"))
            {
                foreach (var williamsRacer in racers["Williams"])
                {
                    WriteLine(williamsRacer);
                }
            }
        }

        static void Aggregate2()
        {
            var countries = (from c in
                                 from r in Formula1.GetChampions()
                                 group r by r.Country into c
                                 select new
                                 {
                                     Country = c.Key,
                                     Wins = (from r1 in c
                                             select r1.Wins).Sum()
                                 }
                             orderby c.Wins descending, c.Country
                             select c).Take(5);

            foreach (var country in countries)
            {
                WriteLine($"{country.Country} {country.Wins}");
            }

        }

        static void Aggregate()
        {
            var query = from r in Formula1.GetChampions()
                        let numberYears = r.Years.Count()
                        where numberYears >= 3
                        orderby numberYears descending, r.LastName
                        select new
                        {
                            Name = r.FirstName + " " + r.LastName,
                            TimesChampion = numberYears
                        };

            foreach (var r in query)
            {
                WriteLine($"{r.Name} {r.TimesChampion}");
            }
        }

        static void Partitioning()
        {
            int pageSize = 5;

            int numberPages = (int)Math.Ceiling(Formula1.GetChampions().Count() /
                  (double)pageSize);

            for (int page = 0; page < numberPages; page++)
            {
                WriteLine("Page {0}", page);

                var racers =
                   (from r in Formula1.GetChampions()
                    orderby r.LastName, r.FirstName
                    select r.FirstName + " " + r.LastName).
                   Skip(page * pageSize).Take(pageSize);

                foreach (var name in racers)
                {
                    WriteLine(name);
                }
                WriteLine();
            }

        }

        static void SetOperations()
        {
            Func<string, IEnumerable<Racer>> racersByCar =
                car => from r in Formula1.GetChampions()
                       from c in r.Cars
                       where c == car
                       orderby r.LastName
                       select r;

            WriteLine("World champion with Ferrari and McLaren");
            foreach (var racer in racersByCar("Ferrari").Intersect(racersByCar("McLaren")))
            {
                WriteLine(racer);
            }
        }


        static void InnerJoin()
        {
            var racers = from r in Formula1.GetChampions()
                         from y in r.Years
                         select new
                         {
                             Year = y,
                             Name = r.FirstName + " " + r.LastName
                         };

            var teams = from t in Formula1.GetConstructorChampions()
                        from y in t.Years
                        select new
                        {
                            Year = y,
                            Name = t.Name
                        };

            var racersAndTeams =
                  (from r in racers
                   join t in teams on r.Year equals t.Year
                   orderby t.Year
                   select new
                   {
                       Year = r.Year,
                       Champion = r.Name,
                       Constructor = t.Name
                   }).Take(10);

            WriteLine("Year  World Champion\t   Constructor Title");
            foreach (var item in racersAndTeams)
            {
                WriteLine($"{item.Year}: {item.Champion,-20} {item.Constructor}");
            }

        }

        static void GroupJoin2()
        {
            var racers = Formula1.GetChampionships()
              .SelectMany(cs => new List<RacerInfo>()
              {
                 new RacerInfo {
                   Year = cs.Year,
                   Position = 1,
                   FirstName = cs.First.FirstName(),
                   LastName = cs.First.LastName()
                 },
                 new RacerInfo {
                   Year = cs.Year,
                   Position = 2,
                   FirstName = cs.Second.FirstName(),
                   LastName = cs.Second.LastName()
                 },
                 new RacerInfo {
                   Year = cs.Year,
                   Position = 3,
                   FirstName = cs.Third.FirstName(),
                   LastName = cs.Third.LastName()
                 }
              });

            var q = (from r in Formula1.GetChampions()
                     join r2 in racers on
                     new
                     {
                         FirstName = r.FirstName,
                         LastName = r.LastName
                     }
                     equals
                     new
                     {
                         FirstName = r2.FirstName,
                         LastName = r2.LastName
                     }
                     into yearResults
                     select new
                     {
                         FirstName = r.FirstName,
                         LastName = r.LastName,
                         Wins = r.Wins,
                         Starts = r.Starts,
                         Results = yearResults
                     });

            foreach (var r in q)
            {
                WriteLine($"{r.FirstName} {r.LastName}");
                foreach (var results in r.Results)
                {
                    WriteLine($"{results.Year} {results.Position}");
                }
            }

        }

        static void GroupJoin()
        {
            //  var q =
            //from c in categories
            //join p in products on c equals p.Category into ps
            //select new { Category = c, Products = ps }; 

            var racers = from r in Formula1.GetChampions()
                         from y in r.Years
                         select new
                         {
                             Year = y,
                             Name = r.FirstName + " " + r.LastName
                         };

            var teams = from t in Formula1.GetConstructorChampions()
                        from y in t.Years
                        select new
                        {
                            Year = y,
                            Name = t.Name
                        };

            var racersAndTeams =
                  from r in racers
                  join t in teams on r.Year equals t.Year into ts
                  select new
                  {
                      Year = r.Year,
                      Racer = r.Name,
                      Constructor = ts
                  };

            foreach (var r in racersAndTeams)
            {
                WriteLine($"{r.Year} {r.Racer}");
                foreach (var t in r.Constructor)
                {
                    WriteLine($"\t{t.Name}");
                }
            }
        }

        static void CrossJoinWithGroupJoin()
        {
            //  var q =
            //from c in categories
            //join p in products on c equals p.Category into ps
            //from p in ps
            //select new { Category = c, p.ProductName }; 

        }

        static void LeftOuterJoin()
        {
            var racers = from r in Formula1.GetChampions()
                         from y in r.Years
                         select new
                         {
                             Year = y,
                             Name = r.FirstName + " " + r.LastName
                         };

            var teams = from t in Formula1.GetConstructorChampions()
                        from y in t.Years
                        select new
                        {
                            Year = y,
                            Name = t.Name
                        };

            var racersAndTeams =
              (from r in racers
               join t in teams on r.Year equals t.Year into rt
               from t in rt.DefaultIfEmpty()
               orderby r.Year
               select new
               {
                   Year = r.Year,
                   Champion = r.Name,
                   Constructor = t == null ? "no constructor championship" : t.Name
               }).Take(10);

            WriteLine("Year  Champion\t\t   Constructor Title");
            foreach (var item in racersAndTeams)
            {
                WriteLine($"{item.Year}: {item.Champion,-20} {item.Constructor}");
            }
        }


        static void GroupingWithNestedObjects()
        {
            var countries = from r in Formula1.GetChampions()
                            group r by r.Country into g
                            let count = g.Count()
                            orderby count descending, g.Key
                            where count >= 2
                            select new
                            {
                                Country = g.Key,
                                Count = count,
                                Racers = from r1 in g
                                         orderby r1.LastName
                                         select r1.FirstName + " " + r1.LastName
                            };
            foreach (var item in countries)
            {
                WriteLine($"{item.Country,-10} {item.Count}");
                foreach (var name in item.Racers)
                {
                    Console.Write($"{name}; ");
                }
                WriteLine();
            }

        }

        static void Grouping()
        {
            var countries = from r in Formula1.GetChampions()
                            group r by r.Country into g
                            orderby g.Count() descending, g.Key
                            where g.Count() >= 2
                            select new
                            {
                                Country = g.Key,
                                Count = g.Count()
                            };

            foreach (var item in countries)
            {
                WriteLine($"{item.Country,-10} {item.Count}");
            }

        }

        static void Grouping2()
        {
            var countries = Formula1.GetChampions()
              .GroupBy(r => r.Country)
              .OrderByDescending(g => g.Count())
              .ThenBy(g => g.Key)
              .Where(g => g.Count() >= 2)
              .Select(g => new
              {
                  Country = g.Key,
                  Count = g.Count()
              });


            foreach (var item in countries)
            {
                WriteLine($"{item.Country,-10} {item.Count}");
            }

        }

        static void Grouping3()
        {
            var countries = from r in Formula1.GetChampions()
                            group r by r.Country into g
                            let count = g.Count()
                            orderby count descending, g.Key
                            where count >= 2
                            select new
                            {
                                Country = g.Key,
                                Count = count
                            };

            foreach (var item in countries)
            {
                WriteLine($"{item.Country,-10} {item.Count}");
            }

        }

        static void Grouping4()
        {
            var countries = Formula1.GetChampions()
              .GroupBy(r => r.Country)
              .Select(g => new { Group = g, Count = g.Count() })
              .OrderByDescending(g => g.Count)
              .ThenBy(g => g.Group.Key)
              .Where(g => g.Count >= 2)
              .Select(g => new
              {
                  Country = g.Group.Key,
                  Count = g.Count
              });

            foreach (var item in countries)
            {
                WriteLine($"{item.Country,-10} {item.Count}");
            }

        }

        static void CompoundFrom()
        {
            var ferrariDrivers = from r in Formula1.GetChampions()
                                 from c in r.Cars
                                 where c == "Ferrari"
                                 orderby r.LastName
                                 select r.FirstName + " " + r.LastName;

            foreach (var racer in ferrariDrivers)
            {
                WriteLine(racer);
            }

        }

        static void TypeFiltering()
        {
            object[] data = { "one", 2, 3, "four", "five", 6 };
            var query = data.OfType<string>();
            foreach (var s in query)
            {
                WriteLine(s);
            }

        }

        static void IndexFiltering()
        {
            var racers = Formula1.GetChampions().
                    Where((r, index) => r.LastName.StartsWith("A") && index % 2 != 0);
            foreach (var r in racers)
            {
                WriteLine($"{r:A}");
            }
        }


        static void Filtering()
        {
            var racers = from r in Formula1.GetChampions()
                         where r.Wins > 15 && (r.Country == "Brazil" || r.Country == "Austria")
                         select r;

            foreach (var r in racers)
            {
                WriteLine($"{r:A}");
            }
        }
    }

}
