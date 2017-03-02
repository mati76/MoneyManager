using MoneyManager.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.WebApi.Services
{
    public interface IExpenseService
    {
        Task<TransactionCollection> GetExpenses(SearchCriteria criteria);

        Task<IEnumerable<Transaction>> GetExpenses(DateTime date);

        Task<IEnumerable<Transaction>> GetExpenses(int year, int month);

        Task DeleteExpense(int id);

        Task<Transaction> GetExpense(int id);

        Task SaveExpense(Transaction expense);

        Task<TransactionTotals> GetExpenseTotals(DateTime currentDate);

        Task<IEnumerable<DTO.CategoryTotal>> GetCategoryTotals(DateTime dateFrom, DateTime dateTo);

        Task<IEnumerable<DTO.CategoryTotal>> GetCategoryTotals(DateTime dateFrom, DateTime dateTo, int categoryId);
    }
}
