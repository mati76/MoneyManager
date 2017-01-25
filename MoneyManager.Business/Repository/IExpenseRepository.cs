using MoneyManager.Business.Models;
using System;
using System.Collections.Generic;

namespace MoneyManager.Business.Repository
{
    public interface IExpenseRepository : IRepository<Expense>
    {
        IEnumerable<Expense> GetExpenses(int year, int month);

        IEnumerable<Expense> GetExpenses(DateTime date);

        IEnumerable<Expense> GetExpensesByCriteria(SearchCriteria criteria);

        decimal GetExpenseTotalsFromDates(DateTime from, DateTime to);

        List<CategoryTotal> GeCategoryTotals(DateTime dateFrom, DateTime dateTo);

        List<CategoryTotal> GeCategoryTotals(DateTime dateFrom, DateTime dateTo, int categoryId);
    }
}
