using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using BotRetreat.DataTransferObjects;

namespace BotRetreat.Framework.Wpf.Converters
{
    public class DeathToBrushConverter : IValueConverter
    {
        public Brush DeathBrush { get; set; }

        public Brush AliveBrush { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var health = value as Health;
            return health == null || health.Current == 0 ? DeathBrush : AliveBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Converting from Brush to Health is not supported!");
        }
    }
}
