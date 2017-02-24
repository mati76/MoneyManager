using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using MoneyManager.Business.Repository;
using MoneyManager.Business.Models;
using MoneyManager.Business.Utilities;
using System.Threading.Tasks;

namespace MoneyManager.Business.Tests
{
    [TestFixture]
    public class BudgetBusinessTests
    {
        private Mock<IUnitOfWorkFactory> _unitOfWorkFactoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IExpenseRepository> _expenseRepositoryMock;
        private Mock<IBudgetExpenseRepository> _budgetRepositoryMock;
        private Mock<IDateHelper> _dateHelperMock;

        [SetUp]
        public void Init()
        {
            _expenseRepositoryMock = new Mock<IExpenseRepository>();
            _budgetRepositoryMock = new Mock<IBudgetExpenseRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkFactoryMock = new Mock<IUnitOfWorkFactory>();
            _dateHelperMock = new Mock<IDateHelper>();

            _unitOfWorkFactoryMock.Setup(e => e.GetSession()).Returns(_unitOfWorkMock.Object);
        }

        [Test]
        [TestCase(new int[] { 1, 2, 3, 4 },  new int[] { 5600, 6200, 6000, 7100 }, new int[] { 5800, 6000, 5900, 6200 }, 0.04)]
        public async void GetAvgExpenseDeviation_Should_Return_Correct_Value(int[] months, int[] expenses, int[] budget, decimal epectedResult)
        {
            _unitOfWorkMock.Setup(e => e.GetRepository<IExpenseRepository>()).Returns(_expenseRepositoryMock.Object);
            _unitOfWorkMock.Setup(e => e.GetRepository<IBudgetExpenseRepository>()).Returns(_budgetRepositoryMock.Object);

            _expenseRepositoryMock.Setup(e => e.GetExpenseAggregates()).Returns(
                Task.FromResult(expenses.Select(e => new TransactionAggregates { Year = 2016, Month = months[expenses.ToList().IndexOf(e)], Sum = e }).ToList()));

            _budgetRepositoryMock.Setup(e => e.GetExpenseAggregates()).Returns(
                Task.FromResult(budget.Select(e => new TransactionAggregates { Year = 2016, Month = months[budget.ToList().IndexOf(e)], Sum = e }).ToList()));

            var business = new BudgetBusiness(_unitOfWorkFactoryMock.Object, _dateHelperMock.Object);
            var result = await business.GetAvgExpenseDeviation();

            Assert.AreEqual(epectedResult, Math.Round(result, 2));
        }
    }
}
