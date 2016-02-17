using System;
using System.Collections.Generic;
using MoneyManager.Business.Models;

namespace MoneyManager.Business.Repository
{
    public interface IIncomeRepository : IRepository<Income>
    {
        IEnumerable<Income> GetIncome(int year, int month);

        IEnumerable<Income> GetIncome(DateTime from, DateTime to);
    }
}
