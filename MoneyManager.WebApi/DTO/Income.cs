using System;

namespace MoneyManager.WebApi.DTO
{
    public class Income : BaseDTO
    {
        public decimal Value { get; set; }

        public DateTime Date { get; set; }

        public string Note { get; set; }
    }
}