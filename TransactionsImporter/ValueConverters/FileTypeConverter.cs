using MoneyManager.Integrations.CSV.Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace TransactionsImporter.ValueConverters
{
    public class FileTypeConverter : IValueConverter
	{
        public object Convert(object value, Type targetType, object parameter,
                      System.Globalization.CultureInfo culture)
        {
            return (bool)((FileType)value == FileType.MbankCSV && parameter?.ToString().ToLower() == "mbank" ||
                (FileType)value == FileType.PkoCSV && parameter?.ToString().ToLower() == "pko");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool) value == true)
                {
                    switch (parameter?.ToString().ToLower())
                    {
                        case "mbank":
                            return FileType.MbankCSV;
                        case "pko":
                            return FileType.PkoCSV;
                    }
                }
                else
                {
                    switch (parameter?.ToString().ToLower())
                    {
                        case "mbank":
                            return FileType.PkoCSV;
                        case "pko":
                            return FileType.MbankCSV;
                    }
                }
            } 
            throw new NotImplementedException("Not supported file type");
        }
    }
}
