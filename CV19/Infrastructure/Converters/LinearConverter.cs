using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CV19.Infrastructure.Converters
{
    /// <summary>
    /// Реализация линейного преобразования f(x) = k*x + b
    /// </summary>
    internal class LinearConverter : BaseConverter
    {
        public double K { get; set; } = 1;
        public double B { get; set; }


        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;

            var x = System.Convert.ToDouble(value, culture);
            return K * x + B;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;

            var y = System.Convert.ToDouble(value, culture);

            return (y - B) / K;
        }

    }
}
