using System;
using System.Collections.Generic;
using MoneyManager.Business.Interfaces;
using MoneyManager.Business.Models;
using MoneyManager.Business.Repository;

namespace MoneyManager.Business
{
    public class ExpenseBusiness : BaseBusiness, IExpenseBusiness
    {
        public ExpenseBusiness(IUnitOfWorkFactory unitOfWorkFactory) : base(unitOfWorkFactory)
        {
            if (unitOfWorkFactory == null)
            {
                throw new ArgumentNullException(nameof(unitOfWorkFactory));
            }
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public Expense GetExpense(int id)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return session.GetRepository<IExpenseRepository>().GetById(id);
            }
        }

        public IEnumerable<Expense> GetExpenses(DateTime date)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return session.GetRepository<IExpenseRepository>().GetExpenses(date);
            }
        }

        public IEnumerable<Expense> GetExpenses(int year, int month)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return session.GetRepository<IExpenseRepository>().GetExpenses(year, month);
            }
        }

        public IEnumerable<Expense> GetExpenses(ExpenseCriteria criteria)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return session.GetRepository<IExpenseRepository>().GetExpensesByCriteria(criteria);
            }
        }

        public void Delete(Expense expense)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                session.GetRepository<IExpenseRepository>().Delete(expense);
            }
        }
    }
}
