namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingProviderToTheDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Providers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LegalId = c.String(nullable: false, maxLength: 50),
                        CreatedDate = c.DateTime(nullable: false),
                        DeactivationDate = c.DateTime(),
                        Name = c.String(nullable: false, maxLength: 50),
                        Country = c.String(nullable: false, maxLength: 75),
                        ContactName = c.String(nullable: false, maxLength: 50),
                        ContactPhone = c.String(nullable: false, maxLength: 50),
                        ContactEmail = c.String(nullable: false, maxLength: 50),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Providers");
        }
    }
}
