using System.Collections.Generic;

namespace MoneyManager.Business.Models
{
    public class TransactionCollection
    {
        public IEnumerable<Transaction> Transactions { get; set; }

        public IEnumerable<Category> Categories { get; set; }
    }
}
