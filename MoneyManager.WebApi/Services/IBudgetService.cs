using System.Collections.Generic;
using MoneyManager.WebApi.DTO;
using System;
using System.Threading.Tasks;

namespace MoneyManager.WebApi.Services
{
    public interface IBudgetService
    {
        Task DeleteExpense(int id);
        Task DeleteIncome(int id);
        Task<Transaction> GetExpense(int id);
        Task<IEnumerable<Transaction>> GetExpenses(SearchCriteria criteria);
        Task<IEnumerable<Transaction>> GetExpenses(int year, int month);
        Task<IEnumerable<Transaction>> GetIncome(SearchCriteria criteria);
        Task<Transaction> GetIncome(int id);
        Task<IEnumerable<Transaction>> GetIncome(int year, int month);
        Task SaveExpense(Transaction expense);
        Task SaveIncome(Transaction income);
        Task<BudgetTotals> GetBudgetTotals(DateTime dateFrom, DateTime dateTo);
        Task<IEnumerable<BudgetRealization>> GetBudgetRealization(DateTime dateFrom, DateTime dateTo);
    }
}