﻿using System;
using System.Collections.Generic;
using System.Web.Http;
using MoneyManager.WebApi.Services;
using MoneyManager.WebApi.DTO;
using System.Threading.Tasks;

namespace MoneyManager.WebApi.Controllers
{
    [RoutePrefix("api/category")]
    [Authorize]
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

        [Route("expense")]
        public Task<IEnumerable<Category>> GetExpenseCategories()
        {
            return _categoryService.GetCategories(Enums.CategoryTypeEnum.Expense);
        }

        [Route("income")]
        public Task<IEnumerable<Category>> GetIncomeCategories()
        {
            return _categoryService.GetCategories(Enums.CategoryTypeEnum.Income);
        }

        [Route("expense/{id:int}")]
        public Task<Category> Get(int id)
        {
            return _categoryService.GetCategory(id);
        }

        [Route("expense")]
        public Task Post(Category category)
        {
            return _categoryService.SaveCategory(category);
        }

        [Route("expense")]
        public Task<int> Delete(int id)
        {
            return _categoryService.DeleteCategory(id);
        }

        [Route("expense/top")]
        public Task<IEnumerable<CategoryInfo>> GetTopFive()
        {
            return _categoryService.GetTopFiveCategories();
        }
        /*

        Validation - all

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
