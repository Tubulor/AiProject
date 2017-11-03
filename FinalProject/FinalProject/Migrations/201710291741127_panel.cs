namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class panel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Panel", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Panel");
        }
    }
}
