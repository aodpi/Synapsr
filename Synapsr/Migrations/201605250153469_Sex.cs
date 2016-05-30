namespace Synapsr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sex : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.users", "Sex", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.users", "Sex");
        }
    }
}
