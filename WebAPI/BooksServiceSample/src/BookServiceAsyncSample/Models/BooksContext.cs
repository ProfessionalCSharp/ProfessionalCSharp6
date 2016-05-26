using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BooksServiceSample.Models
{
    public class BooksContext : DbContext
    {
        public BooksContext(DbContextOptions<BooksContext> options)
            : base(options)
        {
        }

        public DbSet<BookChapter> Chapters { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            EntityTypeBuilder<BookChapter> chapter = modelBuilder.Entity<BookChapter>();
            chapter.ToTable("Chapters").HasKey(p => p.Id);
            chapter.Property<Guid>(p => p.Id).HasColumnType("UniqueIdentifier").HasDefaultValueSql("newid()");
            chapter.Property<string>(p => p.Title).HasMaxLength(120);

        }
    }
}
