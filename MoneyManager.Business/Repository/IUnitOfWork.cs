using MoneyManager.Business.Repository;
using System;
using System.Threading.Tasks;

namespace MoneyManager.Business.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveAsync();

        T GetRepository<T>() where T : IRepository;
    }
}
