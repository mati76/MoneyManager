using MoneyManager.Business.Models;
using System;
using System.Collections.Generic;

namespace MoneyManager.Business.Interfaces
{
    public interface IExpenseBusiness
    {
        Expense GetExpense(int id);

        IEnumerable<Expense> GetExpenses(int year, int month);

        IEnumerable<Expense> GetExpenses(DateTime date);

        IEnumerable<Expense> GetExpenses(SearchCriteria criteria);

        void SaveExpense(Expense expense);

        void DeleteExpense(int id);

        TransactionTotals GetExpenseTotals(DateTime currentDate);

        List<CategoryTotal> GetCategoryTotals(DateTime dateFrom, DateTime dateTo, int categoryId);
    }
}
