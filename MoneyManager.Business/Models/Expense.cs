using System;

namespace MoneyManager.Business.Models
{
    public class Expense : BaseEntity
    {
        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string Comment { get; set; }

        public virtual Category Category { get; set; }
    }
}
