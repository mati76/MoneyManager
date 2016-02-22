using System;

namespace MoneyManager.WebApi.DTO
{
    public class Expense : BaseDTO
    {
        public decimal Value { get; set; }

        public DateTime Date { get; set; }

        public string Note { get; set; }

        public virtual Category Category { get; set; }
    }
}