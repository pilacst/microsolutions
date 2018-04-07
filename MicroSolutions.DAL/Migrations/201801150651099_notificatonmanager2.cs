namespace MicroSolutions.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notificatonmanager2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notification",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        InvoiceId = c.Int(nullable: false),
                        PartNumberId = c.String(maxLength: 128),
                        IsNotified = c.Boolean(nullable: false),
                        NextNotificationDate = c.DateTime(nullable: false),
                        UserName = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Invoice", t => t.InvoiceId, cascadeDelete: true)
                .ForeignKey("dbo.PartsForInvoice", t => t.PartNumberId)
                .Index(t => t.InvoiceId)
                .Index(t => t.PartNumberId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notification", "PartNumberId", "dbo.PartsForInvoice");
            DropForeignKey("dbo.Notification", "InvoiceId", "dbo.Invoice");
            DropIndex("dbo.Notification", new[] { "PartNumberId" });
            DropIndex("dbo.Notification", new[] { "InvoiceId" });
            DropTable("dbo.Notification");
        }
    }
}
