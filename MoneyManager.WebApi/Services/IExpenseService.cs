using MoneyManager.WebApi.DTO;
using System;
using System.Collections.Generic;

namespace MoneyManager.WebApi.Services
{
    public interface IExpenseService
    {
        IEnumerable<Expense> GetExpenses(SearchCriteria criteria);

        IEnumerable<Expense> GetExpenses(DateTime date);

        IEnumerable<Expense> GetExpenses(int year, int month);

        void DeleteExpense(int id);

        Expense GetExpense(int id);

        void SaveExpense(Expense expense);

        TransactionTotals GetExpenseTotals(DateTime currentDate);

        IEnumerable<DTO.CategoryTotal> GetCategoryTotals(DateTime dateFrom, DateTime dateTo);

        IEnumerable<DTO.CategoryTotal> GetCategoryTotals(DateTime dateFrom, DateTime dateTo, int categoryId);
    }
}
