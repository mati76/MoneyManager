using MoneyManager.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.WebApi.Services
{
    public interface IIncomeService
    {
        Task<IEnumerable<Income>> GetIncome(DateTime from, DateTime to);

        Task<IEnumerable<Income>> GetIncome(int year, int month);

        Task DeleteIncome(int id);

        Task<Income> GetIncome(int id);

        Task SaveIncome(Income income);

        Task<IEnumerable<Income>> GetIncome(SearchCriteria criteria);

        Task<TransactionTotals> GetIncomeTotals(DateTime currentDate);

        Task<IEnumerable<CategoryTotal>> GetCategoryTotals(DateTime dateFrom, DateTime dateTo);
    }
}
