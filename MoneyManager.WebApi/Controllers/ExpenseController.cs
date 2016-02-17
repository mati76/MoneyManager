using MoneyManager.WebApi.Services;
using MoneyManager.WebApi.ViewModels;
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

        public IEnumerable<ExpenseViewModel> Get([FromUri]ExpenseCriteriaViewModel criteria)
        {
            return _expenseService.GetExpenses(criteria);
        }

        [Route("{id:int}")]
        public ExpenseViewModel GetById(int id)
        {
            return _expenseService.GetExpense(id);
        }

        [Route("{date:datetime}")]
        public IEnumerable<ExpenseViewModel> GetByDate(DateTime date)
        {
            return _expenseService.GetExpenses(date);
        }

        [Route("{year:int:min(1900)}/{month:int:range(1,12)}")]
        public IEnumerable<ExpenseViewModel> GetByDate(int year, int month)
        {
            return _expenseService.GetExpenses(year, month);
        }

        public void Post([FromBody]ExpenseViewModel expense)
        {
        }
        
        public void Delete(int id)
        {
        }
    }
}
