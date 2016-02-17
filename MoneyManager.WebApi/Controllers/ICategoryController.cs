using MoneyManager.WebApi.ViewModels;
using System.Collections.Generic;

namespace MoneyManager.WebApi.Controllers
{
    public interface ICategoryController
    {
        IEnumerable<CategoryViewModel> Get();

        CategoryViewModel Get(int id);

        void Post(CategoryViewModel category);

        void Delete(int id);
    }
}
