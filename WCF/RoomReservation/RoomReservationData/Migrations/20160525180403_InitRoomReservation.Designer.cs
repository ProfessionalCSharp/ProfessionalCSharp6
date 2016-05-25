using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Wrox.ProCSharp.WCF.Data;

namespace RoomReservationData.Migrations
{
    [DbContext(typeof(RoomReservationContext))]
    [Migration("20160525180403_InitRoomReservation")]
    partial class InitRoomReservation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Wrox.ProCSharp.WCF.Contracts.RoomReservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Contact")
                        .HasAnnotation("MaxLength", 30);

                    b.Property<DateTime>("EndTime");

                    b.Property<string>("RoomName")
                        .HasAnnotation("MaxLength", 30);

                    b.Property<DateTime>("StartTime");

                    b.Property<string>("Text")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.ToTable("RoomReservations");
                });
        }
    }
}
