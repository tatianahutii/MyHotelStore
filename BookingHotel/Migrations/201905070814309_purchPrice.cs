namespace BookingHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class purchPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Purchases", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Purchases", "Price");
        }
    }
}
