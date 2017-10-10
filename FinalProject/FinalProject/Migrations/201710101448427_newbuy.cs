namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newbuy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Buys", "Discount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Buys", "Discount");
        }
    }
}
