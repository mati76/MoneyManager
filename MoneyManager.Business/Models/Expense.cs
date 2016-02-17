using System;

namespace MoneyManager.Business.Models
{
    public class Expense : BaseEntity
    {
        public decimal Value { get; set; }

        public DateTime Date { get; set; }

        public string Note { get; set; }

        public virtual Category Category { get; set; }
    }
}
