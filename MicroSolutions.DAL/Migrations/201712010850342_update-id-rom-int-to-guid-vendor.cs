namespace MicroSolutions.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateidrominttoguidvendor : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Vendors");
            AlterColumn("dbo.Vendors", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Vendors", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Vendors");
            AlterColumn("dbo.Vendors", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Vendors", "Id");
        }
    }
}
