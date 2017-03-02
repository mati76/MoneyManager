using System;
using System.Linq;
using System.Collections.Generic;
using MoneyManager.Business.Interfaces;
using MoneyManager.Business.Models;
using MoneyManager.Business.Repository;
using MoneyManager.Business.Utilities;
using System.Threading.Tasks;
using System.Diagnostics;

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

        public async Task<Transaction> GetExpense(int id)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return await session.GetRepository<IExpenseRepository>().GetById(id);
            }
        }

        public async Task<IEnumerable<Transaction>> GetExpenses(DateTime date)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return await session.GetRepository<IExpenseRepository>().GetExpenses(date);
            }
        }

        public async Task<IEnumerable<Transaction>> GetExpenses(int year, int month)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return await session.GetRepository<IExpenseRepository>().GetExpenses(year, month);
            }
        }

        public async Task<TransactionCollection> GetExpenses(SearchCriteria criteria)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return await session.GetRepository<IExpenseRepository>().GetExpensesByCriteria(criteria);
            }
        }

        public async Task DeleteExpense(int id)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                await session.GetRepository<IExpenseRepository>().DeleteById(id);
            }
        }

        public async Task SaveExpense(Transaction expense)
        {
            using (var session =_unitOfWorkFactory.GetSession())
            {
                var repository = session.GetRepository<IExpenseRepository>();
                repository.AddOrUpdate(expense);
                await session.SaveAsync();
            }
        }

        public async Task<TransactionTotals> GetExpenseTotals(DateTime currentDate)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                var repository = session.GetRepository<IExpenseRepository>();
                var currentWeek = _dateHelper.GetWeekBoundaries(currentDate);
                var currentMonth = _dateHelper.GetMonthBoundaries(currentDate);
                var currentYear = _dateHelper.GetYearBoundaries(currentDate);

                return new TransactionTotals
                {
                    Today = await repository.GetExpenseTotalsFromDates(currentDate, currentDate.AddHours(23).AddMinutes(59).AddSeconds(59)),
                    CurrentWeek = await repository.GetExpenseTotalsFromDates(currentWeek.Item1, currentWeek.Item2),
                    CurrentMonth = await repository.GetExpenseTotalsFromDates(currentMonth.Item1, currentMonth.Item2),
                    CurrentYear = await repository.GetExpenseTotalsFromDates(currentYear.Item1, currentYear.Item2)
                };
            }
        }

        public async Task<IEnumerable<CategoryTotal>> GetCategoryTotals(DateTime dateFrom, DateTime dateTo, int categoryId)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                var repository = session.GetRepository<IExpenseRepository>();
                var totals = categoryId > 0 ? await repository.GetCategoryTotals(dateFrom, dateTo, categoryId) : await repository.GetCategoryTotals(dateFrom, dateTo);
                totals?.ToList().ForEach(i => i.Percent = i.TotalAmount / totals.Sum(t => t.TotalAmount));
                return totals;
            }
        }
    }
}
