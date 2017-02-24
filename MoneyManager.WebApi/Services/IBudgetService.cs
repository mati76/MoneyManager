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
        Task<Expense> GetExpense(int id);
        Task<IEnumerable<Expense>> GetExpenses(SearchCriteria criteria);
        Task<IEnumerable<Expense>> GetExpenses(int year, int month);
        Task<IEnumerable<Income>> GetIncome(SearchCriteria criteria);
        Task<Income> GetIncome(int id);
        Task<IEnumerable<Income>> GetIncome(int year, int month);
        Task SaveExpense(Expense expense);
        Task SaveIncome(Income income);
        Task<BudgetTotals> GetBudgetTotals(DateTime dateFrom, DateTime dateTo);
        Task<IEnumerable<BudgetRealization>> GetBudgetRealization(DateTime dateFrom, DateTime dateTo);
    }
}