using Microsoft.EntityFrameworkCore;
using Wrox.ProCSharp.WCF.Contracts;
using static System.Console;

namespace Wrox.ProCSharp.WCF.Data
{
    public class RoomReservationContext : DbContext
    {
        private const string ConnectionString = @"server=(localdb)\mssqllocaldb;Database=RoomReservations;trusted_connection=true";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(ConnectionString);
        }
        public DbSet<RoomReservation> RoomReservations { get; set; }
    }
}
