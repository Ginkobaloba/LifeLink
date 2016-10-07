namespace LifeLink.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedaddresstoholdnearestlocation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "LocationId", c => c.Int(nullable: false));
            CreateIndex("dbo.Addresses", "LocationId");
            AddForeignKey("dbo.Addresses", "LocationId", "dbo.Locations", "LocationId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "LocationId", "dbo.Locations");
            DropIndex("dbo.Addresses", new[] { "LocationId" });
            DropColumn("dbo.Addresses", "LocationId");
        }
    }
}
