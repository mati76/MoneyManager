using System;
using System.Collections.Generic;
using MoneyManager.Business.Interfaces;
using MoneyManager.Business.Models;
using MoneyManager.Business.Repository;
using MoneyManager.Business.Utilities;
using System.Linq;

namespace MoneyManager.Business
{
    public class IncomeBusiness : BaseBusiness, IIncomeBusiness
    {
        private IDateHelper _dateHelper;

        public IncomeBusiness(IUnitOfWorkFactory unitOfWorkFactory, IDateHelper dateHelper) : base(unitOfWorkFactory)
        {
            if (dateHelper == null)
            {
                throw new ArgumentNullException(nameof(dateHelper));
            }
            _dateHelper = dateHelper;
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

        public void DeleteIncome(int id)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                session.GetRepository<IIncomeRepository>().DeleteById(id);
            }
        }

        public IEnumerable<Income> GetIncome(SearchCriteria criteria)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {

                return session.GetRepository<IIncomeRepository>().GetIncomeByCriteria(criteria);
            }
        }

        public void SaveIncome(Income expense)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                var repository = session.GetRepository<IIncomeRepository>();
                repository.AddOrUpdate(expense);
                session.Save();
            }
        }

        public TransactionTotals GetIncomeTotals(DateTime currentDate)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                var repository = session.GetRepository<IIncomeRepository>();
                var currentWeek = _dateHelper.GetWeekBoundaries(currentDate);
                var currentMonth = _dateHelper.GetMonthBoundaries(currentDate);
                var currentYear = _dateHelper.GetYearBoundaries(currentDate);

                var totals = new TransactionTotals();
                totals.Today = repository.GetIncomeTotalsFromDates(currentDate, currentDate.AddHours(23).AddMinutes(59).AddSeconds(59));
                totals.CurrentWeek = repository.GetIncomeTotalsFromDates(currentWeek.Item1, currentWeek.Item2);
                totals.CurrentMonth = repository.GetIncomeTotalsFromDates(currentMonth.Item1, currentMonth.Item2);
                totals.CurrentYear = repository.GetIncomeTotalsFromDates(currentYear.Item1, currentYear.Item2);

                return totals;
            }
        }

        public IEnumerable<CategoryTotal> GetCategoryTotals(DateTime dateFrom, DateTime dateTo)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                var repository = session.GetRepository<IIncomeRepository>();
                var totals = repository.GeCategoryTotals(dateFrom, dateTo);
                totals?.ToList().ForEach(i => i.Percent = i.TotalAmount / totals.Sum(t => t.TotalAmount));
                return totals;
            }
        }
    }
}
