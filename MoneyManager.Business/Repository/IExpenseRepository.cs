using MoneyManager.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.Business.Repository
{
    public interface IExpenseRepository : IRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> GetExpenses(int year, int month);

        Task<IEnumerable<Transaction>> GetExpenses(DateTime date);

        Task<IEnumerable<Transaction>> GetExpenses(DateTime dateFrom, DateTime dateTo);

        Task<TransactionCollection> GetExpensesByCriteria(SearchCriteria criteria);

        Task<decimal> GetExpenseTotalsFromDates(DateTime from, DateTime to);

        Task<List<CategoryTotal>> GetCategoryTotals(DateTime dateFrom, DateTime dateTo);

        Task<List<CategoryTotal>> GetCategoryTotals(DateTime dateFrom, DateTime dateTo, int categoryId);

        Task<List<TransactionAggregates>> GetExpenseAggregates();
    }
}
