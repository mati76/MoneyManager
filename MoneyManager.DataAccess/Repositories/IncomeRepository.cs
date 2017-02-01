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

namespace MoneyManager.DataAccess.Repositories
{
    public class IncomeRepository : BaseRepository<DAC.Income, Income>, IIncomeRepository
    {
        public IncomeRepository(IMapperService mapperService, IDbContext dbContext) : base(mapperService, dbContext) { }

        public IEnumerable<Income> GetIncome(DateTime from, DateTime to)
        {
            return _mapperService.Map<IEnumerable<Income>>(_dbset.Where(o => DbFunctions.TruncateTime(o.Date) >= from && DbFunctions.TruncateTime(o.Date) <= to));
        }

        public IEnumerable<Income> GetIncome(int year, int month)
        {
            return _mapperService.Map<IEnumerable<Income>>(_dbset.Where(o => o.Date.Year == year && o.Date.Month == month));
        }

        public IEnumerable<Income> GetIncomeByCriteria(SearchCriteria criteria)
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
            if (criteria.CurrentPage.HasValue)
                qry = qry.Skip(criteria.CurrentPage.Value * criteria.PageSize).Take(criteria.PageSize);

            return _mapperService.Map<IEnumerable<Income>>(qry.Include(e => e.Category));
        }

        public decimal GetIncomeTotalsFromDates(DateTime from, DateTime to)
        {
            return _dbset.Where(e => e.Date >= from && e.Date <= to).Select(e => e.Amount).DefaultIfEmpty(0).Sum();
        }

        public IEnumerable<CategoryTotal> GeCategoryTotals(DateTime dateFrom, DateTime dateTo)
        {
            return _dbset.Where(e => DbFunctions.TruncateTime(e.Date) >= dateFrom && DbFunctions.TruncateTime(e.Date) <= dateTo).GroupBy(e => e.Category,
                (key, g) => new CategoryTotal { CategoryId = key.Id, CategoryName = key.Name, TotalAmount = g.Sum(c => c.Amount) })
                .OrderByDescending(c => c.TotalAmount).ToList();
        }
    }
}
