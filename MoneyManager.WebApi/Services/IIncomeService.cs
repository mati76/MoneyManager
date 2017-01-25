using MoneyManager.WebApi.DTO;
using System;
using System.Collections.Generic;

namespace MoneyManager.WebApi.Services
{
    public interface IIncomeService
    {
        IEnumerable<Income> GetIncome(DateTime from, DateTime to);

        IEnumerable<Income> GetIncome(int year, int month);

        void DeleteIncome(int id);

        Income GetIncome(int id);

        void SaveIncome(DTO.Income income);
        
        IEnumerable<DTO.Income> GetIncome(SearchCriteria criteria);

        TransactionTotals GetIncomeTotals(DateTime currentDate);

        IEnumerable<DTO.CategoryTotal> GetCategoryTotals(DateTime dateFrom, DateTime dateTo);
    }
}
