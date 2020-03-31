namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingDeactivateDateToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "DeactivationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "DeactivationDate");
        }
    }
}
