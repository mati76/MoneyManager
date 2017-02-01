using MoneyManager.Business.Models;
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

        BudgetTotals GetTotals(int year, int month);
    }
}
