namespace MoneyManager.Business.Models
{
    public class TransactionAggregates
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public decimal Sum { get; set; } 

        public decimal Avg { get; set; }
    }
}
