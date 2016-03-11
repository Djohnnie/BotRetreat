using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BotRetreat.Framework.Wpf.Converters
{
    public class TrueToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var booleanValue = value as Boolean?;
            return booleanValue.HasValue && booleanValue.Value ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Converting from Visibility to true is not supported!");
        }
    }
}