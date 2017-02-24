using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MoneyManager.Business;
using MoneyManager.Business.Models;
using BLL = MoneyManager.Business.Models;
using DAC = MoneyManager.DataAccess.Models;
using AutoMapper;

namespace MoneyManager.WebApi.Services
{
    public class MapperService : IMapperService
    {
        public TDestination Map<TDestination>(object source)
        {
            return Mapper.Map<TDestination>(source);
        }

        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DTO.Category, Category>().PreserveReferences();
                cfg.CreateMap<Category, DTO.Category>().PreserveReferences();
                cfg.CreateMap<Category, DTO.CategoryInfo>().PreserveReferences();
                cfg.CreateMap<DTO.CategoryInfo, Category>().PreserveReferences();
                cfg.CreateMap<Expense, DTO.Expense>().AfterMap((bll, dto) => dto.CategoryName = bll.Category?.Name);
                cfg.CreateMap<DTO.Expense, Expense>().AfterMap((dto, bll) => { bll.CategoryId = dto.CategoryId; bll.Category = null; });
                cfg.CreateMap<Income, DTO.Income>().AfterMap((bll, dto) => dto.CategoryName = bll.Category?.Name);
                cfg.CreateMap<DTO.Income, Income>().AfterMap((dto, bll) => { bll.CategoryId = dto.CategoryId; bll.Category = null; });
                cfg.CreateMap<DTO.SearchCriteria, SearchCriteria>();
                cfg.CreateMap<TransactionTotals, DTO.TransactionTotals>();
                cfg.CreateMap<CategoryTotal, DTO.CategoryTotal>();
                cfg.CreateMap<DTO.AccountDTO, AppUser>();
                cfg.CreateMap<IdentityResult, ActionResult>();
                cfg.CreateMap<AppUser, IdentityUser>();

                cfg.CreateMap<DAC.ExpenseCategory, BLL.Category>().PreserveReferences();
                cfg.CreateMap<BLL.Category, DAC.ExpenseCategory>().PreserveReferences();
                cfg.CreateMap<DAC.IncomeCategory, BLL.Category>().PreserveReferences();
                cfg.CreateMap<BLL.Category, DAC.IncomeCategory>().PreserveReferences();
                cfg.CreateMap<DAC.Expense, BLL.Expense>();
                cfg.CreateMap<BLL.Expense, DAC.Expense>();
                cfg.CreateMap<DAC.Income, BLL.Income>();
                cfg.CreateMap<BLL.Expense, DAC.Expense>();
                cfg.CreateMap<BLL.Income, DAC.Income>();
                cfg.CreateMap<DAC.BudgetExpense, BLL.Expense>();
                cfg.CreateMap<BLL.Expense, DAC.BudgetExpense>();
            });
        }
    }
}
