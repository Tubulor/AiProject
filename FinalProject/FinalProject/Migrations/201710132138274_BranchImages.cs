namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BranchImages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Branches", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Branches", "Image");
        }
    }
}
