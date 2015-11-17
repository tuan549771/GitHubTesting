using System;
using System.Globalization;
using System.Windows.Data;

namespace DMController.Converters
{
    class DatetimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date = (DateTime)value;
            return date.ToString("  dd/MM/yyyy  hh:mm:ss  ");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
