namespace SaleOfDetails.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLogEntry : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "serv.LogEntry",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.String(),
                        Level = c.String(),
                        Logger = c.String(),
                        ClassMethod = c.String(),
                        Message = c.String(),
                        Username = c.String(),
                        RequestUri = c.String(),
                        RemoteAddress = c.String(),
                        UserAgent = c.String(),
                        Exception = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("serv.LogEntry");
        }
    }
}
