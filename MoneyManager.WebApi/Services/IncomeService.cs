using MoneyManager.Business.Interfaces;
using MoneyManager.Business.Models;
using System;
using System.Collections.Generic;
using DTO = MoneyManager.WebApi.DTO;

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

        public IEnumerable<DTO.Income> GetIncome(DateTime from, DateTime to)
        {
            return _mapperService.Map<IEnumerable<DTO.Income>>(_incomeBusiness.GetIncome(from, to));
        }

        public IEnumerable<DTO.Income> GetIncome(int year, int month)
        {
            return _mapperService.Map<IEnumerable<DTO.Income>>(_incomeBusiness.GetIncome(year, month));
        }

        public IEnumerable<DTO.Income> GetIncome(DTO.SearchCriteria criteria)
        {
            return _mapperService.Map<IEnumerable<DTO.Income>>(_incomeBusiness.GetIncome(_mapperService.Map<SearchCriteria>(criteria)));
        }

        public DTO.Income GetIncome(int id)
        {
            return _mapperService.Map<DTO.Income>(_incomeBusiness.GetIncome(id));
        }

        public void DeleteIncome(int id)
        {
            _incomeBusiness.DeleteIncome(id);
        }

        public void SaveIncome(DTO.Income income)
        {
            _incomeBusiness.SaveIncome(_mapperService.Map<Income>(income));
        }

        public DTO.TransactionTotals GetIncomeTotals(DateTime currentDate)
        {
            return _mapperService.Map<DTO.TransactionTotals>(_incomeBusiness.GetIncomeTotals(currentDate));
        }

        public IEnumerable<DTO.CategoryTotal> GetCategoryTotals(DateTime dateFrom, DateTime dateTo)
        {
            return _mapperService.Map<IEnumerable<DTO.CategoryTotal>>(_incomeBusiness.GetCategoryTotals(dateFrom, dateTo));
        }
    }
}