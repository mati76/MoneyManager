using MoneyManager.WebApi.DTO;
using System;
using System.Collections.Generic;

namespace MoneyManager.WebApi.Controllers
{
    public interface IIncomeController
    {
        Income GetById(int id);

        IEnumerable<Income> GetByDateRange(DateTime from, DateTime to);

        IEnumerable<Income> GetByMonth(int year, int month);

        void Post(Income Income);

        void Delete(int id);
    }
}
