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
                var nfi = (NumberFormatInfo)CultureInfo.CurrentCulture.NumberFormat.Clone();
                var format = string.Format("#{0}0.00", nfi.NumberDecimalSeparator);
                nfi.NumberGroupSeparator = " ";
                if (parameter is int && (int)parameter > 0)
                    format = string.Format("#{0}0." + string.Join("", Enumerable.Repeat('0', (int)parameter)), nfi.NumberDecimalSeparator);

                return ((decimal)value).ToString(format, nfi);
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return decimal.Parse(value.ToString());
        }
    }
}
