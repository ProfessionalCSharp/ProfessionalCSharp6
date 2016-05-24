using Microsoft.EntityFrameworkCore;

namespace MenuPlanner.Models
{
    public class MenuCardsContext : DbContext
    {
        public MenuCardsContext(DbContextOptions<MenuCardsContext> options)
            : base(options)
        {
        }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuCard> MenuCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuCard>().ForSqlServerToTable("MenuCards");
            modelBuilder.Entity<Menu>().ForSqlServerToTable("Menus");
            modelBuilder.Entity<Menu>().Property(p => p.Text).HasMaxLength(50).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }

}
