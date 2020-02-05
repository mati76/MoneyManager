namespace MoneyManager.Api.DTO
{
    public class BudgetTotals
    {
        public decimal BudgetLimit { get; set; }

        public decimal BudgetBalance { get; set; }

        public decimal Deviation { get; set; }

        public decimal AvgDeviation { get; set; }
    }
}