using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.Model
{
    public class Expense : BaseEntity
    {
        public decimal Value { get; set; }

        public DateTime Date { get; set; }

        public string Note { get; set; }

        public virtual Category Category { get; set; }
    }
}
