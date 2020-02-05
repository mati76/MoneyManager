using MoneyManager.Api.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.Api.Services
{
    public interface IIncomeService
    {
        Task<IEnumerable<Transaction>> GetIncome(DateTime from, DateTime to);

        Task<IEnumerable<Transaction>> GetIncome(int year, int month);

        Task DeleteIncome(int id);

        Task<Transaction> GetIncome(int id);

        Task SaveIncome(Transaction income);

        Task<IEnumerable<Transaction>> GetIncome(SearchCriteria criteria);

        Task<TransactionTotals> GetIncomeTotals(DateTime currentDate);

        Task<IEnumerable<CategoryTotal>> GetCategoryTotals(DateTime dateFrom, DateTime dateTo);
    }
}
