namespace LifeLink.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeclientinfomodel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ClientInfoes", "DateOfBirth");
            DropColumn("dbo.ClientInfoes", "Sex");
            DropColumn("dbo.ClientInfoes", "height");
            DropColumn("dbo.ClientInfoes", "weight");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClientInfoes", "weight", c => c.Int(nullable: false));
            AddColumn("dbo.ClientInfoes", "height", c => c.Int(nullable: false));
            AddColumn("dbo.ClientInfoes", "Sex", c => c.String());
            AddColumn("dbo.ClientInfoes", "DateOfBirth", c => c.DateTime(nullable: false));
        }
    }
}
