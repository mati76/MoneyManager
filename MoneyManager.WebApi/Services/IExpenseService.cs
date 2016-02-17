using MoneyManager.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.WebApi.Services
{
    public interface IExpenseService
    {
        IEnumerable<ExpenseViewModel> GetExpenses(ExpenseCriteriaViewModel criteria);

        IEnumerable<ExpenseViewModel> GetExpenses(DateTime date);

        IEnumerable<ExpenseViewModel> GetExpenses(int year, int month);

        ExpenseViewModel GetExpense(int id);


    }
}
