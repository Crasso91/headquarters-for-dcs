using Headquarters4DCS.Template;
using System;
using System.ComponentModel;
using System.Globalization;

namespace Headquarters4DCS.TypeConverters
{
    public sealed class MissionTemplatePlayerFlightGroupConverter : ArrayConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string)) return true;
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return ((MissionTemplatePlayerFlightGroup[])value).Length.ToString() + " flight group(s)";
        }
    }
}
