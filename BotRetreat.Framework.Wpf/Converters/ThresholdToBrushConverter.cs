using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace BotRetreat.Framework.Wpf.Converters
{
    public class ThresholdToBrushConverter : IValueConverter
    {
        public Brush BelowBrush { get; set; }
        public Brush AboveBrush { get; set; }
        public Int32 Threshold { get; set; }

        public object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            return (Int32)value < Threshold ? BelowBrush : AboveBrush;
        }

        public object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Converting from Brush to Threshold is not supported!");
        }
    }
}