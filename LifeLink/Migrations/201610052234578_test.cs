namespace LifeLink.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Events", newName: "Appointments");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Appointments", newName: "Events");
        }
    }
}
