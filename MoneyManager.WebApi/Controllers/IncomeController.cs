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
    [RoutePrefix("api/income")]
    public class IncomeController : ApiController, IIncomeController
    {
        private IIncomeService _IncomeService;

        public IncomeController(IIncomeService IncomeService)
        {
            if(IncomeService == null)
            {
                throw new ArgumentNullException(nameof(IncomeService));
            }
            _IncomeService = IncomeService;
        }

        [Route("{id:int}")]
        public Income GetById(int id)
        {
            return _IncomeService.GetIncome(id);
        }

        [Route("{from:datetime}/{to:datetime}")]
        public IEnumerable<Income> GetByDateRange(DateTime from, DateTime to)
        {
            return _IncomeService.GetIncome(from, to);
        }

        [Route("{year:int}/{month:int}")]
        public IEnumerable<Income> GetByMonth(int year, int month)
        {
            return _IncomeService.GetIncome(year, month);
        }

        public void Post([FromBody]Income income)
        {
        }
        
        public void Delete(int id)
        {
        }
    }
}
