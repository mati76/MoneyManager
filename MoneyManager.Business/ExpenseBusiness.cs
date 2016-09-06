using System;
using System.Collections.Generic;
using MoneyManager.Business.Interfaces;
using MoneyManager.Business.Models;
using MoneyManager.Business.Repository;
using MoneyManager.Business.Utilities;

namespace MoneyManager.Business
{
    public class ExpenseBusiness : BaseBusiness, IExpenseBusiness
    {
        private IDateHelper _dateHelper;

        public ExpenseBusiness(IUnitOfWorkFactory unitOfWorkFactory, IDateHelper dateHelper) : base(unitOfWorkFactory)
        {
            if (unitOfWorkFactory == null)
            {
                throw new ArgumentNullException(nameof(unitOfWorkFactory));
            }
            if (dateHelper == null)
            {
                throw new ArgumentNullException(nameof(dateHelper));
            }
            _unitOfWorkFactory = unitOfWorkFactory;
            _dateHelper = dateHelper;
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

        public void SaveExpense(Expense expense)
        {
            using (var session =_unitOfWorkFactory.GetSession())
            {
                var repository = session.GetRepository<IExpenseRepository>();
                repository.AddOrUpdate(expense);
                session.Save();
            }
        }

        public ExpenseTotals GetExpenseTotals(DateTime currentDate)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                var repository = session.GetRepository<IExpenseRepository>();
                var currentWeek = _dateHelper.GetWeekBoundaries(currentDate);
                var currentMonth = _dateHelper.GetMonthBoundaries(currentDate);
                var currentYear = _dateHelper.GetYearBoundaries(currentDate);

                var totals = new ExpenseTotals();
                totals.Today = repository.GetExpenseTotalsFromDates(currentDate, currentDate);
                totals.CurrentWeek = repository.GetExpenseTotalsFromDates(currentWeek.Item1, currentWeek.Item2);
                totals.CurrentMonth = repository.GetExpenseTotalsFromDates(currentMonth.Item1, currentMonth.Item2);
                totals.CurrentYear = repository.GetExpenseTotalsFromDates(currentYear.Item1, currentYear.Item2);

                return totals;
            }
        }
    }
}
