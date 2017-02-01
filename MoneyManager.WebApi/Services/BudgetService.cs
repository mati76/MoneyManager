using MoneyManager.Business;
using MoneyManager.Business.Models;
using System;
using System.Collections.Generic;

namespace MoneyManager.WebApi.Services
{
    public class BudgetService : BaseService, IBudgetService
    {
        private readonly IBudgetBusiness _budgetBusiness;

        public BudgetService(IBudgetBusiness budgetBusiness, MapperService mapperService) :  base(mapperService)
        {
            if (budgetBusiness == null)
            {
                throw new ArgumentNullException(nameof(budgetBusiness));
            }
            _budgetBusiness = budgetBusiness;
        }

        public IEnumerable<DTO.Expense> GetExpenses(DTO.SearchCriteria criteria)
        {
            return _mapperService.Map<IEnumerable<DTO.Expense>>(_budgetBusiness.GetExpenses(_mapperService.Map<SearchCriteria>(criteria)));
        }

        public IEnumerable<DTO.Expense> GetExpenses(int year, int month)
        {
            return _mapperService.Map<IEnumerable<DTO.Expense>>(_budgetBusiness.GetExpenses(year, month));
        }

        public DTO.Expense GetExpense(int id)
        {
            return _mapperService.Map<DTO.Expense>(_budgetBusiness.GetExpense(id));
        }

        public void DeleteExpense(int id)
        {
            _budgetBusiness.DeleteExpense(id);
        }

        public void SaveExpense(DTO.Expense expense)
        {
            _budgetBusiness.SaveExpense(_mapperService.Map<Expense>(expense));
        }

        public IEnumerable<DTO.Income> GetIncome(int year, int month)
        {
            return _mapperService.Map<IEnumerable<DTO.Income>>(_budgetBusiness.GetIncomes(year, month));
        }

        public IEnumerable<DTO.Income> GetIncome(DTO.SearchCriteria criteria)
        {
            return _mapperService.Map<IEnumerable<DTO.Income>>(_budgetBusiness.GetIncomes(_mapperService.Map<SearchCriteria>(criteria)));
        }

        public DTO.Income GetIncome(int id)
        {
            return _mapperService.Map<DTO.Income>(_budgetBusiness.GetIncome(id));
        }

        public void DeleteIncome(int id)
        {
            _budgetBusiness.DeleteIncome(id);
        }

        public void SaveIncome(DTO.Income income)
        {
            _budgetBusiness.SaveIncome(_mapperService.Map<Income>(income));
        }

        public DTO.BudgetTotals GetBudgetTotals(int year, int month)
        {
            return _mapperService.Map<DTO.BudgetTotals>(_budgetBusiness.GetTotals(year, month));
        }
    }
}