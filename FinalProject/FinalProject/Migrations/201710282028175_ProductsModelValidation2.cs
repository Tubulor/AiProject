namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductsModelValidation2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Inches", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Resolution", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Image", c => c.String(nullable: false));
            DropColumn("dbo.Products", "Video");
            DropColumn("dbo.Products", "Discount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Discount", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "Video", c => c.String());
            AlterColumn("dbo.Products", "Image", c => c.String());
            AlterColumn("dbo.Products", "Resolution", c => c.String());
            AlterColumn("dbo.Products", "Inches", c => c.String());
        }
    }
}
