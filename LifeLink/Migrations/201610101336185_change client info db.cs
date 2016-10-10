namespace LifeLink.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeclientinfodb : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ClientInfoes", "height", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ClientInfoes", "height", c => c.Single(nullable: false));
        }
    }
}
