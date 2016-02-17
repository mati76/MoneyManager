using Microsoft.Practices.Unity;
using MoneyManager.Business.Repository;

namespace MoneyManager.DataAccess.Infrastructure
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private IUnityContainer _container;

        public UnitOfWorkFactory(IUnityContainer container)
        {
            _container = container;
        }

        public IUnitOfWork GetSession()
        {
            return _container.Resolve<IUnitOfWork>();
        }
    }
}
