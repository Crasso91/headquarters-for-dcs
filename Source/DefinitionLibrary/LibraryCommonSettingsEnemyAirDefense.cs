/*
==========================================================================
This file is part of Headquarters for DCS World (HQ4DCS), a mission generator for
Eagle Dynamics' DCS World flight simulator.

HQ4DCS has been created by Ambroise Garel (@akaAgar).
You can find more information about the project on its GitHub page,
https://akaAgar.github.io/Headquarters4DCS

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

using System.Linq;

namespace Headquarters4DCS.DefinitionLibrary
{
    public struct LibraryCommonSettingsEnemyAirDefense
    {
        /// <summary>
        /// Totel count of air defense range categories.
        /// </summary>
        private static readonly int AIR_DEFENSE_RANGE_COUNT = HQTools.EnumCount<AirDefenseRange>();

        /// <summary>
        /// Valid unit families for embedded units.
        /// </summary>
        private static readonly UnitFamily[] VALID_EMBEDDED_FAMILIES =
            new UnitFamily[] { UnitFamily.VehicleAAA, UnitFamily.VehicleInfantryMANPADS,
                UnitFamily.VehicleSAMShort, UnitFamily.VehicleSAMShortIR };

        /// <summary>
        /// Valid unit families for units in area.
        /// </summary>
        private static readonly UnitFamily[] VALID_INAREA_FAMILIES =
            new UnitFamily[] { UnitFamily.VehicleAAA, UnitFamily.VehicleInfantryMANPADS,
                UnitFamily.VehicleSAMShort, UnitFamily.VehicleSAMShortIR, UnitFamily.VehicleSAMMedium, UnitFamily.VehicleSAMLong };

        /// <summary>
        /// Chance for short-range air defense units to be "embedded" in a ground vehicle group.
        /// </summary>
        public readonly double EmbeddedChance;

        /// <summary>
        /// The number of short-range air defense units to be "embedded" in ground vehicle groups.
        /// </summary>
        public readonly MinMaxI EmbeddedCount;

        /// <summary>
        /// Unit families to choose from for embedded short-range air defense units.
        /// </summary>
        public readonly UnitFamily[] EmbeddedFamilies;

        /// <summary>
        /// The number of short-range/medium-range/long-range air defense unit groups to spawn in area.
        /// </summary>
        public readonly MinMaxI[] InAreaGroupCount;

        /// <summary>
        /// The number of units in each short-range/medium-range/long-range air defense unit group.
        /// </summary>
        public readonly MinMaxI[] InAreaGroupSize;

        /// <summary>
        /// Unit families to choose from for short-range/medium-range/long-range air defense unit groups.
        /// </summary>
        public readonly UnitFamily[][] InAreaFamilies;

        /// <summary>
        /// Constructor. Loads data from a .ini file.
        /// </summary>
        /// <param name="ini">The .ini file to load from.</param>
        /// <param name="section">The .ini section to load from.</param>
        /// <param name="key">The top level .ini key to load from.</param>
        public LibraryCommonSettingsEnemyAirDefense(INIFile ini, string section, string key)
        {
            EmbeddedChance = HQTools.Clamp(ini.GetValue<int>(section, $"{key}.Embedded.Chance") / 100.0, 0.0, 1.0);
            EmbeddedCount = ini.GetValue<MinMaxI>(section, $"{key}.Embedded.Count");
            EmbeddedFamilies = ini.GetValueArray<UnitFamily>(section, $"{key}.Embedded.Families");
            EmbeddedFamilies = (from f in EmbeddedFamilies where VALID_EMBEDDED_FAMILIES.Contains(f) select f).ToArray();
            if (EmbeddedFamilies.Length == 0) EmbeddedFamilies = new UnitFamily[] { UnitFamily.VehicleAAA };

            InAreaGroupCount = new MinMaxI[AIR_DEFENSE_RANGE_COUNT];
            InAreaGroupSize = new MinMaxI[AIR_DEFENSE_RANGE_COUNT];
            InAreaFamilies = new UnitFamily[AIR_DEFENSE_RANGE_COUNT][];

            for (int i = 0; i < AIR_DEFENSE_RANGE_COUNT; i++)
            {
                string subKey = $"{key}.InArea.{((AirDefenseRange)i).ToString()}";

                InAreaGroupCount[i] = ini.GetValue<MinMaxI>(section, $"{subKey}.GroupCount");
                InAreaGroupSize[i] = ini.GetValue<MinMaxI>(section, $"{subKey}.GroupSize");
                InAreaFamilies[i] = ini.GetValueArray<UnitFamily>(section, $"{subKey}.Families");
                InAreaFamilies[i] = (from f in InAreaFamilies[i] where VALID_INAREA_FAMILIES.Contains(f) select f).ToArray();
                if (InAreaFamilies[i].Length == 0) InAreaFamilies[i] = new UnitFamily[] { UnitFamily.VehicleAAA };
            }
        }
    }
}
