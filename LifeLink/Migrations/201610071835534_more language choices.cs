namespace LifeLink.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class morelanguagechoices : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "LanguageCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "LanguageCode");
        }
    }
}
