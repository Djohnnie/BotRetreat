using System;
using System.Globalization;
using System.Windows.Data;
using BotRetreat.DataTransferObjects;

namespace BotRetreat.Framework.Wpf.Converters
{
    public class PositionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var position = value as Position;
            return position == null ? String.Empty : $"{position.X} x {position.Y}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Converting from String to Position is not supported!");
        }
    }
}