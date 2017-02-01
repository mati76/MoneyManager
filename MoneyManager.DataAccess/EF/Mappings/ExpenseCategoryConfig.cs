using MoneyManager.DataAccess.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MoneyManager.DataAccess.EF.Mappings
{
    public class ExpenseCategoryConfig : EntityTypeConfiguration<ExpenseCategory>
    {
        public ExpenseCategoryConfig()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(50);

            HasKey(p => p.Id).Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(p => p.Parent).WithMany(p => p.Categories).HasForeignKey(p => p.ParentId).WillCascadeOnDelete(false);
        }
    }
}
