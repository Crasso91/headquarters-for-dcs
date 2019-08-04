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
using System.Text.RegularExpressions;

namespace Headquarters4DCS.TypeConverters
{
    /// <summary>
    /// Splits words in camelcase to make enums prettier in PropertyGrids
    /// </summary>
    /// <typeparam name="T">The type of enum to convert</typeparam>
    public sealed class SplitEnumTypeConverter<T> : EnumConverter where T : struct, IConvertible
    {
        public SplitEnumTypeConverter() : base(typeof(T)) { }

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
            string enumString = ((T)value).ToString();
            if (typeof(T) == typeof(TimePeriod)) enumString = enumString.Substring("Decade".Length) + "s";

            string[] words = Regex.Split(enumString, "(?<!(^|[A-Z]))(?=[A-Z])|(?<!^)(?=[A-Z][a-z])");
            for (int i = 1; i < words.Length; i++) words[i] = words[i].ToLowerInvariant();
            return string.Join(" ", words);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string enumString = value.ToString().Replace(" ", "");
            if (typeof(T) == typeof(TimePeriod)) enumString = "Decade" + enumString.Substring(0, enumString.Length - 1);

            if (Enum.TryParse(enumString, true, out T parsedEnum)) return parsedEnum;

            return default(T);
        }
    }
}
