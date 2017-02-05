namespace MoneyManager.WebApi.DTO
{
    public class BudgetRealization
    {
        public string CategoryName { get; set; }

        public decimal? Expense { get; set; }

        public decimal? Left { get; set; }

        public decimal? Over { get; set; }
    }
}