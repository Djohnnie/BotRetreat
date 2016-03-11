using System;
using System.Globalization;
using System.Windows.Data;
using BotRetreat.DataTransferObjects;

namespace BotRetreat.Framework.Wpf.Converters
{
    public class HealthToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var health = value as Health;
            return health == null ? String.Empty : $"{health.Current} / {health.Maximum} (-{health.Drain})";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Converting from String to Health is not supported!");
        }
    }
}