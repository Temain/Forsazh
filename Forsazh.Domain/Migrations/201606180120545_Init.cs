namespace SaleOfDetails.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CrashType",
                c => new
                    {
                        CrashTypeId = c.Int(nullable: false, identity: true),
                        CrashTypeName = c.String(),
                        RepairCost = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CrashTypeId);
            
            CreateTable(
                "dbo.Task",
                c => new
                    {
                        TaskId = c.Int(nullable: false, identity: true),
                        ClientName = c.String(),
                        CarModel = c.String(),
                        CarNumber = c.String(),
                        CrashTypeId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        Comment = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.TaskId)
                .ForeignKey("dbo.CrashType", t => t.CrashTypeId)
                .ForeignKey("dbo.Employee", t => t.EmployeeId)
                .Index(t => t.CrashTypeId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        EmployeeDateStart = c.DateTime(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Person", t => t.PersonId)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        PersonId = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 500),
                        FirstName = c.String(nullable: false, maxLength: 500),
                        MiddleName = c.String(maxLength: 500),
                        Birthday = c.DateTime(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.PersonId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PersonId = c.Int(),
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
                .ForeignKey("dbo.Person", t => t.PersonId)
                .Index(t => t.PersonId)
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SparePart",
                c => new
                    {
                        SparePartId = c.Int(nullable: false, identity: true),
                        SparePartName = c.String(),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InStock = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.SparePartId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        FullName = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.SparePartTask",
                c => new
                    {
                        SparePart_SparePartId = c.Int(nullable: false),
                        Task_TaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SparePart_SparePartId, t.Task_TaskId })
                .ForeignKey("dbo.SparePart", t => t.SparePart_SparePartId)
                .ForeignKey("dbo.Task", t => t.Task_TaskId)
                .Index(t => t.SparePart_SparePartId)
                .Index(t => t.Task_TaskId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.SparePartTask", "Task_TaskId", "dbo.Task");
            DropForeignKey("dbo.SparePartTask", "SparePart_SparePartId", "dbo.SparePart");
            DropForeignKey("dbo.Task", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.Employee", "PersonId", "dbo.Person");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "PersonId", "dbo.Person");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Task", "CrashTypeId", "dbo.CrashType");
            DropIndex("dbo.SparePartTask", new[] { "Task_TaskId" });
            DropIndex("dbo.SparePartTask", new[] { "SparePart_SparePartId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "PersonId" });
            DropIndex("dbo.Employee", new[] { "PersonId" });
            DropIndex("dbo.Task", new[] { "EmployeeId" });
            DropIndex("dbo.Task", new[] { "CrashTypeId" });
            DropTable("dbo.SparePartTask");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.SparePart");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Person");
            DropTable("dbo.Employee");
            DropTable("dbo.Task");
            DropTable("dbo.CrashType");
        }
    }
}
