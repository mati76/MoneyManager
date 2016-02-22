using MoneyManager.WebApi.DTO;
using System.Collections.Generic;

namespace MoneyManager.WebApi.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
        Category GetCategory(int id);
        void SaveCategory(Category category);
        void DeleteCategory(int id);
    }
}
