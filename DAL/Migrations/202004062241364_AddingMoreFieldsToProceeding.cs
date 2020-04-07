namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingMoreFieldsToProceeding : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Proceedings", "ProductDescription", c => c.String(nullable: false, maxLength: 250));
            AddColumn("dbo.Proceedings", "ProductSanitaryCode", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Proceedings", "Notes", c => c.String(nullable: false, maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Proceedings", "Notes", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Proceedings", "ProductSanitaryCode");
            DropColumn("dbo.Proceedings", "ProductDescription");
        }
    }
}
