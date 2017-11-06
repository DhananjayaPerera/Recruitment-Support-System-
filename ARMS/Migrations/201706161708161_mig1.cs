namespace ARMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applicaticants",
                c => new
                    {
                        ApplicantId = c.Int(nullable: false, identity: true),
                        firstName = c.String(),
                        lastName = c.String(),
                        email = c.String(),
                        phoneNumber = c.String(),
                        address = c.String(),
                        DOB = c.DateTime(nullable: false),
                        Gender = c.String(),
                        ZipCode = c.String(),
                        City = c.String(),
                        marks = c.Int(nullable: false),
                        VacancyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ApplicantId)
                .ForeignKey("dbo.Vacancies", t => t.VacancyId, cascadeDelete: true)
                .Index(t => t.VacancyId);
            
            CreateTable(
                "dbo.Vacancies",
                c => new
                    {
                        VacancyId = c.Int(nullable: false, identity: true),
                        closingDate = c.DateTime(nullable: false),
                        publishedDate = c.DateTime(nullable: false),
                        publishedBy = c.String(),
                        isActivated = c.Boolean(nullable: false),
                        JOBId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VacancyId)
                .ForeignKey("dbo.JOBs", t => t.JOBId, cascadeDelete: true)
                .Index(t => t.JOBId);
            
            CreateTable(
                "dbo.JOBs",
                c => new
                    {
                        JOBId = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(),
                        JobDescription = c.String(),
                        JobRequiredQualification = c.String(),
                    })
                .PrimaryKey(t => t.JOBId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        JobRole = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserClaims", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Applicaticants", "VacancyId", "dbo.Vacancies");
            DropForeignKey("dbo.Vacancies", "JOBId", "dbo.JOBs");
            DropIndex("dbo.AspNetUserClaims", new[] { "User_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Applicaticants", new[] { "VacancyId" });
            DropIndex("dbo.Vacancies", new[] { "JOBId" });
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.JOBs");
            DropTable("dbo.Vacancies");
            DropTable("dbo.Applicaticants");
        }
    }
}
