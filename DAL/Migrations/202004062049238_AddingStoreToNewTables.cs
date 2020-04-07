namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingStoreToNewTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Proceedings", "ProductId", "dbo.Products");
            DropIndex("dbo.Proceedings", new[] { "ProductId" });
            AddColumn("dbo.Collaborators", "StoreId", c => c.Int(nullable: false));
            AddColumn("dbo.Proceedings", "StoreId", c => c.Int(nullable: false));
            AddColumn("dbo.Providers", "StoreId", c => c.Int(nullable: false));
            CreateIndex("dbo.Collaborators", "StoreId");
            CreateIndex("dbo.Proceedings", "StoreId");
            CreateIndex("dbo.Providers", "StoreId");
            AddForeignKey("dbo.Collaborators", "StoreId", "dbo.Stores", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Proceedings", "StoreId", "dbo.Stores", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Providers", "StoreId", "dbo.Stores", "Id", cascadeDelete: true);
            DropColumn("dbo.Proceedings", "ProductId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Proceedings", "ProductId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Providers", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.Proceedings", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.Collaborators", "StoreId", "dbo.Stores");
            DropIndex("dbo.Providers", new[] { "StoreId" });
            DropIndex("dbo.Proceedings", new[] { "StoreId" });
            DropIndex("dbo.Collaborators", new[] { "StoreId" });
            DropColumn("dbo.Providers", "StoreId");
            DropColumn("dbo.Proceedings", "StoreId");
            DropColumn("dbo.Collaborators", "StoreId");
            CreateIndex("dbo.Proceedings", "ProductId");
            AddForeignKey("dbo.Proceedings", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
    }
}
