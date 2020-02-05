using AutoMapper;
using MoneyManager.Business.Models;
using DAC = MoneyManager.DataAccess.Models;

namespace MoneyManager.Api.AutoMapper
{
	public class TransactionsProfile : Profile
	{
        public TransactionsProfile()
        {
            CreateMap<DTO.Transaction, Transaction>().AfterMap((dto, bll) => { bll.CategoryId = dto.CategoryId; bll.Category = null; });
            CreateMap<Transaction, DTO.Transaction>().AfterMap((bll, dto) => dto.CategoryName = bll.Category?.Name);
            CreateMap<TransactionTotals, DTO.TransactionTotals>();
            CreateMap<TransactionCollection, DTO.TransactionCollection>();
            
            CreateMap<DAC.Expense, Transaction>();
            CreateMap<Transaction, DAC.Expense>();
            CreateMap<DAC.Income, Transaction>();
            CreateMap<Transaction, DAC.Expense>();
            CreateMap<Transaction, DAC.Income>();
            CreateMap<DAC.BudgetExpense, Transaction>();
            CreateMap<Transaction, DAC.BudgetExpense>();
        }
	}
}
