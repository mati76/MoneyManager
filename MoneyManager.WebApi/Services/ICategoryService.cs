using MoneyManager.WebApi.ViewModels;
using System.Collections.Generic;

namespace MoneyManager.WebApi.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoryViewModel> GetAllCategories();
        CategoryViewModel GetCategory(int id);
        void SaveCategory(CategoryViewModel category);
        void DeleteCategory(int id);
    }
}
