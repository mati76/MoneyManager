using MoneyManager.Business.Interfaces;
using MoneyManager.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.WebApi.Services
{
    public class IncomeService : BaseService, IIncomeService
    {
        private readonly IIncomeBusiness _incomeBusiness;

        public IncomeService(IIncomeBusiness incomeBusiness, MapperService mapperService) :  base(mapperService)
        {
            if(incomeBusiness == null)
            {
                throw new ArgumentNullException(nameof(incomeBusiness));
            }
            _incomeBusiness = incomeBusiness;
        }

        public async Task<IEnumerable<DTO.Transaction>> GetIncome(DateTime from, DateTime to)
        {
            return _mapperService.Map<IEnumerable<DTO.Transaction>>(await _incomeBusiness.GetIncome(from, to));
        }

        public async Task<IEnumerable<DTO.Transaction>> GetIncome(int year, int month)
        {
            return _mapperService.Map<IEnumerable<DTO.Transaction>>(await _incomeBusiness.GetIncome(year, month));
        }

        public async Task<IEnumerable<DTO.Transaction>> GetIncome(DTO.SearchCriteria criteria)
        {
            return _mapperService.Map<IEnumerable<DTO.Transaction>>(await _incomeBusiness.GetIncome(_mapperService.Map<SearchCriteria>(criteria)));
        }

        public async Task<DTO.Transaction> GetIncome(int id)
        {
            return _mapperService.Map<DTO.Transaction>(await _incomeBusiness.GetIncome(id));
        }

        public Task DeleteIncome(int id)
        {
            return _incomeBusiness.DeleteIncome(id);
        }

        public Task SaveIncome(DTO.Transaction income)
        {
            return _incomeBusiness.SaveIncome(_mapperService.Map<Transaction>(income));
        }

        public async Task<DTO.TransactionTotals> GetIncomeTotals(DateTime currentDate)
        {
            return _mapperService.Map<DTO.TransactionTotals>(await _incomeBusiness.GetIncomeTotals(currentDate));
        }

        public async Task<IEnumerable<DTO.CategoryTotal>> GetCategoryTotals(DateTime dateFrom, DateTime dateTo)
        {
            return _mapperService.Map<IEnumerable<DTO.CategoryTotal>>(await _incomeBusiness.GetCategoryTotals(dateFrom, dateTo));
        }
    }
}