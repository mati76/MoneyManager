using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.Business.Repository
{
    public interface IRepository { }
    public interface IRepository<T> : IRepository where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        Task<int> DeleteById(int id);
        void Update(T entity);
        void AddOrUpdate(T entity);
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll(); 
    }
}
