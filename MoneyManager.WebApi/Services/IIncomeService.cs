using MoneyManager.WebApi.DTO;
using System;
using System.Collections.Generic;

namespace MoneyManager.WebApi.Services
{
    public interface IIncomeService
    {
        IEnumerable<Income> GetIncome(DateTime from, DateTime to);

        IEnumerable<Income> GetIncome(int year, int month);

        Income GetIncome(int id);
    }
}
