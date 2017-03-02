using System;
using System.Linq;
using System.Linq.Dynamic;
using MoneyManager.Business.Repository;
using MoneyManager.Business.Models;
using DAC = MoneyManager.DataAccess.Models;
using MoneyManager.DataAccess.Infrastructure;
using System.Collections.Generic;
using System.Data.Entity;
using MoneyManager.Business;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using System.Diagnostics;

namespace MoneyManager.DataAccess.Repositories
{
    public class ExpenseRepository : BaseRepository<DAC.Expense, Transaction>, IExpenseRepository
    {
        public ExpenseRepository(IMapperService mapperService, IDbContext dbContext) : base(mapperService, dbContext) { }

        public async Task<IEnumerable<Transaction>> GetExpenses(DateTime date)
        {
            return _mapperService.Map<IEnumerable<Transaction>>(await _dbset.Where(o => DbFunctions.TruncateTime(o.Date) == date).Include(o => o.Category).ToListAsync());
        }

        public async Task<IEnumerable<Transaction>> GetExpenses(DateTime dateFrom, DateTime dateTo)
        {
            return _mapperService.Map<IEnumerable<Transaction>>(await _dbset.Where(e => DbFunctions.TruncateTime(e.Date) >= DbFunctions.TruncateTime(dateFrom)
                && DbFunctions.TruncateTime(e.Date) <= DbFunctions.TruncateTime(dateTo)).ToListAsync());
        }

        public async Task<IEnumerable<Transaction>> GetExpenses(int year, int month)
        {
            return _mapperService.Map<IEnumerable<Transaction>>(await _dbset.Where(o => o.Date.Year == year && o.Date.Month == month).ToListAsync());
        }

        public async Task<TransactionCollection> GetExpensesByCriteria(SearchCriteria criteria)
        {
            var result = new TransactionCollection();

            var qry = _dbset.Where(e => DbFunctions.TruncateTime(e.Date) >= DbFunctions.TruncateTime(criteria.DateFrom)
                && DbFunctions.TruncateTime(e.Date) <= DbFunctions.TruncateTime(criteria.DateTo));
            if (criteria.MinAmount.HasValue)
                qry = qry.Where(o => o.Amount >= criteria.MinAmount);
            if (criteria.MaxAmount.HasValue)
                qry = qry.Where(o => o.Amount <= criteria.MaxAmount);

            result.Categories = _mapperService.Map<IEnumerable<Category>>(await qry.Select(o => o.Category).Distinct().ToListAsync());

            if (criteria.Categories.Any())
                qry = qry.Where(o => criteria.Categories.Select(c => c.Id).Contains(o.Id));
            if (!string.IsNullOrEmpty(criteria.SortBy))
                qry = qry.OrderBy(criteria.SortBy + (criteria.SortAsc == true ? " ascending" : " descending"));
            if (criteria.Skip.HasValue && criteria.Take.HasValue)
                qry = qry.Skip(criteria.Skip.Value).Take(criteria.Take.Value);

            result.Transactions = _mapperService.Map<IEnumerable<Transaction>>(await qry.Include(e => e.Category).ToListAsync());
            return result;
        }

        public Task<decimal> GetExpenseTotalsFromDates(DateTime from, DateTime to)
        {
            return _dbset.Where(e => e.Date >= from && e.Date <= to).Select(e => e.Amount).DefaultIfEmpty(0).SumAsync();
        }

        public Task<List<CategoryTotal>> GetCategoryTotals(DateTime dateFrom, DateTime dateTo)
        {
            return _dbset.Where(e => DbFunctions.TruncateTime(e.Date) >= DbFunctions.TruncateTime(dateFrom) && DbFunctions.TruncateTime(e.Date) <= DbFunctions.TruncateTime(dateTo)).GroupBy(e => e.Category.Parent,
                (key, g) => new CategoryTotal { CategoryId = key.Id, CategoryName = key.Name, TotalAmount = g.Sum(c => c.Amount) })
                .OrderByDescending(c => c.TotalAmount).ToListAsync();
        }

        public Task<List<CategoryTotal>> GetCategoryTotals(DateTime dateFrom, DateTime dateTo, int categoryId)
        {
            return _dbset.Where(e => DbFunctions.TruncateTime(e.Date) >= DbFunctions.TruncateTime(dateFrom) && DbFunctions.TruncateTime(e.Date) <= DbFunctions.TruncateTime(dateTo)
                && e.Category.ParentId == categoryId).GroupBy(e => e.Category, 
                (key, g) => new CategoryTotal { CategoryId = key.Id, CategoryName = key.Name, TotalAmount = g.Sum(c => c.Amount) })
                .OrderByDescending(c => c.TotalAmount).ToListAsync();
        }

        public Task<List<TransactionAggregates>> GetExpenseAggregates()
        {
            return _dbset.GroupBy(g => new { g.Date.Year, g.Date.Month }, (key, gr) => new TransactionAggregates { Month = key.Month, Year = key.Year, Sum = gr.Sum(g => g.Amount), Avg = gr.Average(g => g.Amount) }).OrderBy(g => new { g.Year, g.Month }).ToListAsync();
        }
    }
}
