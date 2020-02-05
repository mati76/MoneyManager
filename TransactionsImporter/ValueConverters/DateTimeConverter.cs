using System;
using System.Globalization;
using System.Windows.Data;

namespace TransactionsImporter.ValueConverters
{
    public class DateTimeConverter : IValueConverter
	{
        public object Convert(object value, Type targetType, object parameter,
                      System.Globalization.CultureInfo culture)
        {
            if (value is DateTime)
            {
                return ((DateTime)value).ToString("dd/MM/yyyy");
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
