using MoneyManager.DataAccess.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MoneyManager.DataAccess.EF.Mappings
{
    public class IncomeCategoryConfig : EntityTypeConfiguration<IncomeCategory>
    {
        public IncomeCategoryConfig()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(50);

            HasKey(p => p.Id).Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
