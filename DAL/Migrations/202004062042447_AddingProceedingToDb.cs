namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingProceedingToDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Proceedings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderNumber = c.String(nullable: false, maxLength: 50),
                        InstitutionName = c.String(nullable: false, maxLength: 50),
                        Paid = c.Boolean(nullable: false),
                        Notes = c.String(nullable: false, maxLength: 50),
                        ProductId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        DeactivationDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Proceedings", "ProductId", "dbo.Products");
            DropIndex("dbo.Proceedings", new[] { "ProductId" });
            DropTable("dbo.Proceedings");
        }
    }
}
