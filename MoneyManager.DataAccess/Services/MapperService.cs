using BLL = MoneyManager.Business.Models;
using DAC = MoneyManager.DataAccess.Models;

 
namespace MoneyManager.DataAccess.Services
{
    public class MapperService : IMapperService
    {
        public MapperService()
        {
            Configure();
        }

        public TDestination Map<TDestination>(object source)
        {
            return AutoMapper.Mapper.Map<TDestination>(source);
        }

        private void Configure()
        {
            AutoMapper.Mapper.CreateMap<DAC.Category, BLL.Category>();
            AutoMapper.Mapper.CreateMap<BLL.Category, DAC.Category>();
            AutoMapper.Mapper.CreateMap<DAC.Expense, BLL.Expense>();
            AutoMapper.Mapper.CreateMap<BLL.Expense, DAC.Expense>();
            AutoMapper.Mapper.CreateMap<DAC.Income, BLL.Income>();
            AutoMapper.Mapper.CreateMap<BLL.Income, DAC.Income>();
        }
    }
}
