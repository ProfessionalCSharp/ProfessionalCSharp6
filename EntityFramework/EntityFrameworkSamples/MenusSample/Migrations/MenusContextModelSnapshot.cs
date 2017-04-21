using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MenusSample;

namespace MenusSample.Migrations
{
    [DbContext(typeof(MenusContext))]
    partial class MenusContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasDefaultSchema("mc")
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MenusSample.Menu", b =>
                {
                    b.Property<int>("MenuId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MenuCardId");

                    b.Property<decimal>("Price")
                        .HasColumnType("Money");

                    b.Property<string>("Text")
                        .HasMaxLength(120);

                    b.HasKey("MenuId");

                    b.HasIndex("MenuCardId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("MenusSample.MenuCard", b =>
                {
                    b.Property<int>("MenuCardId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title")
                        .HasMaxLength(50);

                    b.HasKey("MenuCardId");

                    b.ToTable("MenuCards");
                });

            modelBuilder.Entity("MenusSample.Menu", b =>
                {
                    b.HasOne("MenusSample.MenuCard", "MenuCard")
                        .WithMany("Menus")
                        .HasForeignKey("MenuCardId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
