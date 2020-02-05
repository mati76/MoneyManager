using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.Api.DTO;
using MoneyManager.Api.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.Api.Controllers
{
    [Route("api/budget")]
    [ApiController]
    [Authorize]
    public class BudgetController : ControllerBase, IBudgetController
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
        public Task<IEnumerable<Transaction>> GetExpense([FromQuery]SearchCriteria criteria)
        {
            return _budgetService.GetExpenses(criteria);
        }

        [Route("income")]
        public Task<IEnumerable<Transaction>> GetIncome([FromQuery]SearchCriteria criteria)
        {
            return _budgetService.GetIncome(criteria);
        }

        [Route("expense/{id:int}")]
        public Task<Transaction> GetExpenseById(int id)
        {
            return _budgetService.GetExpense(id);
        }

        [Route("income/{id:int}")]
        public Task<Transaction> GetIncomeById(int id)
        {
            return _budgetService.GetIncome(id);
        }

        [Route("expense")]
        public Task PostExpense(Transaction expense)
        {
            return _budgetService.SaveExpense(expense);
        }

        [Route("income")]
        public Task PostIncome(Transaction income)
        {
            return _budgetService.SaveIncome(income);
        }

        [Route("expense/{id:int}")]
        public Task DeleteExpense(int id)
        {
            return _budgetService.DeleteExpense(id);
        }

        [Route("income/{id:int}")]
        public Task DeleteIncome(int id)
        {
            return _budgetService.DeleteIncome(id);
        }

        [Route("totals/{from:datetime}/{to:datetime}")]
        public async Task<IActionResult> GetTotals(DateTime from, DateTime to)
        {
            return Ok(await _budgetService.GetBudgetTotals(from, to));
        }

        [Route("realization/{from:datetime}/{to:datetime}")]
        public Task<IEnumerable<BudgetRealization>> GetRealization(DateTime from, DateTime to)
        {
            return _budgetService.GetBudgetRealization(from, to);
        }
    }
}
