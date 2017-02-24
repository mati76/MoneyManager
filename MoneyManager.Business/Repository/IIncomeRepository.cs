using System;
using System.Collections.Generic;
using MoneyManager.Business.Models;
using System.Threading.Tasks;

namespace MoneyManager.Business.Repository
{
    public interface IIncomeRepository : IRepository<Income>
    {
        Task<IEnumerable<Income>> GetIncome(int year, int month);

        Task<IEnumerable<Income>> GetIncome(DateTime from, DateTime to);

        Task<IEnumerable<Income>> GetIncomeByCriteria(SearchCriteria criteria);

        Task<decimal> GetIncomeTotalsFromDates(DateTime from, DateTime to);

        Task<List<CategoryTotal>> GeCategoryTotals(DateTime dateFrom, DateTime dateTo);
    }
}
