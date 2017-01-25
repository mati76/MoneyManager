using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MoneyManager.DataAccess.Models;
using MoneyManager.DataAccess.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;
using MoneyManager.DataAccess.EF.Mappings;

namespace MoneyManager.DataAccess.EF
{
    public class MoneyManagerContext : DbContext, IDbContext
    {
        public MoneyManagerContext()
            : base("name=MoneyManager")
        {
            Database.SetInitializer<MoneyManagerContext>(new DbInitializer());
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<StoreGeneratedIdentityKeyConvention>();

            modelBuilder.Configurations.Add(new ExpenseCategoryConfig());
            modelBuilder.Configurations.Add(new IncomeCategoryConfig());
            modelBuilder.Configurations.Add(new IncomeConfig());
            modelBuilder.Configurations.Add(new ExpenseConfig());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ExpenseCategory> ExpenseCategory { get; set; }
        public DbSet<IncomeCategory> IncomeCategory { get; set; }
        public DbSet<Expense> Expense { get; set; }
        public DbSet<Income> Income { get; set; }
    }
}
