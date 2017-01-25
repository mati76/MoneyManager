namespace MoneyManager.WebApi.DTO
{
    public class TransactionTotals
    {
        public decimal Today { get; set; }

        public decimal CurrentWeek { get; set; }

        public decimal CurrentMonth { get; set; }

        public decimal CurrentYear { get; set; }
    }
}