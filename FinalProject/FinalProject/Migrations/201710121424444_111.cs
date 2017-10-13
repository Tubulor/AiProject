namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _111 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Branches");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BranchName = c.String(nullable: false),
                        Country = c.String(nullable: false),
                        City = c.String(nullable: false),
                        HouseNumber = c.String(nullable: false),
                        Street = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
    }
}
