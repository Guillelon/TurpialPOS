namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovingFactoryAndCountryFromProduct : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "Factory");
            DropColumn("dbo.Products", "Country");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Country", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Products", "Factory", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
