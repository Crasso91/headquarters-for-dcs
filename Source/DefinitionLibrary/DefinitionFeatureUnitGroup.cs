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
using System.Linq;

namespace Headquarters4DCS.DefinitionLibrary
{
    /// <summary>
    /// An unit group template.
    /// </summary>
    public struct DefinitionFeatureUnitGroup
    {
        /// <summary>
        /// Default unit family to use if none is provided.
        /// </summary>
        private const UnitFamily DEFAULT_UNIT_FAMILY = UnitFamily.VehicleTransport;

        /// <summary>
        /// The Lua template to use for the group.
        /// </summary>
        public readonly string LuaGroup;

        /// <summary>
        /// The Lua template to use for each unit of the group.
        /// </summary>
        public readonly string LuaUnit;

        /// <summary>
        /// Which unit families are valid for this unit?
        /// </summary>
        public readonly UnitFamily[] Families;

        /// <summary>
        /// Min/max number of units in this group.
        /// </summary>
        public readonly MinMaxI UnitCount;

        /// <summary>
        /// The type of spawn points where this unit group can be spawned.
        /// </summary>
        public readonly TheaterLocationSpawnPointType[] SpawnPointTypes;

        /// <summary>
        /// Special flags for this unit group.
        /// </summary>
        public readonly MissionObjectiveUnitGroupFlags[] Flags;

        /// <summary>
        /// Custom values for this unit group.
        /// </summary>
        public string[] CustomValues;

        /// <summary>
        /// The chance this group will appear.
        /// </summary>
        public readonly double AppearChance;

        /// <summary>
        /// Min/max number of times this group will appear.
        /// </summary>
        public readonly MinMaxI GroupCount;

        /// <summary>
        /// Constructor. Loads the group template from an INI file.
        /// </summary>
        /// <param name="ini">Ini file to load from.</param>
        /// <param name="section">Ini file section.</param>
        /// <param name="key">Ini file top level key.</param>
        /// <param name="isObjectiveFeature">Does this group belong to an "Objective" feature? If true, AppearChance will always be 100%.</param>
        public DefinitionFeatureUnitGroup(INIFile ini, string section, string key, bool isObjectiveFeature)
        {
            AppearChance = HQTools.Clamp(ini.GetValue<int>(section, $"{key}.AppearChance"), 0, 100) / 100.0;
            if (isObjectiveFeature) GroupCount = new MinMaxI(1, 1);
            else GroupCount = ini.GetValue<MinMaxI>(section, $"{key}.Count");

            LuaGroup = ini.GetValue<string>(section, $"{key}.Lua.Group");
            LuaUnit = ini.GetValue<string>(section, $"{key}.Lua.Unit");

            // Load the list of possible unit famillies, make sure they all belong to the same unit category (can't have planes mixed with ground vehicles)
            Families = ini.GetValueArray<UnitFamily>(section, $"{key}.Unit.Family");
            if (Families.Length == 0) Families = new UnitFamily[] { DEFAULT_UNIT_FAMILY };
            UnitFamily defaultFamilly = Families[0];
            Families = (from uf in Families where HQTools.GetUnitCategoryFromUnitFamily(uf) == HQTools.GetUnitCategoryFromUnitFamily(defaultFamilly) select uf).Distinct().ToArray();

            UnitCount = ini.GetValue<MinMaxI>(section, $"{key}.Unit.Count");
            SpawnPointTypes = ini.GetValueArray<TheaterLocationSpawnPointType>("Node", $"{key}.SpawnPointTypes").Distinct().ToArray();
            if (SpawnPointTypes.Length == 0) SpawnPointTypes = (TheaterLocationSpawnPointType[])Enum.GetValues(typeof(TheaterLocationSpawnPointType));

            Flags = ini.GetValueArray<MissionObjectiveUnitGroupFlags>(section, $"{key}.Flags").Distinct().ToArray();
            CustomValues = ini.GetValueArray<string>(section, $"{key}.CustomValues");
        }
    }
}
