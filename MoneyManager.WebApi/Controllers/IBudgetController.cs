using System.Collections.Generic;
using System.Web.Http;
using MoneyManager.WebApi.DTO;

namespace MoneyManager.WebApi.Controllers
{
    public interface IBudgetController
    {
        void DeleteExpense(int id);
        void DeleteIncome(int id);
        IEnumerable<Expense> GetExpense([FromUri] SearchCriteria criteria);
        Expense GetExpenseById(int id);
        IEnumerable<Income> GetIncome([FromUri] SearchCriteria criteria);
        Income GetIncomeById(int id);
        void Post([FromBody] Income income);
        void Post([FromBody] Expense expense);
    }
}