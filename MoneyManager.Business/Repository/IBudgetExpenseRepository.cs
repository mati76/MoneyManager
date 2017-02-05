using MoneyManager.Business.Models;
using System;
using System.Collections.Generic;

namespace MoneyManager.Business.Repository
{
    public interface IBudgetExpenseRepository : IRepository<Expense>
    {
        IEnumerable<Expense> GetExpenses(int year, int month);

        IEnumerable<Expense> GetExpenses(DateTime dateFrom, DateTime dateTo);

        IEnumerable<Expense> GetExpenses(int year);

        IEnumerable<Expense> GetExpensesByCriteria(SearchCriteria criteria);

        IEnumerable<TransactionAggregates> GetExpenseAggregates();

        IEnumerable<CategoryTotal> GetCategoryTotals(DateTime dateFrom, DateTime dateTo);
    }
}
