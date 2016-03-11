using System;
using System.Globalization;
using System.Windows.Data;

namespace BotRetreat.Framework.Wpf.Converters
{
    public class Int64ToMillisecondsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var intValue = (Int64)value;
            return intValue == Int64.MaxValue ? "> 2000 ms" : $"{intValue} ms";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Converting from milliseconds to Int64 is not supported!");
        }
    }
}