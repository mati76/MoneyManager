using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.Business.Utilities
{
    public interface IDateHelper
    {
        Tuple<DateTime, DateTime> GetWeekBoundaries(DateTime date);
        Tuple<DateTime, DateTime> GetMonthBoundaries(DateTime date);
        Tuple<DateTime, DateTime> GetYearBoundaries(DateTime date);
    }
}
