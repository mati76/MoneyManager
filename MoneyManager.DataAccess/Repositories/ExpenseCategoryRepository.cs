using System.Linq;
using System.Data.Entity;
using EntityFramework.Extensions;
using MoneyManager.Business.Repository;
using MoneyManager.Business.Models;
using DAC = MoneyManager.DataAccess.Models;
using MoneyManager.DataAccess.Infrastructure;
using System.Collections.Generic;
using MoneyManager.Business;
using System.Threading.Tasks;

namespace MoneyManager.DataAccess.Repositories
{
    public class ExpenseCategoryRepository : BaseRepository<DAC.ExpenseCategory, Category>, ICategoryRepository
    {
        public ExpenseCategoryRepository(IMapperService mapperService, IDbContext dbContext) : base(mapperService, dbContext)
        {
            
        }

        public async Task<ICollection<Category>> GetParentCategories()
        {
            var categories = await _dbset.Where(c => c.Parent == null).Include(c => c.Categories).ToListAsync();
            return _mapperService.Map<ICollection<Category>>(categories);
        }

        public async Task<ICollection<Category>> GetTopCategories(int count)
        {
            return _mapperService.Map<ICollection<Category>>(await _dbset.Where(c => c.ParentId > 0 && c.Expenses.Any())
                .Select(c => new { Category = c, ExpenseItems = c.Expenses.Count() })
                .OrderByDescending(c => c.ExpenseItems).Select(c => c.Category).Take(count).ToListAsync());
        }

        public Task<int> DeleteByParentId(int parentId)
        {
            return _dbset.Where(c => c.Parent != null && c.Parent.Id == parentId).DeleteAsync();
        }
    }
}
