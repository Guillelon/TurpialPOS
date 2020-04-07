namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingValidationsToProducts : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Code", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Products", "OrderNumber", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Products", "CatalogNumber", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Products", "SanitaryCode", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Products", "Brand", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Products", "Factory", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Products", "Country", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Country", c => c.String());
            AlterColumn("dbo.Products", "Factory", c => c.String());
            AlterColumn("dbo.Products", "Brand", c => c.String());
            AlterColumn("dbo.Products", "SanitaryCode", c => c.String());
            AlterColumn("dbo.Products", "CatalogNumber", c => c.String());
            AlterColumn("dbo.Products", "OrderNumber", c => c.String());
            AlterColumn("dbo.Products", "Code", c => c.String());
        }
    }
}
