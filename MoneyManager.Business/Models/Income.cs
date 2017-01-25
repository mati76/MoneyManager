using System;

namespace MoneyManager.Business.Models
{
    public class Income : BaseEntity
    {
        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string Comment { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}

