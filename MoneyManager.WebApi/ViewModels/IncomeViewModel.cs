using System;

namespace MoneyManager.WebApi.ViewModels
{
    public class IncomeViewModel : BaseViewModel
    {
        public decimal Value { get; set; }

        public DateTime Date { get; set; }

        public string Note { get; set; }
    }
}