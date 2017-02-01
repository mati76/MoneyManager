using MoneyManager.DataAccess.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MoneyManager.DataAccess.EF.Mappings
{
    public class BudgetIncomeConfig : EntityTypeConfiguration<BudgetIncome>
    {
        public BudgetIncomeConfig()
        {
            HasKey(p => p.Id).Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Amount).IsRequired();
        }
    }
}
