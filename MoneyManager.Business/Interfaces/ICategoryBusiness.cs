using MoneyManager.Business.Models;
using System.Collections.Generic;

namespace MoneyManager.Business
{
    public interface ICategoryBusiness
    {
        IEnumerable<Category> GetCategories();
        Category GetCategoryById(int id);
        void SaveCategory(Category category);
        void DeleteCategoryById(int id);
    }
}