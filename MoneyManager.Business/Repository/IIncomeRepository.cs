using System;
using System.Collections.Generic;
using MoneyManager.Business.Models;
using System.Threading.Tasks;

namespace MoneyManager.Business.Repository
{
    public interface IIncomeRepository : IRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> GetIncome(int year, int month);

        Task<IEnumerable<Transaction>> GetIncome(DateTime from, DateTime to);

        Task<IEnumerable<Transaction>> GetIncomeByCriteria(SearchCriteria criteria);

        Task<decimal> GetIncomeTotalsFromDates(DateTime from, DateTime to);

        Task<List<CategoryTotal>> GeCategoryTotals(DateTime dateFrom, DateTime dateTo);
    }
}
