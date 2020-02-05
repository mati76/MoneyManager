using System;
using System.Collections.Generic;

namespace MoneyManager.Api.DTO
{
    public class SearchCriteria : BaseDTO
    {
        public IEnumerable<int> CategoryIDs { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public decimal? MinAmount { get; set; }

        public decimal? MaxAmount { get; set; }

        public string SortBy { get; set; }

        public bool SortAsc { get; set; }

        public int? Take { get; set; }

        public int? Skip { get; set; }
    }
}