namespace Libanon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LibanonDbV2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageID = c.Int(nullable: false, identity: true),
                        ImageName = c.String(),
                        ImageBinary = c.Binary(),
                        ImageWidth = c.Int(),
                        ImageHeight = c.Int(),
                        Type = c.String(),
                        Book_ISBN = c.Int(),
                    })
                .PrimaryKey(t => t.ImageID)
                .ForeignKey("dbo.Books", t => t.Book_ISBN, cascadeDelete: true)
                .Index(t => t.Book_ISBN);
            
            DropColumn("dbo.Books", "ImageName");
            DropColumn("dbo.Books", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "Image", c => c.Binary());
            AddColumn("dbo.Books", "ImageName", c => c.String());
            DropForeignKey("dbo.Images", "Book_ISBN", "dbo.Books");
            DropIndex("dbo.Images", new[] { "Book_ISBN" });
            DropTable("dbo.Images");
        }
    }
}
