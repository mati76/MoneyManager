
using System.Collections.Generic;

namespace MoneyManager.DataAccess.Models
{
    public class ExpenseCategory : BaseModel
    {
        public string Name { get; set; }

        public string Color { get; set; }

        public int? ParentId { get; set; }

        public virtual ExpenseCategory Parent { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }

        public virtual ICollection<ExpenseCategory> Categories { get; set; }
    }
}
