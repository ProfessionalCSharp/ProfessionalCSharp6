using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1Demo.Model
{
    public class RaceResult
    {
        public int Id { get; set; }
        public int RaceId { get; set; }
        public int Position { get; set; }
        public Nullable<int> Grid { get; set; }
        public Nullable<decimal> Points { get; set; }
        public int RacerId { get; set; }
        public int TeamId { get; set; }

        public virtual Race Race { get; set; }
        public virtual Racer Racer { get; set; }
        public virtual Team Team { get; set; }
    }
}
