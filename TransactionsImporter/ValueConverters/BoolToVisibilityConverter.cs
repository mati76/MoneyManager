using System;
using System.Globalization;
using System.Windows.Data;

namespace TransactionsImporter.ValueConverters
{
    public class BoolToVisibilityConverter : IValueConverter
	{
        public object Convert(object value, Type targetType, object parameter,
                      System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                return (bool)value ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            }

            return System.Windows.Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
