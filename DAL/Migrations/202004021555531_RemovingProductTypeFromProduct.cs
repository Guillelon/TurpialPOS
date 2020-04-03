namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovingProductTypeFromProduct : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "ProductTypeId", "dbo.ProductTypes");
            DropIndex("dbo.Products", new[] { "ProductTypeId" });
            AddColumn("dbo.Products", "StoreId", c => c.Int(nullable: false, defaultValue:1));
            CreateIndex("dbo.Products", "StoreId");
            AddForeignKey("dbo.Products", "StoreId", "dbo.Stores", "Id", cascadeDelete: true);
            DropColumn("dbo.Products", "ProductTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ProductTypeId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Products", "StoreId", "dbo.Stores");
            DropIndex("dbo.Products", new[] { "StoreId" });
            DropColumn("dbo.Products", "StoreId");
            CreateIndex("dbo.Products", "ProductTypeId");
            AddForeignKey("dbo.Products", "ProductTypeId", "dbo.ProductTypes", "Id", cascadeDelete: true);
        }
    }
}
