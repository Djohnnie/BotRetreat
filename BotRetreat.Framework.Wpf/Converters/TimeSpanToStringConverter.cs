using System;
using System.Globalization;
using System.Windows.Data;

namespace BotRetreat.Framework.Wpf.Converters
{
    public class TimeSpanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var timeSpan = (TimeSpan)value;
            var timeSpanSeconds = $"{timeSpan.Seconds} seconds";
            var timeSpanMinutes = $"{timeSpan.Minutes} minutes, ";
            var timeSpanHours = $"{timeSpan.Hours} hours, ";
            var timeSpanDays = $"{timeSpan.Days} days, ";
            return timeSpan == TimeSpan.MaxValue ? "unknown" :
                $"{(timeSpan.Days > 0 ? timeSpanDays : "")}{(timeSpan.Hours > 0 ? timeSpanHours : "")}{(timeSpan.Minutes > 0 ? timeSpanMinutes : "")}{(timeSpan.Seconds > 0 ? timeSpanSeconds : "")}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Converting from String to TimeSpan is not supported!");
        }
    }
}