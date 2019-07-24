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

using Headquarters4DCS.Enums;
using System.Linq;

namespace Headquarters4DCS.Library
{
    /// <summary>
    /// An unit group template.
    /// </summary>
    public struct DefinitionObjectiveUnitGroup
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
        /// Special flags for this unit group.
        /// </summary>
        public readonly MissionObjectiveUnitGroupFlags[] Flags;

        /// <summary>
        /// Custom values for this unit group.
        /// </summary>
        public string[] CustomValues;

        /// <summary>
        /// Constructor. Loads the group template from an INI file.
        /// </summary>
        /// <param name="ini">The INI file to load from.</param>
        /// <param name="section">The INI section.</param>
        /// <param name="isObjectiveGroup">Is this group an "objective group" that should be spawned once at each objective?</param>
        public DefinitionObjectiveUnitGroup(INIFile ini, string section, bool isObjectiveGroup)
        {
            LuaGroup = ini.GetValue<string>(section, "Lua.Group");
            LuaUnit = ini.GetValue<string>(section, "Lua.Unit");

            // Load the list of possible unit famillies, make sure they all belong to the same unit category (can't have planes mixed with ground vehicles)
            Families = ini.GetValueArray<UnitFamily>(section, "Unit.Family");
            if (Families.Length == 0) Families = new UnitFamily[] { DEFAULT_UNIT_FAMILY };
            UnitFamily defaultFamilly = Families[0];
            Families = (from uf in Families where HQTools.GetUnitCategoryFromUnitFamily(uf) == HQTools.GetUnitCategoryFromUnitFamily(defaultFamilly) select uf).Distinct().ToArray();

            UnitCount = ini.GetValue<MinMaxI>(section, "Unit.Count");

            Flags = ini.GetValueArray<MissionObjectiveUnitGroupFlags>(section, "Flags").Distinct().ToArray();
            CustomValues = ini.GetValueArray<string>(section, "CustomValues");
        }
    }
}
