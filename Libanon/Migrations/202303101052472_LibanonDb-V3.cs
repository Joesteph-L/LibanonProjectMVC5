namespace Libanon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LibanonDbV3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "BookISBN", "dbo.Books");
            AddForeignKey("dbo.Images", "BookISBN", "dbo.Books", "ISBN");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "BookISBN", "dbo.Books");
            AddForeignKey("dbo.Images", "BookISBN", "dbo.Books", "ISBN");
        }
    }
}
