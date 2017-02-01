using System;
using System.Collections.Generic;
using MoneyManager.Business.Models;

namespace MoneyManager.Business.Repository
{
    public interface IBudgetIncomeRepository : IRepository<Income>
    {
        IEnumerable<Income> GetIncome(int year, int month);

        IEnumerable<Income> GetIncome(int year);

        IEnumerable<Income> GetIncomeByCriteria(SearchCriteria criteria);
    }
}
