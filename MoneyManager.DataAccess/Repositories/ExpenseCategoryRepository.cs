using System.Linq;
using System.Data.Entity;
using EntityFramework.Extensions;
using MoneyManager.Business.Repository;
using MoneyManager.Business.Models;
using DAC = MoneyManager.DataAccess.Models;
using MoneyManager.DataAccess.Infrastructure;
using System.Collections.Generic;
using MoneyManager.Business;

namespace MoneyManager.DataAccess.Repositories
{
    public class ExpenseCategoryRepository : BaseRepository<DAC.ExpenseCategory, Category>, ICategoryRepository
    {
        public ExpenseCategoryRepository(IMapperService mapperService, IDbContext dbContext) : base(mapperService, dbContext)
        {
            
        }

        public ICollection<Category> GetParentCategories()
        {
            return _mapperService.Map<ICollection<Category>>(_dbset.Where(c => c.Parent == null).Include(c => c.Categories));
        }

        public ICollection<Category> GetTopCategories(int count)
        {
            return _mapperService.Map<ICollection<Category>>(_dbset.Where(c => c.ParentId > 0 && c.Expenses.Any())
                .Select(c => new { Category = c, ExpenseItems = c.Expenses.Count() })
                .OrderByDescending(c => c.ExpenseItems).Select(c => c.Category).Take(count));
        }

        public void DeleteByParentId(int parentId)
        {
            _dbset.Where(c => c.Parent != null && c.Parent.Id == parentId).Delete();
        }
    }
}
