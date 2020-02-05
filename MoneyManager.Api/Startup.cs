using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MoneyManager.Api.AutoMapper;
using MoneyManager.Api.Services;
using MoneyManager.Auth;
using MoneyManager.Business;
using MoneyManager.Business.Interfaces;
using MoneyManager.Business.Repository;
using MoneyManager.Business.Utilities;
using MoneyManager.DataAccess.EF;
using MoneyManager.DataAccess.Infrastructure;
using MoneyManager.DataAccess.Repositories;

namespace MoneyManager.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			services.AddDbContext<MoneyManagerContext>(options => {
				options.UseSqlServer(Configuration.GetConnectionString("MoneyManager"));
			});

			// Auto Mapper Configurations
			var mappingConfig = new MapperConfiguration(mc =>
			{
				mc.AddProfile(new AuthProfile());
				mc.AddProfile(new CategoriesProfile());
				mc.AddProfile(new TransactionsProfile());
				mc.AddProfile(new HelperProfile());
			});

			IMapper mapper = mappingConfig.CreateMapper();
			services.AddSingleton(mapper);

			services.AddTransient<ICategoryService, CategoryService>();
			services.AddTransient<IExpenseService, ExpenseService>();
			services.AddTransient<IIncomeService, IncomeService>();
			services.AddTransient<IAccountService, AccountService>();
			services.AddTransient<IBudgetService, BudgetService>();

			//MoneyManager.DataAccess
			//services.AddSingleton<IDbContext, MoneyManagerContext>();
			services.AddTransient<ICategoryRepository, ExpenseCategoryRepository>();
			services.AddTransient<IIncomeCategoryRepository, IncomeCategoryRepository>();
			services.AddTransient<IExpenseRepository, ExpenseRepository>(sp => new ExpenseRepository(sp.GetRequiredService<IMapper>(), sp.GetRequiredService<IDbContext>()));
			services.AddTransient<IIncomeRepository, IncomeRepository>();
			services.AddTransient<IBudgetExpenseRepository, BudgetExpenseRepository>();
			services.AddTransient<IBudgetIncomeRepository, BudgetIncomeRepository>();

			//MoneyManager.Business
			services.AddTransient<ICategoryBusiness, CategoryBusiness>();
			services.AddTransient<IExpenseBusiness, ExpenseBusiness>();
			services.AddTransient<IIncomeBusiness, IncomeBusiness>();
			services.AddTransient<IUnitOfWorkFactory, UnitOfWorkFactory>();
			services.AddTransient<IUnitOfWork, UnitOfWork>();
			services.AddTransient<IDateHelper, DateHelper>();
			services.AddTransient<IAuthBusiness, AuthBusiness>();
			services.AddTransient<IBudgetBusiness, BudgetBusiness>();

			services.AddTransient<IDbContext, MoneyManagerContext>();

			//Auth
			services.AddTransient<IAuthRepository, AuthRepository>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseAuthorization();

			app.UseCors();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
