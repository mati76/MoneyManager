using MoneyManager.WebApi.DTO;
using System;
using System.Collections.Generic;

namespace MoneyManager.WebApi.Services
{
    public interface IExpenseService
    {
        IEnumerable<Expense> GetExpenses(ExpenseCriteria criteria);

        IEnumerable<Expense> GetExpenses(DateTime date);

        IEnumerable<Expense> GetExpenses(int year, int month);

        Expense GetExpense(int id);

        void SaveExpense(Expense expense);

        ExpenseTotals GetExpenseTotals(DateTime currentDate);
    }
}
