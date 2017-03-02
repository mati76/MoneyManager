using MoneyManager.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.Business
{
    public interface IBudgetBusiness
    {
        Task<Transaction> GetExpense(int id);

        Task<IEnumerable<Transaction>> GetExpenses(int year, int month);

        Task<IEnumerable<Transaction>> GetExpenses(SearchCriteria criteria);

        Task SaveExpense(Transaction expense);

        Task DeleteExpense(int id);


        Task<Transaction> GetIncome(int id);

        Task<IEnumerable<Transaction>> GetIncome(int year, int month);

        Task<IEnumerable<Transaction>> GetIncome(SearchCriteria criteria);

        Task SaveIncome(Transaction income);

        Task DeleteIncome(int id);


        Task<decimal> GetAvgExpenseDeviation();

        Task<decimal> GetBudgetLimit(DateTime dateFrom, DateTime dateTo);

        Task<decimal> GetBudgetDeviation(DateTime dateFrom, DateTime dateTo);

        Task<decimal> GetBudgetBalance(DateTime dateFrom, DateTime dateTo);

        Task<IEnumerable<CategoryBalance>> GetCategoryBalance(DateTime dateFrom, DateTime dateTo);
    }
}
