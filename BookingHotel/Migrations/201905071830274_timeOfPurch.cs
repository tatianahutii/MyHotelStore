namespace BookingHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class timeOfPurch : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Purchases", "TimeOfPurch", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Purchases", "TimeOfPurch");
        }
    }
}
