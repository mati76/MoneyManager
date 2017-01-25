using MoneyManager.WebApi.Services;
using MoneyManager.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MoneyManager.WebApi.Controllers
{
    [RoutePrefix("api/income")]
    [Authorize]
    public class IncomeController : ApiController, IIncomeController
    {
        private IIncomeService _incomeService;

        public IncomeController(IIncomeService incomeService)
        {
            if (incomeService == null)
            {
                throw new ArgumentNullException(nameof(incomeService));
            }
            _incomeService = incomeService;
        }

        public IEnumerable<Income> Get([FromUri]SearchCriteria criteria)
        {
            return _incomeService.GetIncome(criteria);
        }

        [Route("{id:int}")]
        public Income GetById(int id)
        {
            return _incomeService.GetIncome(id);
        }

        [Route("totals/{date:datetime}")]
        public TransactionTotals GetTotals(DateTime date)
        {
            return _incomeService.GetIncomeTotals(date);
        }

        [Route("{dateFrom:datetime}/{dateTo:datetime}/category")]
        public IEnumerable<CategoryTotal> GetCategoryTotals(DateTime dateFrom, DateTime dateTo)
        {
            return _incomeService.GetCategoryTotals(dateFrom, dateTo);
        }

        [Route("{year:int:min(1900)}/{month:int:range(1,12)}")]
        public IEnumerable<Income> GetByDate(int year, int month)
        {
            return _incomeService.GetIncome(year, month);
        }

        public void Post([FromBody]Income income)
        {
            _incomeService.SaveIncome(income);
        }

        [Route("{id:int}")]
        public void Delete(int id)
        {
            _incomeService.DeleteIncome(id);
        }
    }
}
