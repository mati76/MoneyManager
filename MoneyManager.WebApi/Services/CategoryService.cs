using MoneyManager.Business;
using MoneyManager.Business.Models;
using MoneyManager.WebApi.Enums;
using System;
using System.Collections.Generic;

namespace MoneyManager.WebApi.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        private ICategoryBusiness _categoryBusiness;

        public CategoryService(IMapperService mapperService, ICategoryBusiness categoryBusiness) : base(mapperService)
        {
            if(categoryBusiness == null)
            {
                throw new ArgumentNullException(nameof(categoryBusiness));
            }
            _categoryBusiness = categoryBusiness;
        }

        public IEnumerable<DTO.Category> GetCategories(CategoryTypeEnum categoryType)
        {
            return _mapperService.Map<IEnumerable<DTO.Category>>(categoryType == CategoryTypeEnum.Expense ?_categoryBusiness.GetExpenseCategories() : _categoryBusiness.GetIncomeCategories());
        }

        public DTO.Category GetCategory(int id)
        {
            return _mapperService.Map<DTO.Category>(_categoryBusiness.GetCategoryById(id));
        }

        public void SaveCategory(DTO.Category category)
        {
            _categoryBusiness.SaveCategory(_mapperService.Map<Category>(category));
        }

        public void DeleteCategory(int id)
        {
            _categoryBusiness.DeleteCategoryById(id);
        }

        public IEnumerable<DTO.CategoryInfo> GetTopFiveCategories()
        {
            return _mapperService.Map<IEnumerable<DTO.CategoryInfo>>(_categoryBusiness.GetTopCategories(5));
        }
    }
}