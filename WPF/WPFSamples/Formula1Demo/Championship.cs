using Formula1Demo.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Formula1Demo
{
    public class Championship
    {
        public int Year { get; set; }

        private IEnumerable<F1Race> GetRaces()
        {
            using (var context = new Formula1Context())
            {
                return (from r in context.Races
                        where r.Date.Year == Year
                        orderby r.Date
                        select new F1Race
                        {
                            Date = r.Date,
                            Country = r.Circuit.Country
                        }).ToList();
            }
        }

        public Lazy<IEnumerable<F1Race>> Races => new Lazy<IEnumerable<F1Race>>(() => GetRaces());
    }

    public class F1Race
    {
        public string Country { get; set; }
        public DateTime Date { get; set; }

        private IEnumerable<F1RaceResult> GetResults()
        {
            using (var context = new Formula1Context())
            {
                return (from rr in context.RaceResults
                        where rr.Race.Date == this.Date
                        select new F1RaceResult
                        {
                            Position = rr.Position,
                            Racer = rr.Racer.FirstName + " " + rr.Racer.LastName,
                            Car = rr.Team.Name
                        }).ToList();
            }
        }

        public Lazy<IEnumerable<F1RaceResult>> Results => new Lazy<IEnumerable<F1RaceResult>>(() => GetResults());
    }

    public class F1RaceResult
    {
        public int Position { get; set; }
        public string Racer { get; set; }
        public string Car { get; set; }
    }
}
