namespace MoneyManager.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRepeatTransaction : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RepeatTransaction",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CategoryId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDateTime = c.DateTime(),
                        Comment = c.String(),
                        Repeat = c.Int(nullable: false),
                        RepeatPeriod = c.Int(nullable: false),
                        LastExecutionDt = c.DateTime(nullable: false),
                        LastTransactionDt = c.DateTime(nullable: false),
                        AddDateTime = c.DateTime(nullable: false),
                        UpdateDateTime = c.DateTime(),
                        AddUserName = c.String(),
                        UpdateUserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RepeatTransaction");
        }
    }
}
