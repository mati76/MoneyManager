using System;

namespace MoneyManager.Business.Utilities
{
    public class DateHelper : IDateHelper
    {
        public Tuple<DateTime, DateTime> GetMonthBoundaries(DateTime date)
        {
            var lastDay = DateTime.DaysInMonth(date.Year, date.Month);
            return new Tuple<DateTime, DateTime>(new DateTime(date.Year, date.Month, 1),
                new DateTime(date.Year, date.Month, lastDay));
        }

        public Tuple<DateTime, DateTime> GetWeekBoundaries(DateTime date)
        {
            var day = Math.Min((int)date.DayOfWeek + 1, 7);
            return new Tuple<DateTime, DateTime>(
                date.AddDays(-(day - 1)), date.AddDays(7 - day));
        }

        public Tuple<DateTime, DateTime> GetYearBoundaries(DateTime date)
        {
            return new Tuple<DateTime, DateTime>(new DateTime(date.Year, 1, 1), 
                new DateTime(date.Year, 12, DateTime.DaysInMonth(date.Year, 12)));
        }
    }
}