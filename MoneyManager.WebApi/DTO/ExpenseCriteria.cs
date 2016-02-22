using System;
using System.Collections.Generic;

namespace MoneyManager.WebApi.DTO
{
    public class ExpenseCriteria : BaseDTO
    {
        public IEnumerable<Category> Categories { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public decimal? MinAmount { get; set; }

        public decimal? MaxAmount { get; set; }

        public string SortAsc { get; set; }
    }
}