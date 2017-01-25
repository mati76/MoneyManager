using System;
using System.Linq;
using MoneyManager.DataAccess.Infrastructure;
using System.Collections.Generic;
using System.Data.Entity;
using DAC = MoneyManager.DataAccess.Models;
using BLL = MoneyManager.Business.Models;
using EntityFramework.Extensions;
using MoneyManager.Business;

namespace MoneyManager.DataAccess.Repositories
{
    public abstract class BaseRepository<TDAC, TBLL> : IDisposable where TDAC : DAC.BaseModel where TBLL : BLL.BaseEntity
        
    {
        protected readonly IMapperService _mapperService;
        protected readonly DbSet<TDAC> _dbset;
        protected readonly IDbContext _dbContext;
        protected bool _disposed;

        public BaseRepository(IMapperService mapperService, IDbContext dbContext)
        {
            if(mapperService == null)
            {
                throw new ArgumentNullException("mapperService");
            }
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }

            _mapperService = mapperService;
            _dbset = dbContext.Set<TDAC>();
            _dbContext = dbContext;
        }

        public virtual void Add(TBLL o)
        {
            var dal = _mapperService.Map<TDAC>(o);
            dal.AddDateTime = DateTime.Now;
            _dbset.Add(_mapperService.Map<TDAC>(dal));
        }

        public virtual void Delete(TBLL o)
        {
            _dbset.Remove(_mapperService.Map<TDAC>(o));
        }

        public virtual void DeleteById(int id)
        {
            _dbset.Where(o => o.Id == id).Delete();
        }

        public virtual void Update(TBLL o)
        {
            var dal = _mapperService.Map<TDAC>(o);
            dal.UpdateDateTime = DateTime.Now;
            _dbset.Attach(dal);
            _dbContext.Entry(dal).State = EntityState.Modified;
            _dbContext.Entry(dal).Property(e => e.AddDateTime).IsModified = false;
            _dbContext.Entry(dal).Property(e => e.AddUserName).IsModified = false;
        }

        public virtual void AddOrUpdate(TBLL o)
        {
            if(o.Id > 0)
            {
                Update(o);
            }
            else
            {
                Add(o);
            }
        }

        public virtual TBLL GetById(int id)
        {
            return _mapperService.Map<TBLL>(_dbset.Find(id));
        }

        public virtual IEnumerable<TBLL> GetAll()
        {
            return _mapperService.Map<IEnumerable<TBLL>>(_dbset);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~BaseRepository()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }

                _disposed = true;
            }
        }
    }
}
