using MoneyManager.WebApi.DTO;
using System.Collections.Generic;

namespace MoneyManager.WebApi.Controllers
{
    public interface ICategoryController
    {
        IEnumerable<Category> GetExpenseCategories();

        IEnumerable<Category> GetIncomeCategories();

        Category Get(int id);

        void Post(Category category);

        void Delete(int id);
    }
}
