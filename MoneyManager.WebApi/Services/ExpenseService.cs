using MoneyManager.Business.Interfaces;
using MoneyManager.Business.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<DTO.Expense>> GetExpenses(DTO.SearchCriteria criteria)
        {
            return _mapperService.Map<IEnumerable<DTO.Expense>>(await _expenseBusiness.GetExpenses(_mapperService.Map<SearchCriteria>(criteria)));
        }

        public async Task<IEnumerable<DTO.Expense>> GetExpenses(DateTime date)
        {
            return _mapperService.Map<IEnumerable<DTO.Expense>>(await _expenseBusiness.GetExpenses(date));
        }

        public async Task<DTO.Expense> GetExpense(int id)
        {
            return _mapperService.Map<DTO.Expense>(await _expenseBusiness.GetExpense(id));
        }

        public Task DeleteExpense(int id)
        {
            return _expenseBusiness.DeleteExpense(id);
        }

        public Task SaveExpense(DTO.Expense expense)
        {
            return _expenseBusiness.SaveExpense(_mapperService.Map<Expense>(expense));
        }

        public async Task<DTO.TransactionTotals> GetExpenseTotals(DateTime currentDate)
        {
            return _mapperService.Map<DTO.TransactionTotals>(await _expenseBusiness.GetExpenseTotals(currentDate));
        }

        public Task<IEnumerable<DTO.CategoryTotal>> GetCategoryTotals(DateTime dateFrom, DateTime dateTo)
        {
            return GetCategoryTotals(dateFrom, dateTo, 0);
        }

        public async Task<IEnumerable<DTO.CategoryTotal>> GetCategoryTotals(DateTime dateFrom, DateTime dateTo, int categoryId)
        {
            return _mapperService.Map<IEnumerable<DTO.CategoryTotal>>(await _expenseBusiness.GetCategoryTotals(dateFrom, dateTo, categoryId));
        }

        public async Task<IEnumerable<DTO.Expense>> GetExpenses(int year, int month)
        {
            var expenses = await _expenseBusiness.GetExpenses(year, month);
            return _mapperService.Map<List<DTO.Expense>>(expenses);
        }
    }
}