using MoneyManager.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.WebApi.Controllers
{
    public interface IIncomeController
    {
        Task<IEnumerable<Income>> Get(SearchCriteria criteria);

        Task<Income> GetById(int id);

        Task<TransactionTotals> GetTotals(DateTime date);

        Task<IEnumerable<CategoryTotal>> GetCategoryTotals(DateTime dateFrom, DateTime dateTo);

        Task<IEnumerable<Income>> GetByDate(int year, int month);

        Task Post(Income income);

        Task Delete(int id);
    }
}
