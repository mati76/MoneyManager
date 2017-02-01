using System;

namespace MoneyManager.DataAccess.Models
{
    public class BudgetIncome : BaseModel
    {
        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string Comment { get; set; }

        public int CategoryId { get; set; }

        public virtual IncomeCategory Category { get; set; }
    }
}
