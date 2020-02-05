using AutoMapper;
using MoneyManager.Business.Models;
using DAC = MoneyManager.DataAccess.Models;

namespace MoneyManager.Api.AutoMapper
{
	public class CategoriesProfile : Profile
	{
		public CategoriesProfile()
		{
            CreateMap<DTO.Category, Category>().PreserveReferences();
            CreateMap<Category, DTO.Category>().PreserveReferences();
            CreateMap<Category, DTO.CategoryInfo>().PreserveReferences();
            CreateMap<DTO.CategoryInfo, Category>().PreserveReferences();
            CreateMap<CategoryTotal, DTO.CategoryTotal>();

            CreateMap<DAC.ExpenseCategory, Category>().PreserveReferences();
            CreateMap<Category, DAC.ExpenseCategory>().PreserveReferences();
            CreateMap<DAC.IncomeCategory, Category>().PreserveReferences();
            CreateMap<Category, DAC.IncomeCategory>().PreserveReferences();
        }
	}
}
