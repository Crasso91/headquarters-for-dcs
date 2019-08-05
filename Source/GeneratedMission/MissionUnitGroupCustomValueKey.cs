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

namespace Headquarters4DCS.GeneratedMission
{
    /// <summary>
    /// Key for a value in the HQMissionUnitGroup.CustomValues dictionary
    /// </summary>
    public struct MissionUnitGroupCustomValueKey
    {
        /// <summary>
        /// The key to replace in the group/unit Lua file, WITHOUT the enclosing dollar signs.
        /// </summary>
        public readonly string Key;

        /// <summary>
        /// Index of the unit this custom value will be applied to. -1 means "whole group".
        /// Most values apply to the whole group, only a few (tail number, parking spot, etc.) are different from an unit to the next.
        /// </summary>
        public readonly int UnitIndex;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key"></param>
        /// <param name="unitIndex">Index of the unit this custom value will be applied to. -1 means "whole group".</param>
        public MissionUnitGroupCustomValueKey(string key, int unitIndex = -1)
        {
            Key = key.ToUpperInvariant();
            UnitIndex = Math.Max(-1, unitIndex);
        }

        /// <summary>
        /// Implicit conversion from string.
        /// </summary>
        /// <param name="key">The string to use as a key</param>
        public static implicit operator MissionUnitGroupCustomValueKey(string key) { return new MissionUnitGroupCustomValueKey(key); }

        /// <summary>
        /// Equals override. 
        /// </summary>
        /// <param name="obj">Another object.</param>
        /// <returns>True if obj is a HQMissionCustomValueKey with the same Key and UnitIndex. False otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (obj is MissionUnitGroupCustomValueKey)
                return (((MissionUnitGroupCustomValueKey)obj).Key == Key) && (((MissionUnitGroupCustomValueKey)obj).UnitIndex == UnitIndex);

            return false;
        }

        /// <summary>
        /// GetHashCode() override.
        /// </summary>
        /// <returns>An unique Hascode for this HQMissionCustomValueKey.</returns>
        public override int GetHashCode()
        { return Key.GetHashCode() * 7 + UnitIndex.GetHashCode(); }
    }
}
