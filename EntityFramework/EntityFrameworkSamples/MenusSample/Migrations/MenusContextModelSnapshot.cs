using System;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using Microsoft.EntityFrameworkCore.Metadata;
//using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using MenusSample;

namespace MenusSample.Migrations
{
    [DbContext(typeof(MenusContext))]
    partial class MenusContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("Relational:DefaultSchema", "mc")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MenusSample.Menu", b =>
                {
                    b.Property<int>("MenuId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MenuCardId");

                    b.Property<decimal>("Price")
                        .HasAnnotation("Relational:ColumnType", "Money");

                    b.Property<string>("Text")
                        .HasAnnotation("MaxLength", 120);

                    b.HasKey("MenuId");

                    b.HasAnnotation("Relational:TableName", "Menus");
                });

            modelBuilder.Entity("MenusSample.MenuCard", b =>
                {
                    b.Property<int>("MenuCardId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("MenuCardId");

                    b.HasAnnotation("Relational:TableName", "MenuCards");
                });

            modelBuilder.Entity("MenusSample.Menu", b =>
                {
                    b.HasOne("MenusSample.MenuCard")
                        .WithMany()
                        .HasForeignKey("MenuCardId");
                });
        }
    }
}
