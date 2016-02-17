using System;
using System.Collections.Generic;

namespace MoneyManager.WebApi.ViewModels
{
    public class ExpenseCriteriaViewModel : BaseViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public decimal? MinAmount { get; set; }

        public decimal? MaxAmount { get; set; }

        public string SortAsc { get; set; }
    }
}