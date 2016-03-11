using System;
using System.Globalization;
using System.Windows.Data;

namespace BotRetreat.Framework.Wpf.Converters
{
    public class DateTimeToTimeSinceConverter : IValueConverter
    {
        public String NoValueText { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dateTime = value as DateTime?;
            if (dateTime == null) return NoValueText;
            var timeFromNow = DateTime.UtcNow - dateTime.Value;
            return $"About {Math.Floor(timeFromNow.TotalMinutes)} minutes and {timeFromNow.Seconds} seconds ago";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Converting from time since to DateTime is not supported!");
        }
    }
}