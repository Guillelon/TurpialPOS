namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingCollaboratorToTheDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Collaborators",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        LegalId = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 75),
                        Phone = c.String(nullable: false, maxLength: 50),
                        WorkArea = c.String(nullable: false, maxLength: 50),
                        CreatedDate = c.DateTime(nullable: false),
                        DeactivationDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Providers", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Providers", "IsActive");
            DropTable("dbo.Collaborators");
        }
    }
}
