using MoneyManager.WebApi.ViewModels;
using System;
using System.Collections.Generic;

namespace MoneyManager.WebApi.Services
{
    public interface IIncomeService
    {
        IEnumerable<IncomeViewModel> GetIncome(DateTime from, DateTime to);

        IEnumerable<IncomeViewModel> GetIncome(int year, int month);

        IncomeViewModel GetIncome(int id);
    }
}
