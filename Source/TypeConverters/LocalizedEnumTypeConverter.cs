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

using Headquarters4DCS.Forms;
using System;
using System.ComponentModel;
using System.Globalization;

namespace Headquarters4DCS.TypeConverters
{
    public sealed class LocalizedEnumTypeConverter<T> : EnumConverter where T : struct
    {
        public LocalizedEnumTypeConverter() : base(typeof(T)) { }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string)) return true;
            return base.CanConvertTo(context, destinationType);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(T)) return true;
            return base.CanConvertTo(context, sourceType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return FormatEnum((T)value);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            foreach (T e in Enum.GetValues(typeof(T)))
            {
                if (GUITools.Language.GetString("Enumerations", $"{typeof(T).Name}.{e.ToString()}") == value.ToString())
                    return e;
            }

            return default(T);
        }

        public static string FormatEnum(T value)
        {
            return GUITools.Language.GetString("Enumerations", $"{typeof(T).Name}.{value.ToString()}");
        }
    }
}
