namespace BookingHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoomId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Purchases", "RoomId", c => c.Int(nullable: false));
            CreateIndex("dbo.Purchases", "RoomId");
            AddForeignKey("dbo.Purchases", "RoomId", "dbo.Rooms", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Purchases", "RoomId", "dbo.Rooms");
            DropIndex("dbo.Purchases", new[] { "RoomId" });
            DropColumn("dbo.Purchases", "RoomId");
        }
    }
}
