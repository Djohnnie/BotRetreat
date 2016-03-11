using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BotRetreat.Framework.Wpf.Converters
{
    public class NumberToVisibilityConverter : IValueConverter
    {
        public Int16 VisibilityNumber { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Int16)value) == VisibilityNumber ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Converting from Visibility to Int32 is not supported!");
        }
    }
}