using MoneyManager.Business;
using MoneyManager.Business.Models;
using MoneyManager.WebApi.ViewModels;
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

        public IEnumerable<CategoryViewModel> GetAllCategories()
        {
            return _mapperService.Map<IEnumerable<CategoryViewModel>>(_categoryBusiness.GetAllCategories());
        }

        public CategoryViewModel GetCategory(int id)
        {
            return _mapperService.Map<CategoryViewModel>(_categoryBusiness.GetCategoryById(id));
        }

        public void SaveCategory(CategoryViewModel category)
        {
            _categoryBusiness.SaveCategory(_mapperService.Map<Category>(category));
        }

        public void DeleteCategory(int id)
        {
            _categoryBusiness.DeleteCategoryById(id);
        }
    }
}