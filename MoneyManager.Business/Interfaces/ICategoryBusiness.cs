using MoneyManager.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.Business
{
    public interface ICategoryBusiness
    {
        Task<ICollection<Category>> GetExpenseCategories();
        Task<IEnumerable<Category>> GetIncomeCategories();
        Task<Category> GetCategoryById(int id);
        Task SaveCategory(Category category);
        Task<int> DeleteCategoryById(int id);
        Task<IEnumerable<Category>> GetTopCategories(int count);
    }
}