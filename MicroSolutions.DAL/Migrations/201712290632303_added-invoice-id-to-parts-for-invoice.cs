namespace MicroSolutions.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedinvoiceidtopartsforinvoice : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PartsForInvoice", "Invoice_Id", "dbo.Invoice");
            DropIndex("dbo.PartsForInvoice", new[] { "Invoice_Id" });
            AlterColumn("dbo.Customer", "CustomerName", c => c.String(maxLength: 150));
            AlterColumn("dbo.Invoice", "InvoiceNumber", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.PartsForInvoice", "Invoice_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.PartsForInvoice", "Invoice_Id");
            AddForeignKey("dbo.PartsForInvoice", "Invoice_Id", "dbo.Invoice", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PartsForInvoice", "Invoice_Id", "dbo.Invoice");
            DropIndex("dbo.PartsForInvoice", new[] { "Invoice_Id" });
            AlterColumn("dbo.PartsForInvoice", "Invoice_Id", c => c.Int());
            AlterColumn("dbo.Invoice", "InvoiceNumber", c => c.String(maxLength: 50));
            AlterColumn("dbo.Customer", "CustomerName", c => c.String(nullable: false, maxLength: 150));
            CreateIndex("dbo.PartsForInvoice", "Invoice_Id");
            AddForeignKey("dbo.PartsForInvoice", "Invoice_Id", "dbo.Invoice", "Id");
        }
    }
}
