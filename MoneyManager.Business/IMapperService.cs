
namespace MoneyManager.Business
{
    public interface IMapperService
    {
        TDestination Map<TDestination>(object source);
    }
}
