namespace AdminLTE.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class FirstMigrations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KeyType",
                c => new
                {
                    KeyTypeId = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 100, unicode: false),
                    Code = c.String(nullable: false, maxLength: 10, unicode: false),
                    IsDeleted = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.KeyTypeId);

            CreateTable(
                "dbo.KeyValue",
                c => new
                {
                    KeyValueId = c.Int(nullable: false, identity: true),
                    KeyTypeId = c.Int(nullable: false),
                    Key = c.String(nullable: false, maxLength: 255, unicode: false),
                    Value = c.String(nullable: false, maxLength: 255, unicode: false),
                    Description = c.String(maxLength: 500, unicode: false),
                    StartDate = c.DateTime(nullable: false),
                    EndDate = c.DateTime(nullable: false),
                    CreatedDate = c.DateTime(nullable: false),
                    CreatedUser = c.String(nullable: false, maxLength: 50, unicode: false),
                    ModifiedDate = c.DateTime(),
                    ModifiedUser = c.String(maxLength: 50, unicode: false),
                    IsActive = c.Boolean(nullable: false),
                    IsDeleted = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.KeyValueId)
                .ForeignKey("dbo.KeyType", t => t.KeyTypeId)
                .Index(t => t.KeyTypeId);

            CreateTable(
                "dbo.AspNetRoles",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false, maxLength: 256),
                    Discriminator = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

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
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.TranslationLanguage",
                c => new
                {
                    TranslationLanguageId = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 50),
                    Code = c.String(nullable: false, maxLength: 5),
                    IsDeleted = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.TranslationLanguageId);

            CreateTable(
                "dbo.TranslationWord",
                c => new
                {
                    TranslationWordId = c.Int(nullable: false, identity: true),
                    TranslationLanguageId = c.Int(nullable: false),
                    Description = c.String(nullable: false, maxLength: 500),
                    Code = c.String(nullable: false, maxLength: 100),
                    IsDeleted = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.TranslationWordId)
                .ForeignKey("dbo.TranslationLanguage", t => t.TranslationLanguageId, cascadeDelete: true)
                .Index(t => t.TranslationLanguageId);

            CreateTable(
                "dbo.AspNetUsers",
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
                "dbo.AspNetUserClaims",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(nullable: false, maxLength: 128),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                {
                    LoginProvider = c.String(nullable: false, maxLength: 128),
                    ProviderKey = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TranslationWord", "TranslationLanguageId", "dbo.TranslationLanguage");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.KeyValue", "KeyTypeId", "dbo.KeyType");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.TranslationWord", new[] { "TranslationLanguageId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.KeyValue", new[] { "KeyTypeId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.TranslationWord");
            DropTable("dbo.TranslationLanguage");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.KeyValue");
            DropTable("dbo.KeyType");
        }
    }
}
