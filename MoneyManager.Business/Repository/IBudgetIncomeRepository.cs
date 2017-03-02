using System;
using System.Collections.Generic;
using MoneyManager.Business.Models;
using System.Threading.Tasks;

namespace MoneyManager.Business.Repository
{
    public interface IBudgetIncomeRepository : IRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> GetIncome(int year, int month);

        Task<IEnumerable<Transaction>> GetIncome(int year);

        Task<IEnumerable<Transaction>> GetIncomeByCriteria(SearchCriteria criteria);
    }
}
