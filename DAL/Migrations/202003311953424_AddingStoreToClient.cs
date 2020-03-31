namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingStoreToClient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "StoreId", c => c.Int(nullable: false));
            CreateIndex("dbo.Clients", "StoreId");
            AddForeignKey("dbo.Clients", "StoreId", "dbo.Stores", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clients", "StoreId", "dbo.Stores");
            DropIndex("dbo.Clients", new[] { "StoreId" });
            DropColumn("dbo.Clients", "StoreId");
        }
    }
}
