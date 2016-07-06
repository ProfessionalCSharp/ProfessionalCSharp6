using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooksODataService.Models
{
    public class BooksContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Chapter> Chapters { get; set; }

        public BooksContext(DbContextOptions<BooksContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            EntityTypeBuilder<Book> bookBuilder = modelBuilder.Entity<Book>();
            bookBuilder.HasMany(b => b.Chapters).WithOne(c => c.Book).HasForeignKey(c => c.BookId);
            bookBuilder.Property<string>(b => b.Title).HasMaxLength(120).IsRequired();
            bookBuilder.Property<string>(b => b.Isbn).HasMaxLength(20).IsRequired(false);

            EntityTypeBuilder<Chapter> chapterBuilder = modelBuilder.Entity<Chapter>();
            chapterBuilder.Property<string>(c => c.Title).HasMaxLength(120);
        }
    }
}
