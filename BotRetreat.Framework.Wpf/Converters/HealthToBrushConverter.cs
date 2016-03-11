using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using BotRetreat.DataTransferObjects;

namespace BotRetreat.Framework.Wpf.Converters
{
    public class HealthToBrushConverter : IValueConverter
    {
        public Brush LowBrush { get; set; }

        public Brush MediumBrush { get; set; }

        public Brush HighBrush { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var health = value as Health;
            return health == null ? LowBrush : ((Single)health.Current / health.Maximum > .666f ? HighBrush : ((Single)health.Current / health.Maximum < .333f ? LowBrush : MediumBrush));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Converting from Brush to Health is not supported!");
        }
    }
}