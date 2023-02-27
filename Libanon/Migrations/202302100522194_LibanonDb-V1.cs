namespace Libanon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LibanonDbV1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Books", "Image");
            AddColumn("dbo.Books", "ImageName", c => c.String());
            AddColumn("dbo.Books", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Image", c => c.String());
            DropColumn("dbo.Books", "ImageName");
        }
    }
}
