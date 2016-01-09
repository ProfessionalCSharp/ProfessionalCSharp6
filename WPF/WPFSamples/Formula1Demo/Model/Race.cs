using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1Demo.Model
{
    public class Race
    {
        public Race()
        {
            this.RaceResults = new HashSet<RaceResult>();
        }

        public int Id { get; set; }
        public int CircuitId { get; set; }
        public System.DateTime Date { get; set; }

        public virtual Circuit Circuit { get; set; }
        public virtual ICollection<RaceResult> RaceResults { get; set; }
    }
}
