using MoneyManager.Business.Models;
using MoneyManager.Business.Repository;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MoneyManager.Business
{
    public class CategoryBusiness : BaseBusiness, ICategoryBusiness
    {
        public CategoryBusiness(IUnitOfWorkFactory unitOfWorkFactory) : base(unitOfWorkFactory)
        {
        }

        public IEnumerable<Category> GetExpenseCategories()
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return session.GetRepository<ICategoryRepository>().GetParentCategories();
            }
        }

        public IEnumerable<Category> GetIncomeCategories()
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return session.GetRepository<IIncomeCategoryRepository>().GetAll();
            }
        }

        public Category GetCategoryById(int id)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return session.GetRepository<ICategoryRepository>().GetById(id);
            }
        }

        public void SaveCategory(Category category)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                var repository = session.GetRepository<ICategoryRepository>();
                repository.AddOrUpdate(category);
                session.Save();
            }
        }

        public void DeleteCategoryById(int id)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                var repo = session.GetRepository<ICategoryRepository>();
                repo.DeleteByParentId(id);
                repo.DeleteById(id);
            }
        }

        public IEnumerable<Category> GetTopCategories(int count)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                var repo = session.GetRepository<ICategoryRepository>();
                var topCategories = repo.GetTopCategories(count);
                if(topCategories.Count != count)
                {
                    var allCategories = repo.GetAll();
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
