namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class droplist3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Buys_ID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Buys_ID");
            AddForeignKey("dbo.AspNetUsers", "Buys_ID", "dbo.Buys", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Buys_ID", "dbo.Buys");
            DropIndex("dbo.AspNetUsers", new[] { "Buys_ID" });
            DropColumn("dbo.AspNetUsers", "Buys_ID");
        }
    }
}
