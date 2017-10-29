namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllModelsValidation3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Branches", "HouseNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Branches", "HouseNumber", c => c.String(nullable: false));
        }
    }
}
