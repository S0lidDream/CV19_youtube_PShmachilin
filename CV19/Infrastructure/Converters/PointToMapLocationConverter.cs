using MapControl;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace CV19.Infrastructure.Converters
{
    [MarkupExtensionReturnType(typeof(PointToMapLocationConverter))]
    [ValueConversion(typeof(Point), typeof(Location))]
    internal class PointToMapLocationConverter : BaseConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Point point)) return null;
            return new Location(point.X, point.Y);
        }
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Location location)) return null;
            return new Point(location.Latitude, location.Longitude);
        }
    }
}
