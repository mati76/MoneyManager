using System;

namespace MoneyManager.DataAccess.Models
{
    public class Expense : BaseModel
    {
        public decimal Value { get; set; }

        public DateTime Date { get; set; }

        public string Note { get; set; }

        public virtual Category Category { get; set; }
    }
}
