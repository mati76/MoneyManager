using MoneyManager.WebApi.DTO;
using System;
using System.Collections.Generic;

namespace MoneyManager.WebApi.Controllers
{
    public interface IExpenseController
    {
        IEnumerable<Expense> Get(ExpenseCriteria criteria);

        Expense GetById(int id);

        IEnumerable<Expense> GetByDate(DateTime date);

        ExpenseTotals GetTotals(DateTime date);

        IEnumerable<Expense> GetByDate(int year, int month);

        void Post(Expense expense);

        void Delete(int id);
    }
}
