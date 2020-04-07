namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovingMandatoryOnProceedingNotes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Proceedings", "Notes", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Proceedings", "Notes", c => c.String(nullable: false, maxLength: 250));
        }
    }
}
