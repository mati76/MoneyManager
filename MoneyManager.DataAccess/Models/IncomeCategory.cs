
using System.Collections.Generic;

namespace MoneyManager.DataAccess.Models
{
    public class IncomeCategory : BaseModel
    {
        public string Name { get; set; }

        public virtual ICollection<Income> Incomes { get; set; }
    }
}
