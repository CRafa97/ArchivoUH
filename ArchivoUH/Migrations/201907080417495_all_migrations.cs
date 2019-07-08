namespace ArchivoUH.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class all_migrations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administratives",
                c => new
                    {
                        AdministrativeId = c.Int(nullable: false, identity: true),
                        AdministrativeName = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 200),
                        KeyWordId = c.Int(nullable: false),
                        SerialType = c.Int(nullable: false),
                        Serial1 = c.String(nullable: false, maxLength: 3),
                        Serial2 = c.String(nullable: false, maxLength: 3),
                    })
                .PrimaryKey(t => t.AdministrativeId)
                .ForeignKey("dbo.KeyWords", t => t.KeyWordId)
                .Index(t => t.KeyWordId);
            
            CreateTable(
                "dbo.KeyWords",
                c => new
                    {
                        KeyWordId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.KeyWordId);
            
            CreateTable(
                "dbo.IdentityUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IdentityUsers", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.IdentityUsers", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .ForeignKey("dbo.IdentityUsers", t => t.IdentityUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        CountryName = c.String(nullable: false, maxLength: 120),
                    })
                .PrimaryKey(t => t.CountryId);
            
            CreateTable(
                "dbo.Graduateds",
                c => new
                    {
                        GraduatedId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        LocalityId = c.Int(nullable: true),
                        ProvinceId = c.Int(nullable: true),
                        CountryId = c.Int(nullable: false),
                        FacultyId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        TomeUH = c.Int(nullable: false),
                        FolioUH = c.Int(nullable: false),
                        NumberUH = c.Int(nullable: false),
                        FinishTime = c.DateTime(nullable: false),
                        ExpeditionTime = c.DateTime(nullable: false),
                        ExpeditionCauses = c.String(maxLength: 300),
                        Observations = c.String(maxLength: 300),
                        GoldTitle = c.Boolean(nullable: false),
                        ScientistCredit = c.Boolean(nullable: false),
                        Serial1 = c.String(nullable: false, maxLength: 3),
                        Serial2 = c.String(nullable: false, maxLength: 3),
                        SerialType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GraduatedId)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .ForeignKey("dbo.Faculties", t => t.FacultyId)
                .ForeignKey("dbo.Provinces", t => t.ProvinceId)
                .ForeignKey("dbo.Localities", t => t.LocalityId)
                .Index(t => t.LocalityId)
                .Index(t => t.ProvinceId)
                .Index(t => t.CountryId)
                .Index(t => t.FacultyId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        CourseName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.CourseId);
            
            CreateTable(
                "dbo.Leaveds",
                c => new
                    {
                        LeavedId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        FacultyId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        LeavedDate = c.DateTime(nullable: false),
                        Causes = c.String(),
                        Serial1 = c.String(nullable: false, maxLength: 3),
                        Serial2 = c.String(nullable: false, maxLength: 3),
                        SerialType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LeavedId)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .ForeignKey("dbo.Faculties", t => t.FacultyId)
                .Index(t => t.FacultyId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Faculties",
                c => new
                    {
                        FacultyId = c.Int(nullable: false, identity: true),
                        FacultyName = c.String(nullable: false, maxLength: 100),
                        FacultyTome = c.Int(nullable: false),
                        FacultyFolio = c.Int(nullable: false),
                        FacultyNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FacultyId);
            
            CreateTable(
                "dbo.Localities",
                c => new
                    {
                        LocalityId = c.Int(nullable: false, identity: true),
                        LocalityName = c.String(nullable: false, maxLength: 50),
                        ProvinceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LocalityId)
                .ForeignKey("dbo.Provinces", t => t.ProvinceId)
                .Index(t => t.ProvinceId);
            
            CreateTable(
                "dbo.Provinces",
                c => new
                    {
                        ProvinceId = c.Int(nullable: false, identity: true),
                        ProvinceName = c.String(nullable: false, maxLength: 50),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProvinceId)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "IdentityUser_Id", "dbo.IdentityUsers");
            DropForeignKey("dbo.AspNetUserLogins", "IdentityUser_Id", "dbo.IdentityUsers");
            DropForeignKey("dbo.IdentityUserClaims", "IdentityUser_Id", "dbo.IdentityUsers");
            DropForeignKey("dbo.AspNetUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.Graduateds", "LocalityId", "dbo.Localities");
            DropForeignKey("dbo.Localities", "ProvinceId", "dbo.Provinces");
            DropForeignKey("dbo.Graduateds", "ProvinceId", "dbo.Provinces");
            DropForeignKey("dbo.Provinces", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Graduateds", "FacultyId", "dbo.Faculties");
            DropForeignKey("dbo.Graduateds", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Leaveds", "FacultyId", "dbo.Faculties");
            DropForeignKey("dbo.Leaveds", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Graduateds", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Administratives", "KeyWordId", "dbo.KeyWords");
            DropIndex("dbo.Provinces", new[] { "CountryId" });
            DropIndex("dbo.Localities", new[] { "ProvinceId" });
            DropIndex("dbo.Leaveds", new[] { "CourseId" });
            DropIndex("dbo.Leaveds", new[] { "FacultyId" });
            DropIndex("dbo.Graduateds", new[] { "CourseId" });
            DropIndex("dbo.Graduateds", new[] { "FacultyId" });
            DropIndex("dbo.Graduateds", new[] { "CountryId" });
            DropIndex("dbo.Graduateds", new[] { "ProvinceId" });
            DropIndex("dbo.Graduateds", new[] { "LocalityId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "IdentityUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "IdentityUser_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "IdentityUser_Id" });
            DropIndex("dbo.Administratives", new[] { "KeyWordId" });
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.Provinces");
            DropTable("dbo.Localities");
            DropTable("dbo.Faculties");
            DropTable("dbo.Leaveds");
            DropTable("dbo.Courses");
            DropTable("dbo.Graduateds");
            DropTable("dbo.Countries");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.IdentityUsers");
            DropTable("dbo.KeyWords");
            DropTable("dbo.Administratives");
        }
    }
}
