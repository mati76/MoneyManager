using MoneyManager.Business.Models;
using MoneyManager.WebApi.ViewModels;
 
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
            AutoMapper.Mapper.CreateMap<CategoryViewModel, Category>();
            AutoMapper.Mapper.CreateMap<Category, CategoryViewModel>();
        }
    }
}
