﻿using MoneyManager.WebApi.DTO;
using MoneyManager.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public Task<IEnumerable<Expense>> GetExpense([FromUri]SearchCriteria criteria)
        {
            return _budgetService.GetExpenses(criteria);
        }

        [Route("income")]
        public Task<IEnumerable<Income>> GetIncome([FromUri]SearchCriteria criteria)
        {
            return _budgetService.GetIncome(criteria);
        }

        [Route("expense/{id:int}")]
        public Task<Expense> GetExpenseById(int id)
        {
            return _budgetService.GetExpense(id);
        }

        [Route("income/{id:int}")]
        public Task<Income> GetIncomeById(int id)
        {
            return _budgetService.GetIncome(id);
        }

        [Route("expense")]
        public Task Post([FromBody]Expense expense)
        {
            return _budgetService.SaveExpense(expense);
        }

        [Route("income")]
        public Task Post([FromBody]Income income)
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
        public Task<BudgetTotals> GetTotals(DateTime from, DateTime to)
        {
            return _budgetService.GetBudgetTotals(from, to);
        }

        [Route("realization/{from:datetime}/{to:datetime}")]
        public Task<IEnumerable<BudgetRealization>> GetRealization(DateTime from, DateTime to)
        {
            return _budgetService.GetBudgetRealization(from, to);
        }
    }
}
