namespace MoneyManager.Business.Models
{
    public class BudgetTotals
    {
        public decimal BudgetLimit { get; set; }

        public decimal BudgetBalance { get; set; }

        public decimal Deviation { get; set; }

        public decimal AvgDeviaton { get; set; }
    }
}
