using MoneyManager.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.Business.Repository
{
    public interface IBudgetExpenseRepository : IRepository<Expense>
    {
        Task<IEnumerable<Expense>> GetExpenses(int year, int month);

        Task<IEnumerable<Expense>> GetExpenses(DateTime dateFrom, DateTime dateTo);

        Task<IEnumerable<Expense>> GetExpenses(int year);

        Task<IEnumerable<Expense>> GetExpensesByCriteria(SearchCriteria criteria);

        Task<List<TransactionAggregates>> GetExpenseAggregates();

        Task<List<CategoryTotal>> GetCategoryTotals(DateTime dateFrom, DateTime dateTo);
    }
}
