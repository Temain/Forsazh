namespace SaleOfDetails.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldsToCrashType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CrashType", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.CrashType", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.CrashType", "DeletedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CrashType", "DeletedAt");
            DropColumn("dbo.CrashType", "UpdatedAt");
            DropColumn("dbo.CrashType", "CreatedAt");
        }
    }
}
