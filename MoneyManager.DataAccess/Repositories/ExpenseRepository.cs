﻿using System;
using System.Linq;
using System.Linq.Dynamic;
using MoneyManager.Business.Repository;
using MoneyManager.Business.Models;
using DAC = MoneyManager.DataAccess.Models;
using MoneyManager.DataAccess.Infrastructure;
using System.Collections.Generic;
using System.Data.Entity;
using MoneyManager.Business;

namespace MoneyManager.DataAccess.Repositories
{
    public class ExpenseRepository : BaseRepository<DAC.Expense, Expense>, IExpenseRepository
    {
        public ExpenseRepository(IMapperService mapperService, IDbContext dbContext) : base(mapperService, dbContext) { }

        public IEnumerable<Expense> GetExpenses(DateTime date)
        {
            return _mapperService.Map<IEnumerable<Expense>>(_dbset.Where(o => DbFunctions.TruncateTime(o.Date) == date).Include(o => o.Category));
        }

        public IEnumerable<Expense> GetExpenses(int year, int month)
        {
            return _mapperService.Map<IEnumerable<Expense>>(_dbset.Where(o => o.Date.Year == year && o.Date.Month == month));
        }

        public IEnumerable<Expense> GetExpensesByCriteria(SearchCriteria criteria)
        {
            var qry = _dbset.Where(e => DbFunctions.TruncateTime(e.Date) >= DbFunctions.TruncateTime(criteria.DateFrom)
                && DbFunctions.TruncateTime(e.Date) <= DbFunctions.TruncateTime(criteria.DateTo));

            if (criteria.Categories.Any())
                qry = qry.Where(o => criteria.Categories.Select(c => c.Id).Contains(o.Id));
            if (criteria.MinAmount.HasValue)
                qry = qry.Where(o => o.Amount >= criteria.MinAmount);
            if (criteria.MaxAmount.HasValue)
                qry = qry.Where(o => o.Amount <= criteria.MaxAmount);
            if(!string.IsNullOrEmpty(criteria.SortBy))
                qry = qry.OrderBy(criteria.SortBy + (criteria.SortAsc == true ? " ascending" : " descending"));
            if(criteria.CurrentPage.HasValue)
                qry = qry.Skip(criteria.CurrentPage.Value * criteria.PageSize).Take(criteria.PageSize);

            return _mapperService.Map<IEnumerable<Expense>>(qry.Include(e => e.Category));
        }

        public decimal GetExpenseTotalsFromDates(DateTime from, DateTime to)
        {
            return _dbset.Where(e => e.Date >= from && e.Date <= to).Select(e => e.Amount).DefaultIfEmpty(0).Sum();
        }

        public List<CategoryTotal> GeCategoryTotals(DateTime dateFrom, DateTime dateTo)
        {
            return _dbset.Where(e => DbFunctions.TruncateTime(e.Date) >= DbFunctions.TruncateTime(dateFrom) && DbFunctions.TruncateTime(e.Date) <= DbFunctions.TruncateTime(dateTo)).GroupBy(e => e.Category.Parent,
                (key, g) => new CategoryTotal { CategoryId = key.Id, CategoryName = key.Name, TotalAmount = g.Sum(c => c.Amount) })
                .OrderByDescending(c => c.TotalAmount).ToList();
        }

        public List<CategoryTotal> GeCategoryTotals(DateTime dateFrom, DateTime dateTo, int categoryId)
        {
            return _dbset.Where(e => DbFunctions.TruncateTime(e.Date) >= DbFunctions.TruncateTime(dateFrom) && DbFunctions.TruncateTime(e.Date) <= DbFunctions.TruncateTime(dateTo)
                && e.Category.ParentId == categoryId).GroupBy(e => e.Category, 
                (key, g) => new CategoryTotal { CategoryId = key.Id, CategoryName = key.Name, TotalAmount = g.Sum(c => c.Amount) })
                .OrderByDescending(c => c.TotalAmount).ToList();
        }
    }
}
