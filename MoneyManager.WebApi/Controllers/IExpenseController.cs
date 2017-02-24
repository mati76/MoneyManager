using MoneyManager.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.WebApi.Controllers
{
    public interface IExpenseController
    {
        Task<IEnumerable<Expense>> Get(SearchCriteria criteria);

        Task<Expense> GetById(int id);

        Task<IEnumerable<Expense>> GetByDate(DateTime date);

        Task<TransactionTotals> GetTotals(DateTime date);

        Task<IEnumerable<Expense>> GetByDate(int year, int month);

        Task<IEnumerable<CategoryTotal>> GetCategoryTotals(DateTime dateFrom, DateTime dateTo);

        Task<IEnumerable<CategoryTotal>> GetCategoryTotals(DateTime dateFrom, DateTime dateTo, int categoryId);

        Task Post(Expense expense);

        Task Delete(int id);
    }
}
