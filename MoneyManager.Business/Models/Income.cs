using System;

namespace MoneyManager.Business.Models
{
    public class Income : BaseEntity
    {
        public decimal Value { get; set; }

        public DateTime Date { get; set; }

        public string Note { get; set; }
    }
}
