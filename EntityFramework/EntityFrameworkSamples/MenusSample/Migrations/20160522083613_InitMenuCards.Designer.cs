using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MenusSample;

namespace MenusSample.Migrations
{
    [DbContext(typeof(MenusContext))]
    [Migration("20160522083613_InitMenuCards")]
    partial class InitMenuCards
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasDefaultSchema("mc")
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MenusSample.Menu", b =>
                {
                    b.Property<int>("MenuId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MenuCardId");

                    b.Property<decimal>("Price")
                        .HasColumnType("Money");

                    b.Property<string>("Text")
                        .HasAnnotation("MaxLength", 120);

                    b.HasKey("MenuId");

                    b.HasIndex("MenuCardId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("MenusSample.MenuCard", b =>
                {
                    b.Property<int>("MenuCardId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("MenuCardId");

                    b.ToTable("MenuCards");
                });

            modelBuilder.Entity("MenusSample.Menu", b =>
                {
                    b.HasOne("MenusSample.MenuCard")
                        .WithMany()
                        .HasForeignKey("MenuCardId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
