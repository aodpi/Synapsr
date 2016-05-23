namespace Synapsr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.elevations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ElevationName = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false, maxLength: 200),
                        ElevationId = c.Int(nullable: false),
                        avatar_uri = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.elevations", t => t.ElevationId, cascadeDelete: true)
                .Index(t => t.ElevationId);
            
            CreateTable(
                "dbo.notification_channels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NotificationType = c.String(),
                        NotificationChannelUri = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.notification_channels", "UserId", "dbo.users");
            DropForeignKey("dbo.users", "ElevationId", "dbo.elevations");
            DropIndex("dbo.notification_channels", new[] { "UserId" });
            DropIndex("dbo.users", new[] { "ElevationId" });
            DropTable("dbo.notification_channels");
            DropTable("dbo.users");
            DropTable("dbo.elevations");
        }
    }
}
