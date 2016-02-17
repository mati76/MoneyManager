using System;

namespace MoneyManager.DataAccess.Models
{
    public class Income : BaseModel
    {
        public decimal Value { get; set; }

        public DateTime Date { get; set; }

        public string Note { get; set; }
    }
}
