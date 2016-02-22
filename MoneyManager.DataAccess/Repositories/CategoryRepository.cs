using System.Linq;
using EntityFramework.Extensions;
using MoneyManager.Business.Repository;
using MoneyManager.Business.Models;
using DAC = MoneyManager.DataAccess.Models;
using MoneyManager.DataAccess.Services;
using MoneyManager.DataAccess.Infrastructure;
using System.Collections.Generic;

namespace MoneyManager.DataAccess.Repositories
{
    public class CategoryRepository : BaseRepository<DAC.Category, Category>, ICategoryRepository
    {
        public CategoryRepository(IMapperService mapperService, IDbContext dbContext) : base(mapperService, dbContext)
        {
            
        }

        public ICollection<Category> GetParentCategories()
        {
            return _mapperService.Map<ICollection<Category>>(_dbset.Where(c => c.Parent == null));
        }

        public void DeleteByParentId(int parentId)
        {
            _dbset.Where(c => c.Parent != null && c.Parent.Id == parentId).Delete();
        }
    }
}
