using MoneyManager.Business.Repository;
using MoneyManager.Business.Models;
using DAC = MoneyManager.DataAccess.Models;
using MoneyManager.DataAccess.Services;
using MoneyManager.DataAccess.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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
    }
}
