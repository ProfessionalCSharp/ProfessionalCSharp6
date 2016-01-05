using System.Data.Entity;
using Wrox.ProCSharp.WCF.Contracts;

namespace Wrox.ProCSharp.WCF.Data
{
    public class RoomReservationContext : DbContext
    {
        public RoomReservationContext()
            : base(@"server=(localdb)\mssqllocaldb;Database=RoomReservations;trusted_connection=true")
        {

        }

        // use with EF 7
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer(@"server=(localdb)\mssqllocaldb;Database=RoomReservations;trusted_connection=true");
        //}
        public DbSet<RoomReservation> RoomReservations { get; set; }
    }
}
