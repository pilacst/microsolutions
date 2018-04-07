namespace MicroSolutions.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmodifieddateforsuppliermodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Supplier", "ModifiedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Supplier", "ModifiedDate");
        }
    }
}
