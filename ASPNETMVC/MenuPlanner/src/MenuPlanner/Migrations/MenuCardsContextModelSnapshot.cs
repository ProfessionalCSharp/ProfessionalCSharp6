using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MenuPlanner.Models;

namespace MenuPlanner.Migrations
{
    [DbContext(typeof(MenuCardsContext))]
    partial class MenuCardsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MenuPlanner.Models.Menu", b =>
                {
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

                    b.ToTable("Menus");

                    b.HasAnnotation("SqlServer:TableName", "Menus");
                });

            modelBuilder.Entity("MenuPlanner.Models.MenuCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<int>("Order");

                    b.HasKey("Id");

                    b.ToTable("MenuCards");

                    b.HasAnnotation("SqlServer:TableName", "MenuCards");
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
