using MoneyManager.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.Business.Repository
{
    public interface IBudgetExpenseRepository : IRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> GetExpenses(int year, int month);

        Task<IEnumerable<Transaction>> GetExpenses(DateTime dateFrom, DateTime dateTo);

        Task<IEnumerable<Transaction>> GetExpenses(int year);

        Task<IEnumerable<Transaction>> GetExpensesByCriteria(SearchCriteria criteria);

        Task<List<TransactionAggregates>> GetExpenseAggregates();

        Task<List<CategoryTotal>> GetCategoryTotals(DateTime dateFrom, DateTime dateTo);
    }
}
