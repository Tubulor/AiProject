namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class idan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Branches", "Saturday", c => c.Boolean(nullable: false));
            AddColumn("dbo.Branches", "BranchNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Branches", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Branches", "Image");
            DropColumn("dbo.Branches", "BranchNumber");
            DropColumn("dbo.Branches", "Saturday");
        }
    }
}
