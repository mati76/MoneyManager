using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace TransactionsImporter.ValueConverters
{
    public class CurrencyConverter : IValueConverter
	{
        public object Convert(object value, Type targetType, object parameter,
                      System.Globalization.CultureInfo culture)
        {
            if (value is decimal)
            {
                var format = "#,0.00";
                var nfi = (NumberFormatInfo)culture.NumberFormat.Clone();
                nfi.NumberGroupSeparator = " ";
                if (parameter is int && (int)parameter > 0)
                    format = "#,0." + string.Join("", Enumerable.Repeat('0', (int)parameter));

                return ((decimal)value).ToString(format, nfi);
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
