using Microsoft.Practices.Unity;
using MoneyManager.DataAccess.EF;
using MoneyManager.DataAccess.Infrastructure;
using MoneyManager.DataAccess.Repositories;
using MoneyManager.Business.Repository;
using DAC = MoneyManager.DataAccess;
using MoneyManager.WebApi.Controllers;
using MoneyManager.WebApi.Services;
using MoneyManager.Business;
using MoneyManager.Business.Interfaces;
using MoneyManager.Business.Utilities;
using MoneyManager.Auth;

namespace MoneyManager.WebApi.Configuration
{
    public static class ContainerConfig
    {
        public static UnityContainer CofigureContainer()
        {
            var container = new UnityContainer();
            configure(container);
            return container;
        }

        private static void configure(UnityContainer container)
        {
            container.RegisterInstance<IUnityContainer>(container);

            //MoneyManager.WebApi
            container.RegisterType<ICategoryController, CategoryController>();
            container.RegisterType<IExpenseController, ExpenseController>();
            container.RegisterType<IIncomeController, IncomeController>();
            container.RegisterType<IAccountController, AccountController>();
            container.RegisterType<IBudgetController, BudgetController>();
            container.RegisterType<ICategoryService, CategoryService>();
            container.RegisterType<IExpenseService, ExpenseService>();
            container.RegisterType<IIncomeService, IncomeService>();
            container.RegisterType<IMapperService, MapperService>();
            container.RegisterType<IAccountService, AccountService>();
            container.RegisterType<IBudgetService, BudgetService>();

            //MoneyManager.DataAccess
            container.RegisterType<IDbContext, MoneyManagerContext>();
            container.RegisterType<ICategoryRepository, ExpenseCategoryRepository>();
            container.RegisterType<IIncomeCategoryRepository, IncomeCategoryRepository>();
            container.RegisterType<IExpenseRepository, ExpenseRepository>();
            container.RegisterType<IIncomeRepository, IncomeRepository>();
            container.RegisterType<IBudgetExpenseRepository, BudgetExpenseRepository>();
            container.RegisterType<IBudgetIncomeRepository, BudgetIncomeRepository>();

            //MoneyManager.Business
            container.RegisterType<ICategoryBusiness, CategoryBusiness>();
            container.RegisterType<IExpenseBusiness, ExpenseBusiness>();
            container.RegisterType<IIncomeBusiness, IncomeBusiness>();
            container.RegisterType<IUnitOfWorkFactory, UnitOfWorkFactory>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IDateHelper, DateHelper>();
            container.RegisterType<IAuthBusiness, AuthBusiness>();
            container.RegisterType<IBudgetBusiness, BudgetBusiness>();

            //Auth
            container.RegisterType<IAuthRepository, AuthRepository>();
        }
    }
}