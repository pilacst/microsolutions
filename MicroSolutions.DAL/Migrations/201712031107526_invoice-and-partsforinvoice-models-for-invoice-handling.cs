namespace MicroSolutions.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoiceandpartsforinvoicemodelsforinvoicehandling : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Invoice",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceNumber = c.String(maxLength: 50),
                        InvoiceDate = c.DateTime(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.PartsForInvoice",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ItemTypeId = c.Int(nullable: false),
                        PartNumber = c.String(),
                        VendorId = c.Int(nullable: false),
                        SerialNumber = c.String(),
                        ExpirationPeriodId = c.Int(nullable: false),
                        StartingDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        UserName = c.String(),
                        Invoice_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExpirationPeriod", t => t.ExpirationPeriodId, cascadeDelete: true)
                .ForeignKey("dbo.Invoice", t => t.Invoice_Id)
                .ForeignKey("dbo.ItemType", t => t.ItemTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Vendors", t => t.VendorId, cascadeDelete: true)
                .Index(t => t.ItemTypeId)
                .Index(t => t.VendorId)
                .Index(t => t.ExpirationPeriodId)
                .Index(t => t.Invoice_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PartsForInvoice", "VendorId", "dbo.Vendors");
            DropForeignKey("dbo.PartsForInvoice", "ItemTypeId", "dbo.ItemType");
            DropForeignKey("dbo.PartsForInvoice", "Invoice_Id", "dbo.Invoice");
            DropForeignKey("dbo.PartsForInvoice", "ExpirationPeriodId", "dbo.ExpirationPeriod");
            DropForeignKey("dbo.Invoice", "CustomerId", "dbo.Customer");
            DropIndex("dbo.PartsForInvoice", new[] { "Invoice_Id" });
            DropIndex("dbo.PartsForInvoice", new[] { "ExpirationPeriodId" });
            DropIndex("dbo.PartsForInvoice", new[] { "VendorId" });
            DropIndex("dbo.PartsForInvoice", new[] { "ItemTypeId" });
            DropIndex("dbo.Invoice", new[] { "CustomerId" });
            DropTable("dbo.PartsForInvoice");
            DropTable("dbo.Invoice");
        }
    }
}
