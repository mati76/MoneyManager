using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MoneyManager.Business;
using MoneyManager.Business.Models;
using BLL = MoneyManager.Business.Models;
using DAC = MoneyManager.DataAccess.Models;

namespace MoneyManager.WebApi.Services
{
    public class MapperService : IMapperService
    {
        public TDestination Map<TDestination>(object source)
        {
            return AutoMapper.Mapper.Map<TDestination>(source);
        }
        
        public static void Configure()
        {
            AutoMapper.Mapper.CreateMap<DTO.Category, Category>();
            AutoMapper.Mapper.CreateMap<Category, DTO.Category>();
            AutoMapper.Mapper.CreateMap<Category, DTO.CategoryInfo>();
            AutoMapper.Mapper.CreateMap<DTO.CategoryInfo, Category>();
            AutoMapper.Mapper.CreateMap<Expense, DTO.Expense>().AfterMap((bll, dto) => dto.CategoryName = bll.Category?.Name);
            AutoMapper.Mapper.CreateMap<DTO.Expense, Expense>().AfterMap((dto, bll) => { bll.CategoryId = dto.CategoryId; bll.Category = null; });
            AutoMapper.Mapper.CreateMap<Income, DTO.Income>().AfterMap((bll, dto) => dto.CategoryName = bll.Category?.Name);
            AutoMapper.Mapper.CreateMap<DTO.Income, Income>().AfterMap((dto, bll) => { bll.CategoryId = dto.CategoryId; bll.Category = null; });
            AutoMapper.Mapper.CreateMap<DTO.SearchCriteria, SearchCriteria>();
            AutoMapper.Mapper.CreateMap<TransactionTotals, DTO.TransactionTotals>();
            AutoMapper.Mapper.CreateMap<CategoryTotal, DTO.CategoryTotal>();
            AutoMapper.Mapper.CreateMap<DTO.AccountDTO, AppUser>();
            AutoMapper.Mapper.CreateMap<IdentityResult, ActionResult>();
            AutoMapper.Mapper.CreateMap<AppUser, IdentityUser>();
            AutoMapper.Mapper.CreateMap<BudgetTotals, DTO.BudgetTotals>();

            AutoMapper.Mapper.CreateMap<DAC.ExpenseCategory, BLL.Category>();
            AutoMapper.Mapper.CreateMap<BLL.Category, DAC.ExpenseCategory>();
            AutoMapper.Mapper.CreateMap<DAC.IncomeCategory, BLL.Category>();
            AutoMapper.Mapper.CreateMap<BLL.Category, DAC.IncomeCategory>();
            AutoMapper.Mapper.CreateMap<DAC.Expense, BLL.Expense>();
            AutoMapper.Mapper.CreateMap<BLL.Expense, DAC.Expense>();
            AutoMapper.Mapper.CreateMap<DAC.Income, BLL.Income>();
            AutoMapper.Mapper.CreateMap<BLL.Expense, DAC.Expense>();
            AutoMapper.Mapper.CreateMap<BLL.Income, DAC.Income>();
            AutoMapper.Mapper.CreateMap<DAC.BudgetExpense, BLL.Expense>();
            AutoMapper.Mapper.CreateMap<BLL.Expense, DAC.BudgetExpense>();
        }
    }
}
