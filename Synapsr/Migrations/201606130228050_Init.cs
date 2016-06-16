namespace Synapsr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
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
                        Sex = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        UserName = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false, maxLength: 200),
                        IdSpecialitate = c.Int(nullable: false),
                        ElevationId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                        avatar_uri = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.elevations", t => t.ElevationId, cascadeDelete: true)
                .ForeignKey("dbo.grupe", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.specialitati", t => t.IdSpecialitate, cascadeDelete: true)
                .Index(t => t.IdSpecialitate)
                .Index(t => t.ElevationId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.grupe",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.reg_codes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        code = c.String(),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.grupe", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.specialitati",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.news",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        pic_url = c.String(),
                        title = c.String(nullable: false),
                        body = c.String(nullable: false),
                        date_published = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.users", "IdSpecialitate", "dbo.specialitati");
            DropForeignKey("dbo.users", "GroupId", "dbo.grupe");
            DropForeignKey("dbo.reg_codes", "GroupId", "dbo.grupe");
            DropForeignKey("dbo.users", "ElevationId", "dbo.elevations");
            DropIndex("dbo.reg_codes", new[] { "GroupId" });
            DropIndex("dbo.users", new[] { "GroupId" });
            DropIndex("dbo.users", new[] { "ElevationId" });
            DropIndex("dbo.users", new[] { "IdSpecialitate" });
            DropTable("dbo.news");
            DropTable("dbo.specialitati");
            DropTable("dbo.reg_codes");
            DropTable("dbo.grupe");
            DropTable("dbo.users");
            DropTable("dbo.elevations");
        }
    }
}
