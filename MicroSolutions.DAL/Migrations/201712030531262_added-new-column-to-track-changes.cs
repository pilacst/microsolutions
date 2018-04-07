namespace MicroSolutions.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addednewcolumntotrackchanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customer", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.EmailSettings", "UserName", c => c.String());
            AddColumn("dbo.EmailSettings", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ExpirationPeriod", "UserName", c => c.String());
            AddColumn("dbo.ExpirationPeriod", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ItemType", "UserName", c => c.String());
            AddColumn("dbo.ItemType", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Vendors", "ModifiedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vendors", "ModifiedDate");
            DropColumn("dbo.ItemType", "ModifiedDate");
            DropColumn("dbo.ItemType", "UserName");
            DropColumn("dbo.ExpirationPeriod", "ModifiedDate");
            DropColumn("dbo.ExpirationPeriod", "UserName");
            DropColumn("dbo.EmailSettings", "ModifiedDate");
            DropColumn("dbo.EmailSettings", "UserName");
            DropColumn("dbo.Customer", "ModifiedDate");
        }
    }
}
