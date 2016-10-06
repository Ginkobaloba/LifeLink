namespace LifeLink.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SettingUplocations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Locations", "Name", c => c.String());
            AddColumn("dbo.Locations", "StreetAddress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Locations", "StreetAddress");
            DropColumn("dbo.Locations", "Name");
        }
    }
}
