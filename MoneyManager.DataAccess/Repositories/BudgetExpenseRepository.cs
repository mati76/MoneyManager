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

namespace MoneyManager.DataAccess.Repositories
{
    public class BudgetExpenseRepository : BaseRepository<DAC.BudgetExpense, Expense>, IBudgetExpenseRepository
    {
        public BudgetExpenseRepository(IMapperService mapperService, IDbContext dbContext) : base(mapperService, dbContext) { }

        public async Task<IEnumerable<Expense>> GetExpenses(int year, int month)
        {
            return _mapperService.Map<IEnumerable<Expense>>(await _dbset.Where(o => o.Date.Year == year && o.Date.Month == month).ToListAsync());
        }

        public async Task<IEnumerable<Expense>> GetExpenses(int year)
        {
            return _mapperService.Map<IEnumerable<Expense>>(await _dbset.Where(o => o.Date.Year == year).ToListAsync());
        }

        public async Task<IEnumerable<Expense>> GetExpenses(DateTime dateFrom, DateTime dateTo)
        {
            return _mapperService.Map<IEnumerable<Expense>>(await _dbset.Where(e => DbFunctions.TruncateTime(e.Date) >= DbFunctions.TruncateTime(dateFrom)
                && DbFunctions.TruncateTime(e.Date) <= DbFunctions.TruncateTime(dateTo)).ToListAsync());
        }

        public async Task<IEnumerable<Expense>> GetExpensesByCriteria(SearchCriteria criteria)
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
            if(criteria.Skip.HasValue && criteria.Take.HasValue)
                qry = qry.Skip(criteria.Skip.Value).Take(criteria.Take.Value);

            return _mapperService.Map<IEnumerable<Expense>>(await qry.ToListAsync());
        }

        public Task<List<TransactionAggregates>> GetExpenseAggregates()
        {
            return _dbset.GroupBy(g => new { g.Date.Year, g.Date.Month }, (key, gr) => new TransactionAggregates { Month = key.Month, Year = key.Year, Sum = gr.Sum(g => g.Amount), Avg = gr.Average(g => g.Amount) }).OrderBy(g => new { g.Year, g.Month }).ToListAsync();
        }

        public Task<List<CategoryTotal>> GetCategoryTotals(DateTime dateFrom, DateTime dateTo)
        {
            return _dbset.Where(e => DbFunctions.TruncateTime(e.Date) >= DbFunctions.TruncateTime(dateFrom) && DbFunctions.TruncateTime(e.Date) <= DbFunctions.TruncateTime(dateTo)).GroupBy(e => e.Category.Parent,
                (key, g) => new CategoryTotal { CategoryId = key.Id, CategoryName = key.Name, TotalAmount = g.Sum(c => c.Amount) })
                .OrderByDescending(c => c.TotalAmount).ToListAsync();
        }
    }
}
