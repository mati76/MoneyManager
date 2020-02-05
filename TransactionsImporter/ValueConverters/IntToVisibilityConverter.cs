using System;
using System.Globalization;
using System.Windows.Data;

namespace TransactionsImporter.ValueConverters
{
    public class IntToVisibilityConverter : IValueConverter
	{
        public object Convert(object value, Type targetType, object parameter,
                      System.Globalization.CultureInfo culture)
        {
            if (value is int)
            {
                return (int)value > 0 ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            }

            return System.Windows.Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
