using System.Collections.Generic;
using MoneyManager.Business.Models;
using System.Threading.Tasks;

namespace MoneyManager.Business.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<ICollection<Category>> GetParentCategories();

        Task<int> DeleteByParentId(int parentId);

        Task<ICollection<Category>> GetTopCategories(int count);
    }
}
