using System;
using MoneyManager.Business.Repository;
using Microsoft.Practices.Unity;

namespace MoneyManager.DataAccess.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private IUnityContainer _container;
        protected readonly IDbContext _ctx;
        protected bool _disposed;

        public UnitOfWork(IDbContext context, IUnityContainer container)
        {
            _ctx = context;
            _container = container;
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }
      
        public T GetRepository<T>() where T : IRepository
        {
            return _container.Resolve<T>(new ParameterOverride("dbContext", _ctx));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _ctx.Dispose();
                }

                _disposed = true;
            }
        }
    }
}
