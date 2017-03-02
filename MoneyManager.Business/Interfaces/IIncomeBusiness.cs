using MoneyManager.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.Business.Interfaces
{
    public interface IIncomeBusiness
    {
        Task<Transaction> GetIncome(int id);

        Task<IEnumerable<Transaction>> GetIncome(int year, int month);

        Task<IEnumerable<Transaction>> GetIncome(DateTime from, DateTime to);

        Task<IEnumerable<Transaction>> GetIncome(SearchCriteria criteria);

        Task SaveIncome(Transaction income);

        Task DeleteIncome(int id);

        Task<TransactionTotals> GetIncomeTotals(DateTime currentDate);

        Task<IEnumerable<CategoryTotal>> GetCategoryTotals(DateTime dateFrom, DateTime dateTo);
    }
}
