using MoneyManager.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.WebApi.Services
{
    public interface IExpenseService
    {
        Task<IEnumerable<Expense>> GetExpenses(SearchCriteria criteria);

        Task<IEnumerable<Expense>> GetExpenses(DateTime date);

        Task<IEnumerable<Expense>> GetExpenses(int year, int month);

        Task DeleteExpense(int id);

        Task<Expense> GetExpense(int id);

        Task SaveExpense(Expense expense);

        Task<TransactionTotals> GetExpenseTotals(DateTime currentDate);

        Task<IEnumerable<DTO.CategoryTotal>> GetCategoryTotals(DateTime dateFrom, DateTime dateTo);

        Task<IEnumerable<DTO.CategoryTotal>> GetCategoryTotals(DateTime dateFrom, DateTime dateTo, int categoryId);
    }
}
