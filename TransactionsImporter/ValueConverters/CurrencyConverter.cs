using System;
using System.Globalization;
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
                var nfi = (NumberFormatInfo)culture.NumberFormat.Clone();
                nfi.NumberGroupSeparator = " ";
                return ((decimal)value).ToString("#,0.00", nfi);
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
