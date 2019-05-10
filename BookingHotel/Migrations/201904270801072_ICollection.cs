namespace BookingHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ICollection : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoomHotels",
                c => new
                    {
                        Room_Id = c.Int(nullable: false),
                        Hotel_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Room_Id, t.Hotel_ID })
                .ForeignKey("dbo.Rooms", t => t.Room_Id, cascadeDelete: true)
                .ForeignKey("dbo.Hotels", t => t.Hotel_ID, cascadeDelete: true)
                .Index(t => t.Room_Id)
                .Index(t => t.Hotel_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoomHotels", "Hotel_ID", "dbo.Hotels");
            DropForeignKey("dbo.RoomHotels", "Room_Id", "dbo.Rooms");
            DropIndex("dbo.RoomHotels", new[] { "Hotel_ID" });
            DropIndex("dbo.RoomHotels", new[] { "Room_Id" });
            DropTable("dbo.RoomHotels");
        }
    }
}
