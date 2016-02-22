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
            if (unitOfWorkFactory == null)
            {
                throw new ArgumentNullException(nameof(unitOfWorkFactory));
            }
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public IEnumerable<Category> GetCategories()
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                var test = session.GetRepository<ICategoryRepository>().GetParentCategories();
                return test;
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
                if(category.Id > 0)
                {
                    repository.Update(category);
                }
                else
                {
                    repository.Add(category);
                }
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
    }
}
