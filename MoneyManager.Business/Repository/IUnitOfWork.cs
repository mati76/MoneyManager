using MoneyManager.Business.Repository;
using System;

namespace MoneyManager.Business.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();

        T GetRepository<T>() where T : IRepository;
    }
}
