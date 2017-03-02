using System;
using System.Linq;
using System.Collections.Generic;
using MoneyManager.Business.Models;
using MoneyManager.Business.Repository;
using MoneyManager.Business.Utilities;
using System.Threading.Tasks;

namespace MoneyManager.Business
{
    public class BudgetBusiness : BaseBusiness, IBudgetBusiness
    {
        private IDateHelper _dateHelper;

        public BudgetBusiness(IUnitOfWorkFactory unitOfWorkFactory, IDateHelper dateHelper) : base(unitOfWorkFactory)
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

        public async Task<IEnumerable<Transaction>> GetExpenses(int year, int month)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return await session.GetRepository<IBudgetExpenseRepository>().GetExpenses(year, month);
            }
        }

        public async Task<IEnumerable<Transaction>> GetExpenses(int year)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return await session.GetRepository<IBudgetExpenseRepository>().GetExpenses(year);
            }
        }

        public async Task<IEnumerable<Transaction>> GetExpenses(SearchCriteria criteria)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return await session.GetRepository<IBudgetExpenseRepository>().GetExpensesByCriteria(criteria);
            }
        }

        public async Task DeleteExpense(int id)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                await session.GetRepository<IBudgetExpenseRepository>().DeleteById(id);
            }
        }

        public async Task SaveExpense(Transaction expense)
        {
            using (var session =_unitOfWorkFactory.GetSession())
            {
                var repository = session.GetRepository<IBudgetExpenseRepository>();
                repository.AddOrUpdate(expense);
                await session.SaveAsync();
            }
        }

        public async Task<Transaction> GetIncome(int id)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return await session.GetRepository<IBudgetIncomeRepository>().GetById(id);
            }
        }

        public async Task<IEnumerable<Transaction>> GetIncome(int year, int month)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return await session.GetRepository<IBudgetIncomeRepository>().GetIncome(year, month);
            }
        }

        public async Task DeleteIncome(int id)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                await session.GetRepository<IBudgetIncomeRepository>().DeleteById(id);
            }
        }

        public async Task<IEnumerable<Transaction>> GetIncome(SearchCriteria criteria)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return await session.GetRepository<IBudgetIncomeRepository>().GetIncomeByCriteria(criteria);
            }
        }

        public async Task SaveIncome(Transaction income)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                var repository = session.GetRepository<IBudgetIncomeRepository>();
                repository.AddOrUpdate(income);
                await session.SaveAsync();
            }
        }

        public async Task<decimal> GetAvgExpenseDeviation()
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                var expenseAggregates = await session.GetRepository<IExpenseRepository>().GetExpenseAggregates();
                var budgetExpenseAggregates = (await session.GetRepository<IBudgetExpenseRepository>().GetExpenseAggregates()).ToDictionary(a => $"{a.Year}-{a.Month}");
                var deviations = new List<decimal>();

                foreach(var aggr in expenseAggregates)
                {
                    TransactionAggregates budgetAggr = null;
                    if(budgetExpenseAggregates.TryGetValue($"{aggr.Year}-{aggr.Month}", out budgetAggr))
                    {
                        deviations.Add(100 * (budgetAggr.Sum - aggr.Sum) / budgetAggr.Sum);
                    }
                }

                return deviations.Any() ? deviations.Average() : 0;
            }
        }

        public async Task<decimal> GetCategoryLimits(DateTime dateFrom, DateTime dateTo)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return (await session.GetRepository<IBudgetExpenseRepository>().GetExpenses(dateFrom, dateTo)).Select(e => e.Amount).Sum();
            }
        }

        public async Task<decimal> GetBudgetLimit(DateTime dateFrom, DateTime dateTo)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return (await session.GetRepository<IBudgetExpenseRepository>().GetExpenses(dateFrom, dateTo)).Select(e => e.Amount).Sum();
            }
        }

        public async Task<decimal> GetBudgetDeviation(DateTime dateFrom, DateTime dateTo)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                var limit = await GetBudgetLimit(dateFrom, dateTo);
                var expense = (await session.GetRepository<IExpenseRepository>().GetExpenses(dateFrom, dateTo)).Select(e => e.Amount).Sum();

                return limit > 0 ? 100 * (limit - expense) / limit : 0;
            }
        }

        public async Task<decimal> GetBudgetBalance(DateTime dateFrom, DateTime dateTo)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                var limit = await GetBudgetLimit(dateFrom, dateTo);
                var expense = (await session.GetRepository<IExpenseRepository>().GetExpenses(dateFrom, dateTo)).Select(e => e.Amount).Sum();

                return limit - expense;
            }
        }

        public async Task<IEnumerable<CategoryBalance>> GetCategoryBalance(DateTime dateFrom, DateTime dateTo)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                var result = new List<CategoryBalance>();

                var categoryLimits = await session.GetRepository<IBudgetExpenseRepository>().GetCategoryTotals(dateFrom, dateTo);
                var categoryExpense = await session.GetRepository<IExpenseRepository>().GetCategoryTotals(dateFrom, dateTo);

                var categories = categoryLimits.Select(c => new { Id = c.CategoryId, Name = c.CategoryName }).Concat(categoryExpense.Select(e => new { Id = e.CategoryId, Name = e.CategoryName }).ToList()).Distinct();
                foreach(var category in categories)
                {
                    result.Add(new CategoryBalance
                    {
                        CategoryId = category.Id,
                        CategoryName = category.Name,
                        Expense = categoryExpense.FirstOrDefault(e => e.CategoryId == category.Id)?.TotalAmount,
                        Balance = categoryLimits.FirstOrDefault(c => c.CategoryId == category.Id)?.TotalAmount
                    });
                }

                result.Where(r => r.Balance.HasValue).ToList().ForEach(r => r.Balance -= r.Expense ?? 0);
                return result;
            }
        }
    }
}
