using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1Demo.Model
{
    public class Racer
    {
        public Racer()
        {
            this.RaceResults = new HashSet<RaceResult>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public Nullable<int> Starts { get; set; }
        public Nullable<int> Wins { get; set; }

        public virtual ICollection<RaceResult> RaceResults { get; set; }
    }
}
