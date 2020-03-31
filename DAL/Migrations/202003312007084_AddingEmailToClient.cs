namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingEmailToClient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "ContactEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "ContactEmail");
        }
    }
}
