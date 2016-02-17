using System;

namespace MoneyManager.WebApi.ViewModels
{
    public class ExpenseViewModel : BaseViewModel
    {
        public decimal Value { get; set; }

        public DateTime Date { get; set; }

        public string Note { get; set; }

        public virtual CategoryViewModel Category { get; set; }
    }
}