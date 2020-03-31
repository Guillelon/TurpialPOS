namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovingStoreIdFromProduct : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "StoreId", "dbo.Stores");
            DropIndex("dbo.Products", new[] { "StoreId" });
            DropColumn("dbo.Products", "StoreId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "StoreId", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "StoreId");
            AddForeignKey("dbo.Products", "StoreId", "dbo.Stores", "Id", cascadeDelete: true);
        }
    }
}
