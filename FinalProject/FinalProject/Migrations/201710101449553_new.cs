namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Buys", "Discount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Buys", "Discount", c => c.Double(nullable: false));
        }
    }
}
