using System.Collections.Generic;
using MoneyManager.Api.DTO;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MoneyManager.Api.Controllers
{
    public interface IBudgetController
    {
        Task DeleteExpense(int id);
        Task DeleteIncome(int id);
        Task<IEnumerable<Transaction>> GetExpense([FromQuery] SearchCriteria criteria);
        Task<Transaction> GetExpenseById(int id);
        Task<IEnumerable<Transaction>> GetIncome([FromQuery] SearchCriteria criteria);
        Task<Transaction> GetIncomeById(int id);
        Task PostIncome([FromBody] Transaction income);
        Task PostExpense([FromBody] Transaction expense);
        Task<IEnumerable<BudgetRealization>> GetRealization(DateTime from, DateTime to);
    }
}