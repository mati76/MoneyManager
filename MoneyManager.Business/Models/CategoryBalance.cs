namespace MoneyManager.Business.Models
{
    public class CategoryBalance
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public decimal? Expense { get; set; }

        public decimal? Balance { get; set; }
    }
}
