using MoneyManager.Business.Models;
using DTO = MoneyManager.WebApi.DTO;
 
namespace MoneyManager.WebApi.Services
{
    public class MapperService : IMapperService
    {
        public TDestination Map<TDestination>(object source)
        {
            return AutoMapper.Mapper.Map<TDestination>(source);
        }
        
        public static void Configure()
        {
            AutoMapper.Mapper.CreateMap<DTO.Category, Category>();
            AutoMapper.Mapper.CreateMap<Category, DTO.Category>();
            AutoMapper.Mapper.CreateMap<Category, DTO.CategoryInfo>();
            AutoMapper.Mapper.CreateMap<DTO.CategoryInfo, Category>();
            AutoMapper.Mapper.CreateMap<Expense, DTO.Expense>();
            AutoMapper.Mapper.CreateMap<DTO.Expense, Expense>().AfterMap((dto, bll) => { bll.CategoryId = dto.Category.Id; bll.Category = null; });
            AutoMapper.Mapper.CreateMap<ExpenseTotals, DTO.ExpenseTotals>();
        }
    }
}
