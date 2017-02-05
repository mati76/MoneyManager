using System;
using System.Linq;
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
            if (dateHelper == null)
            {
                throw new ArgumentNullException(nameof(dateHelper));
            }
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

        public IEnumerable<Expense> GetExpenses(SearchCriteria criteria)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return session.GetRepository<IExpenseRepository>().GetExpensesByCriteria(criteria);
            }
        }

        public void DeleteExpense(int id)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                session.GetRepository<IExpenseRepository>().DeleteById(id);
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

        public TransactionTotals GetExpenseTotals(DateTime currentDate)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                var repository = session.GetRepository<IExpenseRepository>();
                var currentWeek = _dateHelper.GetWeekBoundaries(currentDate);
                var currentMonth = _dateHelper.GetMonthBoundaries(currentDate);
                var currentYear = _dateHelper.GetYearBoundaries(currentDate);

                var totals = new TransactionTotals();
                totals.Today = repository.GetExpenseTotalsFromDates(currentDate, currentDate.AddHours(23).AddMinutes(59).AddSeconds(59));
                totals.CurrentWeek = repository.GetExpenseTotalsFromDates(currentWeek.Item1, currentWeek.Item2);
                totals.CurrentMonth = repository.GetExpenseTotalsFromDates(currentMonth.Item1, currentMonth.Item2);
                totals.CurrentYear = repository.GetExpenseTotalsFromDates(currentYear.Item1, currentYear.Item2);

                return totals;
            }
        }

        public IEnumerable<CategoryTotal> GetCategoryTotals(DateTime dateFrom, DateTime dateTo, int categoryId)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                var repository = session.GetRepository<IExpenseRepository>();
                var totals = categoryId > 0 ? repository.GetCategoryTotals(dateFrom, dateTo, categoryId) : repository.GetCategoryTotals(dateFrom, dateTo);
                totals?.ToList().ForEach(i => i.Percent = i.TotalAmount / totals.Sum(t => t.TotalAmount));
                return totals;
            }
        }
    }
}
