namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingValidationsToClient : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clients", "LegalId", c => c.String(nullable: false));
            AlterColumn("dbo.Clients", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Clients", "Address", c => c.String(nullable: false, maxLength: 75));
            AlterColumn("dbo.Clients", "ContactName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Clients", "ContactPhone", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Clients", "ContactEmail", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clients", "ContactEmail", c => c.String());
            AlterColumn("dbo.Clients", "ContactPhone", c => c.String());
            AlterColumn("dbo.Clients", "ContactName", c => c.String());
            AlterColumn("dbo.Clients", "Address", c => c.String());
            AlterColumn("dbo.Clients", "Name", c => c.String());
            AlterColumn("dbo.Clients", "LegalId", c => c.String());
        }
    }
}
