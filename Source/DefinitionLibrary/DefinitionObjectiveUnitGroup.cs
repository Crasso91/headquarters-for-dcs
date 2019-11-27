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

namespace Headquarters4DCS.DefinitionLibrary
{
    public struct DefinitionObjectiveUnitGroup
    {
        /// <summary>
        /// Families unity of this group can belong to.
        /// </summary>
        public readonly UnitFamily[] Family;

        /// <summary>
        /// Min/max number of units to spawn in this group.
        /// </summary>
        public readonly MinMaxI Count;

        /// <summary>
        /// The Lua template for the group.
        /// </summary>
        public readonly string LuaGroup;

        /// <summary>
        /// The Lua template for each unit in the group.
        /// </summary>
        public readonly string LuaUnit;

        /// <summary>
        /// Special flags for this unit group.
        /// </summary>
        public readonly MissionObjectiveUnitGroupFlags[] Flags;

        /// <summary>
        /// The fixed ID of this unit group.
        /// Ignored if 0 or less, else set to 1000 * objective index + this value.
        /// Used in scripts, so the game knows for instance that group with ID X001 must be destroyed.
        /// </summary>
        public readonly int GroupID;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ini">The .ini file to load airbase data from.</param>
        /// <param name="key">The .ini file key to load from</param>
        public DefinitionObjectiveUnitGroup(INIFile ini, string key)
        {
            Family = ini.GetValueArray<UnitFamily>("UnitGroups", $"{key}.Family");
            if (Family.Length == 0) Family = new UnitFamily[] { UnitFamily.HelicopterTransport };
            Count = ini.GetValue<MinMaxI>("UnitGroups", $"{key}.Count");
            Flags = ini.GetValueArray<MissionObjectiveUnitGroupFlags>("UnitGroups", $"{key}.Flags");
            LuaGroup = ini.GetValue<string>("UnitGroups", $"{key}.Lua.Group");
            LuaUnit = ini.GetValue<string>("UnitGroups", $"{key}.Lua.Unit");
            GroupID = HQTools.Clamp(ini.GetValue<int>("UnitGroups", $"{key}.ID"), 0, 999);
        }
    }
}
