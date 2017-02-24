using MoneyManager.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.Business
{
    public interface IBudgetBusiness
    {
        Task<Expense> GetExpense(int id);

        Task<IEnumerable<Expense>> GetExpenses(int year, int month);

        Task<IEnumerable<Expense>> GetExpenses(int year);

        Task<IEnumerable<Expense>> GetExpenses(SearchCriteria criteria);

        Task SaveExpense(Expense expense);

        Task DeleteExpense(int id);


        Task<Income> GetIncome(int id);

        Task<IEnumerable<Income>> GetIncomes(int year, int month);

        Task<IEnumerable<Income>> GetIncomes(int year);

        Task<IEnumerable<Income>> GetIncomes(SearchCriteria criteria);

        Task SaveIncome(Income expense);

        Task DeleteIncome(int id);


        Task<decimal> GetAvgExpenseDeviation();

        Task<decimal> GetBudgetLimit(DateTime dateFrom, DateTime dateTo);

        Task<decimal> GetBudgetDeviation(DateTime dateFrom, DateTime dateTo);

        Task<decimal> GetBudgetBalance(DateTime dateFrom, DateTime dateTo);

        Task<IEnumerable<CategoryBalance>> GetCategoryBalance(DateTime dateFrom, DateTime dateTo);
    }
}
