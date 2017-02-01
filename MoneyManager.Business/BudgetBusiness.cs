using System;
using System.Linq;
using System.Collections.Generic;
using MoneyManager.Business.Interfaces;
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
                        deviations.Add((aggr.Sum - budgetAggr.Sum) / budgetAggr.Sum);
                    }
                }

                return deviations.Any() ? deviations.Average() : 0;
            }
        }

        public BudgetTotals GetTotals(int month, int year)
        {
            using (var session = _unitOfWorkFactory.GetSession())
            {
                var budgetRepository = session.GetRepository<IBudgetExpenseRepository>();
                var expenseRepository = session.GetRepository<IExpenseRepository>();
             
                var expense = expenseRepository.GetExpenses(year, month).Select(e => e.Amount).Sum();

                var totals = new BudgetTotals
                {
                    BudgetLimit = budgetRepository.GetExpenses(year, month).Select(e => e.Amount).Sum(),
                    AvgDeviaton = GetAvgExpenseDeviation()
                };
                totals.BudgetBalance = totals.BudgetLimit - expense;
                totals.Deviation = totals.BudgetLimit > 0 ? (expense - totals.BudgetLimit) / totals.BudgetLimit : 0;

                return totals;
            }
        }
    }
}
