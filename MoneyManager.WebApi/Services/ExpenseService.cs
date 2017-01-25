using MoneyManager.Business.Interfaces;
using MoneyManager.Business.Models;
using System;
using System.Collections.Generic;

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

        public IEnumerable<DTO.Expense> GetExpenses(DTO.SearchCriteria criteria)
        {
            return _mapperService.Map<IEnumerable<DTO.Expense>>(_expenseBusiness.GetExpenses(_mapperService.Map<SearchCriteria>(criteria)));
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

        public DTO.TransactionTotals GetExpenseTotals(DateTime currentDate)
        {
            return _mapperService.Map<DTO.TransactionTotals>(_expenseBusiness.GetExpenseTotals(currentDate));
        }

        public IEnumerable<DTO.CategoryTotal> GetCategoryTotals(DateTime dateFrom, DateTime dateTo)
        {
            return GetCategoryTotals(dateFrom, dateTo, 0);
        }

        public IEnumerable<DTO.CategoryTotal> GetCategoryTotals(DateTime dateFrom, DateTime dateTo, int categoryId)
        {
            return _mapperService.Map<IEnumerable<DTO.CategoryTotal>>(_expenseBusiness.GetCategoryTotals(dateFrom, dateTo, categoryId));
        }
    }
}