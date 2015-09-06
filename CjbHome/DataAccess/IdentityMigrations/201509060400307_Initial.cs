namespace CjbHome.DataAccess.IdentityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "CjbHome.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "CjbHome.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("CjbHome.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("CjbHome.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "CjbHome.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "CjbHome.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("CjbHome.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "CjbHome.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("CjbHome.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("CjbHome.AspNetUserRoles", "UserId", "CjbHome.AspNetUsers");
            DropForeignKey("CjbHome.AspNetUserLogins", "UserId", "CjbHome.AspNetUsers");
            DropForeignKey("CjbHome.AspNetUserClaims", "UserId", "CjbHome.AspNetUsers");
            DropForeignKey("CjbHome.AspNetUserRoles", "RoleId", "CjbHome.AspNetRoles");
            DropIndex("CjbHome.AspNetUserLogins", new[] { "UserId" });
            DropIndex("CjbHome.AspNetUserClaims", new[] { "UserId" });
            DropIndex("CjbHome.AspNetUsers", "UserNameIndex");
            DropIndex("CjbHome.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("CjbHome.AspNetUserRoles", new[] { "UserId" });
            DropIndex("CjbHome.AspNetRoles", "RoleNameIndex");
            DropTable("CjbHome.AspNetUserLogins");
            DropTable("CjbHome.AspNetUserClaims");
            DropTable("CjbHome.AspNetUsers");
            DropTable("CjbHome.AspNetUserRoles");
            DropTable("CjbHome.AspNetRoles");
        }
    }
}
