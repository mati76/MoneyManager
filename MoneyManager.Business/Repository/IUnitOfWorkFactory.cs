
namespace MoneyManager.Business.Repository
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork GetSession();
    }
}
