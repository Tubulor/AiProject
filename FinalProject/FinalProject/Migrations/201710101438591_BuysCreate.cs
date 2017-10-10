namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BuysCreate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Buys", "MembersID", "dbo.AspNetUsers");
            DropIndex("dbo.Buys", new[] { "MembersID" });
            AddColumn("dbo.Buys", "ApplicationUsers_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Buys", "MembersID", c => c.String());
            CreateIndex("dbo.Buys", "ApplicationUsers_Id");
            AddForeignKey("dbo.Buys", "ApplicationUsers_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Buys", "ApplicationUsers_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Buys", new[] { "ApplicationUsers_Id" });
            AlterColumn("dbo.Buys", "MembersID", c => c.String(maxLength: 128));
            DropColumn("dbo.Buys", "ApplicationUsers_Id");
            CreateIndex("dbo.Buys", "MembersID");
            AddForeignKey("dbo.Buys", "MembersID", "dbo.AspNetUsers", "Id");
        }
    }
}
