namespace MoneyManager.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BudgetExpense",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        Comment = c.String(),
                        CategoryId = c.Int(nullable: false),
                        AddDateTime = c.DateTime(nullable: false),
                        UpdateDateTime = c.DateTime(),
                        AddUserName = c.String(),
                        UpdateUserName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExpenseCategory", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.ExpenseCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Color = c.String(),
                        ParentId = c.Int(),
                        AddDateTime = c.DateTime(nullable: false),
                        UpdateDateTime = c.DateTime(),
                        AddUserName = c.String(),
                        UpdateUserName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExpenseCategory", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Expense",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        Comment = c.String(),
                        CategoryId = c.Int(nullable: false),
                        AddDateTime = c.DateTime(nullable: false),
                        UpdateDateTime = c.DateTime(),
                        AddUserName = c.String(),
                        UpdateUserName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExpenseCategory", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.BudgetIncome",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        Comment = c.String(),
                        CategoryId = c.Int(nullable: false),
                        AddDateTime = c.DateTime(nullable: false),
                        UpdateDateTime = c.DateTime(),
                        AddUserName = c.String(),
                        UpdateUserName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IncomeCategory", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.IncomeCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        AddDateTime = c.DateTime(nullable: false),
                        UpdateDateTime = c.DateTime(),
                        AddUserName = c.String(),
                        UpdateUserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Income",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        Comment = c.String(),
                        CategoryId = c.Int(nullable: false),
                        AddDateTime = c.DateTime(nullable: false),
                        UpdateDateTime = c.DateTime(),
                        AddUserName = c.String(),
                        UpdateUserName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IncomeCategory", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BudgetIncome", "CategoryId", "dbo.IncomeCategory");
            DropForeignKey("dbo.Income", "CategoryId", "dbo.IncomeCategory");
            DropForeignKey("dbo.BudgetExpense", "CategoryId", "dbo.ExpenseCategory");
            DropForeignKey("dbo.ExpenseCategory", "ParentId", "dbo.ExpenseCategory");
            DropForeignKey("dbo.Expense", "CategoryId", "dbo.ExpenseCategory");
            DropIndex("dbo.Income", new[] { "CategoryId" });
            DropIndex("dbo.BudgetIncome", new[] { "CategoryId" });
            DropIndex("dbo.Expense", new[] { "CategoryId" });
            DropIndex("dbo.ExpenseCategory", new[] { "ParentId" });
            DropIndex("dbo.BudgetExpense", new[] { "CategoryId" });
            DropTable("dbo.Income");
            DropTable("dbo.IncomeCategory");
            DropTable("dbo.BudgetIncome");
            DropTable("dbo.Expense");
            DropTable("dbo.ExpenseCategory");
            DropTable("dbo.BudgetExpense");
        }
    }
}
