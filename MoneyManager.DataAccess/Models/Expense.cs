using System;

namespace MoneyManager.DataAccess.Models
{
    public class Expense : BaseModel
    {
        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string Comment { get; set; }

        public int CategoryId { get; set; }

        public virtual ExpenseCategory Category { get; set; }
    }
}
