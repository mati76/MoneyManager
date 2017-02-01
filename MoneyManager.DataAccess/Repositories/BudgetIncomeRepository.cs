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
    public class BudgetIncomeRepository : BaseRepository<DAC.BudgetIncome, Income>, IBudgetIncomeRepository
    {
        public BudgetIncomeRepository(IMapperService mapperService, IDbContext dbContext) : base(mapperService, dbContext) { }

        public IEnumerable<Income> GetIncome(int year, int month)
        {
            return _mapperService.Map<IEnumerable<Income>>(_dbset.Where(o => o.Date.Year == year && o.Date.Month == month));
        }

        public IEnumerable<Income> GetIncome(int year)
        {
            return _mapperService.Map<IEnumerable<Income>>(_dbset.Where(o => o.Date.Year == year));
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
    }
}
