namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingNotesToClient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "Notes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "Notes");
        }
    }
}
