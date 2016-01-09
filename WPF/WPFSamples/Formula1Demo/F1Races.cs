using Formula1Demo.Model;
using System.Collections.Generic;
using System.Linq;

namespace Formula1Demo
{
    public class F1Races
    {
        private int _lastpageSearched = -1;
        private IEnumerable<object> _cache = null;

        public IEnumerable<object> GetRaces(int page, int pageSize)
        {
            using (var data = new Formula1Context())
            {
                if (_lastpageSearched == page)
                    return _cache;
                _lastpageSearched = page;

                var q = (from r in data.Races
                         from rr in r.RaceResults
                         orderby r.Date ascending
                         select new
                         {
                             Year = r.Date.Year,
                             Country = r.Circuit.Country,
                             Position = rr.Position,
                             Racer = rr.Racer.FirstName + " " + rr.Racer.LastName,
                             Car = rr.Team.Name,
                             Points = rr.Points
                         }).Skip(page * pageSize).Take(pageSize);
                _cache = q.ToList();
                return _cache;
            }

        }
    }
}
