using MoneyManager.Business.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MoneyManager.DataAccess.EF.Mappings
{
    public class RepeatTransactionConfig : EntityTypeConfiguration<RepeatTransaction>
    {
        public RepeatTransactionConfig()
        {
            HasKey(p => p.Id).Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Amount).IsRequired();
            Property(p => p.CategoryId).IsRequired();
            Property(p => p.Name).IsRequired();
            Property(p => p.Repeat).IsRequired();
            Property(p => p.RepeatPeriod).IsRequired();
            Property(p => p.StartDate).IsRequired();
        }
    }
}
