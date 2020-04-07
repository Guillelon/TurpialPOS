namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingAssetsToDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 250),
                        Area = c.String(nullable: false, maxLength: 50),
                        Office = c.String(nullable: false, maxLength: 50),
                        CreatedDate = c.DateTime(nullable: false),
                        DeactivationDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        StoreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.StoreId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assets", "StoreId", "dbo.Stores");
            DropIndex("dbo.Assets", new[] { "StoreId" });
            DropTable("dbo.Assets");
        }
    }
}
