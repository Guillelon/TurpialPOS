namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingPaymentType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StoreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: false)
                .Index(t => t.StoreId);
            
            AddColumn("dbo.Products", "CodeBar", c => c.String());
            AddColumn("dbo.Products", "ProductTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "ProductTypeId");
            AddForeignKey("dbo.Products", "ProductTypeId", "dbo.ProductTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductTypes", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.Products", "ProductTypeId", "dbo.ProductTypes");
            DropIndex("dbo.ProductTypes", new[] { "StoreId" });
            DropIndex("dbo.Products", new[] { "ProductTypeId" });
            DropColumn("dbo.Products", "ProductTypeId");
            DropColumn("dbo.Products", "CodeBar");
            DropTable("dbo.ProductTypes");
        }
    }
}
