namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Paid = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        PrivateCode = c.Guid(nullable: false),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmountPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Deleted = c.Boolean(nullable: false),
                        DeletedDate = c.DateTime(),
                        StoreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.StoreId);
            
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InvoiceDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(nullable: false),
                        Detail = c.String(),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Paid = c.Boolean(nullable: false),
                        ProductId = c.Int(),
                        InvoiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Invoices", t => t.InvoiceId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.InvoiceId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StoreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.StoreId);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(nullable: false),
                        Username = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Reference = c.String(),
                        PaymentTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PaymentTypes", t => t.PaymentTypeId, cascadeDelete: true)
                .Index(t => t.PaymentTypeId);
            
            CreateTable(
                "dbo.PaymentDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(),
                        InvoiceDetailId = c.Int(),
                        PaymentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Invoices", t => t.InvoiceId)
                .ForeignKey("dbo.InvoiceDetails", t => t.InvoiceDetailId)
                .ForeignKey("dbo.Payments", t => t.PaymentId, cascadeDelete: true)
                .Index(t => t.InvoiceId)
                .Index(t => t.InvoiceDetailId)
                .Index(t => t.PaymentId);
            
            CreateTable(
                "dbo.PaymentTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SystemName = c.String(),
                        PublicName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Registers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OpeningUsername = c.String(),
                        ClosingUsername = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        OpeningTime = c.DateTime(nullable: false),
                        ClosingTime = c.DateTime(),
                        HundredBillsOpening = c.Int(nullable: false),
                        FiftyBillsOpening = c.Int(nullable: false),
                        TwentyBillsOpening = c.Int(nullable: false),
                        TenBillsOpening = c.Int(nullable: false),
                        FiveBillsOpening = c.Int(nullable: false),
                        OneBillsOpening = c.Int(nullable: false),
                        OneCoinsOpening = c.Int(nullable: false),
                        FiftyCentsCoinsOpening = c.Int(nullable: false),
                        QuarterCoinsOpening = c.Int(nullable: false),
                        TenCentsCoinsOpening = c.Int(nullable: false),
                        FiveCentsCoinsOpening = c.Int(nullable: false),
                        OneCentCoinsOpening = c.Int(nullable: false),
                        HundredBillsClosing = c.Int(nullable: false),
                        FiftyBillsClosing = c.Int(nullable: false),
                        TwentyBillsClosing = c.Int(nullable: false),
                        TenBillsClosing = c.Int(nullable: false),
                        FiveBillsClosing = c.Int(nullable: false),
                        OneBillsClosing = c.Int(nullable: false),
                        OneCoinsClosing = c.Int(nullable: false),
                        FiftyCentsCoinsClosing = c.Int(nullable: false),
                        QuarterCoinsClosing = c.Int(nullable: false),
                        TenCentsCoinsClosing = c.Int(nullable: false),
                        FiveCentsCoinsClosing = c.Int(nullable: false),
                        OneCentCoinsClosing = c.Int(nullable: false),
                        CashTransactionCount = c.Int(nullable: false),
                        CashTransactionAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DebitTransactionCount = c.Int(nullable: false),
                        DebitTransactionAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreditTransactionCount = c.Int(nullable: false),
                        CreditTransactionAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CheckTransactionCount = c.Int(nullable: false),
                        CheckTransactionAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TransferTransactionCount = c.Int(nullable: false),
                        TransferTransactionAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StoreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.StoreId);
            
            CreateTable(
                "dbo.RegisterCashExits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        Username = c.String(),
                        CashEntering = c.Boolean(nullable: false),
                        RegisterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Registers", t => t.RegisterId, cascadeDelete: true)
                .Index(t => t.RegisterId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Registers", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.RegisterCashExits", "RegisterId", "dbo.Registers");
            DropForeignKey("dbo.Payments", "PaymentTypeId", "dbo.PaymentTypes");
            DropForeignKey("dbo.PaymentDetails", "PaymentId", "dbo.Payments");
            DropForeignKey("dbo.PaymentDetails", "InvoiceDetailId", "dbo.InvoiceDetails");
            DropForeignKey("dbo.PaymentDetails", "InvoiceId", "dbo.Invoices");
            DropForeignKey("dbo.InvoiceDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.InvoiceDetails", "InvoiceId", "dbo.Invoices");
            DropForeignKey("dbo.Invoices", "StoreId", "dbo.Stores");
            DropIndex("dbo.RegisterCashExits", new[] { "RegisterId" });
            DropIndex("dbo.Registers", new[] { "StoreId" });
            DropIndex("dbo.PaymentDetails", new[] { "PaymentId" });
            DropIndex("dbo.PaymentDetails", new[] { "InvoiceDetailId" });
            DropIndex("dbo.PaymentDetails", new[] { "InvoiceId" });
            DropIndex("dbo.Payments", new[] { "PaymentTypeId" });
            DropIndex("dbo.Products", new[] { "StoreId" });
            DropIndex("dbo.InvoiceDetails", new[] { "InvoiceId" });
            DropIndex("dbo.InvoiceDetails", new[] { "ProductId" });
            DropIndex("dbo.Invoices", new[] { "StoreId" });
            DropTable("dbo.RegisterCashExits");
            DropTable("dbo.Registers");
            DropTable("dbo.PaymentTypes");
            DropTable("dbo.PaymentDetails");
            DropTable("dbo.Payments");
            DropTable("dbo.Products");
            DropTable("dbo.InvoiceDetails");
            DropTable("dbo.Stores");
            DropTable("dbo.Invoices");
        }
    }
}
