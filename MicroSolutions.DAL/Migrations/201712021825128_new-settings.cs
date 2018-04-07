namespace MicroSolutions.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newsettings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FromEmail = c.String(),
                        FromEmailPassword = c.String(),
                        ToEmail = c.String(),
                        WarningPeriod = c.Int(nullable: false),
                        SmtpAddress = c.String(),
                        PortNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExpirationPeriod",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExpirationPeriodName = c.String(),
                        ExpirationPeriodValue = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemTypeName = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Vendors", "UserName", c => c.String());
            AlterColumn("dbo.Vendors", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vendors", "Status", c => c.Short(nullable: false));
            DropColumn("dbo.Vendors", "UserName");
            DropTable("dbo.ItemType");
            DropTable("dbo.ExpirationPeriod");
            DropTable("dbo.EmailSettings");
        }
    }
}
