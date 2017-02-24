using MoneyManager.WebApi.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.WebApi.Controllers
{
    public interface ICategoryController
    {
        Task<IEnumerable<Category>> GetExpenseCategories();

        Task<IEnumerable<Category>> GetIncomeCategories();

        Task<Category> Get(int id);

        Task Post(Category category);

        Task<int> Delete(int id);
    }
}
