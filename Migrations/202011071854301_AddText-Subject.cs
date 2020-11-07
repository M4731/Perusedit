namespace Perusedit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTextSubject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subjects", "Text", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subjects", "Text");
        }
    }
}
