
namespace MoneyManager.WebApi.Services
{
    public interface IMapperService
    {
        TDestination Map<TDestination>(object source);
    }
}
