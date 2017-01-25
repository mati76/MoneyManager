using MoneyManager.WebApi.Services;
using MoneyManager.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MoneyManager.WebApi.Controllers
{
    [RoutePrefix("api/expense")]
    [Authorize]
    public class ExpenseController : ApiController, IExpenseController
    {
        private IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            if(expenseService == null)
            {
                throw new ArgumentNullException(nameof(expenseService));
            }
            _expenseService = expenseService;
        }

        public IEnumerable<Expense> Get([FromUri]SearchCriteria criteria)
        {
            return _expenseService.GetExpenses(criteria);
        }

        [Route("{id:int}")]
        public Expense GetById(int id)
        {
            return _expenseService.GetExpense(id);
        }

        [Route("{date:datetime}")]
        public IEnumerable<Expense> GetByDate(DateTime date)
        {
            return _expenseService.GetExpenses(date);
        }

        [Route("totals/{date:datetime}")]
        public TransactionTotals GetTotals(DateTime date)
        {
            return _expenseService.GetExpenseTotals(date);
        }

        [Route("{dateFrom:datetime}/{dateTo:datetime}/category")]
        public IEnumerable<CategoryTotal> GetCategoryTotals(DateTime dateFrom, DateTime dateTo)
        {
            return _expenseService.GetCategoryTotals(dateFrom, dateTo);
        }

        [Route("{dateFrom:datetime}/{dateTo:datetime}/category/{categoryId:int}")]
        public IEnumerable<CategoryTotal> GetCategoryTotals(DateTime dateFrom, DateTime dateTo, int categoryId)
        {
            return _expenseService.GetCategoryTotals(dateFrom, dateTo, categoryId);
        }

        [Route("{year:int:min(1900)}/{month:int:range(1,12)}")]
        public IEnumerable<Expense> GetByDate(int year, int month)
        {
            return _expenseService.GetExpenses(year, month);
        }

        public void Post([FromBody]Expense expense)
        {
            _expenseService.SaveExpense(expense);
        }

        [Route("{id:int}")]
        public void Delete(int id)
        {
            _expenseService.DeleteExpense(id);
        }
    }
}
