using Microsoft.Data.Entity;
using Wrox.ProCSharp.WCF.Contracts;

namespace Wrox.ProCSharp.WCF.Data
{
    public class RoomReservationContext : DbContext
    {
        private const string ConnectionString = @"server=(localdb)\mssqllocaldb;Database=RoomReservations;trusted_connection=true";
        public RoomReservationContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(ConnectionString);
        }
        public DbSet<RoomReservation> RoomReservations { get; set; }
    }
}
