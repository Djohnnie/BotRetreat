using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using BotRetreat.Enums;

namespace BotRetreat.Arena.Wpf.Converters
{
    public class HistoryTypeToBackgroundConverter : IValueConverter
    {
        public Brush DefaultBrush { get; set; }
        public Brush MessageBrush { get; set; }
        public Brush WarningBrush { get; set; }
        public Brush ErrorBrush { get; set; }

        public object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            var background = DefaultBrush;
            var historyType = (HistoryType)value;
            switch (historyType)
            {
                case HistoryType.Message:
                    background = MessageBrush;
                    break;
                case HistoryType.Warning:
                    background = WarningBrush;
                    break;
                case HistoryType.Error:
                    background = ErrorBrush;
                    break;
            }
            return background;
        }

        public object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}