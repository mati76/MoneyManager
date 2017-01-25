namespace MoneyManager.WebApi.DTO
{
    public class CategoryTotal
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal Percent { get; set; }
    }
}