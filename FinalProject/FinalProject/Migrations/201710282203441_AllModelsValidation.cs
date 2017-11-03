namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllModelsValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Branches", "Image", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Branches", "Image", c => c.String());
        }
    }
}
