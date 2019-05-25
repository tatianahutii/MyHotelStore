namespace BookingHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hotelDiscount : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Hotels", "Discount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Hotels", "Discount", c => c.Int(nullable: false));
        }
    }
}
