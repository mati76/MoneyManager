using System.Collections.Generic;
using System.Web.Http;
using MoneyManager.WebApi.DTO;
using System;
using System.Threading.Tasks;

namespace MoneyManager.WebApi.Controllers
{
    public interface IBudgetController
    {
        Task DeleteExpense(int id);
        Task DeleteIncome(int id);
        Task<IEnumerable<Transaction>> GetExpense([FromUri] SearchCriteria criteria);
        Task<Transaction> GetExpenseById(int id);
        Task<IEnumerable<Transaction>> GetIncome([FromUri] SearchCriteria criteria);
        Task<Transaction> GetIncomeById(int id);
        Task PostIncome([FromBody] Transaction income);
        Task PostExpense([FromBody] Transaction expense);
        Task<IEnumerable<BudgetRealization>> GetRealization(DateTime from, DateTime to);
    }
}