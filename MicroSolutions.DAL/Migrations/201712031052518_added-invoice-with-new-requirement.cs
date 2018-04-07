namespace MicroSolutions.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedinvoicewithnewrequirement : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customer", "CustomerName", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customer", "CustomerName", c => c.String(maxLength: 150));
        }
    }
}
