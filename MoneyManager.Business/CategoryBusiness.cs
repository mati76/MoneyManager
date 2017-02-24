using MoneyManager.Business.Models;
using MoneyManager.Business.Repository;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.Business
{
    public class CategoryBusiness : BaseBusiness, ICategoryBusiness
    {
        public CategoryBusiness(IUnitOfWorkFactory unitOfWorkFactory) : base(unitOfWorkFactory)
        {
        }

        public async Task<ICollection<Category>> GetExpenseCategories()
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return await session.GetRepository<ICategoryRepository>().GetParentCategories();
            }
        }

        public async Task<IEnumerable<Category>> GetIncomeCategories()
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return await session.GetRepository<IIncomeCategoryRepository>().GetAll();
            }
        }

        public async Task<Category> GetCategoryById(int id)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return await session.GetRepository<ICategoryRepository>().GetById(id);
            }
        }

        public async Task SaveCategory(Category category)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                var repository = session.GetRepository<ICategoryRepository>();
                repository.AddOrUpdate(category);
                await session.SaveAsync();
            }
        }

        public async Task<int> DeleteCategoryById(int id)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                var repo = session.GetRepository<ICategoryRepository>();
                await repo.DeleteByParentId(id);
                return await repo.DeleteById(id);
            }
        }

        public async Task<IEnumerable<Category>> GetTopCategories(int count)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                var repo = session.GetRepository<ICategoryRepository>();
                var topCategories = await repo.GetTopCategories(count);
                if(topCategories.Count != count)
                {
                    var allCategories = await repo.GetAll();
                    for(var i = topCategories.Count; i < count; i++)
                    {
                        var cat = allCategories.Where(c => !topCategories.Any(t => t.ParentId == c.Id)).SelectMany(c => c.Categories).First();
                        topCategories.Add(cat);
                    }
                }
                return topCategories;
            }
        }
    }
}
