namespace MicroSolutions.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newcustomer : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Vendors");
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(),
                        ContactPersonName = c.String(),
                        ContactNumber = c.String(),
                        Address = c.String(),
                        Memorandum = c.String(),
                        Status = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.UserProfile",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            UserName = c.String(),
            //        })
            //    .PrimaryKey(t => t.ID);
            
            //CreateTable(
            //    "dbo.webpages_Membership",
            //    c => new
            //        {
            //            UserId = c.Int(nullable: false),
            //            CreateDate = c.DateTime(nullable: false),
            //            ConfirmationToken = c.String(),
            //            IsConfirmed = c.Boolean(nullable: false),
            //            LastPasswordFailureDate = c.DateTime(),
            //            PasswordFailuresSinceLastSuccess = c.Int(nullable: false),
            //            Password = c.String(),
            //            PasswordChangedDate = c.DateTime(),
            //            PasswordSalt = c.String(),
            //            PasswordVerificationToken = c.String(),
            //            PasswordVerificationTokenExpirationDate = c.DateTime(),
            //        })
            //    .PrimaryKey(t => t.UserId)
            //    .ForeignKey("dbo.UserProfile", t => t.UserId)
            //    .Index(t => t.UserId);
            
            //CreateTable(
            //    "dbo.webpages_Roles",
            //    c => new
            //        {
            //            RoleId = c.Int(nullable: false, identity: true),
            //            RoleName = c.String(),
            //        })
            //    .PrimaryKey(t => t.RoleId);
            
            //CreateTable(
            //    "dbo.webpages_UsersInRoles",
            //    c => new
            //        {
            //            UserId = c.Int(nullable: false),
            //            RoleId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.UserId, t.RoleId })
            //    .ForeignKey("dbo.UserProfile", t => t.UserId, cascadeDelete: true)
            //    .ForeignKey("dbo.webpages_Roles", t => t.RoleId, cascadeDelete: true)
            //    .Index(t => t.UserId)
            //    .Index(t => t.RoleId);
            
            AlterColumn("dbo.Vendors", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Vendors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.webpages_UsersInRoles", "RoleId", "dbo.webpages_Roles");
            DropForeignKey("dbo.webpages_UsersInRoles", "UserId", "dbo.UserProfile");
            DropForeignKey("dbo.webpages_Membership", "UserId", "dbo.UserProfile");
            DropIndex("dbo.webpages_UsersInRoles", new[] { "RoleId" });
            DropIndex("dbo.webpages_UsersInRoles", new[] { "UserId" });
            DropIndex("dbo.webpages_Membership", new[] { "UserId" });
            DropPrimaryKey("dbo.Vendors");
            AlterColumn("dbo.Vendors", "Id", c => c.Guid(nullable: false));
            DropTable("dbo.webpages_UsersInRoles");
            DropTable("dbo.webpages_Roles");
            DropTable("dbo.webpages_Membership");
            DropTable("dbo.UserProfile");
            DropTable("dbo.Customer");
            AddPrimaryKey("dbo.Vendors", "Id");
        }
    }
}
