using MoneyManager.WebApi.Services;
using MoneyManager.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.WebApi.Controllers
{
    public interface IIncomeController
    {
        IncomeViewModel GetById(int id);

        IEnumerable<IncomeViewModel> GetByDateRange(DateTime from, DateTime to);

        IEnumerable<IncomeViewModel> GetByMonth(int year, int month);

        void Post(IncomeViewModel Income);

        void Delete(int id);
    }
}
