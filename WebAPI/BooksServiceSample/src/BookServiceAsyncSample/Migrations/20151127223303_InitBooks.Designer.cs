using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BooksServiceSample.Models;

namespace BookServiceAsyncSample.Migrations
{
    [DbContext(typeof(BooksContext))]
    [Migration("20151127223303_InitBooks")]
    partial class InitBooks
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BooksServiceSample.Models.BookChapter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Relational:ColumnType", "UniqueIdentifier")
                        .HasAnnotation("Relational:GeneratedValueSql", "newid()");

                    b.Property<int>("Number");

                    b.Property<int>("Pages");

                    b.Property<string>("Title")
                        .HasAnnotation("MaxLength", 120);

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "Chapters");
                });
        }
    }
}
