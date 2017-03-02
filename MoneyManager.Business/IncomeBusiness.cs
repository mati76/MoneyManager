using System;
using System.Collections.Generic;
using MoneyManager.Business.Interfaces;
using MoneyManager.Business.Models;
using MoneyManager.Business.Repository;
using MoneyManager.Business.Utilities;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<Transaction> GetIncome(int id)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return await session.GetRepository<IIncomeRepository>().GetById(id);
            }
        }

        public async Task<IEnumerable<Transaction>> GetIncome(DateTime from, DateTime to)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return await session.GetRepository<IIncomeRepository>().GetIncome(from, to);
            }
        }

        public async Task<IEnumerable<Transaction>> GetIncome(int year, int month)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return await session.GetRepository<IIncomeRepository>().GetIncome(year, month);
            }
        }

        public async Task DeleteIncome(int id)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                await session.GetRepository<IIncomeRepository>().DeleteById(id);
            }
        }

        public async Task<IEnumerable<Transaction>> GetIncome(SearchCriteria criteria)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return await session.GetRepository<IIncomeRepository>().GetIncomeByCriteria(criteria);
            }
        }

        public async Task SaveIncome(Transaction income)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                var repository = session.GetRepository<IIncomeRepository>();
                repository.AddOrUpdate(income);
                await session.SaveAsync();
            }
        }

        public async Task<TransactionTotals> GetIncomeTotals(DateTime currentDate)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                var repository = session.GetRepository<IIncomeRepository>();
                var currentWeek = _dateHelper.GetWeekBoundaries(currentDate);
                var currentMonth = _dateHelper.GetMonthBoundaries(currentDate);
                var currentYear = _dateHelper.GetYearBoundaries(currentDate);

                return new TransactionTotals
                {
                    Today = await repository.GetIncomeTotalsFromDates(currentDate, currentDate.AddHours(23).AddMinutes(59).AddSeconds(59)),
                    CurrentWeek = await repository.GetIncomeTotalsFromDates(currentWeek.Item1, currentWeek.Item2),
                    CurrentMonth = await repository.GetIncomeTotalsFromDates(currentMonth.Item1, currentMonth.Item2),
                    CurrentYear = await repository.GetIncomeTotalsFromDates(currentYear.Item1, currentYear.Item2)
                };
            }
        }

        public async Task<IEnumerable<CategoryTotal>> GetCategoryTotals(DateTime dateFrom, DateTime dateTo)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                var repository = session.GetRepository<IIncomeRepository>();
                var totals = await repository.GeCategoryTotals(dateFrom, dateTo);
                totals?.ForEach(i => i.Percent = i.TotalAmount / totals.Sum(t => t.TotalAmount));
                return totals;
            }
        }
    }
}
