using Microsoft.EntityFrameworkCore;

namespace BooksSample
{
    public class BooksContext : DbContext
    {
        private const string ConnectionString = @"server=(localdb)\MSSQLLocalDb;database=BooksConcurrency;trusted_connection=true";
        public DbSet<Book> Books { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var book = modelBuilder.Entity<Book>();
            book.HasKey(p => p.BookId);
            book.Property(p => p.Title).HasMaxLength(120).IsRequired();
            book.Property(p => p.Publisher).HasMaxLength(50);
            book.Property(p => p.TimeStamp)
                .HasColumnType("timestamp")
                .ValueGeneratedOnAddOrUpdate()
                .IsConcurrencyToken();
        }
    }
}
