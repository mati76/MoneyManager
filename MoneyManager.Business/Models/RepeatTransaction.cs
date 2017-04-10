using MoneyManager.Business.Models.Enums;
using System;

namespace MoneyManager.Business.Models
{
    public class RepeatTransaction : BaseEntity
    {
        public string Name { get; set; }

        public decimal Amount { get; set; }

        public int CategoryId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDateTime { get; set; }

        public string Comment { get; set; }

        public int Repeat { get; set; }

        public RepeatPeriod RepeatPeriod { get; set; }
    }
}
