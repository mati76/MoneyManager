using MoneyManager.Business.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.Business
{
    public abstract class BaseBusiness
    {
        protected IUnitOfWorkFactory _unitOfWorkFactory;

        public BaseBusiness(IUnitOfWorkFactory unitOfWorkFactory)
        {
            if(unitOfWorkFactory == null)
            {
                throw new ArgumentNullException("unitOfWorkFactory");
            }
            _unitOfWorkFactory = unitOfWorkFactory;
        }
    }
}
