using MoneyManager.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.WebApi.Controllers
{
    public interface IExpenseController
    {
        Task<TransactionCollection> Get(SearchCriteria criteria);

        Task<Transaction> GetById(int id);

        Task<IEnumerable<Transaction>> GetByDate(DateTime date);

        Task<TransactionTotals> GetTotals(DateTime date);

        Task<IEnumerable<Transaction>> GetByDate(int year, int month);

        Task<IEnumerable<CategoryTotal>> GetCategoryTotals(DateTime dateFrom, DateTime dateTo);

        Task<IEnumerable<CategoryTotal>> GetCategoryTotals(DateTime dateFrom, DateTime dateTo, int categoryId);

        Task Post(Transaction expense);

        Task Delete(int id);
    }
}
