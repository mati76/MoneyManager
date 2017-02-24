using System;
using System.Collections.Generic;
using MoneyManager.Business.Models;
using System.Threading.Tasks;

namespace MoneyManager.Business.Repository
{
    public interface IBudgetIncomeRepository : IRepository<Income>
    {
        Task<IEnumerable<Income>> GetIncome(int year, int month);

        Task<IEnumerable<Income>> GetIncome(int year);

        Task<IEnumerable<Income>> GetIncomeByCriteria(SearchCriteria criteria);
    }
}
