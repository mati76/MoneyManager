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
        public IncomeViewModel GetById(int id)
        {
            return _IncomeService.GetIncome(id);
        }

        [Route("{from:datetime}/{to:datetime}")]
        public IEnumerable<IncomeViewModel> GetByDateRange(DateTime from, DateTime to)
        {
            return _IncomeService.GetIncome(from, to);
        }

        [Route("{year:int}/{month:int}")]
        public IEnumerable<IncomeViewModel> GetByMonth(int year, int month)
        {
            return _IncomeService.GetIncome(year, month);
        }

        public void Post([FromBody]IncomeViewModel income)
        {
        }
        
        public void Delete(int id)
        {
        }
    }
}
