using MoneyManager.Business.Interfaces;
using MoneyManager.Business.Models;
using System;
using System.Collections.Generic;
using DTO = MoneyManager.WebApi.DTO;

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

        public IEnumerable<DTO.Expense> GetExpenses(DTO.ExpenseCriteria criteria)
        {
            return _mapperService.Map<IEnumerable<DTO.Expense>>(_expenseBusiness.GetExpenses(_mapperService.Map<ExpenseCriteria>(criteria)));
        }

        public IEnumerable<DTO.Expense> GetExpenses(DateTime date)
        {
            return _mapperService.Map<IEnumerable<DTO.Expense>>(_expenseBusiness.GetExpenses(date));
        }

        public IEnumerable<DTO.Expense> GetExpenses(int year, int month)
        {
            return _mapperService.Map<IEnumerable<DTO.Expense>>(_expenseBusiness.GetExpenses(year, month));
        }

        public DTO.Expense GetExpense(int id)
        {
            return _mapperService.Map<DTO.Expense>(_expenseBusiness.GetExpense(id));
        }

        public void DeleteExpense(int id)
        {
            _expenseBusiness.DeleteExpense(id);
        }

        public void SaveExpense(DTO.Expense expense)
        {
            _expenseBusiness.SaveExpense(_mapperService.Map<Expense>(expense));
        }

        public DTO.ExpenseTotals GetExpenseTotals(DateTime currentDate)
        {
            return _mapperService.Map<DTO.ExpenseTotals>(_expenseBusiness.GetExpenseTotals(currentDate));
        }
    }
}