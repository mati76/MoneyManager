using System;
using System.Collections.Generic;
using MoneyManager.Business.Interfaces;
using MoneyManager.Business.Models;
using MoneyManager.Business.Repository;

namespace MoneyManager.Business
{
    public class IncomeBusiness : BaseBusiness, IIncomeBusiness
    {
        public IncomeBusiness(IUnitOfWorkFactory unitOfWorkFactory) : base(unitOfWorkFactory)
        {
            if (unitOfWorkFactory == null)
            {
                throw new ArgumentNullException(nameof(unitOfWorkFactory));
            }
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public Income GetIncome(int id)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return session.GetRepository<IIncomeRepository>().GetById(id);
            }
        }

        public IEnumerable<Income> GetIncome(DateTime from, DateTime to)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return session.GetRepository<IIncomeRepository>().GetIncome(from, to);
            }
        }

        public IEnumerable<Income> GetIncome(int year, int month)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return session.GetRepository<IIncomeRepository>().GetIncome(year, month);
            }
        }

        public void Delete(Income Income)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                session.GetRepository<IIncomeRepository>().Delete(Income);
            }
        }
    }
}
