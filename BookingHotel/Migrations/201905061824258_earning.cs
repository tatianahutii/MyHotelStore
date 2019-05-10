namespace BookingHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class earning : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HotelsRooms", "Earning", c => c.Int(nullable: false));
            AddColumn("dbo.Purchases", "Earning", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Purchases", "Earning");
            DropColumn("dbo.HotelsRooms", "Earning");
        }
    }
}
