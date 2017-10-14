namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BranchUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Branches", "Saturday", c => c.Boolean(nullable: false));
            AddColumn("dbo.Branches", "BranchNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Branches", "BranchNumber");
            DropColumn("dbo.Branches", "Saturday");
        }
    }
}
