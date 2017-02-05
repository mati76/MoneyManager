using MoneyManager.WebApi.DTO;
using MoneyManager.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MoneyManager.WebApi.Controllers
{
    [RoutePrefix("api/budget")]
    [Authorize]
    public class BudgetController : ApiController, IBudgetController
    {
        private IBudgetService _budgetService;

        public BudgetController(IBudgetService budgetService)
        {
            if (budgetService == null)
            {
                throw new ArgumentNullException(nameof(budgetService));
            }
            _budgetService = budgetService;
        }

        [Route("expense")]
        public IEnumerable<Expense> GetExpense([FromUri]SearchCriteria criteria)
        {
            return _budgetService.GetExpenses(criteria);
        }

        [Route("income")]
        public IEnumerable<Income> GetIncome([FromUri]SearchCriteria criteria)
        {
            return _budgetService.GetIncome(criteria);
        }

        [Route("expense/{id:int}")]
        public Expense GetExpenseById(int id)
        {
            return _budgetService.GetExpense(id);
        }

        [Route("income/{id:int}")]
        public Income GetIncomeById(int id)
        {
            return _budgetService.GetIncome(id);
        }

        [Route("expense")]
        public void Post([FromBody]Expense expense)
        {
            _budgetService.SaveExpense(expense);
        }

        [Route("income")]
        public void Post([FromBody]Income income)
        {
            _budgetService.SaveIncome(income);
        }

        [Route("expense/{id:int}")]
        public void DeleteExpense(int id)
        {
            _budgetService.DeleteExpense(id);
        }

        [Route("income/{id:int}")]
        public void DeleteIncome(int id)
        {
            _budgetService.DeleteIncome(id);
        }

        [Route("totals/{from:datetime}/{to:datetime}")]
        public BudgetTotals GetTotals(DateTime from, DateTime to)
        {
            return _budgetService.GetBudgetTotals(from, to);
        }

        [Route("realization/{from:datetime}/{to:datetime}")]
        public IEnumerable<BudgetRealization> GetRealization(DateTime from, DateTime to)
        {
            return _budgetService.GetBudgetRealization(from, to);
        }
    }
}
