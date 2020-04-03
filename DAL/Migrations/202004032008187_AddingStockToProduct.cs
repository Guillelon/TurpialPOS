namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingStockToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Stock", c => c.Int(nullable: false, defaultValue: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Stock");
        }
    }
}
