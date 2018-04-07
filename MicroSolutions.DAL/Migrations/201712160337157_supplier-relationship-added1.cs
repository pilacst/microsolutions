namespace MicroSolutions.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class supplierrelationshipadded1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PartsForInvoice", "SupplierId", c => c.Int(nullable: false));
            CreateIndex("dbo.PartsForInvoice", "SupplierId");
            AddForeignKey("dbo.PartsForInvoice", "SupplierId", "dbo.Supplier", "Id", cascadeDelete: true);
            DropColumn("dbo.PartsForInvoice", "Supplier");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PartsForInvoice", "Supplier", c => c.String());
            DropForeignKey("dbo.PartsForInvoice", "SupplierId", "dbo.Supplier");
            DropIndex("dbo.PartsForInvoice", new[] { "SupplierId" });
            DropColumn("dbo.PartsForInvoice", "SupplierId");
        }
    }
}
