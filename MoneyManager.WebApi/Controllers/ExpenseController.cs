using MoneyManager.WebApi.Services;
using MoneyManager.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MoneyManager.WebApi.Controllers
{
    [RoutePrefix("api/expense")]
    //[Authorize]
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

        public async Task<TransactionCollection> Get([FromUri]SearchCriteria criteria)
        {
            return await _expenseService.GetExpenses(criteria);
        }

        [Route("{id:int}")]
        public Task<Transaction> GetById(int id)
        {
            return _expenseService.GetExpense(id);
        }

        [Route("{date:datetime}")]
        public Task<IEnumerable<Transaction>> GetByDate(DateTime date)
        {
            return _expenseService.GetExpenses(date);
        }

        [Route("totals/{date:datetime}")]
        public Task<TransactionTotals> GetTotals(DateTime date)
        {
            return _expenseService.GetExpenseTotals(date);
        }

        [Route("{dateFrom:datetime}/{dateTo:datetime}/category")]
        public Task<IEnumerable<CategoryTotal>> GetCategoryTotals(DateTime dateFrom, DateTime dateTo)
        {
            return _expenseService.GetCategoryTotals(dateFrom, dateTo);
        }

        [Route("{dateFrom:datetime}/{dateTo:datetime}/category/{categoryId:int}")]
        public Task<IEnumerable<CategoryTotal>> GetCategoryTotals(DateTime dateFrom, DateTime dateTo, int categoryId)
        {
            return _expenseService.GetCategoryTotals(dateFrom, dateTo, categoryId);
        }

        [Route("{year:int:min(1900)}/{month:int:range(1,12)}")]
        public async Task<IEnumerable<Transaction>> GetByDate(int year, int month)
        {
            return await _expenseService.GetExpenses(year, month);
        }

        public Task Post([FromBody]Transaction expense)
        {
            return _expenseService.SaveExpense(expense);
        }

        [Route("{id:int}")]
        public Task Delete(int id)
        {
            return _expenseService.DeleteExpense(id);
        }
    }
}
