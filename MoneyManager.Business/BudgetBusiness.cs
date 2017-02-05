using System;
using System.Linq;
using System.Collections.Generic;
using MoneyManager.Business.Models;
using MoneyManager.Business.Repository;
using MoneyManager.Business.Utilities;

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

        public Expense GetExpense(int id)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return session.GetRepository<IExpenseRepository>().GetById(id);
            }
        }

        public IEnumerable<Expense> GetExpenses(int year, int month)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return session.GetRepository<IBudgetExpenseRepository>().GetExpenses(year, month);
            }
        }

        public IEnumerable<Expense> GetExpenses(int year)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return session.GetRepository<IBudgetExpenseRepository>().GetExpenses(year);
            }
        }

        public IEnumerable<Expense> GetExpenses(SearchCriteria criteria)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return session.GetRepository<IBudgetExpenseRepository>().GetExpensesByCriteria(criteria);
            }
        }

        public void DeleteExpense(int id)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                session.GetRepository<IBudgetExpenseRepository>().DeleteById(id);
            }
        }

        public void SaveExpense(Expense expense)
        {
            using (var session =_unitOfWorkFactory.GetSession())
            {
                var repository = session.GetRepository<IBudgetExpenseRepository>();
                repository.AddOrUpdate(expense);
                session.Save();
            }
        }

        public Income GetIncome(int id)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return session.GetRepository<IBudgetIncomeRepository>().GetById(id);
            }
        }

        public IEnumerable<Income> GetIncomes(int year, int month)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return session.GetRepository<IBudgetIncomeRepository>().GetIncome(year, month);
            }
        }

        public IEnumerable<Income> GetIncomes(int year)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return session.GetRepository<IBudgetIncomeRepository>().GetIncome(year);
            }
        }

        public void DeleteIncome(int id)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                session.GetRepository<IBudgetIncomeRepository>().DeleteById(id);
            }
        }

        public IEnumerable<Income> GetIncomes(SearchCriteria criteria)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return session.GetRepository<IBudgetIncomeRepository>().GetIncomeByCriteria(criteria);
            }
        }

        public void SaveIncome(Income expense)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                var repository = session.GetRepository<IBudgetIncomeRepository>();
                repository.AddOrUpdate(expense);
                session.Save();
            }
        }

        public decimal GetAvgExpenseDeviation()
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                var expenseAggregates = session.GetRepository<IExpenseRepository>().GetExpenseAggregates();
                var budgetExpenseAggregates = session.GetRepository<IBudgetExpenseRepository>().GetExpenseAggregates().ToDictionary(a => $"{a.Year}-{a.Month}");
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

        public decimal GetCategoryLimits(DateTime dateFrom, DateTime dateTo)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return session.GetRepository<IBudgetExpenseRepository>().GetExpenses(dateFrom, dateTo).Select(e => e.Amount).Sum();
            }
        }

        public decimal GetBudgetLimit(DateTime dateFrom, DateTime dateTo)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                return session.GetRepository<IBudgetExpenseRepository>().GetExpenses(dateFrom, dateTo).Select(e => e.Amount).Sum();
            }
        }

        public decimal GetBudgetDeviation(DateTime dateFrom, DateTime dateTo)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                var limit = GetBudgetLimit(dateFrom, dateTo);
                var expense = session.GetRepository<IExpenseRepository>().GetExpenses(dateFrom, dateTo).Select(e => e.Amount).Sum();

                return limit > 0 ? 100 * (limit - expense) / limit : 0;
            }
        }

        public decimal GetBudgetBalance(DateTime dateFrom, DateTime dateTo)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                var limit = GetBudgetLimit(dateFrom, dateTo);
                var expense = session.GetRepository<IExpenseRepository>().GetExpenses(dateFrom, dateTo).Select(e => e.Amount).Sum();

                return limit - expense;
            }
        }

        public IEnumerable<CategoryBalance> GetCategoryBalance(DateTime dateFrom, DateTime dateTo)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                var result = new List<CategoryBalance>();
                var categoryLimits = session.GetRepository<IBudgetExpenseRepository>().GetCategoryTotals(dateFrom, dateTo);
                var categoryExpense = session.GetRepository<IExpenseRepository>().GetCategoryTotals(dateFrom, dateTo);

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
