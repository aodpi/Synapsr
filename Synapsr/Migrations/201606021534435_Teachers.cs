namespace Synapsr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Teachers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        Grade = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.teachers");
        }
    }
}
