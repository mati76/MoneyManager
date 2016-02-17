using MoneyManager.Business.Repository;
using MoneyManager.Business.Models;
using DAC = MoneyManager.DataAccess.Models;
using MoneyManager.DataAccess.Services;
using MoneyManager.DataAccess.Infrastructure;

namespace MoneyManager.DataAccess.Repositories
{
    public class CategoryRepository : BaseRepository<DAC.Category, Category>, ICategoryRepository
    {
        public CategoryRepository(IMapperService mapperService, IDbContext dbContext) : base(mapperService, dbContext)
        {

        }
    }
}
