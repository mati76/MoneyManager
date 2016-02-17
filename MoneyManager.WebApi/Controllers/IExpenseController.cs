using MoneyManager.WebApi.Services;
using MoneyManager.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.WebApi.Controllers
{
    public interface IExpenseController
    {
        IEnumerable<ExpenseViewModel> Get(ExpenseCriteriaViewModel criteria);

        ExpenseViewModel GetById(int id);

        IEnumerable<ExpenseViewModel> GetByDate(DateTime date);

        IEnumerable<ExpenseViewModel> GetByDate(int year, int month);

        void Post(ExpenseViewModel expense);

        void Delete(int id);
        }
}
