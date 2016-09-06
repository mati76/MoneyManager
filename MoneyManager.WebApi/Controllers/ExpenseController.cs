using MoneyManager.WebApi.Services;
using MoneyManager.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MoneyManager.WebApi.Controllers
{
    [RoutePrefix("api/expense")]
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

        public IEnumerable<Expense> Get([FromUri]ExpenseCriteria criteria)
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
        public ExpenseTotals GetTotals(DateTime date)
        {
            return _expenseService.GetExpenseTotals(date);
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
        
        public void Delete(int id)
        {
        }
    }
}
