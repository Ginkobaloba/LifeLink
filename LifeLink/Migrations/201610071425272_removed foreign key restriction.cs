namespace LifeLink.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedforeignkeyrestriction : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Addresses", "LocationId", "dbo.Locations");
            DropIndex("dbo.Addresses", new[] { "LocationId" });
            AddColumn("dbo.Addresses", "ClosestLocationId", c => c.Int(nullable: false));
            DropColumn("dbo.Addresses", "LocationId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Addresses", "LocationId", c => c.Int(nullable: false));
            DropColumn("dbo.Addresses", "ClosestLocationId");
            CreateIndex("dbo.Addresses", "LocationId");
            AddForeignKey("dbo.Addresses", "LocationId", "dbo.Locations", "LocationId", cascadeDelete: true);
        }
    }
}
