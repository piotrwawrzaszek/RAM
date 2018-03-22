using System;
using System.Globalization;
using System.Windows.Data;

namespace RAM.Infrastructure.Converters
{
    public class NumberingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is int number) || number == 0)
                return string.Empty;
            return number.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is string number) || number == string.Empty)
                return 0;
            return int.Parse(number);
        }
    }
}