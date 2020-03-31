namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingStoreIdToPayments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "StoreId", c => c.Int(nullable: false));
            CreateIndex("dbo.Payments", "StoreId");
            AddForeignKey("dbo.Payments", "StoreId", "dbo.Stores", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "StoreId", "dbo.Stores");
            DropIndex("dbo.Payments", new[] { "StoreId" });
            DropColumn("dbo.Payments", "StoreId");
        }
    }
}
