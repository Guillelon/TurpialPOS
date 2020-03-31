namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingLegalIdToClient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "LegalId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "LegalId");
        }
    }
}
