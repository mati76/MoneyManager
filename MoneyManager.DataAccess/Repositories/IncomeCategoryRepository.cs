using System.Linq;
using System.Data.Entity;
using EntityFramework.Extensions;
using MoneyManager.Business.Repository;
using MoneyManager.Business.Models;
using DAC = MoneyManager.DataAccess.Models;
using MoneyManager.DataAccess.Infrastructure;
using MoneyManager.Business;

namespace MoneyManager.DataAccess.Repositories
{
    public class IncomeCategoryRepository : BaseRepository<DAC.IncomeCategory, Category>, IIncomeCategoryRepository
    {
        public IncomeCategoryRepository(IMapperService mapperService, IDbContext dbContext) : base(mapperService, dbContext)
        {
            
        }
    }
}
