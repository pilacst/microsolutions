namespace MicroSolutions.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customerfieldslengthsadded : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customer", "CustomerName", c => c.String(maxLength: 150));
            AlterColumn("dbo.Customer", "ContactPersonName", c => c.String(maxLength: 150));
            AlterColumn("dbo.Customer", "ContactNumber", c => c.String(maxLength: 15));
            AlterColumn("dbo.Customer", "Address", c => c.String(maxLength: 350));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customer", "Address", c => c.String());
            AlterColumn("dbo.Customer", "ContactNumber", c => c.String());
            AlterColumn("dbo.Customer", "ContactPersonName", c => c.String());
            AlterColumn("dbo.Customer", "CustomerName", c => c.String());
        }
    }
}
