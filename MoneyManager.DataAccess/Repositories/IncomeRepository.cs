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
    public class IncomeRepository : BaseRepository<DAC.Income, Income>, IIncomeRepository
    {
        public IncomeRepository(IMapperService mapperService, IDbContext dbContext) : base(mapperService, dbContext) { }

        public async Task<IEnumerable<Income>> GetIncome(DateTime from, DateTime to)
        {
            return _mapperService.Map<IEnumerable<Income>>(await _dbset.Where(o => DbFunctions.TruncateTime(o.Date) >= from && DbFunctions.TruncateTime(o.Date) <= to).ToListAsync());
        }

        public async Task<IEnumerable<Income>> GetIncome(int year, int month)
        {
            return _mapperService.Map<IEnumerable<Income>>(await _dbset.Where(o => o.Date.Year == year && o.Date.Month == month).ToListAsync());
        }

        public async Task<IEnumerable<Income>> GetIncomeByCriteria(SearchCriteria criteria)
        {
            var qry = _dbset.Where(e => DbFunctions.TruncateTime(e.Date) >= DbFunctions.TruncateTime(criteria.DateFrom)
                && DbFunctions.TruncateTime(e.Date) <= DbFunctions.TruncateTime(criteria.DateTo));

            if (criteria.Categories.Any())
                qry = qry.Where(o => criteria.Categories.Select(c => c.Id).Contains(o.Id));
            if (criteria.MinAmount.HasValue)
                qry = qry.Where(o => o.Amount >= criteria.MinAmount);
            if (criteria.MaxAmount.HasValue)
                qry = qry.Where(o => o.Amount <= criteria.MaxAmount);
            if (!string.IsNullOrEmpty(criteria.SortBy))
                qry = qry.OrderBy(criteria.SortBy + (criteria.SortAsc == true ? " ascending" : " descending"));
            if (criteria.Skip.HasValue && criteria.Take.HasValue)
                qry = qry.Skip(criteria.Skip.Value).Take(criteria.Take.Value);

            return _mapperService.Map<IEnumerable<Income>>(await qry.Include(e => e.Category).ToListAsync());
        }

        public async Task<decimal> GetIncomeTotalsFromDates(DateTime from, DateTime to)
        {
            return (await _dbset.Where(e => e.Date >= from && e.Date <= to).ToListAsync()).Select(e => e.Amount).DefaultIfEmpty(0).Sum();
        }

        public Task<List<CategoryTotal>> GeCategoryTotals(DateTime dateFrom, DateTime dateTo)
        {
            return _dbset.Where(e => DbFunctions.TruncateTime(e.Date) >= dateFrom && DbFunctions.TruncateTime(e.Date) <= dateTo).GroupBy(e => e.Category,
                (key, g) => new CategoryTotal { CategoryId = key.Id, CategoryName = key.Name, TotalAmount = g.Sum(c => c.Amount) })
                .OrderByDescending(c => c.TotalAmount).ToListAsync();
        }
    }
}
