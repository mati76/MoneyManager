using MoneyManager.WebApi.DTO;
using System;
using System.Collections.Generic;

namespace MoneyManager.WebApi.Controllers
{
    public interface IExpenseController
    {
        IEnumerable<Expense> Get(SearchCriteria criteria);

        Expense GetById(int id);

        IEnumerable<Expense> GetByDate(DateTime date);

        TransactionTotals GetTotals(DateTime date);

        IEnumerable<Expense> GetByDate(int year, int month);

        IEnumerable<CategoryTotal> GetCategoryTotals(DateTime dateFrom, DateTime dateTo);

        IEnumerable<CategoryTotal> GetCategoryTotals(DateTime dateFrom, DateTime dateTo, int categoryId);

        void Post(Expense expense);

        void Delete(int id);
    }
}
