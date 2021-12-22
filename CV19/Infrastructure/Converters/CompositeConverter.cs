using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;

namespace CV19.Infrastructure.Converters
{
    [MarkupExtensionReturnType(typeof(CompositeConverter))]
    internal class CompositeConverter : BaseConverter
    {
        [ConstructorArgument("FirstConverter")]
        public IValueConverter FirstConverter { get; set; }
        [ConstructorArgument("SecondConverter")]
        public IValueConverter SecondConverter { get; set; }

        public CompositeConverter() { }

        public CompositeConverter(IValueConverter first)
        {
            FirstConverter = first;
        }

        public CompositeConverter(IValueConverter first, IValueConverter second) : this(first) => this.SecondConverter = second;

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result1 = FirstConverter?.Convert(value, targetType, parameter, culture) ?? value;
            var result2 = SecondConverter?.Convert(result1, targetType, parameter, culture) ?? result1;
            return result2;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result2 = SecondConverter?.Convert(value, targetType, parameter, culture) ?? value;
            var result1 = FirstConverter?.Convert(result2, targetType, parameter, culture) ?? result2;
            return result1;
        }
    }
}
