using MoneyManager.Business.Models;
using System;
using System.Collections.Generic;

namespace MoneyManager.Business.Interfaces
{
    public interface IIncomeBusiness
    {
        Income GetIncome(int id);

        IEnumerable<Income> GetIncome(int year, int month);

        IEnumerable<Income> GetIncome(DateTime from, DateTime to);
    }
}
