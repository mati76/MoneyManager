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
        Task<IEnumerable<Expense>> GetExpense([FromUri] SearchCriteria criteria);
        Task<Expense> GetExpenseById(int id);
        Task<IEnumerable<Income>> GetIncome([FromUri] SearchCriteria criteria);
        Task<Income> GetIncomeById(int id);
        Task Post([FromBody] Income income);
        Task Post([FromBody] Expense expense);
        Task<IEnumerable<BudgetRealization>> GetRealization(DateTime from, DateTime to);
    }
}