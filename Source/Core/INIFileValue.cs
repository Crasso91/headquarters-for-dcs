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

namespace Headquarters4DCS
{
    /// <summary>
    /// An ini file value.
    /// </summary>
    public struct INIFileValue : IComparable
    {
        /// <summary>
        /// The section this value belongs to.
        /// </summary>
        public readonly string Section;

        /// <summary>
        /// The key to this value.
        /// </summary>
        public readonly string Key;

        /// <summary>
        /// The value.
        /// </summary>
        public readonly string Value;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="section">The ini section.</param>
        /// <param name="key">The ini key.</param>
        /// <param name="value">The value.</param>
        public INIFileValue(string section, string key, string value)
        {
            Section = section;
            Key = key;
            Value = value;
        }

        /// <summary>
        /// Compares the value to another INIFileValue. INIFileValues are sorted by section, then by key.
        /// </summary>
        /// <param name="other">The other INIFileValue.</param>
        /// <returns>-1 if this INIFileValue should be sorted before other, 1 if it should be sorted after.</returns>
        public int CompareTo(object other)
        {
            if (!(other is INIFileValue)) return 0;
            INIFileValue ifv = (INIFileValue)other;

            int compare = Section.CompareTo(ifv.Section);
            if (compare != 0) return compare;
            return Key.CompareTo(ifv.Key);
        }
    }
}
