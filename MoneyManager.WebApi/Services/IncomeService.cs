using MoneyManager.Business.Interfaces;
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

        public DTO.Income GetIncome(int id)
        {
            return _mapperService.Map<DTO.Income>(_incomeBusiness.GetIncome(id));
        }
    }
}