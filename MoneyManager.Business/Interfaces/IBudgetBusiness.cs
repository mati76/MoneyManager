using MoneyManager.Business.Models;
using System;
using System.Collections.Generic;

namespace MoneyManager.Business
{
    public interface IBudgetBusiness
    {
        Expense GetExpense(int id);

        IEnumerable<Expense> GetExpenses(int year, int month);

        IEnumerable<Expense> GetExpenses(int year);

        IEnumerable<Expense> GetExpenses(SearchCriteria criteria);

        void SaveExpense(Expense expense);

        void DeleteExpense(int id);


        Income GetIncome(int id);

        IEnumerable<Income> GetIncomes(int year, int month);

        IEnumerable<Income> GetIncomes(int year);

        IEnumerable<Income> GetIncomes(SearchCriteria criteria);

        void SaveIncome(Income expense);

        void DeleteIncome(int id);


        decimal GetAvgExpenseDeviation();

        decimal GetBudgetLimit(DateTime dateFrom, DateTime dateTo);

        decimal GetBudgetDeviation(DateTime dateFrom, DateTime dateTo);

        decimal GetBudgetBalance(DateTime dateFrom, DateTime dateTo);

        IEnumerable<CategoryBalance> GetCategoryBalance(DateTime dateFrom, DateTime dateTo);
    }
}
