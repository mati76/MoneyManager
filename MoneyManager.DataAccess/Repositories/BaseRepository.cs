using System;
using System.Linq;
using MoneyManager.DataAccess.Infrastructure;
using MoneyManager.DataAccess.Services;
using System.Collections.Generic;
using System.Data.Entity;
using DAC = MoneyManager.DataAccess.Models;
using BLL = MoneyManager.Business.Models;
using EntityFramework.Extensions;

namespace MoneyManager.DataAccess.Repositories
{
    public abstract class BaseRepository<TDAC, TBLL> where TDAC : DAC.BaseModel where TBLL : BLL.BaseEntity
    {
        protected readonly IMapperService _mapperService;
        protected readonly DbSet<TDAC> _dbset;

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
        }

        public virtual void Add(TBLL o)
        {
            o.AddDateTime = DateTime.Now;
            _dbset.Add(_mapperService.Map<TDAC>(o));
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
            o.UpdDateTime = DateTime.Now;
            _dbset.Attach(_mapperService.Map<TDAC>(o));
        }

        public virtual TBLL GetById(int id)
        {
            return _mapperService.Map<TBLL>(_dbset.Find(id));
        }

        public virtual IEnumerable<TBLL> GetAll()
        {
            return _mapperService.Map<IEnumerable<TBLL>>(_dbset);
        }
    }
}
