/*
==========================================================================
This file is part of Headquarters for DCS World (HQ4DCS), a mission generator for
Eagle Dynamics' DCS World flight simulator.

HQ4DCS was created by Ambroise Garel (@akaAgar).
You can find more information about the project on its GitHub page,
https://akaAgar.github.io/headquarters-for-dcs

HQ4DCS is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

HQ4DCS is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with HQ4DCS. If not, see https://www.gnu.org/licenses/
==========================================================================
*/

using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Headquarters4DCS.TypeConverters
{
    public sealed class NumericConverter : TypeConverter
    {
        public NumericConverter() : base() { }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context) { return true; }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) { return true; }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            MinMaxI minMax = ((NumericConverterMinMaxParameter)context.PropertyDescriptor.Attributes[typeof(NumericConverterMinMaxParameter)]).MinMax;

            return new StandardValuesCollection(Enumerable.Range(minMax.Min, minMax.Max + (1 - minMax.Min)).ToArray());
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(int)) return true;
            return base.CanConvertTo(context, destinationType);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(int)) return true;
            return base.CanConvertTo(context, sourceType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return ((int)value).ToString(NumberFormatInfo.InvariantInfo);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return Convert.ToInt32(value, NumberFormatInfo.InvariantInfo);
        }
    }
}
