using MoneyManager.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.Business.Interfaces
{
    public interface IExpenseBusiness
    {
        Task<Transaction> GetExpense(int id);

        Task<IEnumerable<Transaction>> GetExpenses(int year, int month);

        Task<IEnumerable<Transaction>> GetExpenses(DateTime date);

        Task<TransactionCollection> GetExpenses(SearchCriteria criteria);

        Task SaveExpense(Transaction expense);

        Task DeleteExpense(int id);

        Task<TransactionTotals> GetExpenseTotals(DateTime currentDate);

        Task<IEnumerable<CategoryTotal>> GetCategoryTotals(DateTime dateFrom, DateTime dateTo, int categoryId);
    }
}
