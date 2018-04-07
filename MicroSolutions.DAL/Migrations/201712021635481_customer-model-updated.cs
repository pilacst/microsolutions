namespace MicroSolutions.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customermodelupdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customer", "UserName", c => c.String());
            AlterColumn("dbo.Customer", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customer", "Status", c => c.Short(nullable: false));
            DropColumn("dbo.Customer", "UserName");
        }
    }
}
