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
            container.RegisterType<ICategoryService, CategoryService>();
            container.RegisterType<IExpenseService, ExpenseService>();
            container.RegisterType<IIncomeService, IncomeService>();
            container.RegisterType<IMapperService, MapperService>();

            //MoneyManager.DataAccess
            container.RegisterInstance<DAC.Services.IMapperService>(new DAC.Services.MapperService());
            container.RegisterType<IDbContext, MoneyManagerContext>();
            container.RegisterType<ICategoryRepository, CategoryRepository>();
            container.RegisterType<IExpenseRepository, ExpenseRepository>();
            container.RegisterType<IIncomeRepository, IncomeRepository>();

            //MoneyManager.Business
            container.RegisterType<ICategoryBusiness, CategoryBusiness>();
            container.RegisterType<IExpenseBusiness, ExpenseBusiness>();
            container.RegisterType<IIncomeBusiness, IncomeBusiness>();
            container.RegisterType<IUnitOfWorkFactory, UnitOfWorkFactory>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IDateHelper, DateHelper>();
        }
    }
}