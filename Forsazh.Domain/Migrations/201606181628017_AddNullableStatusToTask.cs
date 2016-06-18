namespace SaleOfDetails.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNullableStatusToTask : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Task", "Status", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Task", "Status", c => c.Int(nullable: false));
        }
    }
}
