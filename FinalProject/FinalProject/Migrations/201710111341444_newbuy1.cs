namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newbuy1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Buys", new[] { "ApplicationUsers_Id" });
            DropColumn("dbo.Buys", "MembersID");
            RenameColumn(table: "dbo.Buys", name: "ApplicationUsers_Id", newName: "MembersID");
            AlterColumn("dbo.Buys", "MembersID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Buys", "MembersID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Buys", new[] { "MembersID" });
            AlterColumn("dbo.Buys", "MembersID", c => c.String());
            RenameColumn(table: "dbo.Buys", name: "MembersID", newName: "ApplicationUsers_Id");
            AddColumn("dbo.Buys", "MembersID", c => c.String());
            CreateIndex("dbo.Buys", "ApplicationUsers_Id");
        }
    }
}
