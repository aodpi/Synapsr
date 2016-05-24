namespace Synapsr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
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
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        UserName = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false, maxLength: 200),
                        IdSpecialitate = c.Int(nullable: false),
                        ElevationId = c.Int(nullable: false),
                        avatar_uri = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.elevations", t => t.ElevationId, cascadeDelete: true)
                .ForeignKey("dbo.specialitati", t => t.IdSpecialitate, cascadeDelete: true)
                .Index(t => t.IdSpecialitate)
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
            
            CreateTable(
                "dbo.specialitati",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.users", "IdSpecialitate", "dbo.specialitati");
            DropForeignKey("dbo.notification_channels", "UserId", "dbo.users");
            DropForeignKey("dbo.users", "ElevationId", "dbo.elevations");
            DropIndex("dbo.notification_channels", new[] { "UserId" });
            DropIndex("dbo.users", new[] { "ElevationId" });
            DropIndex("dbo.users", new[] { "IdSpecialitate" });
            DropTable("dbo.specialitati");
            DropTable("dbo.notification_channels");
            DropTable("dbo.users");
            DropTable("dbo.elevations");
        }
    }
}
