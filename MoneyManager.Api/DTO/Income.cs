using System;

namespace MoneyManager.Api.DTO
{
    public class Income : BaseDTO
    {
        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public int CategoryId { get; set; }

        public string Comment { get; set; }

        public string CategoryName { get; set; }
    }
}