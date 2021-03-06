using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;

namespace CV19.Infrastructure.Converters
{
    [MarkupExtensionReturnType(typeof(RatioConverter))]
    internal class RatioConverter : BaseConverter
    {
        // 1:32:19
        [ConstructorArgument("K")]
        public double K { get; set; } = 1;

        public RatioConverter() { }

        public RatioConverter(double K) => this.K = K;
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;

            var x = System.Convert.ToDouble(value, culture);
            return x * K;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;
            var x = System.Convert.ToDouble(value, culture);
            return x / K;
        }
    }
}
