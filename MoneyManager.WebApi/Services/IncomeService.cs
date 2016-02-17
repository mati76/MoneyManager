using MoneyManager.Business.Interfaces;
using MoneyManager.Business.Models;
using MoneyManager.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        public IEnumerable<IncomeViewModel> GetIncome(DateTime from, DateTime to)
        {
            return _mapperService.Map<IEnumerable<IncomeViewModel>>(_incomeBusiness.GetIncome(from, to));
        }

        public IEnumerable<IncomeViewModel> GetIncome(int year, int month)
        {
            return _mapperService.Map<IEnumerable<IncomeViewModel>>(_incomeBusiness.GetIncome(year, month));
        }

        public IncomeViewModel GetIncome(int id)
        {
            return _mapperService.Map<IncomeViewModel>(_incomeBusiness.GetIncome(id));
        }
    }
}