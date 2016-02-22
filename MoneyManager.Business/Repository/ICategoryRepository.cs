using System.Collections.Generic;
using MoneyManager.Business.Models;

namespace MoneyManager.Business.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        ICollection<Category> GetParentCategories();

        void DeleteByParentId(int parentId);
    }
}
