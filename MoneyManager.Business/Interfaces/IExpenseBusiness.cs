using MoneyManager.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.Business.Interfaces
{
    public interface IExpenseBusiness
    {
        Task<Expense> GetExpense(int id);

        Task<IEnumerable<Expense>> GetExpenses(int year, int month);

        Task<IEnumerable<Expense>> GetExpenses(DateTime date);

        Task<IEnumerable<Expense>> GetExpenses(SearchCriteria criteria);

        Task SaveExpense(Expense expense);

        Task DeleteExpense(int id);

        Task<TransactionTotals> GetExpenseTotals(DateTime currentDate);

        Task<IEnumerable<CategoryTotal>> GetCategoryTotals(DateTime dateFrom, DateTime dateTo, int categoryId);
    }
}
