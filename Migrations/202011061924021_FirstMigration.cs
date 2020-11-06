namespace Perusedit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Responses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        SubjectId = c.Int(nullable: false),
                        FatherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Responses", t => t.FatherId)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.SubjectId)
                .Index(t => t.FatherId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Responses", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Responses", "FatherId", "dbo.Responses");
            DropForeignKey("dbo.Subjects", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Responses", new[] { "FatherId" });
            DropIndex("dbo.Responses", new[] { "SubjectId" });
            DropIndex("dbo.Subjects", new[] { "CategoryId" });
            DropTable("dbo.Responses");
            DropTable("dbo.Subjects");
            DropTable("dbo.Categories");
        }
    }
}
