using MoneyManager.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.Business.Interfaces
{
    public interface IIncomeBusiness
    {
        Task<Income> GetIncome(int id);

        Task<IEnumerable<Income>> GetIncome(int year, int month);

        Task<IEnumerable<Income>> GetIncome(DateTime from, DateTime to);

        Task<IEnumerable<Income>> GetIncome(SearchCriteria criteria);

        Task SaveIncome(Income income);

        Task DeleteIncome(int id);

        Task<TransactionTotals> GetIncomeTotals(DateTime currentDate);

        Task<IEnumerable<CategoryTotal>> GetCategoryTotals(DateTime dateFrom, DateTime dateTo);
    }
}
