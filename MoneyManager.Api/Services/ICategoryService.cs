using MoneyManager.Api.DTO;
using MoneyManager.Api.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.Api.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategories(CategoryTypeEnum categoryType);
        Task<Category> GetCategory(int id);
        Task SaveCategory(Category category);
        Task<int> DeleteCategory(int id);
        Task<IEnumerable<CategoryInfo>> GetTopFiveCategories();
    }
}
