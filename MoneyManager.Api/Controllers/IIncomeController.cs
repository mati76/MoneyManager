using MoneyManager.Api.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.Api.Controllers
{
    public interface IIncomeController
    {
        Task<IEnumerable<Transaction>> Get(SearchCriteria criteria);

        Task<Transaction> GetById(int id);

        Task<TransactionTotals> GetTotals(DateTime date);

        Task<IEnumerable<CategoryTotal>> GetCategoryTotals(DateTime dateFrom, DateTime dateTo);

        Task<IEnumerable<Transaction>> GetByDate(int year, int month);

        Task Post(Transaction income);

        Task Delete(int id);
    }
}
