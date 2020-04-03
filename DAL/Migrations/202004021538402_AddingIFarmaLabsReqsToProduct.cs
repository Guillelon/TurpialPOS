namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingIFarmaLabsReqsToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Code", c => c.String());
            AddColumn("dbo.Products", "OrderNumber", c => c.String());
            AddColumn("dbo.Products", "CatalogNumber", c => c.String());
            AddColumn("dbo.Products", "SanitaryCode", c => c.String());
            AddColumn("dbo.Products", "Brand", c => c.String());
            AddColumn("dbo.Products", "Factory", c => c.String());
            AddColumn("dbo.Products", "Country", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Country");
            DropColumn("dbo.Products", "Factory");
            DropColumn("dbo.Products", "Brand");
            DropColumn("dbo.Products", "SanitaryCode");
            DropColumn("dbo.Products", "CatalogNumber");
            DropColumn("dbo.Products", "OrderNumber");
            DropColumn("dbo.Products", "Code");
        }
    }
}
