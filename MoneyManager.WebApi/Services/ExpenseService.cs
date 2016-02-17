using MoneyManager.Business.Interfaces;
using MoneyManager.Business.Models;
using MoneyManager.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyManager.WebApi.Services
{
    public class ExpenseService : BaseService, IExpenseService
    {
        private readonly IExpenseBusiness _expenseBusiness;

        public ExpenseService(IExpenseBusiness expenseBusiness, MapperService mapperService) :  base(mapperService)
        {
            if(expenseBusiness == null)
            {
                throw new ArgumentNullException(nameof(expenseBusiness));
            }
            _expenseBusiness = expenseBusiness;
        }

        public IEnumerable<ExpenseViewModel> GetExpenses(ExpenseCriteriaViewModel criteria)
        {
            return _mapperService.Map<IEnumerable<ExpenseViewModel>>(_expenseBusiness.GetExpenses(_mapperService.Map<ExpenseCriteria>(criteria)));
        }

        public IEnumerable<ExpenseViewModel> GetExpenses(DateTime date)
        {
            return _mapperService.Map<IEnumerable<ExpenseViewModel>>(_expenseBusiness.GetExpenses(date));
        }

        public IEnumerable<ExpenseViewModel> GetExpenses(int year, int month)
        {
            return _mapperService.Map<IEnumerable<ExpenseViewModel>>(_expenseBusiness.GetExpenses(year, month));
        }

        public ExpenseViewModel GetExpense(int id)
        {
            return _mapperService.Map<ExpenseViewModel>(_expenseBusiness.GetExpense(id));
        }
    }
}