using MoneyManager.WebApi.DTO;
using MoneyManager.WebApi.Enums;
using System.Collections.Generic;

namespace MoneyManager.WebApi.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories(CategoryTypeEnum categoryType);
        Category GetCategory(int id);
        void SaveCategory(Category category);
        void DeleteCategory(int id);
        IEnumerable<CategoryInfo> GetTopFiveCategories();
    }
}
