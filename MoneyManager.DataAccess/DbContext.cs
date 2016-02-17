using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MoneyManager.DataAccess.Models;
using MoneyManager.DataAccess.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyManager.DataAccess
{
    public class MoneyManagerContext : DbContext, IDbContext
    {
        public MoneyManagerContext()
            : base("name=MoneyManager")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<StoreGeneratedIdentityKeyConvention>();
            modelBuilder.Entity<Category>().HasKey(p => p.Id).Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Expense>().HasKey(p => p.Id).Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Income>().HasKey(p => p.Id).Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Expense> Expense { get; set; }
        public DbSet<Income> Income { get; set; }
    }
}
