using System;
using System.Collections;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BotRetreat.Framework.Wpf.Converters
{
    public class NullOrEmptyToVisibilityConverter : IValueConverter
    {
        public Visibility NullOrEmptyVisibility { get; set; }
        public Visibility NotNullOrEmptyVisibility { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var enumerableValue = value as IList;
            return enumerableValue == null || enumerableValue.Count == 0 ? NullOrEmptyVisibility : NotNullOrEmptyVisibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Converting from Visibility to null or empty is not supported!");
        }
    }
}