
namespace MoneyManager.DataAccess.Services
{
    public interface IMapperService
    {
        TDestination Map<TDestination>(object source);
    }
}
