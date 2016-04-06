using System.Collections.Generic;

namespace MoneyManager.Business.Repository
{
    public interface IRepository { }
    public interface IRepository<T> : IRepository where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        void DeleteById(int id);
        void Update(T entity);
        void AddOrUpdate(T entity);
        T GetById(int id);
        IEnumerable<T> GetAll(); 
    }
}
