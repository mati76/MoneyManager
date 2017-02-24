using MoneyManager.Business;
using MoneyManager.Business.Models;
using MoneyManager.WebApi.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<DTO.Category>> GetCategories(CategoryTypeEnum categoryType)
        {
            return _mapperService.Map<IEnumerable<DTO.Category>>(categoryType == CategoryTypeEnum.Expense ? await _categoryBusiness.GetExpenseCategories() : await _categoryBusiness.GetIncomeCategories());
        }

        public async Task<DTO.Category> GetCategory(int id)
        {
            return _mapperService.Map<DTO.Category>(await _categoryBusiness.GetCategoryById(id));
        }

        public Task SaveCategory(DTO.Category category)
        {
            return _categoryBusiness.SaveCategory(_mapperService.Map<Category>(category));
        }

        public Task<int> DeleteCategory(int id)
        {
            return _categoryBusiness.DeleteCategoryById(id);
        }

        public async Task<IEnumerable<DTO.CategoryInfo>> GetTopFiveCategories()
        {
            return _mapperService.Map<IEnumerable<DTO.CategoryInfo>>(await _categoryBusiness.GetTopCategories(5));
        }
    }
}