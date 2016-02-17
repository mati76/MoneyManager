using System;
using System.Collections.Generic;
using System.Web.Http;
using MoneyManager.WebApi.Services;
using MoneyManager.WebApi.ViewModels;

namespace MoneyManager.WebApi.Controllers
{
    public class CategoryController : ApiController, ICategoryController
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            if(categoryService == null)
            {
                throw new ArgumentNullException("categoryService");
            }

            _categoryService = categoryService;
        }

        public IEnumerable<CategoryViewModel> Get()
        {
            return _categoryService.GetAllCategories();
        }

        public CategoryViewModel Get(int id)
        {
            return _categoryService.GetCategory(id);
        }

        public void Post(CategoryViewModel category)
        {
            _categoryService.SaveCategory(category);
        }

        public void Delete(int id)
        {
            _categoryService.DeleteCategory(id);
        }

        /*

        Categories:
            GetAll()
            Add()
            Update()
            Delete()
        Expense:
            GetAll()
            GetByCriteria(DateFrom, DateTo, List<Category>, SortBy, SortAsc, MinValue, MaxValue)
            Add()
            Delete()
            Update(Value, Criteria, DateTime)
        Income:
            Add()
            Update()
            Delete()
            GetByCriteria(DateFrom, DateTo, SortBy, SortAsc, MinValue, MaxValue)
        Budget:
            Income(Value, Month):
                GetByCriteria(Month, SortBy, SortAsc)
                Update()
                Delete()
                Add()
            OutCome(Value, Month, Category)
                GetByCriteria(Month, SortBy, SortAsc, List<Category>)
                Add()
                Delete()
                UPdate()
        Balance (Budget - Expences):
            GetByCriteria(MonthFrom, MonthTo, List<Categories>)
            
            


    */
    }
}
