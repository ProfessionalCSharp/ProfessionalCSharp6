namespace Wrox.ProCSharp.WCF.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitRoomReservation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoomReservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoomName = c.String(maxLength: 30),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Contact = c.String(maxLength: 30),
                        Text = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RoomReservations");
        }
    }
}
