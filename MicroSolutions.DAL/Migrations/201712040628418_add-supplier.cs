namespace MicroSolutions.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsupplier : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Supplier",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SupplierName = c.String(),
                        Status = c.Boolean(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.PartsForInvoice", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.PartsForInvoice", "Supplier", c => c.String());
            AddColumn("dbo.PartsForInvoice", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PartsForInvoice", "Description");
            DropColumn("dbo.PartsForInvoice", "Supplier");
            DropColumn("dbo.PartsForInvoice", "Quantity");
            DropTable("dbo.Supplier");
        }
    }
}
