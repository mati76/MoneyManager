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

        public async Task<IEnumerable<DTO.Income>> GetIncome(DateTime from, DateTime to)
        {
            return _mapperService.Map<IEnumerable<DTO.Income>>(await _incomeBusiness.GetIncome(from, to));
        }

        public async Task<IEnumerable<DTO.Income>> GetIncome(int year, int month)
        {
            return _mapperService.Map<IEnumerable<DTO.Income>>(await _incomeBusiness.GetIncome(year, month));
        }

        public async Task<IEnumerable<DTO.Income>> GetIncome(DTO.SearchCriteria criteria)
        {
            return _mapperService.Map<IEnumerable<DTO.Income>>(await _incomeBusiness.GetIncome(_mapperService.Map<SearchCriteria>(criteria)));
        }

        public async Task<DTO.Income> GetIncome(int id)
        {
            return _mapperService.Map<DTO.Income>(await _incomeBusiness.GetIncome(id));
        }

        public Task DeleteIncome(int id)
        {
            return _incomeBusiness.DeleteIncome(id);
        }

        public Task SaveIncome(DTO.Income income)
        {
            return _incomeBusiness.SaveIncome(_mapperService.Map<Income>(income));
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