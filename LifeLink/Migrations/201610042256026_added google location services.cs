namespace LifeLink.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedgooglelocationservices : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "FirstName", c => c.String(nullable: false));
            AddColumn("dbo.Addresses", "LastName", c => c.String(nullable: false));
            AddColumn("dbo.Addresses", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Addresses", "PhoneNumber");
            DropColumn("dbo.Addresses", "LastName");
            DropColumn("dbo.Addresses", "FirstName");
        }
    }
}
