using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using MenuPlanner.Models;

namespace MenuPlanner.Migrations
{
    [DbContext(typeof(MenuCardsContext))]
    partial class MenuCardsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc2-16649")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MenuPlanner.Models.Menu", b =>
                {
                    b.ToTable("Menu");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<DateTime>("Day");

                    b.Property<int>("MenuCardId");

                    b.Property<int>("Order");

                    b.Property<decimal>("Price");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("MenuCardId");
                });

            modelBuilder.Entity("MenuPlanner.Models.MenuCard", b =>
                {
                    b.ToTable("MenuCard");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<int>("Order");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("MenuPlanner.Models.Menu", b =>
                {
                    b.HasOne("MenuPlanner.Models.MenuCard")
                        .WithMany()
                        .HasForeignKey("MenuCardId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
