﻿using AutoMapper;
using MoneyManager.Business;
using MoneyManager.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.Api.Services
{
    public class BudgetService : BaseService, IBudgetService
    {
        private readonly IBudgetBusiness _budgetBusiness;

        public BudgetService(IBudgetBusiness budgetBusiness, IMapper mapperService) :  base(mapperService)
        {
            if (budgetBusiness == null)
            {
                throw new ArgumentNullException(nameof(budgetBusiness));
            }
            _budgetBusiness = budgetBusiness;
        }

        public async Task<IEnumerable<DTO.Transaction>> GetExpenses(DTO.SearchCriteria criteria)
        {
            return _mapperService.Map<IEnumerable<DTO.Transaction>>(await _budgetBusiness.GetExpenses(_mapperService.Map<Business.Models.SearchCriteria>(criteria)));
        }

        public async Task<IEnumerable<DTO.Transaction>> GetExpenses(int year, int month)
        {
            return _mapperService.Map<IEnumerable<DTO.Transaction>>(await _budgetBusiness.GetExpenses(year, month));
        }

        public async Task<DTO.Transaction> GetExpense(int id)
        {
            return _mapperService.Map<DTO.Transaction>(await _budgetBusiness.GetExpense(id));
        }

        public Task DeleteExpense(int id)
        {
            return _budgetBusiness.DeleteExpense(id);
        }

        public Task SaveExpense(DTO.Transaction expense)
        {
            return _budgetBusiness.SaveExpense(_mapperService.Map<Business.Models.Transaction>(expense));
        }

        public async Task<IEnumerable<DTO.Transaction>> GetIncome(int year, int month)
        {
            return _mapperService.Map<IEnumerable<DTO.Transaction>>(await _budgetBusiness.GetIncome(year, month));
        }

        public async Task<IEnumerable<DTO.Transaction>> GetIncome(DTO.SearchCriteria criteria)
        {
            return _mapperService.Map<IEnumerable<DTO.Transaction>>(await _budgetBusiness.GetIncome(_mapperService.Map<Business.Models.SearchCriteria>(criteria)));
        }

        public async Task<DTO.Transaction> GetIncome(int id)
        {
            return _mapperService.Map<DTO.Transaction>(await _budgetBusiness.GetIncome(id));
        }

        public Task DeleteIncome(int id)
        {
            return _budgetBusiness.DeleteIncome(id);
        }

        public Task SaveIncome(DTO.Transaction income)
        {
            return _budgetBusiness.SaveIncome(_mapperService.Map<Business.Models.Transaction>(income));
        }

        public async Task<DTO.BudgetTotals> GetBudgetTotals(DateTime dateFrom, DateTime dateTo)
        {
            var tasks = new Task<decimal>[]
            {
                _budgetBusiness.GetBudgetLimit(dateFrom, dateTo),
                _budgetBusiness.GetAvgExpenseDeviation(),
                _budgetBusiness.GetBudgetBalance(dateFrom, dateTo),
                _budgetBusiness.GetBudgetDeviation(dateFrom, dateTo)
            };

            var results = await Task.WhenAll(tasks);

            return new DTO.BudgetTotals
            {
                BudgetLimit = results[0],
                AvgDeviation = results[1],
                BudgetBalance = results[2],
                Deviation = results[3]
            };
        }

        public async Task<IEnumerable<DTO.BudgetRealization>> GetBudgetRealization(DateTime dateFrom, DateTime dateTo)
        {
            var result = new List<DTO.BudgetRealization>();
            foreach (var categoryBalance in await _budgetBusiness.GetCategoryBalance(dateFrom, dateTo))
            {
                result.Add(new DTO.BudgetRealization
                {
                    CategoryName = categoryBalance.CategoryName,
                    Expense = categoryBalance.Expense,
                    Left = categoryBalance.Balance.HasValue ? (categoryBalance.Balance.Value > 0 ? categoryBalance.Balance.Value : 0) : 0,
                    Over = categoryBalance.Balance.HasValue ? (categoryBalance.Balance.Value < 0 ? Math.Abs(categoryBalance.Balance.Value) : 0) : 0
                });
            }
            return result;
        }
    }
}