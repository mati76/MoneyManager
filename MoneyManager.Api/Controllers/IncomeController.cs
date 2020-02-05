using MoneyManager.Api.Services;
using MoneyManager.Api.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MoneyManager.Api.Controllers
{
    [Route("api/income")]
    [Authorize]
    [ApiController]
    public class IncomeController : ControllerBase, IIncomeController
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

        public Task<IEnumerable<Transaction>> Get([FromQuery]SearchCriteria criteria)
        {
            return _incomeService.GetIncome(criteria);
        }

        [Route("{id:int}")]
        public Task<Transaction> GetById(int id)
        {
            return _incomeService.GetIncome(id);
        }

        [Route("totals/{date:datetime}")]
        public Task<TransactionTotals> GetTotals(DateTime date)
        {
            return _incomeService.GetIncomeTotals(date);
        }

        [Route("{dateFrom:datetime}/{dateTo:datetime}/category")]
        public Task<IEnumerable<CategoryTotal>> GetCategoryTotals(DateTime dateFrom, DateTime dateTo)
        {
            return _incomeService.GetCategoryTotals(dateFrom, dateTo);
        }

        [Route("{year:int:min(1900)}/{month:int:range(1,12)}")]
        public Task<IEnumerable<Transaction>> GetByDate(int year, int month)
        {
            return _incomeService.GetIncome(year, month);
        }

        public Task Post(Transaction income)
        {
            return _incomeService.SaveIncome(income);
        }

        [Route("{id:int}")]
        public Task Delete(int id)
        {
            return _incomeService.DeleteIncome(id);
        }
    }
}
