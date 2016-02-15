using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace MenusSample
{
    public class MenusContext : DbContext
    {
        private const string ConnectionString = @"server=(localdb)\MSSQLLocalDb;Database=MenuCards;Trusted_Connection=True";
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuCard> MenuCards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.HasDefaultSchema("mc");
            //modelBuilder.Entity<MenuCard>()
            //    .ToTable("MenuCards")
            //    .HasKey(c => c.MenuCardId);
            //modelBuilder.Entity<MenuCard>().HasMany(c => c.Menus).WithOne(m => m.MenuCard); q
                
            //modelBuilder.Entity<Menu>().ToTable("Menus").HasKey(m => m.MenuId)
            //modelBuilder.Entity(typeof(Menu)).ToTable("Menus");
            //modelBuilder.Entity<MenuCard>().HasMany<Menu>(m => m.Menus).WithOne()
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

    }
}
