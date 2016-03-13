using Microsoft.Data.Entity;

namespace Formula1Demo.Model
{
    public class Formula1Context : DbContext
    {
        private const string connectionString = @"server=(localdb)\MSSQLLocalDb;database=Formula1;trusted_connection=true";

        public Formula1Context()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(connectionString);
        }
        public virtual DbSet<Circuit> Circuits { get; set; }
        public virtual DbSet<Race> Races { get; set; }
        public virtual DbSet<RaceResult> RaceResults { get; set; }
        public virtual DbSet<Racer> Racers { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
    }
}
