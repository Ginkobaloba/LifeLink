namespace LifeLink.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reset : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Appointments", "allDay");
        }

        public override void Down()
        {
            AddColumn("dbo.Appointments", "allDay", c => c.String());
        }
    }
}
