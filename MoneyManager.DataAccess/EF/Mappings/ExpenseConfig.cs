using MoneyManager.DataAccess.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MoneyManager.DataAccess.EF.Mappings
{
    public class ExpenseConfig : EntityTypeConfiguration<Expense>
    {
        public ExpenseConfig()
        {
            HasKey(p => p.Id).Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Amount).IsRequired();
        }
    }
}
