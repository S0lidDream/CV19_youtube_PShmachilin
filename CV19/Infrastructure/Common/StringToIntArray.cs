using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;

namespace CV19.Infrastructure.Common
{
    [MarkupExtensionReturnType(typeof(int[]))]
    internal class StringToIntArray : MarkupExtension
    {
        [ConstructorArgument("Str")]
        public string Str { get; set; }

        public StringToIntArray() { }

        public StringToIntArray(string str)
        {
            this.Str = str;
        }
        public char Separator { get; set; } = ';';


        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Str.Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries)
                      .DefaultIfEmpty()
                      .Select(int.Parse)
                      .ToArray();
        }
    }
}
