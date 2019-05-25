namespace BookingHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hotelPriceandCountry : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Hotels", "Price");
            DropColumn("dbo.Hotels", "Country");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Hotels", "Country", c => c.String());
            AddColumn("dbo.Hotels", "Price", c => c.Int(nullable: false));
        }
    }
}
