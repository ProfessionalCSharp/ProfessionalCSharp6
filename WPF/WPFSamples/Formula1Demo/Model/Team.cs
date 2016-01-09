using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1Demo.Model
{
    public class Team
    {
        public Team()
        {
            this.RaceResults = new HashSet<RaceResult>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RaceResult> RaceResults { get; set; }
    }
}
