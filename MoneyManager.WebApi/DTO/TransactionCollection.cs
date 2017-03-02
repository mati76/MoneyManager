using System.Collections.Generic;

namespace MoneyManager.WebApi.DTO
{
    public class TransactionCollection
    {
        public IEnumerable<Transaction> Transactions { get; set; }

        public IEnumerable<CategoryInfo> Categories { get; set; }
    }
}