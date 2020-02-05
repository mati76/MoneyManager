using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TransactionsImporter.ValueConverters
{
    public class CurrencyToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                      System.Globalization.CultureInfo culture)
        {
            if (value is decimal)
            {
                return ((decimal)value) < 0 ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.Green);
            }

            return Colors.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
