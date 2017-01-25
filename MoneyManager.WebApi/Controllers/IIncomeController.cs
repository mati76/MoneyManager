using MoneyManager.WebApi.DTO;
using System;
using System.Collections.Generic;

namespace MoneyManager.WebApi.Controllers
{
    public interface IIncomeController
    {
        IEnumerable<Income> Get(SearchCriteria criteria);

        Income GetById(int id);

        TransactionTotals GetTotals(DateTime date);

        IEnumerable<CategoryTotal> GetCategoryTotals(DateTime dateFrom, DateTime dateTo);

        IEnumerable<Income> GetByDate(int year, int month);

        void Post(Income income);

        void Delete(int id);
    }
}
