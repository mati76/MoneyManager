﻿using System.Collections.Generic;
using MoneyManager.WebApi.DTO;
using System;

namespace MoneyManager.WebApi.Services
{
    public interface IBudgetService
    {
        void DeleteExpense(int id);
        void DeleteIncome(int id);
        Expense GetExpense(int id);
        IEnumerable<Expense> GetExpenses(SearchCriteria criteria);
        IEnumerable<Expense> GetExpenses(int year, int month);
        IEnumerable<Income> GetIncome(SearchCriteria criteria);
        Income GetIncome(int id);
        IEnumerable<Income> GetIncome(int year, int month);
        void SaveExpense(Expense expense);
        void SaveIncome(Income income);
        BudgetTotals GetBudgetTotals(DateTime dateFrom, DateTime dateTo);
        IEnumerable<BudgetRealization> GetBudgetRealization(DateTime dateFrom, DateTime dateTo);
    }
}