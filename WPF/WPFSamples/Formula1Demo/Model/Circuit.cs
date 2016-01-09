using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1Demo.Model
{
    public class Circuit
    {
        public Circuit()
        {
            this.Races = new HashSet<Race>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public virtual ICollection<Race> Races { get; set; }
    }
}
