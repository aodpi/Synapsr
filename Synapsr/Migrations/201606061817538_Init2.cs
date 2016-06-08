namespace Synapsr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.users", "GroupId", c => c.Int(nullable: false));
            CreateIndex("dbo.users", "GroupId");
            AddForeignKey("dbo.users", "GroupId", "dbo.grupe", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.users", "GroupId", "dbo.grupe");
            DropIndex("dbo.users", new[] { "GroupId" });
            DropColumn("dbo.users", "GroupId");
        }
    }
}
