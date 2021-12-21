using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace CV19.Infrastructure.Converters
{
    internal class ToArrayConverter : MultiConverter
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var collection = new CompositeCollection();
            foreach(var value in values)
            {
                collection.Add(value);
            }
            return collection;
        }

        //public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        //{
        //    return value as object[];
        //}
    }
}
